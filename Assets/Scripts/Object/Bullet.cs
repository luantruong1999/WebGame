using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject boom;
    private Rigidbody2D rig;
    private Tank tank;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        boom = transform.GetChild(0).gameObject;
        rig = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        rig.velocity = transform.up*speed;
        _spriteRenderer.enabled = true;
        if(tank)tank.canAttack = false;
    }

    public void SetBullet(Team team, Tank tank)
    {
        switch (team)
        {
           case Team.Player:
               gameObject.layer = LayerMask.NameToLayer("BulletPlayer");
               break;
           case Team.Enemy:
               gameObject.layer = LayerMask.NameToLayer("BulletEnemy");
               break;
        }
        this.tank = tank;
    }

    private void OnDisable()
    {
        if(tank)tank.canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        rig.velocity = Vector2.zero;
        _spriteRenderer.enabled = false;
        boom.SetActive(true);
        Tank tankcol = col.gameObject.GetComponent<Tank>();
        if (tankcol)
        {
            tankcol.TakenDame(1);
        }
        if(col.gameObject.tag=="Gach") col.gameObject.SetActive(false);
        if (col.gameObject.tag == "Thanh")
        {
            col.gameObject.SetActive(false);
            GameObject obj=ObjectPooler.Instance.GetPoolObj("TankDie");
            obj.transform.position = col.gameObject.transform.position+new Vector3(2,-1,0);
            obj.transform.localScale=Vector3.one;
            obj.SetActive(true);
            Debug.Log("GameOver");
        }
        
    }
}
