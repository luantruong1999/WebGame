using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public void SpawnEnemyPos()
    {
        GameManager.Instance.OnEnemySpawn();
        gameObject.SetActive(false);
    }
}
