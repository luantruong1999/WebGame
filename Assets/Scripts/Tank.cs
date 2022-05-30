using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public enum Team
{
    Player,
    Enemy,
}

public abstract class Tank : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected int curHp;
    [SerializeField] protected int maxHp;
    protected Team team;

    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private LayerMask layerMask;
    protected bool isMoving = false;

    [SerializeField] private float delayAttack;
    private float lastAttack;
    [HideInInspector] public bool canAttack=true;
    private Vector3 canon;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        layerMask=LayerMask.GetMask("Eviroment","Enemy","Player","Water");
        curHp = maxHp;

    }

    /*protected bool CheckVector(Vector2 vector2)
    {
        LayerMask gameobjectLayer = 1 << gameObject.layer;
        LayerMask layerCheck = layerMask-gameobjectLayer;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size*0.9f,0f, vector2, 0.2f,
            layerCheck);
        return !hit.collider;
    }*/
    protected bool CheckVector(Vector2 vector2)
    {
        Vector2 origin = new Vector2(boxCollider2D.bounds.center.x + vector2.x * (boxCollider2D.bounds.size.x / 2 + 0.25f),
            boxCollider2D.bounds.center.y + vector2.y * (boxCollider2D.bounds.size.y / 2 + 0.25f));
        Vector2 size = new Vector2(vector2.y!=0? boxCollider2D.bounds.size.x-0.5f:0.2f,
            vector2.x!=0? boxCollider2D.bounds.size.y-0.5f:0.2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f,Vector2.zero,0f,layerMask);
        return !hit.collider;
    }

    protected void RotationVector(Vector2 vector2)
    {
        float z = -90 * vector2.x + 90 * (Mathf.Abs(vector2.y) - vector2.y);
        transform.rotation=Quaternion.Euler(0,0,z);
    }

    protected IEnumerator Move(Vector2 vector2)
    {
        isMoving = true;
        rigidbody2D.position = new Vector2(Mathf.Round(rigidbody2D.position.x), Mathf.Round(rigidbody2D.position.y));
        float movefloat = 0;
        Vector2 endPos;
        while (movefloat<1)
        {
            movefloat += speed * Time.deltaTime;
            movefloat = Mathf.Clamp(movefloat, 0f, 1f);
            endPos = rigidbody2D.position + vector2 * Time.deltaTime * speed;
            if (movefloat >= 1)
            {
                endPos = new Vector2(Mathf.Round(endPos.x), Mathf.Round(endPos.y));
            }
            rigidbody2D.MovePosition(endPos);
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
    }

    protected void Attack()
    {
        if(Time.time-lastAttack<delayAttack || !canAttack) return;
        lastAttack = Time.time;
        GameObject obj = ObjectPooler.Instance.GetPoolObj("Bullet");
        obj.GetComponent<Bullet>()?.SetBullet(team,this);
        canon = transform.position+transform.up*0.125f;
        obj.transform.position = canon;
        obj.transform.rotation=transform.rotation;
        obj.SetActive(true);
    }
    public virtual void TakenDame(int dame)
    {
        curHp -= dame;
        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        GameObject tankDieEff = ObjectPooler.Instance.GetPoolObj("TankDie");
        tankDieEff.transform.position = gameObject.transform.position;
        tankDieEff.gameObject.SetActive(true);
    }
}
