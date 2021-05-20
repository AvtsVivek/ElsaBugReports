using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P257WakeupSleepingWorkflow.Models
{
    public class WfInstanceDetails
    {
        public List<WfInstanceInfo> WfInstances { get; set; }
        public int InstanceCount { get; set; }
    }

    public class WfInstanceInfo
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
