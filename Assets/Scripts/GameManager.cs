using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Level level;
    [SerializeField] private EnemyObj enemyData;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private Player player;


    private int curEnemy;
    private int curLevel;
    private Dictionary<EnemyType, GameObject> enemyObjs = new Dictionary<EnemyType, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < enemyData.enemyObjects.Length ; i++)
        {
            enemyObjs.Add(enemyData.enemyObjects[i].enemyType,enemyData.enemyObjects[i].EnemyObj);
        }
    }

    public void SpawnPlayer()
    {
        player.gameObject.transform.position = spawnPlayer.position;
        player.gameObject.SetActive(true);
    }

    public void SpawnEnemy()
    {
        Vector3 RandomSpawnPos = level.LevelDatas[curLevel]
            .spawmEnemyPos[Random.Range(0, level.LevelDatas[curLevel].spawmEnemyPos.Count)];
        GameObject Obj = enemyObjs[level.LevelDatas[curLevel].enemyTypes[curEnemy]];
        Instantiate(Obj, RandomSpawnPos,Quaternion.identity);
    }
    
}
