using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject enemyImage;
    [SerializeField] private TextMeshProUGUI text;


    private Stack<GameObject> enemyImages = new Stack<GameObject>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Push(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            enemyImages.Push(Instantiate(enemyImage,enemyParent));
        }
    }

    public void DestroyEnemy()
    {
        Destroy(enemyImages.Pop());
    }

    public void UpdateLiveUI(int lives)
    {
        text.text = lives.ToString();
    }
}
