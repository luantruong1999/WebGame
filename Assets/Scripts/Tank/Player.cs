using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Tank
{
    private PlayerController playerController;
    private Animator animator;
    
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        team = Team.Player;
    }

    private void OnEnable()
    {
        StartBatTu();
    }

    private void Update()
    {
        if (playerController.FireInput && EventSystem.current.IsPointerOverGameObject()==false)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (playerController.InputMove != Vector2.zero)
        {
            animator.Play("PlayerMove");
            RotationVector(playerController.InputMove);
            
            if (!isMoving && CheckVector(playerController.InputMove))
            {
                StartCoroutine(Move(playerController.InputMove));
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        isMoving = false;
    }

    IEnumerator BatTu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        curHp = 999;
        yield return new WaitForSeconds(5f);
        curHp = 1;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void StartBatTu()
    {
        StartCoroutine(BatTu());
    }
}

