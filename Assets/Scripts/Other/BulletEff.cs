using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEff : MonoBehaviour
{
    public void Dis()
    {
        this.gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
