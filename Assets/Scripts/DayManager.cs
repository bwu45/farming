using UnityEngine;

public class DayManager : MonoBehaviour
{
    public DayCycle Cycle { get; } = new DayCycle();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            Cycle.Advance();
    }
}
