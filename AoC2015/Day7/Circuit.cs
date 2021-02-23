using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015.Day7 {
class Circuit {
    private Dictionary<string, ushort> _wires = new Dictionary<string, ushort>();

    public void ClearDict() {
        _wires = new Dictionary<string, ushort>();
    }

    /// <summary>
    /// Modifies the content of Dict
    /// </summary>
    private void Modify(string key, ushort value) {
        if (!_wires.ContainsKey(key))
            _wires.Add(key, value);
        else _wires[key] = value;
    }

    /// <summary>
    /// Returns same values if it is ushort, or value from dict corresponding to the key
    /// </summary>
    private ushort ValueOrDict(dynamic value) {
        if (value is string)
            return _wires[value];
        return value;
    }

    public ushort GetWire(string key) {
        return _wires.ContainsKey(key) ? _wires[key] : (ushort) 0;
    }


    public void RunCircuit(List<Operation> opList) {
        int operationCount = opList.Count;
        //create queue of operations
        Queue<Operation> opQueue = new Queue<Operation>(opList);
        //create list of operations which cannot be executed yet
        List<Operation> opDelayed = new List<Operation>();


        while (operationCount > 0) {
            Operation op = opDelayed.FirstOrDefault(delayed => CanBeExecuted(GetWiresSymbols(delayed)));
            //before using queue, check if we cannot use any op from delayed

            //it executes when working op was found in delayed list
            if (op is not null)
                //so we can remove it from this list
                opDelayed.Remove(op);

            //it executes when working op was NOT found in delayed list
            if (op is null) {
                //so we can dequeue now
                op = opQueue.Dequeue();
                //if cannot be executed yet, add this op to delayed and skip to next iteration
                if (!CanBeExecuted(GetWiresSymbols(op))) {
                    opDelayed.Add(op);
                    continue;
                }
            }

            //op can be executed now
            Execute(op);
            //after execution, remove it from list
            //opList.Remove(op);
            operationCount--;
        }
    }


    /// <summary>
    /// Checks if current set of keys are present in Dict
    /// </summary>
    private bool CanBeExecuted(string[] keys) {
        bool check = true;
        for (int i = 0; i < keys.Length; i++) {
            if (_wires.ContainsKey(keys[i])) continue;
            check = false;
            break;
        }

        return check;
    }

    /// <summary>
    /// Returns string array of wires on the left of assignment operator (->)
    /// </summary>
    private string[] GetWiresSymbols(Operation op) {
        List<string> values = new List<string>();
        for (int i = 0; i < op.Count - 1; i++) {
            dynamic value = op.GetOperationValue(i);
            if (value is string)
                values.Add(value);
        }

        return values.ToArray();
    }

    private void Execute(Operation op) {
        ushort result;
        switch (op.Gate) {
            case LogicGates.NOT:
                Modify(op.GetOperationValue(1), (ushort) ~ValueOrDict(op.GetOperationValue(0)));
                break;
            case LogicGates.AND:
                result = (ushort) (ValueOrDict(op.GetOperationValue(0)) & ValueOrDict(op.GetOperationValue(1)));
                Modify(op.GetOperationValue(2), result);
                break;
            case LogicGates.OR:
                result = (ushort) (ValueOrDict(op.GetOperationValue(0)) | ValueOrDict(op.GetOperationValue(1)));
                Modify(op.GetOperationValue(2), result);
                break;
            case LogicGates.LSHIFT:
                result = (ushort) (ValueOrDict(op.GetOperationValue(0)) << ValueOrDict(op.GetOperationValue(1)));
                Modify(op.GetOperationValue(2), result);
                break;
            case LogicGates.RSHIFT:
                result = (ushort) (ValueOrDict(op.GetOperationValue(0)) >> ValueOrDict(op.GetOperationValue(1)));
                Modify(op.GetOperationValue(2), result);
                break;
            case null:
                Modify(op.GetOperationValue(1), ValueOrDict(op.GetOperationValue(0)));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
}