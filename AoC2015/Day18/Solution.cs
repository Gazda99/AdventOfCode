using System;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day18 {
class Solution : BaseSolution, ISolution<Grid, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }


    public Grid Data { get; set; }
    private const int Size = 100;
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    public Grid ReadData() {
        Grid grid = new Grid(Size);
        try {
            StreamReader file = new StreamReader(DataPath());
            string line;
            int lineNumber = 0;

            while ((line = file.ReadLine()) != null) {
                for (int i = 0; i < line.Length; i++)
                    grid[lineNumber, i] = line[i];

                lineNumber++;
            }


            file.Close();
            return grid;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return new Grid(0);
    }

    public int SolveFirst() {
        bool SkipFunc(int y, int x) => false;
        return SwitchLights(Data, 100, SkipFunc);
    }


    public int SolveSecond() {
        Grid tmpGrid = Data;
        tmpGrid[0, 0] = '#';
        tmpGrid[Size - 1, 0] = '#';
        tmpGrid[0, Size - 1] = '#';
        tmpGrid[Size - 1, Size - 1] = '#';

        Func<int, int, bool> skipFunc = Skip;

        return SwitchLights(tmpGrid, 100, skipFunc);
    }


    private static int SwitchLights(Grid tmpGrid, int times, Func<int, int, bool> skipFunc) {
        for (int i = 0; i < times; i++) {
            Grid copy = new Grid(tmpGrid);

            for (int y = 0; y < Size; y++) {
                for (int x = 0; x < Size; x++) {
                    if (skipFunc(y, x)) continue;
                    int neighborsTurnedOd = tmpGrid.GetSurroundings(y, x).Count(c => c == '#');
                    switch (tmpGrid[y, x]) {
                        case '#': {
                            if (neighborsTurnedOd != 2 && neighborsTurnedOd != 3)
                                copy[y, x] = '.';
                            break;
                        }
                        case '.': {
                            if (neighborsTurnedOd == 3)
                                copy[y, x] = '#';
                            break;
                        }
                    }
                }
            }

            tmpGrid = copy;
        }

        return tmpGrid.Count('#');
    }

    private bool Skip(int y, int x) {
        if (y == 0 && x == 0) return true;
        if (y == 0 && x == Size - 1) return true;
        if (y == Size - 1 && x == 0) return true;
        if (y == Size - 1 && x == Size - 1) return true;
        return false;
    }
}
}