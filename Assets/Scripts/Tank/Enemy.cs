using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Tank
{ 
        private Vector2[] vectorDir = new Vector2[4] {Vector2.up, Vector2.right, Vector2.down, Vector2.left};
        private bool isCouturine;
        private Vector2 curVector;
        [HideInInspector]public bool enemyAttack = true;
        protected override void Awake()
        {
                base.Awake();
                team = Team.Enemy;
        }

        protected virtual void Start()
        {
                StartCoroutine(RandomVector());
                isMoving = false;
                GameManager.Instance.EnemyActive++;
                PowerUp.timeStop.AddListener(StartTimeStop);
                PowerUp.dietEnemy.AddListener(()=>Destroy(gameObject));
        }

        protected void OnDestroy()
        {
                if(GameManager.Instance==null) return;
                GameManager.Instance.EnemyActive--;
                GameManager.Instance.SpawnCourotine();
                UIManager.Instance.DestroyEnemy();
                if (GameManager.Instance.EnemyActive == 0 && GameManager.Instance.CurEnemy >= 20)
                { 
                        Debug.Log("win");
                        GameManager.Instance.Winner();
                }
                PowerUp.timeStop.RemoveListener(StartTimeStop);
                PowerUp.dietEnemy.RemoveListener(()=>gameObject.SetActive(false));
        }

        private void Update()
        {
                if(enemyAttack) Attack();
        }

        private void FixedUpdate()
        {
                RotationVector(curVector);
                if (CheckVector(curVector))
                { 
                        if (!isMoving) StartCoroutine(Move(curVector));    
                }
                else
                {
                        if (!isCouturine) StartCoroutine(RandomVector());
                }
        }
        IEnumerator RandomVector()
        {
                isCouturine = true;
                curVector=vectorDir[Random.Range(0, 4)];
                yield return new WaitForSeconds(0.1f);
                isCouturine = false;
        }

        IEnumerator TimeStop()
        {
                float curSpeed = speed;
                speed = 0;
                enemyAttack = false;
                yield return new WaitForSeconds(5f);
                speed = curSpeed;
                enemyAttack = true;
        }
        void StartTimeStop()
        {
                StartCoroutine(TimeStop());
        }
}
