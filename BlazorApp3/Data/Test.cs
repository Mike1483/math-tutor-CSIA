namespace BlazorApp3.Data;
using System;
using System.Collections.Generic;
using System.Linq;

public class Test
{
    public string TestId { get; set; } = Guid.NewGuid().ToString();
    public int TopicId { get; set; }
    public int QuestionCount { get; set; }
    public string Difficulty { get; set; }
    public bool IsTimed { get; set; }
    public int DurationMinutes { get; set; }
    public int NumberOfQuestions { get; set; }
    public List<Question> Questions { get; set; } = new List<Question>();

    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int TotalQuestionsAttempt { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime TestEndTime { get; set; }

    public Test(int topicId, string difficulty, int length, bool isTimed, int durationMinutes, List<Question> questions)  
    {
        NumberOfQuestions = length;
        TopicId = topicId;
        Difficulty = difficulty;
        IsTimed = isTimed;
        DurationMinutes = durationMinutes;
        Questions = questions;
        
    }

    public double CalculateScorePercentage()
    {
        if (TotalQuestionsAttempt == 0) return 0.0;
        return (double) CorrectAnswerCount * 100.0 / TotalQuestionsAttempt;
    }
}