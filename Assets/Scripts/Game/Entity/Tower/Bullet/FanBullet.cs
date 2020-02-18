/****************************************************
	文件：FanBullet.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/16 19:02   	
	功能：风车子弹
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBullet : Bullet
{
    public float existTime;
    private float existTimer;
    private bool hasTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        existTimer = 0;
        hasTarget = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(gameController.isGameOver || existTimer >= existTime )
        {
            DestroyBullet();
            return;
        }
        if(gameController.isGamePause)
        {
            return;
        }

        existTimer += Time.deltaTime;

        if(hasTarget)
        {
            transform.Translate(Vector3.forward * moveSpeed * gameController.gameSpeed * Time.deltaTime,Space.Self);
        }
        else
        {
            if(targetTrans != null && targetTrans.gameObject.activeSelf)
            {
                hasTarget = true;
                InitTarget();
            }
        }
    }

    private void InitTarget()
    {
        if(targetTrans.gameObject.tag == "Item")
        {
            transform.LookAt(targetTrans.position + new Vector3(0, 0, 3));
        }
        else if(targetTrans.gameObject.tag == "Monster")
        {
            transform.LookAt(targetTrans.position);
        }

        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles += new Vector3(0, 90, 0);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster" || collision.tag == "Item")
        {
            if(collision.gameObject.activeSelf)
            {
                collision.SendMessage("TakeDamage", atkValue);
                SpawnEffect();
            }
        }
    }
}
