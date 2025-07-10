namespace BlazorApp3.Services;

using BlazorApp3.Data;
using Microsoft.EntityFrameworkCore;

public class TopicService
{
//Class for communicating with the data base
    private readonly ApplicationDbContext _dbContext;

    public TopicService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Topic>> GetAllTopicsAsync() //Method to get all topics
    {
        return await _dbContext.Topics.ToListAsync();
    }

    public async Task<Topic?> GetTopicByIdAsync(int topicId) //Get Topic by topicID.
    {
        return await _dbContext.Topics.FirstOrDefaultAsync(t => t.TopicId == topicId);
    }

    public async Task AddTopicAsync(Topic topic)
    {
        _dbContext.Topics.Add(topic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTopicAsync(Topic topic)
    {
        _dbContext.Topics.Update(topic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTopicAsync(int topicId)
    {
        var topicToDelete = await _dbContext.Topics.FindAsync(topicId);
        if (topicToDelete != null)
        {
            _dbContext.Topics.Remove(topicToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}