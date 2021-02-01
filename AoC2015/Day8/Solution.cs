using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCBase;
using System.Web;

namespace AoC2015.Day8 {
class Solution : BaseSolution, ISolution<string[], int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public string[] Data { get; set; }
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    private int _totalCharacters;

    private int TotalCharacters {
        get {
            if (_totalCharacters == 0)
                _totalCharacters = Data.Sum(s => s.Length);
            return _totalCharacters;
        }
    }

    public string[] ReadData() {
        try {
            StreamReader file = new StreamReader(DataPath());
            string[] data = file.ReadToEnd().Split(Environment.NewLine);
            file.Close();
            return data;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return Array.Empty<string>();
    }

    public int SolveFirst() {
        int charactersInMemory = Data.Sum(s => Regex.Unescape(s).Length - 2);
        return TotalCharacters - charactersInMemory;
    }


    public int SolveSecond() {
        int charactersInMemory = Data.Sum(s => HttpUtility.JavaScriptStringEncode(s).Length + 2);
        return charactersInMemory - TotalCharacters;
    }
}
}