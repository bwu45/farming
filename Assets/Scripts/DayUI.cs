using UnityEngine;
using TMPro;
public class DayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText; 
    private void Start()
    {
        DayCycle cycle = GameManager.instance.dayManager.Cycle;
        cycle.OnDayAdvanced += UpdateDay;
        UpdateDay(cycle.CurrentDay);
    }

    private void OnDestroy()
    {
        if (GameManager.instance == null || GameManager.instance.dayManager == null) return;
        GameManager.instance.dayManager.Cycle.OnDayAdvanced -= UpdateDay;
    }

    private void UpdateDay(int day)
    {
        dayText.text = $"Day: {day}";
    }
}
