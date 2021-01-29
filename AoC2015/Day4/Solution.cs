using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AoCBase;

namespace AoC2015.Day4 {
class Solution : BaseSolution, ISolution<string, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public string Data { get; set; }

    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    private readonly MD5 _md5 = MD5.Create();

    public string ReadData() {
        try {
            StreamReader file = new StreamReader(DataPath());

            string data = file.ReadToEnd();

            file.Close();
            return data;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return String.Empty;
    }

    public int SolveFirst() {
        int i = 0;
        while (true) {
            string hash = CreateHash(_md5, Data + i);
            if (hash.Substring(0, 5) == "00000")
                return i;
            i++;
        }
    }

    public int SolveSecond() {
        int i = 0;
        while (true) {
            string hash = CreateHash(_md5, Data + i);
            if (hash.Substring(0, 6) == "000000")
                return i;
            i++;
        }
    }


    /// <summary>
    /// Calculates the hash
    /// </summary>
    private static string CreateHash(MD5 md5, string key) {
        MD5 chuj = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(key);
        byte[] hashBytes = chuj.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++) {
            sb.Append(hashBytes[i].ToString("X2"));
        }

        return sb.ToString();
    }
}
}