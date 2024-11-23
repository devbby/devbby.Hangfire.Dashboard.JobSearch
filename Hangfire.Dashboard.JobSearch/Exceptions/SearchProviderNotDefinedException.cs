using System;

namespace Hangfire.Dashboard.JobSearch.Exceptions
{
    public class SearchProviderNotDefinedException : Exception
    {
        public SearchProviderNotDefinedException() : base("Job search provider not defined.")
        {
             
        }
    }
}