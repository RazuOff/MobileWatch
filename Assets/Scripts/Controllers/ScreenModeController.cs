using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenModeController : MonoBehaviour
{
    [SerializeField] AlarmController _alaramController;
    [SerializeField] GameObject _downloadScreen;
    private const string HOURS_PREFS = "Hours", MINUTES_PREFS = "Minutes", SECONDS_PREFS = "Seconds", ACTIVE_PREFS = "isActive", START_PREF = "Start";
    private const int LANDSCAPE_SCENE = 1, PORTRAIT_SCENE = 0;
    private ScreenOrientation _lastOrientation;


    private void OnEnable()
    {
        _alaramController.AlarmEnded += OnAlarmEnded;
    }

    private void OnDisable()
    {
        _alaramController.AlarmEnded -= OnAlarmEnded;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt(START_PREF) != 1)
        {
            StartScene();
        }
        StartCoroutine(LoadAlarm());
        _lastOrientation = Screen.orientation;

    }

    private void Update()
    {
        if (_lastOrientation != Screen.orientation)
        {
            ChangeScene();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnAlarmEnded()
    {
        PlayerPrefs.SetInt(ACTIVE_PREFS, 0);
    }
    private void StartScene()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            SceneManager.LoadScene(LANDSCAPE_SCENE);
            PlayerPrefs.SetInt(START_PREF, 1);
        }
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            SceneManager.LoadScene(PORTRAIT_SCENE);
            PlayerPrefs.SetInt(START_PREF, 1);
        }
    }

    private void ChangeScene()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            _downloadScreen.SetActive(true);
            PlayerPrefs.SetFloat(HOURS_PREFS, _alaramController.Hours);
            PlayerPrefs.SetFloat(MINUTES_PREFS, _alaramController.Minutes);
            PlayerPrefs.SetFloat(SECONDS_PREFS, _alaramController.Seconds);
            PlayerPrefs.SetInt(ACTIVE_PREFS, _alaramController.IsActive ? 1 : 0);
            SceneManager.LoadScene(LANDSCAPE_SCENE);
        }

        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            _downloadScreen.SetActive(true);
            PlayerPrefs.SetFloat(HOURS_PREFS, _alaramController.Hours);
            PlayerPrefs.SetFloat(MINUTES_PREFS, _alaramController.Minutes);
            PlayerPrefs.SetFloat(SECONDS_PREFS, _alaramController.Seconds);
            PlayerPrefs.SetInt(ACTIVE_PREFS, _alaramController.IsActive ? 1 : 0);
            SceneManager.LoadScene(PORTRAIT_SCENE);
        }
    }

    private IEnumerator LoadAlarm()
    {
        float hours = PlayerPrefs.GetFloat(HOURS_PREFS);
        float minutes = PlayerPrefs.GetFloat(MINUTES_PREFS);
        float seconds = PlayerPrefs.GetFloat(SECONDS_PREFS);
        int active = PlayerPrefs.GetInt(ACTIVE_PREFS);
        if (active == 1)
        {
            _alaramController.LoadAlarm(hours, minutes, seconds);
        }
        yield return new WaitForSeconds(0.2f);
        _downloadScreen.SetActive(false);
    }
}
