using AoCBase;

namespace AoC2015 {
class UserInterface : BaseUserInterface {
    /// <summary>
    /// Prints the answer for part one and two for day specified in param
    /// </summary>
    /// <param name="dayNumber">Number of day</param>
    public override void ShowAnswer(int dayNumber) {
        IAnswer iAnswer = dayNumber switch {
            1 => new Day1.Solution(dayNumber),
            2 => new Day2.Solution(dayNumber),
            3 => new Day3.Solution(dayNumber),
            4 => new Day4.Solution(dayNumber),
            5 => new Day5.Solution(dayNumber),
            6 => new Day6.Solution(dayNumber),
            7 => new Day7.Solution(dayNumber),
            8 => new Day8.Solution(dayNumber),
            9 => new Day9.Solution(dayNumber),
            10 => new Day10.Solution(dayNumber),
            _ => null
        };

        WriteAnswer(iAnswer, dayNumber);
    }
}
}