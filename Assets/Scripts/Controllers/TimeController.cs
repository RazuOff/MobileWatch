using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float _watchSpeed = 1f;
    [SerializeField] private APIManager apiManager;
    private bool _isActive = true;
    public event Action<float, float, float> TimeChange;
    public float CurrentHours { get; private set; } = 0;
    public float CurrentMinutes { get; private set; } = 0;
    public float CurrentSeconds { get; private set; } = 0;

    public bool IsActive
    {
        set
        {
            if (value == true)
            {
                _isActive = value;
                StartCoroutine(GetTime());
            }
            else
            {
                _isActive = value;
            }
        }
        get { return _isActive; }
    }

    private void Start()
    {
        StartCoroutine(GetTime());
    }

    private void Update()
    {
        if (IsActive)
            TimeControll();
    }

    private void TimeControll()
    {
        CurrentSeconds += Time.deltaTime * _watchSpeed;
        if (CurrentSeconds >= 60f)
        {
            CurrentSeconds = 0;
            CurrentMinutes++;
            if (CurrentMinutes >= 60f)
            {
                CurrentMinutes = 0;
                CurrentHours++;
                StartCoroutine(GetTime());
                if (CurrentHours == 24)
                {
                    CurrentHours = 0;
                }
            }
        }
        TimeChange?.Invoke(CurrentHours, CurrentMinutes, CurrentSeconds);
    }

    private IEnumerator GetTime()
    {
        yield return StartCoroutine(apiManager.FetchAndUseTimeData());
        CurrentHours = GetHours();
        CurrentMinutes = GetMinutes();
        CurrentSeconds = GetSeconds();
        TimeChange?.Invoke(CurrentHours, CurrentMinutes, CurrentSeconds);
    }

    private float GetHours()
    {
        return float.Parse(apiManager.TimeData.hour);
    }

    private float GetSeconds()
    {
        return float.Parse(apiManager.TimeData.seconds);
    }

    private float GetMinutes()
    {
        return float.Parse(apiManager.TimeData.minute);
    }
}