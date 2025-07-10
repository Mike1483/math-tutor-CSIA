namespace BlazorApp3.Data;

public class TypeInQuestion : Question
{
    public string CorrectAnswerText { get; set; }

    public override bool Grade(object UserAnswer)
    {
        if (UserAnswer is string result)
        {
            return CorrectAnswerText == result;
        }

        return false;
    }
}