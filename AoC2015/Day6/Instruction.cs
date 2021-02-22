namespace AoC2015.Day6 {
class Instruction {
    public Turning TurnActivity;
    public Coordinate From;
    public Coordinate To;

    public Instruction(string turnActivity, Coordinate from, Coordinate to) {
        TurnActivity = turnActivity switch {
            "on" => Turning.TurnOn,
            "off" => Turning.TurnOff,
            "toggle" => Turning.Toggle
        };
        From = from;
        To = to;
    }
}
}