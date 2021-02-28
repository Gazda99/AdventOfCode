using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AoCBase;

namespace AoC2015.Day16 {
class Solution : BaseSolution, ISolution<List<Aunt>, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public List<Aunt> Data { get; set; }

    private readonly Aunt _perfectAunt = new Aunt() {
        Children = 3, Cats = 7, Goldfish = 5, Trees = 3, Cars = 2, Perfumes = 1,
        DogsDict = new Dictionary<Breeds, int>() {
            {Breeds.Samoyeds, 2},
            {Breeds.Pomeranians, 3},
            {Breeds.Akitas, 0},
            {Breeds.Vizslas, 0}
        }
    };


    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    public List<Aunt> ReadData() {
        List<Aunt> aunts = new List<Aunt>();
        try {
            StreamReader file = new StreamReader(DataPath());
            string line;

            while ((line = file.ReadLine()) != null) {
                Aunt aunt = new Aunt();
                string[] splitted = line.Split();

                aunt.ID = Int32.Parse(WithoutLastIndex(splitted[1]));
                //add other properties
                for (int i = 2; i < splitted.Length; i += 2) {
                    string value = WithoutLastIndex(splitted[i]);
                    //last count is good without removing last index
                    var count = Int32.Parse(i + 1 == splitted.Length - 1
                        ? splitted[i + 1]
                        : WithoutLastIndex(splitted[i + 1]));
                    aunt.AddAuntProperty(value, count);
                }

                aunts.Add(aunt);
            }

            file.Close();
            return aunts;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return aunts;
    }

    private static string WithoutLastIndex(string value) {
        return value.Substring(0, value.Length - 1);
    }


    public int SolveFirst() {
        foreach (Aunt aunt in Data) {
            if (aunt.IsMatchPartOne(_perfectAunt))
                return aunt.ID;
        }

        return -1;
    }

    public int SolveSecond() {
        foreach (Aunt aunt in Data) {
            if (aunt.IsMatchPartTwo(_perfectAunt))
                return aunt.ID;
        }

        return -1;
    }
}
}