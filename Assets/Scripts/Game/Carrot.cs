using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrot : MonoBehaviour
{
    //萝卜的不同状态
    private Sprite[] carrotSprites;

    private Text txt_Hp;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float idleTimer;

    private void Awake()
    {
        idleTimer = 0;

        txt_Hp = transform.Find("HpCanvas").Find("Image").Find("txt_Hp").GetComponent<Text>();
        animator = transform.GetComponent<Animator>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();

        carrotSprites = new Sprite[7];
        for(int i = 0; i < 7; i++)
        {
            carrotSprites[i] = GameController.Instance.GetSprite("NormalMordel/Game/Carrot/" + i.ToString());
        }
    }

    private void Update()
    {
        idleTimer += Time.deltaTime;
        if(idleTimer >= 3)
        {
            animator.Play("Idle");
            idleTimer = 0;
        }
    }

    public void SetHp(int hp)
    {
        if(hp < 10)
        {
            animator.enabled = false;
        }
        txt_Hp.text = hp.ToString();
        if(hp >= 7 && hp <= 10)
        {
            spriteRenderer.sprite = carrotSprites[6];
        }
        else if(hp < 7 && hp > 0)
        {
            spriteRenderer.sprite = carrotSprites[hp - 1];
        }
        else
        {
            GameController.Instance.isGamePause = true;
            GameController.Instance.normalModePanel.ShowGameOverPage();
        }
    }

    private void OnMouseDown()
    {
        if(GameController.Instance.carrotHp >= 10)
        {
            animator.Play("Touch");
            idleTimer = 0;
        }
    }
}
