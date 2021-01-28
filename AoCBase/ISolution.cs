namespace AoCBase {
/// <summary>
/// 
/// </summary>
/// <typeparam name="T0">Read data type</typeparam>
/// <typeparam name="T1">Part one return type, conversion to string must be possible</typeparam>
/// <typeparam name="T2">Part two return type, conversion to string must be possible</typeparam>
public interface ISolution<T0, T1, T2> {
    /// <summary>
    /// Stores the data
    /// </summary>
    public T0 Data { get; set; }

    /// <summary>
    /// Returns the read data from file
    /// </summary>
    /// <returns></returns>
    public T0 ReadData();

    /// <summary>
    /// Calculates answer for task part one 
    /// </summary>
    /// <returns></returns>
    public T1 SolveFirst();

    /// <summary>
    ///  Calculates answer for task part two 
    /// </summary>
    /// <returns></returns>
    public T2 SolveSecond();
}
}