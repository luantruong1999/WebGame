using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanhBaoVe : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    IEnumerator SatBaoVe()
    {
        animator.Play("SatBaoVe");
        yield return new WaitForSeconds(5f);
        animator.Play("ChuyenDoiSatVSGach");
    }

    public void StartSatBaoVe()
    {
        StartCoroutine(SatBaoVe());
    }

    public void SetTag(string tag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            obj.SetActive(true);
            obj.tag = tag;
        }
    }
}
