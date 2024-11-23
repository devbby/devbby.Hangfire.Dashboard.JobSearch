using System.IO;
using Hangfire.Dashboard.JobSearch.Dashboard;
using Hangfire.Dashboard.JobSearch.Dashboard.Pages;
using Hangfire.Dashboard.JobSearch.Options;
using Hangfire.Storage.SQLite;

namespace Hangfire.Dashboard.JobSearch
{
    public static class Configuration
    {
        public static IGlobalConfiguration UseJobSearch(this IGlobalConfiguration config, JobSearchOptions options)
        {
            var storage = JobStorage.Current as SQLiteStorage;
            
            JobSearch.SetJobSearchProvider(options.SearchProvider);
            
            
            DashboardRoutes.Routes.AddRazorPage("/JobSearch", x => new JobSearchPage());
            NavigationMenu.Items.Add(page => new MenuItem("Job Search", page.Url.To("/JobSearch"))
            {
                Active = page.RequestPath.Contains("JobSearch"), 
                Metric = DashboardMetrics.RecurringJobCount,
                
            });
            
            DashboardRoutes.Routes.Add("/SearchJobs",   new SearchJobs());
              
            
            // DashboardRoutes.Routes.Add("/JobSearch",  new PageDispatcher(match => page));
            // DashboardRoutes.Routes.Add("", new PageDispatcher()  );
            
            return config;
        }
    }
}