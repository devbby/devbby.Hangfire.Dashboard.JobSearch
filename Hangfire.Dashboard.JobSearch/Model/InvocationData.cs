using System;
using System.Collections.Generic;

namespace Hangfire.Dashboard.JobSearch.Model;

public class InvocationData
{
    public string Type { get; set; }
    public string Method { get; set; }
    // public object[] ParameterTypes { get; set; }
    // public object[] Arguments { get; set; }
}