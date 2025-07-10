namespace BlazorApp3.Data;

public class Topic
{
    public int TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Information { get; set; } = string.Empty;
    public string Theory { get; set; } = string.Empty;
    public string? Formulas { get; set; }
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}