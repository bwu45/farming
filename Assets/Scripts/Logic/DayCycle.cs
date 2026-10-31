using System;

public class DayCycle
{
    public int CurrentDay { get; private set; } = 1;
    public event Action<int> OnDayAdvanced;
    public void Advance()
    {
        CurrentDay++;
        OnDayAdvanced?.Invoke(CurrentDay);
    }
}
