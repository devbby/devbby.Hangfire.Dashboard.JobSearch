namespace Hangfire.Dashboard.JobSearch.Dashboard.Model
{
    public class JobInfo
    {
        public long JobId { get; }
        public string Method { get; }
        public bool HasSucceeded { get; }

        public JobInfo(long jobId, string method, bool hasSucceeded)
        {
            JobId = jobId;
            Method = method;
            HasSucceeded = hasSucceeded;
        }
    }
}