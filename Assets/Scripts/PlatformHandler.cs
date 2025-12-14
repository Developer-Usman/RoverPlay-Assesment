using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PlatformHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> androidObjects = new List<GameObject>();
    void Awake()
    {
        if(Application.isEditor)
        {
            Switch(false);
        }
        else
        {
            Switch(true);
        }
    }
    void Switch(bool action)
    {
        foreach (var item in androidObjects)
        {
            item.SetActive(action);
        }
    }
}
