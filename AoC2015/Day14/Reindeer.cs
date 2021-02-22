namespace AoC2015.Day14 {
class Reindeer {
    private readonly int _speed;
    private readonly int _flyTime;
    private readonly int _restTime;
    public int Distance { get; private set; }
    public int Score { get; set; }

    public Reindeer(int speed, int flyTime, int restTime) {
        _speed = speed;
        _flyTime = flyTime;
        _restTime = restTime;
        _remainingTime = flyTime;
    }


    private bool _flying = true;
    private int _remainingTime;

    public void Fly() {
        if (_remainingTime == 0) {
            _remainingTime = _flying ? _restTime : _flyTime;
            _flying = !_flying;
        }

        if (_flying)
            Distance += _speed;

        _remainingTime--;
    }
}
}