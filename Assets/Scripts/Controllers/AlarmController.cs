using System;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    [SerializeField] private UIAlarmController _uiAlarmController;
    [SerializeField] private TimeController _timeController;
    private AudioSource _audioSource;
    public float Hours { get; private set; }
    public float Minutes { get; private set; }
    public float Seconds { get; private set; }
    public bool IsActive { get; private set; } = false;
    public event Action<float, float, float> AnalogChanged, DigitalChanged, Load;
    public event Action AlarmEnded;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _uiAlarmController.CreateAlarm += OnCreateAlarm;
        _uiAlarmController.AnalogAlarmChanged += OnChangeAnalog;
        _uiAlarmController.DigitalAlarmChanged += OnChangeDigital;
    }

    private void OnDisable()
    {
        _uiAlarmController.CreateAlarm -= OnCreateAlarm;
        _uiAlarmController.AnalogAlarmChanged -= OnChangeAnalog;
        _uiAlarmController.DigitalAlarmChanged -= OnChangeDigital;
    }

    private void Update()
    {
        if (IsActive)
            CheckTime();
    }

    private void OnChangeAnalog(float hour, float second, float minute)
    {
        Hours = hour;
        Seconds = second;
        Minutes = minute;
        DigitalChanged?.Invoke(Hours, Seconds, Minutes);
    }

    private void OnChangeDigital(float hour, float second, float minute)
    {
        Hours = hour;
        Seconds = second;
        Minutes = minute;
        AnalogChanged?.Invoke(Hours, Seconds, Minutes);
    }

    public void LoadAlarm(float hour, float minute, float second)
    {
        Hours = hour;
        Seconds = second;
        Minutes = minute;
        IsActive = true;
        Load?.Invoke(Hours, Seconds, Minutes);
    }

    private void CheckTime()
    {
        if (_timeController.CurrentHours == Hours && _timeController.CurrentSeconds >= Seconds && _timeController.CurrentMinutes == Minutes)
        {
            StartAlarm();
        }
    }

    private void StartAlarm()
    {
        IsActive = false;
        Debug.Log("ALARM!!");
        _audioSource.Play();
        AlarmEnded?.Invoke();
    }

    private void OnCreateAlarm(float hours, float minutes, float seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        IsActive = true;
    }
}