using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Level level;
    [SerializeField] private EnemyObj enemyData;
    [SerializeField] private GameObject spawnPlayer;
    [SerializeField] private GameObject spawnEnemy;
    [SerializeField] private Player player;
    [SerializeField] private GameObject powerUp;
    private GameObject GachLvl;
    private int enemyActive;
    public int EnemyActive
    {
        get => enemyActive;
        set => enemyActive = value;
    }
    private int curEnemy;
    public int CurEnemy => curEnemy;

    private int lives;
    public int Lives
    {
        get { return lives; }
        set => lives = value;
    }

    private Dictionary<EnemyType, GameObject> enemyObjs = new Dictionary<EnemyType, GameObject>();
    private bool spawned;

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
        GameObject lvl = Instantiate(level.LevelDatas[Value.Instance.curLvl].LevelObject);
        GachLvl = lvl.transform.GetChild(0).Find("Gach").gameObject;
        lives =Value.Instance.curLive;
        for (int i = 0; i < enemyData.enemyObjects.Length ; i++)
        {
            enemyObjs.Add(enemyData.enemyObjects[i].enemyType,enemyData.enemyObjects[i].EnemyObj);
        }
        StartCoroutine(Spawn());
        SpawnPlayer();
        InvokeRepeating("SpawnPower",20f,20f);
        UIManager.Instance.Push(20);
        
    }

    public void OnSpawnPlayer()
    {
        player.gameObject.transform.position = spawnPlayer.transform.position;
        player.gameObject.SetActive(true);
    }

    public void SpawnPlayer()
    {
        UIManager.Instance.UpdateLiveUI(lives);
        spawnPlayer.SetActive(true);
    }

    public void OnEnemySpawn()
    {
        GameObject Enemy = enemyObjs[level.LevelDatas[Value.Instance.curLvl].enemyTypes[curEnemy]];
        Instantiate(Enemy, spawnEnemy.transform.position,Quaternion.identity);
        curEnemy++;
    }

    IEnumerator Spawn()
    {
        spawned = true;
        Vector3 RandomSpawnPos = level.LevelDatas[Value.Instance.curLvl]
            .spawmEnemyPos[Random.Range(0, level.LevelDatas[Value.Instance.curLvl].spawmEnemyPos.Count)];
        spawnEnemy.transform.position = RandomSpawnPos;
        spawnEnemy.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (enemyActive < 4 && curEnemy<20) StartCoroutine(Spawn());
        spawned = false;
    }

    public void SpawnCourotine()
    {
        if(!gameObject) return;
        if (!spawned && curEnemy<20) StartCoroutine(Spawn());
    }

    public void SpawnPower()
    {
        powerUp.transform.position = GachLvl.transform.GetChild(Random.Range(0, GachLvl.transform.childCount)).position;
        powerUp.SetActive(true);
    }

    public void NextLv()
    {
        Value.Instance.curLvl++;
        Value.Instance.curLive = lives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void AddLive()
    {
        if(lives<3) lives++;
    }

    public void GameOver()
    {
        
    }
}
