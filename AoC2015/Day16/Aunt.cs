using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AoC2015.Day16 {
class Aunt {
    public int ID { get; set; }
    public int? Children { get; set; }
    public int? Cats { get; set; }
    public int? Goldfish { get; set; }
    public int? Trees { get; set; }
    public int? Cars { get; set; }
    public int? Perfumes { get; set; }
    public Dictionary<Breeds, int> DogsDict { get; set; }

    public Aunt() {
        DogsDict = new Dictionary<Breeds, int>();
    }


    /// <summary>
    /// Adds property to Aunt class using Reflection
    /// </summary>
    public void AddAuntProperty(string propertyName, int count) {
        PropertyInfo propertyInfo = this.GetType().GetProperty(FirstToUpperCase(propertyName));
        //it is null when value is breed of dog
        if (propertyInfo is null) {
            Breeds breed = Enum.Parse<Breeds>(FirstToUpperCase(propertyName));
            this.DogsDict.Add(breed, count);
        }
        else
            propertyInfo.SetValue(this, count, null);
    }

    private static string FirstToUpperCase(string value) {
        return value.First().ToString().ToUpper() + value.Substring(1);
    }

    public bool IsMatchPartOne(Aunt other) {
        if (!CheckCommon(other)) return false;
        if (!CheckIfEquals(this.Cats, other.Cats)) return false;
        if (!CheckIfEquals(this.Goldfish, other.Goldfish)) return false;
        if (!CheckIfEquals(this.Trees, other.Trees)) return false;

        //check dogdict
        foreach (var dog in this.DogsDict)
            if (!other.DogsDict.Contains(dog))
                return false;


        return true;
    }

    public bool IsMatchPartTwo(Aunt other) {
        if (!CheckCommon(other)) return false;
        if (!CheckIfGreater(this.Cats, other.Cats)) return false;
        if (!CheckIfGreater(this.Trees, other.Trees)) return false;
        if (!CheckIfLesser(this.Goldfish, other.Goldfish)) return false;

        foreach (var dog in this.DogsDict) {
            if (dog.Key is Breeds.Pomeranians) {
                if (this.DogsDict[Breeds.Pomeranians] >= other.DogsDict[Breeds.Pomeranians])
                    return false;
                continue;
            }

            if (!other.DogsDict.Contains(dog)) return false;
        }

        return true;
    }


    private bool CheckCommon(Aunt other) {
        if (!CheckIfEquals(this.Children, other.Children)) return false;
        if (!CheckIfEquals(this.Cars, other.Cars)) return false;
        if (!CheckIfEquals(this.Perfumes, other.Perfumes)) return false;
        return true;
    }

    private static bool CheckIfEquals(int? a, int? b) {
        if (CheckNulls(a, b)) return true;
        return a == b;
    }

    private static bool CheckIfGreater(int? a, int? b) {
        if (CheckNulls(a, b)) return true;
        return a > b;
    }

    private static bool CheckIfLesser(int? a, int? b) {
        if (CheckNulls(a, b)) return true;
        return a < b;
    }

    private static bool CheckNulls(int? a, int? b) {
        return a is null || b is null;
    }
}
}