namespace BlazorApp3.Data;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public abstract class Question
{
    public int QuestionId { get; set; }
    [ForeignKey("Topic")] public int TopicId { get; set; }
    public string QuestionText { get; set; }

    public QuestionType QuestionType { get; set; }
    public string QuestionHint { get; set; }

    [Required] public string Difficulty { get; set; }

    public Topic Topic { get; set; }

    public abstract bool Grade(object UserAnswer);
}