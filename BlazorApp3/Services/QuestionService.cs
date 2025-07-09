namespace BlazorApp3.Services;
using Microsoft.EntityFrameworkCore;
using BlazorApp3.Data; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;


public class QuestionService
{
    //Class for communicating with the database
    private readonly ApplicationDbContext _dbContext;
    private static readonly Random _random = new Random(); // Static Random instance for efficient randomness

    public QuestionService(ApplicationDbContext dbContext)

    {
        _dbContext = dbContext;
    }
    //Get a question based on TopicId and Difficulty
    public async Task<Question> GetRandomQuestion(int topicId, string difficulty)
    {

        List<Question> allMatchingQuestions = await _dbContext.Questions
            .Where(q => q.TopicId == topicId && q.Difficulty == difficulty)
            .ToListAsync();

        if (!allMatchingQuestions.Any()) // If no questions were found return null
        {
            return null;
        }
        
        int randomIndex = _random.Next(allMatchingQuestions.Count); //Selecting random index from questions
        Question question = allMatchingQuestions[randomIndex];
        
    if (question is MultipleChoiceQuestion mcq)
        {
            mcq.QuestionRandomization(); // Call the randomization method directly
        }

        return question;
    }
    
    //Get a question based only on the TopicId
    public async Task<Question> GetRandomQuestion(int topicId)
    {

        List<Question> allMatchingQuestions = await _dbContext.Questions
            .Where(q => q.TopicId == topicId)
            .ToListAsync();
        
        if (!allMatchingQuestions.Any()) // If no questions were found
        {
            return null;
        }
        int randomIndex = _random.Next(allMatchingQuestions.Count);
        Question question = allMatchingQuestions[randomIndex];
        
        if (question is MultipleChoiceQuestion mcq)
        {
            mcq.QuestionRandomization(); // Call the randomization method directly
        }

        return question;
    }
    //Get any question from the database
    public async Task<Question> GetRandomQuestion()
    {

        List<Question> allMatchingQuestions = await _dbContext.Questions
            .ToListAsync();
        
        if (!allMatchingQuestions.Any()) // If no questions were found
        {
            return null;
        }
        int randomIndex = _random.Next(allMatchingQuestions.Count);
        Question question = allMatchingQuestions[randomIndex];
        
        if (question is MultipleChoiceQuestion mcq)
        {
            mcq.QuestionRandomization(); // Call the randomization method directly
        }

        return question;
    }

    public async Task<List<Question>> GetQuestionsForTest(int topicId, string difficulty, int numberOfQuestions)
    {
        var query = _dbContext.Questions.
            Where(q => q.TopicId == topicId && (difficulty == "Any" || q.Difficulty == difficulty));
        
        var availableQuestions = await query.ToListAsync();

        if (!availableQuestions.Any())
        {
            return new List<Question>();
        }

        var shuffledQuestions = availableQuestions.OrderBy(q => _random.Next()).ToList();
        
        var selectedQuestions = shuffledQuestions.Take(numberOfQuestions).ToList();
        foreach (var question in selectedQuestions)
        {
            if (question is MultipleChoiceQuestion mcq)
            {
                mcq.QuestionRandomization();
            }
        }
        return selectedQuestions;
    }
    public async Task<List<Question>> GetAllQuestionsAsync()
    {
        return await _dbContext.Questions.ToListAsync();
    }
    public async Task AddQuestionAsync(Question question)
    {
        _dbContext.Questions.Add(question);
        await _dbContext.SaveChangesAsync();
    }


}