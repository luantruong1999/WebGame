using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public void SpawnPlayerPos()
    {
        GameManager.Instance.OnSpawnPlayer();
        gameObject.SetActive(false);
    }
}
