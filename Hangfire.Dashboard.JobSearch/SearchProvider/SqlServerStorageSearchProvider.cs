using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Dashboard.JobSearch.Dashboard.Model;
using Hangfire.Dashboard.JobSearch.Model;
using Hangfire.Dashboard.JobSearch.Options;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Hangfire.Dashboard.JobSearch.SearchProvider;

public class SqlServerStorageSearchProvider : IJobSearchProvider 
{
    private readonly string _connectionString;

    public SqlServerStorageSearchProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<List<JobInfo>> SearchAsync(string term, CancellationToken cancellationToken)
    {
        var list = new List<JobInfo>();
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = @"
                        select distinct(J.Id), J.InvocationData, J.StateId, J.StateName, J.ExpireAt
                        from HangFire.Job J
                        left join HangFire.Hash H on H.Field = 'jobId' and H.Value = J.Id
                        left join HangFire.[Set] S on S.[Key] = replace( H.[Key], ':refs:', ':') and H.[Key] IS NOT NULL
                        where S.Value like @term or J.InvocationData like @term  or J.Id like @term
                    ";
        await using var command = new SqlCommand(query, connection); 
        command.Parameters.AddWithValue("@term","%" + term + "%");
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            int.TryParse(reader["Id"].ToString(), out var id);
            var invocationDataStr = reader["InvocationData"].ToString();
            var status = reader["StateName"].ToString();
            var data = JsonConvert.DeserializeObject<InvocationData>(invocationDataStr);
            list.Add( new JobInfo(id, data.Method, status));  
        }

        return list;
    }
}