using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Dashboard.JobSearch.Dashboard.Model;
using Hangfire.Dashboard.JobSearch.Options;
using Hangfire.Storage.SQLite;
using Microsoft.Data.Sqlite;

namespace Hangfire.Dashboard.JobSearch.SearchProvider
{
    public class SqlLiteStorageSearchProvider   : IJobSearchProvider 
    {
        private readonly string _connectionString;
        

        public SqlLiteStorageSearchProvider(string connectionString)
        {
            var storage = JobStorage.Current as SQLiteStorage;
            _connectionString = connectionString;
        }

        public async Task<List<JobInfo>> SearchAsync(string term, CancellationToken cancellationToken)
        {
            var list = new List<JobInfo>(); 
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var command = connection.CreateCommand(); 
                command.CommandText = 
                    @"
                        select J.Id, J.InvocationData, J.StateId, J.StateName, J.ExpireAt
                        from Job J
                        left join 
                            Hash H on H.Field = 'jobId' and H.Value = J.Id 

                        left join  [Set]  S on S.Key like   '%'+ ltrim( H.Key, 'console:refs:')   and H.Key IS NOT NULL 
                        where 
                               (S.Value like $term or  S.Value like $term or J.InvocationData like $term or J.Id like $term)  
                    ";
                command.Parameters.AddWithValue("$term", term);

                await using (var reader =  await command.ExecuteReaderAsync(cancellationToken))
                {
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out var id);
                        list.Add( new JobInfo(id, "", true));  
                        
                    }
                }
            }
             
            return list; 
        }

       
    }
}