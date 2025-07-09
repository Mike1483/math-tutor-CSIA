namespace BlazorApp3.Services;
using BlazorApp3.Data; 
using Microsoft.EntityFrameworkCore;

public class TestResultService
{
    private readonly ApplicationDbContext _dbContext;

    public TestResultService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveTestResult(TestResult testResult)
    {
        _dbContext.TestResults.Add(testResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<TestResult>> RetrieveTestResults(int UserId)
    {
        return await _dbContext.TestResults.Where (x => x.UserId == UserId).ToListAsync();
    }

    public async Task<List<TestResult>> RetrieveTestResults(int UserId, int TopicId)
    {
        return await _dbContext.TestResults.Where (x => x.UserId == UserId && x.TopicId == TopicId).ToListAsync();
    }
    
}