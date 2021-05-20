using Elsa.Activities.Temporal;
using Elsa.Activities.Temporal.Common.Services;
using Elsa.Attributes;
using Elsa.Persistence;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace P257WakeupSleepingWorkflow.Activities
{
    [Activity]
    public class Sleep : Timer
    {
        public Sleep(IClock clock, ILogger<Sleep> logger) : base(clock, logger)
        { }
    }
}
