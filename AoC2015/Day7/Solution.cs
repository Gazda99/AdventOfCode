using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCBase;

namespace AoC2015.Day7 {
class Solution : BaseSolution, ISolution<List<Operation>, int, int>, IAnswer {
    public Solution(int dayNumber) : base(dayNumber) {
        Data = ReadData();
    }

    public List<Operation> Data { get; set; }
    public string PartOneAnswer => SolveFirst().ToString();
    public string PartTwoAnswer => SolveSecond().ToString();

    private Circuit _circuit = new Circuit();

    public List<Operation> ReadData() {
        List<Operation> opList = new List<Operation>();

        try {
            StreamReader file = new StreamReader(DataPath());
            string line;

            while ((line = file.ReadLine()) != null) {
                Operation op = new Operation();

                string[] splitted = line.Split();

                switch (splitted.Length) {
                    case 3:
                        op.SetData(new string[] {splitted[0], splitted[2]});
                        break;
                    case 4:
                        op.SetGate(splitted[0]);
                        op.SetData(new string[] {splitted[1], splitted[3]});
                        break;
                    case 5:
                        op.SetGate(splitted[1]);
                        op.SetData(new string[] {splitted[0], splitted[2], splitted[4]});
                        break;
                    default:
                        throw new ArgumentException("Something went Wrong!");
                }

                opList.Add(op);
            }

            file.Close();
            return opList;
        }
        catch (IOException e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return opList;
    }

    public int SolveFirst() {
        _circuit.RunCircuit(Data);
        return _circuit.GetWire("a");
    }

    public int SolveSecond() {
        ushort aValue = _circuit.GetWire("a");
        _circuit = new Circuit();
        Operation b = Data.FirstOrDefault(o => o.Count == 2 && o.GetOperationValue(1) == "b");
        b.SetData(new string[] {aValue.ToString(), "b"});
        _circuit.RunCircuit(Data);
        return _circuit.GetWire("a");
    }
}
}