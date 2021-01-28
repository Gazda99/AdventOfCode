using System;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day1 {
class Solution : BaseSolution, ISolution<char[], int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public char[] Data { get; set; }
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    public char[] ReadData() {
        try {
            StreamReader file = new StreamReader(DataPath());

            char[] data = file.ReadToEnd().ToCharArray();

            file.Close();
            return data;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return Array.Empty<char>();
    }

    public int SolveFirst() {
        //count floors that goes up
        int leftParenthesisCount = Data.Count(c => c == '(');
        //count floors that goes down
        int rightParenthesisCount = Data.Count(c => c == ')');
        //sum them
        int finalFloor = 0 + leftParenthesisCount - rightParenthesisCount;
        return finalFloor;
    }

    public int SolveSecond() {
        int basementFloor = 0;
        int currentFloor = 0;

        //move floor by floor and check if we finally get to the basement
        for (int i = 0; i < Data.Length; i++) {
            if (Data[i] == '(')
                currentFloor++;
            else currentFloor--;
            basementFloor++;
            if (currentFloor == -1)
                return basementFloor;
        }

        return -1;
    }
}
}