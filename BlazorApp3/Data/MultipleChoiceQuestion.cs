using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp3.Data;

public class MultipleChoiceQuestion : Question
{
    public string Choice1 { get; set; }
    public string Choice2 { get; set; }
    public string Choice3 { get; set; }
    public string Choice4 { get; set; }
    public char CorrectOption { get; set; }

    [NotMapped] public List<string> ShuffledOptions { get; set; } = new List<string>();
    [NotMapped] public char NewCorrectOption { get; set; }
    [NotMapped] public int NewCorrectOptionIndex { get; set; }

    public override bool Grade(object UserAnswer)
    {
        if (UserAnswer is char selectedOptionChar)
            return char.ToUpper(selectedOptionChar) == char.ToUpper(CorrectOption);
        return false;
    }
}