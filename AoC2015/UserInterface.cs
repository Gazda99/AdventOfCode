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
            11 => new Day11.Solution(dayNumber),
            12 => new Day12.Solution(dayNumber),
            //  13 => new Day13.Solution(dayNumber),
            14 => new Day14.Solution(dayNumber),
            //15 => new Day15.Solution(dayNumber),
            16 => new Day16.Solution(dayNumber),
            //17 => new Day17.Solution(dayNumber),
            18 => new Day18.Solution(dayNumber),
            _ => null
        };

        WriteAnswer(iAnswer, dayNumber);
    }
}
}