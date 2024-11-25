using System.IO;
using Hangfire.Dashboard;
using Hangfire.Dashboard.Pages;

namespace devbby.Hangfire.Dashboard.JobSearch.Dashboard.Pages
{
    public class JobSearchPage : RazorPage
    {
        private readonly string _pageHtml;

        public string Title => "Job Search";

        
        public JobSearchPage()
        {
            using var stream = typeof(JobSearchPage).Assembly.GetManifestResourceStream("devbby.Hangfire.Dashboard.JobSearch.Dashboard.Pages.JobSearchPage.html");
            if (stream != null) _pageHtml = new StreamReader(stream).ReadToEnd();
            ;
        }
        public override void Execute()
        {
            Layout = new LayoutPage(Title);
            WriteLiteral("\r\n");
            WriteLiteral(_pageHtml);
            WriteLiteral("\r\n"); 
        }

    }
}