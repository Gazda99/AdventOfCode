namespace AoCBase {
public interface ISolution<out T1, out T2> {
    private static string DataPath(int dayNumber) {
        return $"../../../Data/{dayNumber}.txt";
    }
    public T1 SolveFirst();
    public T2 SolveSecond();
}
}