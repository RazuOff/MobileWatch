using System;
using UnityEngine;

public class UIAnalogWatch : MonoBehaviour
{
	[SerializeField] private Transform _minutesArrow, _secondsArrow, _hoursArrow;
	[SerializeField] private TimeController _timeController;
	[SerializeField] private AlarmController _alarmController;


	private void OnEnable()
	{
		_timeController.TimeChange += OnTimeChange;
		_alarmController.AnalogChanged += OnTimeChange;
	}

	private void OnDisable()
	{
		_timeController.TimeChange -= OnTimeChange;
		_alarmController.AnalogChanged -= OnTimeChange;
	}

	private void OnTimeChange(float currentHours, float currentMinutes, float currentSeconds)
	{
		currentHours = Mathf.Floor(currentHours);
		currentMinutes = Mathf.Floor(currentMinutes);
		currentSeconds = Mathf.Floor(currentSeconds);
		currentHours = currentHours % 12;
		_hoursArrow.localRotation = Quaternion.Euler(0, 0, -(currentHours * 30f + currentMinutes / 60f * 30f));
		_minutesArrow.localRotation = Quaternion.Euler(0, 0, -(currentMinutes * 6f + currentSeconds / 60f * 6f));
		_secondsArrow.localRotation = Quaternion.Euler(0, 0, -(currentSeconds * 6f));

	}
}
