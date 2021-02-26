using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2015.Day7 {
class Operation {
    private ushort?[] _signals;
    private string[] _wires;
    public int Count => _signals.Length;
    public LogicGates? Gate { get; private set; }

    public void SetGate(string value) {
        Gate = Enum.Parse<LogicGates>(value);
    }

    public void SetData(string[] values) {
        if (values.Length < 1 || values.Length > 3)
            throw new InvalidDataException("Bad array provided");

        _signals = new ushort?[values.Length];
        _wires = new string[values.Length];

        for (int i = 0; i < values.Length; i++)
            AddToArrays(values[i], i);
    }

    private void AddToArrays(string value, int i) {
        if (UInt16.TryParse(value, out ushort parsedValue))
            _signals[i] = parsedValue;
        else
            _wires[i] = value;
    }

    public dynamic GetOperationValue(int i) {
        if (_signals[i] is null)
            return _wires[i];
        return _signals[i];
    }

    /// Returns string array of wires on the left of assignment operator (->)
    public string[] GetNeededWiresArray() {
        List<string> neededWires = new List<string>();
        for (int i = 0; i < _wires.Length - 1; i++) {
            if (_wires[i] is not null)
                neededWires.Add(_wires[i]);
        }

        return neededWires.ToArray();
    }
}
}