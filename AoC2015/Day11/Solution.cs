using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day11 {
class Solution : BaseSolution, ISolution<char[], string, string>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public char[] Data { get; set; }

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

    private string _partOneAnswer;

    public string PartOneAnswer {
        get {
            if (_partOneAnswer is null)
                _partOneAnswer = SolveFirst();
            return _partOneAnswer;
        }
    }

    public string PartTwoAnswer => SolveSecond();

    private const int A = 97;
    private const int Z = 122;

    private const int InRange = Z - A + 1;

    private readonly char[] _forbiddenLetters = new[] {'i', 'o', 'l'};


    public string SolveFirst() {
        return new string(FindPassword(Data));
    }


    public string SolveSecond() {
        long checkSum = CalculateCheckSum(SolveFirst().ToCharArray());
        checkSum++;
        return new string(FindPassword(CreateNextPassword(Data.Length, checkSum)));
    }

    /// <summary>
    /// Finds the password which fullfils requirements
    /// </summary>
    private char[] FindPassword(char[] data) {
        long checkSum = CalculateCheckSum(data);
        char[] password = CreateNextPassword(data.Length, checkSum);

        //todo: skip forbidden letters to speed up the while loop 

        while (true) {
            bool containsForbidden = DoIntersect(password, _forbiddenLetters);
            bool isIncreasing = IncreasingLetters(password);
            bool hasPair = HasPair(password);

            if (!containsForbidden && isIncreasing && hasPair)
                return password;

            checkSum++;
            password = CreateNextPassword(data.Length, checkSum);
        }
    }


    /// <summary>
    /// Calculates checksum of password
    /// </summary>
    private static long CalculateCheckSum(char[] array) {
        long check = 0;
        for (int j = 0; j < array.Length; j++)
            check += (array[j] - A) * (long) Math.Pow(InRange, array.Length - 1 - j);

        return check;
    }

    /// <summary>
    /// Creates password as char array based on checksum
    /// </summary>
    private static char[] CreateNextPassword(int length, long checkSum) {
        char[] password = new char[length];
        for (int i = length - 1; i >= 0; i--)
            password[^(i + 1)] = (char) ((checkSum / (long) Math.Pow(InRange, i)) % InRange + A);

        return password;
    }


    /// <summary>
    /// Checks if two provided array intersects
    /// </summary>
    private static bool DoIntersect(char[] array1, char[] array2) {
        return array1.Intersect(array2).Any();
    }

    /// <summary>
    /// Checks if provided array has 2 different int pairs
    /// </summary>
    private static bool HasPair(char[] array) {
        List<int> pairs = new List<int>(2);

        for (int i = 0; i < array.Length - 1; i++) {
            //check if next element is the same
            if (array[i] == array[i + 1]) {
                //check if we already have this pair 
                if (!pairs.Contains(array[i]))
                    pairs.Add(array[i]);
                //skip one because we cannot overlap
                i++;
            }
        }

        return pairs.Count == 2;
    }

    /// <summary>
    /// Checks if provided array has 3 increasing ints in a row
    /// </summary>
    private static bool IncreasingLetters(char[] array) {
        for (int i = 0; i < array.Length - 2; i++) {
            //check if letter is y or z, because they cannot increase 2 times
            if (array[i] == 121 || array[i] == 122)
                continue;
            //check if they increasing by 1
            if (array[i] + 2 == (array[i + 1] + 1) && array[i] + 2 == array[i + 2])
                return true;
        }

        return false;
    }
}
}