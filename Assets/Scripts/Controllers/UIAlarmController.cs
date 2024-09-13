using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlarmController : MonoBehaviour
{
	[SerializeField] private Transform _hoursArrow, _minutesArrow, _secondsArrow;
	[SerializeField] private TMP_InputField _hours, _minutes, _seconds;
	[SerializeField] private Button _button;
	[SerializeField] private TimeController _timeController;
	[SerializeField] private WatchHandController _watchHandController;
	[SerializeField] private TMP_Text _alarmText;
	[SerializeField] private AlarmController _alarmController;
	private bool _analogChanged = false;
	public bool madeChoice = false;
	public event Action<float, float, float> CreateAlarm, AnalogAlarmChanged, DigitalAlarmChanged;


	private void Start()
	{

		_hours.onValueChanged.AddListener(OnDigitalWatchChange);
		_minutes.onValueChanged.AddListener(OnDigitalWatchChange);
		_seconds.onValueChanged.AddListener(OnDigitalWatchChange);
		_hours.onValueChanged.AddListener(BlockButtonOnEmpty);
		_minutes.onValueChanged.AddListener(BlockButtonOnEmpty);
		_seconds.onValueChanged.AddListener(BlockButtonOnEmpty);
	}
	private void OnEnable()
	{
		_watchHandController.AnalogWatchChanged += OnAnalogWatchChanged;
		_alarmController.Load += OnLoadAlarm;
		_alarmController.AlarmEnded += OnAlarmEnded;
	}

	private void OnDisable()
	{
		_watchHandController.AnalogWatchChanged -= OnAnalogWatchChanged;
		_alarmController.Load -= OnLoadAlarm;
		_alarmController.AlarmEnded -= OnAlarmEnded;
	}

	private void OnDigitalWatchChange(string text)
	{
		if (madeChoice && text != "")
		{
			if (!_analogChanged)
			{
				DigitalAlarmChanged?.Invoke(float.Parse(_hours.text), float.Parse(_minutes.text), float.Parse(_seconds.text));
			}
			else
			{
				_analogChanged = false;
			}
		}
	}

	private void OnAnalogWatchChanged()
	{
		if (madeChoice)
		{
			float hours = (360 - NormalizeAngle(_hoursArrow.rotation.eulerAngles.z)) * (12f / 360f);
			float minutes = (360 - NormalizeAngle(_minutesArrow.rotation.eulerAngles.z)) * (60f / 360f);
			float seconds = (360 - NormalizeAngle(_secondsArrow.rotation.eulerAngles.z)) * (60f / 360f);
			_analogChanged = true;
			AnalogAlarmChanged.Invoke(hours, minutes, seconds);
		}
	}

	private void OnLoadAlarm(float hours, float minutes, float seconds)
	{
		_alarmText.text = hours.ToString("00") + " : " + minutes.ToString("00") + " : " + seconds.ToString("00");
	}

	private void OnAlarmEnded()
	{
		_alarmText.text = "----------";
	}


	private void BlockButtonOnEmpty(string text)
	{
		if (madeChoice)
		{
			if (text == "")
			{
				_button.interactable = false;
				return;
			}
			_button.interactable = true;
		}
	}

	public void SetAlarm()
	{
		if (!madeChoice)
		{
			ActiveAlarmChoice();
		}
		else
		{
			AcceptAlarm();
		}
	}

	private void AcceptAlarm()
	{
		_timeController.IsActive = true;
		_hours.interactable = false;
		_minutes.interactable = false;
		_seconds.interactable = false;
		madeChoice = false;
		_alarmText.text = float.Parse(_hours.text).ToString("00") + " : " + float.Parse(_minutes.text).ToString("00")
			+ " : " + float.Parse(_seconds.text).ToString("00");
		CreateAlarm?.Invoke(float.Parse(_hours.text), float.Parse(_minutes.text), float.Parse(_seconds.text));
	}

	private void ActiveAlarmChoice()
	{
		_timeController.IsActive = false;
		_hours.interactable = true;
		_minutes.interactable = true;
		_seconds.interactable = true;
		madeChoice = true;
	}

	private float NormalizeAngle(float angle)
	{
		if (angle < 0)
			angle += 360;
		return angle;
	}
}