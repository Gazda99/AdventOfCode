using System.Collections.Generic;

namespace AoC2015.Day18 {
class Grid {
    private readonly char[][] _grid;
    private readonly int _size;

    // private string BadArgument(int arg1, int arg2) =>
    //     $"Argument {arg1} or {arg2} was out of range, which is 0 to {Size}";

    public char this[int y, int x] {
        get => GetGrid(y, x);
        set => SetGrid(y, x, value);
    }

    /// <summary>
    /// Gets and returns the element of grid
    /// </summary>
    private char GetGrid(int y, int x) {
        if (CheckArgument(y) && CheckArgument(x))
            return _grid[y][x];
        return (char) 0;
    }

    /// <summary>
    /// Sets the element of grid
    /// </summary>
    private void SetGrid(int y, int x, char value) {
        if (CheckArgument(y) && CheckArgument(x))
            _grid[y][x] = value;
    }

    public Grid(int size) {
        _size = size;
        _grid = CreateGrid(_size);
    }

    public Grid(Grid other) {
        _size = other._size;
        _grid = CopyArray(other._grid);
    }

    /// <summary>
    /// Returns the list of surrounding items
    /// </summary>
    public List<char> GetSurroundings(int y, int x) {
        const int len = 8;
        int[] ymod = new[] {-1, -1, -1, 0, 0, 1, 1, 1};
        int[] xmod = new[] {-1, 0, 1, -1, 1, -1, 0, 1};
        List<char> surroundings = new List<char>(len);

        for (int i = 0; i < len; i++)
            surroundings.Add(this[y + ymod[i], x + xmod[i]]);

        return surroundings;
    }


    /// <summary>
    /// Counts the occurances of given element in grid
    /// </summary>
    public int Count(char toFind) {
        int count = 0;
        for (int i = 0; i < _size; i++) {
            for (int j = 0; j < _size; j++) {
                if (_grid[i][j] == toFind)
                    count++;
            }
        }

        return count;
    }


    /// <summary>
    /// Checks if argument is inside array bound
    /// </summary>
    private bool CheckArgument(int s) {
        return s >= 0 && s < _size;
    }

    /// <summary>
    /// Creates a jagged array size x size
    /// </summary>
    private static char[][] CreateGrid(int size) {
        char[][] grid = new char[size][];
        for (int i = 0; i < grid.Length; i++)
            grid[i] = new char[size];
        return grid;
    }

    /// <summary>
    /// Creates a copy of jagged array
    /// </summary>
    private static char[][] CopyArray(char[][] array) {
        char[][] copyArray = new char[array.Length][];
        for (int i = 0; i < array.Length; i++) {
            copyArray[i] = new char[array[i].Length];
            for (int j = 0; j < array[i].Length; j++) {
                copyArray[i][j] = array[i][j];
            }
        }

        return copyArray;
    }
}
}