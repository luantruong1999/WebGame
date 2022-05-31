using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDieEff : MonoBehaviour
{
    public void DisEff()
    {
        gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        gameObject.SetActive(false);
    }
}
