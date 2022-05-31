using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    public enum Power
    {
        ThemMang,
        LoCot,
        BatTu,
        DietEnemy,
        TimeStop,
    }
    [System.Serializable]

    public class SpritePower
    {
        public Power power;
        public Sprite sprite;
    }

    private Power power;
    private SpriteRenderer spriteRenderer;
    public SpritePower[] spritePowers;
    public static UnityEvent timeStop = new UnityEvent();
    public Player player;
    public static UnityEvent dietEnemy = new UnityEvent();
    public ThanhBaoVe thanhBaoVe;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        power = (Power)Random.Range(0, spritePowers.Length);
        for (int i = 0; i < spritePowers.Length; i++)
        {
            if (power == spritePowers[i].power)
            {
                spriteRenderer.sprite = spritePowers[i].sprite;
                break;
            }
        }
        InvokeRepeating("Fade",0.5f,0.5f);
        Invoke("Dis",8f);
    }

    private void OnDisable()
    {
        CancelInvoke("Fade");
    }

    void Fade()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        switch (power)
        {
            case Power.TimeStop:
                timeStop?.Invoke();
                break;
            case Power.BatTu:
                player.StartBatTu();
                break;
            case Power.DietEnemy:
                dietEnemy?.Invoke();
                break;
            case Power.LoCot:
                thanhBaoVe.StartSatBaoVe();
                break;
            case Power.ThemMang:
                break;
        }
        gameObject.SetActive(false);
    }

    public void Dis()
    {
        gameObject.SetActive(false);
    }
}
