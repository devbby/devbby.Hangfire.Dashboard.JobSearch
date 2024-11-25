using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using devbby.Hangfire.Dashboard.JobSearch.Dashboard.Model;

namespace devbby.Hangfire.Dashboard.JobSearch.Options
{
    public interface IJobSearchProvider 
    { 
        Task<List<JobInfo>> SearchAsync(string term, CancellationToken cancellationToken);

        // private bool IsStorageSupported(T storage)
        // {
        //     if (storage == null) throw new ArgumentNullException(nameof(storage));
        //     
        //     var storageType = storage.GetType();
        //     // JobStorage.Current.
        //     return true;
        // }
    }
}