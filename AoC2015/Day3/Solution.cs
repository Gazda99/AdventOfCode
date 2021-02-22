using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day3 {
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
        return Traverse(Data).Count;
    }

    public int SolveSecond() {
        char[] santaDirections = Data.Where((c, i) => i % 2 == 0).ToArray();
        char[] robotDirections = Data.Where((c, i) => i % 2 != 0).ToArray();

        var santaHouses = Traverse(santaDirections);
        var robotHouses = Traverse(robotDirections);

        return santaHouses.Union(robotHouses).ToList().Count;
    }

    public static LinkedList<Coordinate> Traverse(char[] dirArray) {
        //assuming we are using Cartesian coordinate system
        LinkedList<Coordinate> visitedCoordinates = new LinkedList<Coordinate>();
        int x = 0;
        int y = 0;

        //add first position
        Coordinate startPos = new Coordinate(x, y);
        visitedCoordinates.AddFirst(startPos);

        for (int i = 0; i < dirArray.Length; i++) {
            switch (dirArray[i]) {
                case '^':
                    y++;
                    break;
                case '>':
                    x++;
                    break;
                case 'v':
                    y--;
                    break;
                case '<':
                    x--;
                    break;
            }

            Coordinate coord = new Coordinate(x, y);
            //check if list already contains this coord
            if (!visitedCoordinates.Contains(coord))
                //if not, add
                visitedCoordinates.AddFirst(coord);
        }

        return visitedCoordinates;
    }
}
}