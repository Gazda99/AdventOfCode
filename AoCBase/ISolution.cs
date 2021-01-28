namespace AoCBase {
/// <summary>
/// 
/// </summary>
/// <typeparam name="T0">Read data type</typeparam>
/// <typeparam name="T1">Part one return type, conversion to string must be possible</typeparam>
/// <typeparam name="T2">Part two return type, conversion to string must be possible</typeparam>
public interface ISolution<T0, T1, T2> {
    public T0 ReadData();
    public T1 SolveFirst();
    public T2 SolveSecond();
}
}