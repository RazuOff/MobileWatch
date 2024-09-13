using TMPro;
using UnityEngine;

public class UIDigitalWatch : MonoBehaviour
{
    [SerializeField] private TMP_InputField hour, minute, second;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private AlarmController _alarmController;

    private void OnEnable()
    {
        _timeController.TimeChange += OnTimeChange;
        _alarmController.DigitalChanged += OnTimeChange;
    }

    private void OnDisable()
    {
        _timeController.TimeChange -= OnTimeChange;
        _alarmController.DigitalChanged -= OnTimeChange;
    }

    private void OnTimeChange(float hours, float minutes, float seconds)
    {
        hour.text = hours.ToString("00");
        second.text = seconds.ToString("00");
        minute.text = minutes.ToString("00");
    }
}