namespace BlazorApp3.Data;

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }
    
    public override bool Grade(object UserAnswer)
    {
       
        if (UserAnswer is bool selectedBool)
        {
            return selectedBool == CorrectAnswer;
        }
        return false;
    }
}