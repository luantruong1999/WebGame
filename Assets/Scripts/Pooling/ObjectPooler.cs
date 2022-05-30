using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<ObjectItem> itemsPool;

    private List<GameObject> pooledObject;

    private void Awake()
    {
        if (Instance != null && Instance==this)
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
        pooledObject = new List<GameObject>();
        foreach (ObjectItem item in itemsPool)
        {
            for (int i = 0; i < item.amountPool; i++)
            {
                GameObject obj = Instantiate(item.objectPool);
                obj.SetActive(false);
                pooledObject.Add(obj);
                obj.name = item.name;
            }
        }
    }

    public GameObject GetPoolObj(string name)
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy && pooledObject[i].name.StartsWith(name))
            {
                return pooledObject[i];
            }
        }

        foreach (ObjectItem item in itemsPool)
        {
            if (item.name == name)
            {
                GameObject obj = Instantiate(item.objectPool);
                obj.SetActive(false);
                obj.name = name;
                pooledObject.Add(obj);
                return obj;
            }
        }
        Debug.Log("Name pooler object null: "+name);
        return null;
    }
}
