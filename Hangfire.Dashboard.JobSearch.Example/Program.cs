using Hangfire;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.Dashboard.JobSearch.Options;
using Hangfire.Dashboard.JobSearch.SearchProvider;
using Hangfire.RecurringJobAdmin;
using Hangfire.Storage;
using Hangfire.Storage.SQLite;

namespace Hangfire.Dashboard.JobSearch.Example;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var connectionString = "hangfire.db";
        builder.Services.AddHangfire((provider, options) =>
        {
            options.UseSQLiteStorage(connectionString);
            options.UseJobSearch(new JobSearchOptions()
            {
                SearchProvider = new SqlLiteStorageSearchProvider("Data Source = "+connectionString)
            });
            options.UseRecurringJobAdmin(typeof(Program).Assembly);
            options.UseConsole();
        });
        builder.Services.AddHangfireServer();
        builder.Services.AddLogging();
        builder.Services.AddHangfireConsoleExtensions();
        
        var app = builder.Build();
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
 
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.MapHangfireDashboard("/jobs", new DashboardOptions()
        {
            DashboardTitle = "Jobs - Hangfire Dashboard",
            // Authorization = new List<HangfireAuthorizeFilter> {new HangfireAuthorizeFilter()}
        });

        var jobManager = app.Services.GetService<IRecurringJobManager>();
         
        jobManager.AddOrUpdate<TestJob>("Test job", job =>  job.DoWork() 
         ,   "*/10 * * * * *");
        
        app.Run();
    }
}