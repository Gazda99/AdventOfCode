using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day10 {
class Solution : BaseSolution, ISolution<List<int>, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public List<int> Data { get; set; }

    public List<int> ReadData() {
        List<int> data = new List<int>();
        try {
            StreamReader file = new StreamReader(DataPath());
            data = file.ReadToEnd().ToCharArray().Select(c => (int) Char.GetNumericValue(c)).ToList();

            file.Close();
            return data;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return data;
    }

    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    public int SolveFirst() {
        List<int> data = new List<int>(Data);

        for (int i = 0; i < 40; i++)
            data = LookAndSay(data);

        return data.Count;
    }

    public int SolveSecond() {
        List<int> data = new List<int>(Data);

        for (int i = 0; i < 50; i++)
            data = LookAndSay(data);

        return data.Count;
    }


    private List<int> LookAndSay(List<int> value) {
        List<int> lookAndSayList = new List<int>();

        int lastDigit = value[0];
        int digitCount = 1;

        for (int j = 1; j < value.Count; j++) {
            if (value[j] == lastDigit)
                digitCount++;

            else {
                lookAndSayList.Add(digitCount);
                lookAndSayList.Add(lastDigit);
                lastDigit = value[j];
                digitCount = 1;
            }
        }

        lookAndSayList.Add(digitCount);
        lookAndSayList.Add(lastDigit);

        return lookAndSayList;
    }
}
}