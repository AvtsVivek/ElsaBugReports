using Elsa.ActivityResults;
using Elsa.Services;
using System;

namespace P20031SimpleForkConsole
{
    public class ForkBranchDecisionActivity : Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Choose a branch. A or B");
            Console.WriteLine("Typing 'a' will result in going through branch A");
            Console.WriteLine("Any other key will result in branch B");
            var userChoosenBranch = Console.ReadLine();

            if (userChoosenBranch!.ToUpper() == "A")
                return Outcome("A");
            
            return Outcome("B");
        }
    }
}
