using System;
using System.IO;
using AoCBase;

namespace AoC2015.Day2 {
class Solution : BaseSolution, ISolution<Present[], int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public Present[] Data { get; set; }

    public string PartOneAnswer => SolveFirst().ToString();

    public string PartTwoAnswer => SolveSecond().ToString();


    public Present[] ReadData() {
        try {
            StreamReader file = new StreamReader(DataPath());

            string[] lines = file.ReadToEnd().Split(Environment.NewLine);
            Present[] data = new Present[lines.Length];

            for (int i = 0; i < lines.Length; i++) {
                string[] line = lines[i].Split("x");
                Present present = new Present {
                    L = Int32.Parse(line[0].Trim().Replace("X", String.Empty)),
                    W = Int32.Parse(line[1].Trim().Replace("X", String.Empty)),
                    H = Int32.Parse(line[2].Trim().Replace("X", String.Empty))
                };

                data[i] = present;
            }

            file.Close();
            return data;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return Array.Empty<Present>();
    }

    public int SolveFirst() {
        int wrappingPaper = 0;
        //sum needed paper for every present
        for (int i = 0; i < Data.Length; i++) {
            wrappingPaper += PaperForOnePresent(Data[i]);
        }

        return wrappingPaper;
    }

    public int SolveSecond() {
        int ribbon = 0;
        //sum needed ribbon for every present
        for (int i = 0; i < Data.Length; i++) {
            ribbon += RibbonForOnePresent(Data[i]);
        }

        return ribbon;
    }

    /// <summary>
    /// Returns the needed paper amount for one present
    /// </summary>
    private static int PaperForOnePresent(Present present) {
        int a = present.L * present.W;
        int b = present.W * present.H;
        int c = present.H * present.L;

        int min = Math.Min(Math.Min(a, b), c);
        return 2 * (a + b + c) + min;
    }

    /// <summary>
    /// Returns the needed ribbon amount for one present
    /// </summary>
    private static int RibbonForOnePresent(Present present) {
        int volume = present.L * present.W * present.H;
        int[] values = present.ToArray();
        Array.Sort(values);
        return volume + 2 * values[0] + 2 * values[1];
    }
}

class Present {
    public int L;
    public int W;
    public int H;

    public int[] ToArray() {
        return new int[] {L, W, H};
    }
}
}