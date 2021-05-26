using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Models;

namespace P20240PersistenceEfMsSql
{
    public class HelloWorldPersistanceWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder // You need to set this option for your workflow to persist
                .WithPersistenceBehavior(WorkflowPersistenceBehavior.WorkflowBurst)
                .WriteLine("Hello World!");
        }
    }
}
