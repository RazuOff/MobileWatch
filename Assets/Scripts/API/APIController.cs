using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class APIController : MonoBehaviour
{
    private TimeData _timeData;
    protected string _url;
    protected bool _useKey = false;
    protected string _keyName, _key;

    public IEnumerator GetWorldTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_url))
        {
            if (_useKey)
                webRequest.SetRequestHeader(_keyName,_key);
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