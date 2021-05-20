using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using P257WakeupSleepingWorkflow.Workflows;
using System.Threading.Tasks;

namespace P257WakeupSleepingWorkflow.Controllers
{
    [ApiController]
    [Route("startworkflow")]
    public class StartWorkflowController : Controller
    {
        private readonly IBuildsAndStartsWorkflow _workflowRunner;

        public StartWorkflowController(IBuildsAndStartsWorkflow workflowRunner)
        {
            _workflowRunner = workflowRunner;
        }

        [HttpGet]
        public async Task<IActionResult> StartWorkflow()
        {
            var workflowInstance = await _workflowRunner.BuildAndStartWorkflowAsync<InterruptableWorkflow>();

            return new EmptyResult();
        }
    }
}
