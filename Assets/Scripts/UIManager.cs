using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject enemyImage;
    [SerializeField] private TextMeshProUGUI livetext;
    [SerializeField] private TextMeshProUGUI LvlText;
    [SerializeField] private TextMeshProUGUI lvlInGameText;


    private Stack<GameObject> enemyImages = new Stack<GameObject>();
    private Animator animator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
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
        livetext.text = lives.ToString();
    }

    public void UpdateLvL(int lvl)
    {
        LvlText.text = "Level " + lvl.ToString();
        lvlInGameText.text = lvl.ToString();
    }

    public void GameOver()
    {
        animator.Play("GameOver");
    }
}
