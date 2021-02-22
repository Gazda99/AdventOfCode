using System;

namespace AoC2015.Day3 {
class Coordinate : IEquatable<Coordinate> {
    public int X;
    public int Y;

    public Coordinate(int x, int y) {
        X = x;
        Y = y;
    }

    public bool Equals(Coordinate other) {
        if (other == null) return false;
        return this.X == other.X && this.Y == other.Y;
    }

    public override int GetHashCode() => (X, Y).GetHashCode();
}
}