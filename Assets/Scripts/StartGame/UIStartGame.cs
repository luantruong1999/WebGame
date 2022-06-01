using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UIStartGame : MonoBehaviour
{
    private InputSystemUIInputModule input;

    private void Awake()
    {
        input = GetComponent<InputSystemUIInputModule>();
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    
}
