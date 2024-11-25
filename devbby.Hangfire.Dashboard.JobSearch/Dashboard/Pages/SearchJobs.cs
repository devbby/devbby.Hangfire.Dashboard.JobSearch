using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Storage;
using Newtonsoft.Json;

namespace devbby.Hangfire.Dashboard.JobSearch.Dashboard.Pages
{
    public class SearchJobs : IDashboardDispatcher
    {
        private readonly IStorageConnection _connection;
        private readonly IMonitoringApi _monitoringApi;

        public SearchJobs()
        {
            
            _connection = JobStorage.Current.GetConnection();
            _monitoringApi = JobStorage.Current.GetMonitoringApi();
        }

        public async Task Dispatch(DashboardContext context)
        {
            var term = context.Request.GetQuery("term");
            var jobs = await JobSearch.SearchAsync(term);
            
            // 
            //
            //  
            //
            // var successfulJobs = _monitoringApi.SucceededJobs(0, (int)_monitoringApi.SucceededListCount());
            // var failedJobs = _monitoringApi.FailedJobs(0, (int)_monitoringApi.FailedCount());
            //
            // var jobs = successfulJobs.Select(x => new JobInfo(  long.Parse(x.Key),
            //     x.Value.Job.Method.Name,
            //     x.Value.InSucceededState)).ToList();
            //
            // jobs.AddRange(
            //     failedJobs.Select(x=> new JobInfo( long.Parse(x.Key), x.Value.Job.Method.Name, !x.Value.InFailedState  )).ToList()
            // );
            // context.Response.StatusCode = 200;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(jobs)); 
        }
    }
}