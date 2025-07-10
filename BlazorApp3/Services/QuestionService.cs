namespace BlazorApp3.Services;

using Microsoft.EntityFrameworkCore;
using BlazorApp3.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

/// <summary>
/// Class for communicating with questions in the database
/// </summary>
public class QuestionService
{
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
            RandomizeMultipleChoiceQuestions(mcq); // Call the randomization method directly
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
            RandomizeMultipleChoiceQuestions(mcq); // Call the randomization method directly
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
            RandomizeMultipleChoiceQuestions(mcq); // Call the randomization method directly
        }

        return question;
    }

    public async Task<List<Question>> GetQuestionsForTest(int topicId, string difficulty, int numberOfQuestions)
    {
        var query = _dbContext.Questions.Where(q =>
            q.TopicId == topicId && (difficulty == "Any" || q.Difficulty == difficulty));

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
                RandomizeMultipleChoiceQuestions(mcq);
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

    public void RandomizeMultipleChoiceQuestions(MultipleChoiceQuestion question)
    {
        var optionsToShuffle = new List<string>()
        {
            question.Choice1, question.Choice2, question.Choice3, question.Choice4,
        }; // Creating a list of current options

        string CorrectOptionText = ""; //For the new correct text
        switch (char.ToUpper(question.CorrectOption)) //Assigns ChoiceX to CorrectOptionText
        {
            case 'A':
                CorrectOptionText = question.Choice1;
                break;
            case 'B':
                CorrectOptionText = question.Choice2;
                break;
            case 'C':
                CorrectOptionText = question.Choice3;
                break;
            case 'D':
                CorrectOptionText = question.Choice4;
                break;
            default:
                ;
                return;
        }

        int n = optionsToShuffle.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            string temp = optionsToShuffle[i];
            optionsToShuffle[i] = optionsToShuffle[j];
            optionsToShuffle[j] = temp;
        }

        question.Choice1 = optionsToShuffle[0];
        question.Choice2 = optionsToShuffle[1];
        question.Choice3 = optionsToShuffle[2];
        question.Choice4 = optionsToShuffle[3];

        int newCorrectOptionIndex = optionsToShuffle.IndexOf(CorrectOptionText);

        if (newCorrectOptionIndex != -1) // Check if it was found
        {
            question.CorrectOption = (char)('A' + newCorrectOptionIndex); // Converting back by adding a number to 'A'
        }

        question.ShuffledOptions = optionsToShuffle; //The initial list is assigned the values of options to shuffle
        question.NewCorrectOptionIndex = newCorrectOptionIndex;

        if (newCorrectOptionIndex != -1) //Checks if the NewCorrectOptonsIndex is found
        {
            question.NewCorrectOption = (char)('A' + newCorrectOptionIndex);
        }
    }
}