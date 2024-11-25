using devbby.Hangfire.Dashboard.JobSearch.Dashboard.Pages;
using devbby.Hangfire.Dashboard.JobSearch.Options;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Storage.SQLite;

namespace devbby.Hangfire.Dashboard.JobSearch
{
    public static class Configuration
    {
        public static IGlobalConfiguration UseJobSearch(this IGlobalConfiguration config, JobSearchOptions options)
        {
            var storage = JobStorage.Current as SQLiteStorage;
            
            global::devbby.Hangfire.Dashboard.JobSearch.JobSearch.SetJobSearchProvider(options.SearchProvider);
            
            
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