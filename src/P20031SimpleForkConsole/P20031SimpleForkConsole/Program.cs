using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace P20031SimpleForkConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivity<ForkBranchDecisionActivity>()
                    .AddWorkflow<SimpleForkWorkflow>())
                .BuildServiceProvider();

            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            await workflowStarter.BuildAndStartWorkflowAsync<SimpleForkWorkflow>();
            Console.WriteLine("Done. Good bye");
        }
    }
}