namespace Hangfire.Dashboard.JobSearch.Example;

public class TestJob
{
    private readonly ILogger<TestJob> _logger;

    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }
    public async Task DoWork()
    {
        _logger.Log(LogLevel.Information, "Job started");
        await Task.Delay(4000);
        
        var rand = new Random().Next(0,10);
        if(rand%5 == 0)
        {
            _logger.Log(LogLevel.Error, "Info: " + rand);
            throw new Exception("Something bad happened"); 
        }
        _logger.Log(LogLevel.Critical, "Info success: " + rand);
        
        System.Console.WriteLine("Hello World!");
        _logger.Log(LogLevel.Information, "Job ended");
    }
}