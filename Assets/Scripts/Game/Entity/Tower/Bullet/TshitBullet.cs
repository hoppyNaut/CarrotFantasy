/****************************************************
	文件：TshitBullet.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/16 19:03   	
	功能：便便子弹
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TshitBullet : Bullet
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.activeSelf)
        {
            return;
        }
        if(collision.tag == "Monster" && collision.transform == targetTrans)
        {
            Monster monster = collision.GetComponent<Monster>();
            monster.AddBuff(new SlowDownBuff(monster, BuffType.SlowDownBuff,towerLevel * 0.7f,towerLevel * 0.4f));
        }
        base.OnTriggerEnter2D(collision);
    }
}
