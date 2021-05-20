using Elsa.Persistence;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using P257WakeupSleepingWorkflow.Models;
using P257WakeupSleepingWorkflow.Workflows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P257WakeupSleepingWorkflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowDetailsController : Controller
    {
        private readonly IWorkflowBlueprintMaterializer _workflowBlueprintMaterializer;
        private readonly IWorkflowBlueprintReflector _workflowBlueprintReflector;
        private readonly IWorkflowContextManager _workflowContextManager;
        private readonly IWorkflowContextProvider _workflowContextProvider;
        private readonly IWorkflowDefinitionStore _workflowDefinitionStore;
        private readonly IWorkflowExecutionLog _workflowExecutionLog;
        private readonly IWorkflowExecutionLogStore _workflowExecutionLogStore;
        private readonly IWorkflowFactory _workflowFactory;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        private readonly IWorkflowProvider _workflowProvider;
        private readonly IWorkflowPublisher _workflowPublisher;
        private readonly IWorkflowRegistry _workflowRegistry;
        private readonly IWorkflowReviver _workflowReviver;
        private readonly IWorkflowRunner _workflowRunner;
        private readonly IWorkflowTriggerInterruptor _workflowTriggerInterruptor;


        public WorkflowDetailsController(

            IWorkflowBlueprintMaterializer workflowBlueprintMaterializer,
            IWorkflowBlueprintReflector workflowBlueprintReflector,
            IWorkflowContextManager workflowContextManager,
            //IWorkflowContextProvider workflowContextProvider,
            IWorkflowDefinitionStore workflowDefinitionStore,
            IWorkflowExecutionLog workflowExecutionLog,
            IWorkflowExecutionLogStore workflowExecutionLogStore,
            IWorkflowFactory workflowFactory,
            IWorkflowInstanceStore workflowInstanceStore,
            IWorkflowProvider workflowProvider,
            IWorkflowPublisher workflowPublisher,
            IWorkflowRegistry workflowRegistry,
            IWorkflowReviver workflowReviver,
            IWorkflowRunner workflowRunner,
            IWorkflowTriggerInterruptor workflowTriggerInterruptor
            )
        {
            _workflowBlueprintMaterializer = workflowBlueprintMaterializer;
            _workflowBlueprintReflector = workflowBlueprintReflector;
            _workflowContextManager = workflowContextManager;
            //_workflowContextProvider = workflowContextProvider;
            _workflowDefinitionStore = workflowDefinitionStore;
            _workflowExecutionLog = workflowExecutionLog;
            _workflowExecutionLogStore = workflowExecutionLogStore;
            _workflowFactory = workflowFactory;
            _workflowInstanceStore = workflowInstanceStore;
            _workflowProvider = workflowProvider;
            _workflowPublisher = workflowPublisher;
            _workflowRegistry = workflowRegistry;
            _workflowReviver = workflowReviver;
            _workflowRunner = workflowRunner;
            _workflowTriggerInterruptor = workflowTriggerInterruptor;

        }

        private WorkflowDefinitionIdSpecification GetWorkflowDefIdSpec()
        {
            return new WorkflowDefinitionIdSpecification(nameof(InterruptableWorkflow));
        }

        [HttpGet("GetWorkflowInstanceCount")]
        public async Task<ActionResult<int>> GetWorkflowInstanceCount()
        {
            var workflowDefIdSpec = GetWorkflowDefIdSpec();

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            return count;
        }

        [HttpGet("GetWorkflow/{Id}")]
        public async Task<ActionResult<WfInstanceInfo>> GetWorkflow(string Id)
        {
            var workflowDefIdSpec = GetWorkflowDefIdSpec();

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            var wfInstances = await _workflowInstanceStore.FindManyAsync(workflowDefIdSpec);

            var wfInstance = wfInstances.ToList().Find(wf => wf.Id == Id);

            return new WfInstanceInfo {
                Id = wfInstance.Id,
                Status = wfInstance.WorkflowStatus.ToString()
            };
        }

        [HttpGet("GetWorkflowDetails")]
        public async Task<ActionResult<WfInstanceDetails>> GetWorkflowDetails()
        {
            var workflowDefIdSpec = GetWorkflowDefIdSpec();

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            var wfInstances = await _workflowInstanceStore.FindManyAsync(workflowDefIdSpec);

            var wfInstanceDetails = new WfInstanceDetails();

            if (count == 0)
                return wfInstanceDetails;           

            wfInstanceDetails.InstanceCount = count;
            wfInstanceDetails.WfInstances = new List<WfInstanceInfo>();

            foreach (var instance in wfInstances)
            {
                var wfInstanceInfo = new WfInstanceInfo
                {
                    Id = instance.Id,
                    Status = instance.WorkflowStatus.ToString(),
                };

                wfInstanceDetails.WfInstances.Add(wfInstanceInfo);
            }

            return wfInstanceDetails;
        }

    }

    
}
