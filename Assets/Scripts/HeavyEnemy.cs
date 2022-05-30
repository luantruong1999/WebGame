using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    private SpriteRenderer spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        maxHp = 4;
        spriteRenderer.color=Color.green;
    }

    public override void TakenDame(int dame)
    {
        base.TakenDame(dame);
        switch (curHp)
        {
            case 3:
                spriteRenderer.color=Color.cyan;
                break;
            case 2:
                spriteRenderer.color=Color.yellow;
                break;
            case 1:
                spriteRenderer.color = Color.white;
                break;
        }
    }
}
