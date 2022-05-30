using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Normal,
    Fast,
    Heavy
}

[System.Serializable]
public class Data
{
    public GameObject LevelObject;
    public EnemyType[] enemyTypes = new EnemyType[20];
    public List<Vector3> spawmEnemyPos;
    
}


[CreateAssetMenu(menuName = "Data/Level",fileName = "Level")]
public class Level : ScriptableObject
{
    public List<Data> LevelDatas;

    
}
