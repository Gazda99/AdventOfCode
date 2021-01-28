namespace AoCBase {
public abstract class BaseSolution {
    protected int DayNumber;

    /// <summary>
    /// Returns the path to a data file for this day
    /// </summary>
    protected string DataPath() {
        return $"../../../Data/{DayNumber}.txt";
    }

    protected BaseSolution(int dayNumber) {
        DayNumber = dayNumber;
    }
}
}