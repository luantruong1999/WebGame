using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyObject
{
    public EnemyType enemyType;
    public GameObject EnemyObj;
}
[CreateAssetMenu(menuName = "Data/EnemyObj",fileName = "Enemy")]
public class EnemyObj : ScriptableObject
{
    public EnemyObject[] enemyObjects = new EnemyObject[3];
}
