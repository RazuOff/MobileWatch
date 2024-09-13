using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class APIManager : MonoBehaviour
{
    [SerializeField] private List<APIController> apiControllers;

    public TimeData TimeData { get; private set; }

    public IEnumerator FetchAndUseTimeData()
    {
        foreach (var api in apiControllers)
        {
            yield return StartCoroutine(api.GetWorldTime());
            if (api.GetCurrentTimeData() != null)
            {
                TimeData = api.GetCurrentTimeData();
                break;
            }
        }
    }
}

