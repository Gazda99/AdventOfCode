using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AoCBase {
public abstract class BaseUserInterface {
    public abstract void ShowAnswer(int dayNumber);

    public void Start() {
        while (AskUser()) { }
    }


    /// <summary>
    /// Asks user for input
    /// </summary>
    /// <returns>Bool value indicating if program should still be running or not</returns>
    public bool AskUser() {
        Console.WriteLine("Type number of the day which You`d like to see an answer for." +
                          "Type quit if you want to quit");
        var input = Console.ReadLine();
        if (input != null && input.ToLower() == "quit")
            return false;
        int dayNumber;
        try {
            dayNumber = Int32.Parse(input);
        }
        catch {
            BadNumber();
            return true;
        }

        if (dayNumber < 1 || dayNumber > 25) {
            BadNumber();
            return true;
        }

        ShowAnswer(dayNumber);
        return true;
    }

    /// <summary>
    /// Shows error message when user typed invalid number in
    /// </summary>
    private static void BadNumber() {
        Console.WriteLine("Invalid day number, please try again.");
    }


    /// <summary>
    /// Write the answer for specified day using IAnswer interface passed as param
    /// </summary>
    /// <param name="iAnswer"></param>
    /// <param name="dayNumber"></param>
    public void WriteAnswer(IAnswer iAnswer, int dayNumber) {
        if (iAnswer == null) {
            Console.WriteLine("Something went wrong");
            return;
        }

        string partOne = iAnswer.PartOneAnswer;
        Console.WriteLine($"Day {dayNumber} part 1 answer:");
        Console.WriteLine(partOne);

        string partTwo = iAnswer.PartTwoAnswer;
        Console.WriteLine($"Day {dayNumber} part 2 answer:");
        Console.WriteLine(partTwo);
    }
}
}