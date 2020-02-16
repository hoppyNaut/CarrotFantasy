/****************************************************
	文件：StarBullet.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/16 19:03   	
	功能：星型子弹
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : MonoBehaviour
{
    public int attackValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.activeSelf)
        {
            return;
        }
        if(collision.tag == "Monster" || collision.tag == "Item")
        {
            collision.SendMessage("TakeDamage", attackValue);
        }
    }

}
