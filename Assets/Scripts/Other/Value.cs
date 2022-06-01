using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    public static Value Instance;
    public int curLive;
    public int curLvl;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ReloadValue();
        DontDestroyOnLoad(gameObject);
    }

    public void ReloadValue()
    {
        curLive = 3;
        curLvl = 0; 
    }

    
}
