using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hangfire.Dashboard.JobSearch.Dashboard
{
    public class PageDispatcher : IDashboardDispatcher
    {
        private readonly Func<Match, RazorPage> _pageFunc;

        public PageDispatcher(Func<Match, RazorPage> pageFunc)
        {
            _pageFunc = pageFunc;
        }

        public Task Dispatch(DashboardContext context)
        {
            context.Response.ContentType = "text/html";

            var page = _pageFunc(context.UriMatch);

            return context.Response.WriteAsync(page.ToString());
        }
    }
}