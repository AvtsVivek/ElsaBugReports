using System;
using System.Threading.Tasks;
using Elsa.Persistence;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa;

namespace P20240PersistenceEfMsSql
{
    public class Program
    {
        private static async Task Main()
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa20;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    })
                    .AddConsoleActivities()
                    )
                .AddAutoMapperProfiles<Program>()
                .BuildServiceProvider();

            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            var workflowRunner = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            var runWorkflowResult = await workflowRunner.BuildAndStartWorkflowAsync<HelloWorldPersistanceWorkflow>();

            var store = services.GetRequiredService<IWorkflowInstanceStore>();

            var count = await store.CountAsync(new WorkflowDefinitionIdSpecification(nameof(HelloWorldPersistanceWorkflow)));

            Console.WriteLine(count);

            var loadedWorkflowInstance = await store.FindByIdAsync(runWorkflowResult.WorkflowInstance.Id);
            Console.WriteLine(loadedWorkflowInstance);
        }
    }
}
