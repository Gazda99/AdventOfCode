using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day5 {
class Solution : BaseSolution, ISolution<string[], int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public string[] Data { get; set; }
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();


    private readonly char[] _vowels = new char[] {'a', 'e', 'i', 'o', 'u'};

    private readonly string[] _forbiddenStrings = new string[] {"ab", "cd", "pq", "xy"};

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
        int niceStrings = 0;
        //for each string in data array
        for (int i = 0; i < Data.Length; i++) {
            char[] chars = Data[i].ToCharArray();
            string[] strings = SplitStrings(Data[i]);

            //check if string contains forbidden pairs
            if (strings.Intersect(_forbiddenStrings).Any())
                continue;
            //check if string contains at least 3 vowels
            if (CountVowels(chars, _vowels) < 3)
                continue;
            //check if string contains two same chars in a row
            if (CountSameChars(chars, 1) < 1)
                continue;

            niceStrings++;
        }

        return niceStrings;
    }

    public int SolveSecond() {
        int niceStrings = 0;
        //for each string in data array
        for (int i = 0; i < Data.Length; i++) {
            char[] chars = Data[i].ToCharArray();
            string[] strings = SplitStrings(Data[i]);

            //check if string contains two same chars with one space between them
            if (CountSameChars(chars, 2) < 1)
                continue;

            //find all indexes of particular string
            bool isPairWithGap = false;
            for (int j = 0; j < strings.Length; j++) {
                //get all indexes of this string
                var indexes = FindOccurrences(strings, strings[j]);
                //checks if those indexes have a gap > 1
                if (MeasureGap(indexes)) {
                    isPairWithGap = true;
                    break;
                }
            }

            if (!isPairWithGap)
                continue;

            niceStrings++;
        }

        return niceStrings;
    }

    /// <summary>
    /// Returns list of indexes of word in stringArray
    /// </summary>
    private static List<int> FindOccurrences(string[] stringArray, string word) {
        List<int> indexesList = new List<int>();
        for (int i = 0; i < stringArray.Length; i++) {
            if (stringArray[i] == word)
                indexesList.Add(i);
        }

        return indexesList;
    }

    /// <summary>
    /// Calculate the gap beetwen numbers in list, if any of numbers has a gap greater than one between them returns true
    /// </summary>
    private static bool MeasureGap(List<int> intList) {
        for (int i = 0; i < intList.Count; i++) {
            for (int j = 0; j < intList.Count - i; j++) {
                if (Math.Abs(intList[i] - intList[j]) > 1)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Splits string into strings length of 2
    /// </summary>
    private static string[] SplitStrings(string toSplit) {
        string[] splitted = new string[toSplit.Length - 1];
        for (int i = 0; i < toSplit.Length - 1; i++) {
            splitted[i] = (toSplit.Substring(i, 2));
        }

        return splitted;
    }

    /// <summary>
    /// Counts how many chars from charArray are in word array
    /// </summary>
    private static int CountVowels(char[] word, char[] charArray) {
        int count = 0;
        for (int i = 0; i < charArray.Length; i++) {
            count += word.Count(c => c == charArray[i]);
        }

        return count;
    }

    /// <summary>
    /// Returns int, how many times letters are repeating with a gap beetwen them 
    /// </summary>
    private static int CountSameChars(char[] word, int gap) {
        int count = 0;
        for (int i = 0; i < word.Length - gap; i++) {
            if (word[i] == word[i + gap])
                count++;
        }

        return count;
    }
}
}