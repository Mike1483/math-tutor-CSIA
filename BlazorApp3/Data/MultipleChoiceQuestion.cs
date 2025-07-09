using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp3.Data;

public class MultipleChoiceQuestion : Question
{
    public string Choice1 { get; set; }
    public string Choice2 { get; set; }
    public string Choice3 { get; set; }
    public string Choice4 { get; set; }
    public char CorrectOption { get; set; }
    
    [NotMapped] 
    public List<string> ShuffledOptions { get; set; } = new List<string>();
    [NotMapped] 
    public char NewCorrectOption { get; set; }
    [NotMapped] 
    public int NewCorrectOptionIndex { get; set; }

    // public override bool Grade(object userAnswer) { }

    private static readonly Random random = new Random();

    public void QuestionRandomization()
    {
        Console.WriteLine($"--- Randomization for Question ID: {QuestionId} ---"); //DEBUG
        Console.WriteLine($"Original Options: A:{Choice1}, B:{Choice2}, C:{Choice3}, D:{Choice4}"); //DEBUG
        Console.WriteLine($"Original CorrectOption (before shuffle): {CorrectOption}");//DEBUG
        var optionsToShuffle = new List<string>()
        {
            Choice1, Choice2, Choice3, Choice4,
        }; // Creating a list of current options

        string CorrectOptionText = ""; //For the new correct text
        switch (char.ToUpper(CorrectOption)) //Assigns ChoiceX to CorrectOptionText
        {
            case 'A': CorrectOptionText = Choice1; 
                break;
            case 'B': CorrectOptionText = Choice2;
                break;
            case 'C': CorrectOptionText = Choice3;
                break;
            case 'D': CorrectOptionText = Choice4; 
                break;
            default:
                ;
                return;
        }
        Console.WriteLine($"Correct Option Text Before Shuffle: '{CorrectOptionText}'"); //DEBUG


        int swaps = 20; //Number of swaps that will be made
        for (int i = 0; i < swaps; i++)
        {
            int index1 = random.Next(optionsToShuffle.Count);
            int index2 = random.Next(optionsToShuffle.Count);

            string temp = optionsToShuffle[index1];
            optionsToShuffle[index1] = optionsToShuffle[index2];
            optionsToShuffle[index2] = temp;

            Choice1 = optionsToShuffle[0];
            Choice2 = optionsToShuffle[1];
            Choice3 = optionsToShuffle[2];
            Choice4 = optionsToShuffle[3];
        }
        Console.WriteLine($"Shuffled List (internal): {string.Join(", ", optionsToShuffle)}"); //DEBUG

        int NewCorrectOptionIndex = optionsToShuffle.IndexOf(CorrectOptionText);
        Console.WriteLine($"New Correct Index (0-3): {NewCorrectOptionIndex}"); //DEBUG

            
            if (NewCorrectOptionIndex != -1) // Check if it was found
            {
                CorrectOption = (char)('A' + NewCorrectOptionIndex); // Converting back by adding a number to 'A'
                Console.WriteLine($"Updated CorrectOption (char A-D): {CorrectOption}");
            }
            
            ShuffledOptions = optionsToShuffle; //The initial list is assigned the values of options to shuffle
            
            if (NewCorrectOptionIndex != -1) //Checks if the NewCorrectOptonsIndex is found
            {
                NewCorrectOption = (char)('A' + NewCorrectOptionIndex);
                Console.WriteLine($"Updated CorrectOption (char A-D): {CorrectOption}"); //DEBUG

            }
            else
            {
                Console.WriteLine($"Error: Correct option text '{CorrectOptionText}' not found after shuffling for question {QuestionId}.");//DEBUG
            }
        }

    public override bool Grade(object UserAnswer)
    {
        if (UserAnswer is char selectedOptionChar)
            return char.ToUpper(selectedOptionChar) == char.ToUpper(CorrectOption);
        return false;
    }
}


