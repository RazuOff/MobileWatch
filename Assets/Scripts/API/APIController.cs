using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class APIController : MonoBehaviour
{
    private TimeData _timeData;
    protected string _url;

    public IEnumerator GetWorldTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                _timeData = JsonUtility.FromJson<TimeData>(jsonResponse);
            }
            else
            {
                Debug.Log($"Error: {webRequest.error}");
            }
        }
    }

    public TimeData GetCurrentTimeData()
    {
        return _timeData;
    }
}