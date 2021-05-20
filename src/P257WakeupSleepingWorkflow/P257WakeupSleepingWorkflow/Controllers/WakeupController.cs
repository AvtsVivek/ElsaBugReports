using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using P257WakeupSleepingWorkflow.Activities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace P257WakeupSleepingWorkflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeupController : Controller
    {
        private readonly IWorkflowTriggerInterruptor _workflowInterruptor;

        public WakeupController(IWorkflowTriggerInterruptor workflowInterruptor)
        {
            _workflowInterruptor = workflowInterruptor;
        }

        [HttpGet]
        public async Task<IActionResult> Handle(CancellationToken cancellationToken)
        {
            // Interrupt each workflow by triggering the "Sleep" activity.
            var workflowInstances = (await _workflowInterruptor.InterruptActivityTypeAsync(nameof(Sleep), 
                cancellationToken: cancellationToken)).ToList();
            return Ok($"Interrupted {workflowInstances.Count} workflows.");
        }
    }
}
