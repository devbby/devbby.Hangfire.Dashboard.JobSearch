using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using devbby.Hangfire.Dashboard.JobSearch.Dashboard.Model;
using devbby.Hangfire.Dashboard.JobSearch.Exceptions;
using devbby.Hangfire.Dashboard.JobSearch.Options;

namespace devbby.Hangfire.Dashboard.JobSearch
{
    public static class JobSearch
    {
        private static IJobSearchProvider  _searchProvider;
        public static async Task<List<JobInfo>> SearchAsync(string term)
        {
            if (_searchProvider == null) throw new SearchProviderNotDefinedException();
            
            return await _searchProvider.SearchAsync(term, CancellationToken.None);
        }

        public static void SetJobSearchProvider(IJobSearchProvider  searchProvider)
        {
            _searchProvider = searchProvider;
        }
    }
}