using TMPro;
using UnityEngine;

public class UIDigitalWatch : MonoBehaviour
{
    [SerializeField] private TMP_InputField _hour, _minute, _second;
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
        _hour.text = hours.ToString("00");
        _second.text = seconds.ToString("00");
        _minute.text = minutes.ToString("00");
    }
}