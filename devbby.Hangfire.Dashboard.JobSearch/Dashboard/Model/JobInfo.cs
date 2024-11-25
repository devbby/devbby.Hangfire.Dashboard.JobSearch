using System;

namespace devbby.Hangfire.Dashboard.JobSearch.Dashboard.Model
{
    public class JobInfo
    {
      
        public long JobId { get; }
        public string Method { get; }
        public string Status { get; }
        public bool HasSucceeded => string.Equals(Status, "Succeeded", StringComparison.InvariantCultureIgnoreCase);

        public JobInfo(long jobId, string method, string status)
        { 
            JobId = jobId;
            Method = method;
            Status = status;
        }
    }
}