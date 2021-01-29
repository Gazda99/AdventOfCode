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
            _ => null
        };

        WriteAnswer(iAnswer, dayNumber);
    }
}
}