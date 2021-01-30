using System;
using System.Collections.Generic;
using System.IO;
using AoCBase;

namespace AoC2015.Day6 {
class Solution : BaseSolution, ISolution<List<Solution.Instruction>, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
        _grid = CreateGrid(GridSize);
    }

    public List<Instruction> Data { get; set; }
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    private const int GridSize = 1000;
    private int[][] _grid;

    public List<Instruction> ReadData() {
        List<Instruction> instructionList = new List<Instruction>();

        try {
            StreamReader file = new StreamReader(DataPath());
            string line;

            while ((line = file.ReadLine()) != null) {
                string turn;
                Coordinate from;
                Coordinate to;

                //read input and create Instructions
                string[] rawData = line.Split(" ");
                if (rawData.Length == 4) {
                    from = new Coordinate(Array.ConvertAll(rawData[1].Split(","),
                        s => int.Parse(s)));
                    to = new Coordinate(Array.ConvertAll(rawData[3].Split(","),
                        s => int.Parse(s)));
                    turn = rawData[0].Trim();
                }
                else {
                    from = new Coordinate(Array.ConvertAll(rawData[2].Split(","),
                        s => int.Parse(s)));
                    to = new Coordinate(Array.ConvertAll(rawData[4].Split(","),
                        s => int.Parse(s)));
                    turn = rawData[1].Trim();
                }

                Instruction instruction = new Instruction(turn, from, to);

                instructionList.Add(instruction);
            }

            file.Close();
            return instructionList;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return instructionList;
    }

    /// <summary>
    /// Creates new Grid gridSize x gridSize
    /// </summary>
    private static int[][] CreateGrid(int gridSize) {
        int[][] grid = new int[gridSize][];
        for (int y = 0; y < grid.Length; y++) {
            grid[y] = new int[gridSize];

            for (int x = 0; x < grid[y].Length; x++)
                grid[y][x] = 0;
        }

        return grid;
    }

    public int SolveFirst() {
        foreach (Instruction instruction in Data) {
            for (int y = instruction.From.B; y <= instruction.To.B; y++) {
                for (int x = instruction.From.A; x <= instruction.To.A; x++) {
                    _grid[y][x] = instruction.TurnActivity switch {
                        Turning.TurnOn => 1,
                        Turning.TurnOff => 0,
                        Turning.Toggle => (_grid[y][x] + 1) % 2,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }
        }

        return CountTurnedOn(_grid);
    }

    public int SolveSecond() {
        _grid = CreateGrid(GridSize);
        foreach (Instruction instruction in Data) {
            for (int y = instruction.From.B; y <= instruction.To.B; y++) {
                for (int x = instruction.From.A; x <= instruction.To.A; x++) {
                    switch (instruction.TurnActivity) {
                        case Turning.TurnOn:
                            _grid[y][x] += 1;
                            break;
                        case Turning.TurnOff:
                            if (_grid[y][x] > 0)
                                _grid[y][x] -= 1;
                            break;
                        case Turning.Toggle:
                            _grid[y][x] += 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        return CountTurnedOn(_grid);
    }

    /// <summary>
    /// Counts lights turned on 
    /// </summary>
    private int CountTurnedOn(int[][] grid) {
        int sum = 0;
        for (int y = 0; y < grid.Length; y++) {
            for (int x = 0; x < grid[y].Length; x++) {
                if (grid[y][x] != 0) sum += grid[y][x];
            }
        }

        return sum;
    }

    public class Instruction {
        public Turning TurnActivity;
        public Coordinate From;
        public Coordinate To;

        public Instruction(string turnActivity, Coordinate from, Coordinate to) {
            TurnActivity = turnActivity switch {
                "on" => Turning.TurnOn,
                "off" => Turning.TurnOff,
                "toggle" => Turning.Toggle
            };
            From = from;
            To = to;
        }
    }

    public class Coordinate {
        public int A;
        public int B;

        public Coordinate(int[] values) {
            A = values[0];
            B = values[1];
        }
    }

    public enum Turning {
        TurnOn,
        TurnOff,
        Toggle
    }
}
}