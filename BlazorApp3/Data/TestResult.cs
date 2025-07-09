using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp3.Data;


public class TestResult
{
    [Key]
    public int TestResultId { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int TopicId { get; set; }
    [ForeignKey("TopicId")]
    public Topic Topic { get; set; }
    public string TopicTitle { get; set; }
    public string Difficulty { get; set; }
    public int NumberOfQuestions { get; set; }
    
    public int NumberOfCorrectAnswers { get; set; }
    public int NumberOfQuestionsAttempted {get; set; }
    public double ScorePercentage { get; set; }
    public DateTime TestEndTime { get; set; }
   /* public TimeSpan TestStartTime { get; set; }
    public TimeSpan TimeTakenTest { get; set; }  */
    
    public TestResult() { }
    
    public TestResult(int userId,int topicId, string topicTitle, string difficulty, int numberOfQuestions,
        int numberOfCorrectAnswers, int numberOfQuestionsAttempted, DateTime testEndTime)
    {
        UserId = userId;
        TopicId = topicId;
        TopicTitle = topicTitle;
        Difficulty = difficulty;
        NumberOfQuestions = numberOfQuestions;
        NumberOfCorrectAnswers = numberOfCorrectAnswers;
        NumberOfQuestionsAttempted = numberOfQuestionsAttempted;
        ScorePercentage =(double) numberOfCorrectAnswers / numberOfQuestionsAttempted *100;
        TestEndTime = testEndTime;

    }
}