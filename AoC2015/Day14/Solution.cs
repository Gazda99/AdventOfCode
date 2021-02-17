using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCBase;

namespace AoC2015.Day14 {
class Solution : BaseSolution, ISolution<List<Solution.Reindeer>, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
        Race();
    }

    public List<Reindeer> Data { get; set; }

    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    public List<Reindeer> ReadData() {
        Regex rgx = new Regex(@"\d+");
        List<Reindeer> reindeers = new List<Reindeer>();
        try {
            StreamReader file = new StreamReader(DataPath());
            string line;
            while ((line = file.ReadLine()) != null) {
                int[] array = rgx.Matches(line).Select(m => Int32.Parse(m.Value)).ToArray();
                Reindeer reindeer = new Reindeer(array[0], array[1], array[2]);
                reindeers.Add(reindeer);
            }

            file.Close();
            return reindeers;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return reindeers;
    }

    public int SolveFirst() {
        return Data.Max(r => r.Distance);
    }

    public int SolveSecond() {
        return Data.Max(r => r.Score);
    }

    public void Race() {
        const int time = 2503;
        for (int i = 0; i < time; i++) {
            foreach (Reindeer reindeer in Data)
                reindeer.Fly();
            //find current max distance
            int currentMaxDist = Data.Aggregate((r1, r2) => r1.Distance > r2.Distance ? r1 : r2).Distance;
            foreach (Reindeer reindeer in Data.Where(r => r.Distance == currentMaxDist))
                //increase score by one for each reindeer with distance same as current max
                reindeer.Score++;
        }
    }

    public class Reindeer {
        private readonly int _speed;
        private readonly int _flyTime;
        private readonly int _restTime;
        public int Distance { get; private set; }
        public int Score { get; set; }

        public Reindeer(int speed, int flyTime, int restTime) {
            _speed = speed;
            _flyTime = flyTime;
            _restTime = restTime;
            _remainingTime = flyTime;
        }


        private bool _flying = true;
        private int _remainingTime;

        public void Fly() {
            if (_remainingTime == 0) {
                _remainingTime = _flying ? _restTime : _flyTime;
                _flying = !_flying;
            }

            if (_flying)
                Distance += _speed;

            _remainingTime--;
        }
    }
}
}