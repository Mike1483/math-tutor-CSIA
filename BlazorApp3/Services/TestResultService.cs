namespace BlazorApp3.Services;

using BlazorApp3.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Class for communicating with Test Results in the database
/// </summary>
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

    public async Task<List<TestResult>> RetrieveTestResults(int userId)
    {
        return await _dbContext.TestResults.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<List<TestResult>> RetrieveTestResults(int userId, int topicId)
    {
        return await _dbContext.TestResults.Where(x => x.UserId == userId && x.TopicId == topicId).ToListAsync();
    }
}