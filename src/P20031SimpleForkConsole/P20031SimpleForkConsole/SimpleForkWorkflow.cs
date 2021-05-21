using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;

namespace P20031SimpleForkConsole
{
    public class SimpleForkWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("This demonistrates a simple workflow with fork .")
                .WriteLine("Using forks we can branch a workflow.")
                .Then<ForkBranchDecisionActivity>()
                .Then<Fork>(
                    fork => fork.WithBranches("A", "B"),
                    fork =>
                    {
                        var aBranch = fork
                            .When("A")
                            .WriteLine("You are in A branch. First line")
                            .WriteLine("You are in A branch. Second line.")
                            .ThenNamed("AfterJoin");

                        var bBranch = fork
                            .When("B")
                            .WriteLine("You are in B branch. First line")
                            .WriteLine("You are in B branch. Second line.")
                            .ThenNamed("AfterJoin");
                    })
                .WithName("AB Fork")
                .Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny)).WithName("AfterJoin")
                .WriteLine("Workflow finished.");
        }


    }
}
