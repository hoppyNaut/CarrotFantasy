/****************************************************
	文件：Bullet.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/15 10:53   	
	功能：子弹基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform targetTrans;
    public float moveSpeed;
    public int atkValue;

    [HideInInspector]
    public int towerID;
    [HideInInspector]
    public int towerLevel;

    protected GameController gameController;

    protected virtual void Start()
    {
        gameController = GameController.Instance;
    }

    protected virtual void Update()
    {
        //游戏结束
        if(gameController.isGameOver)
        {
            DestroyBullet();
        }
        //游戏暂停
        if(gameController.isGamePause )
        {
            return;
        }
        if( targetTrans == null||!targetTrans.gameObject.activeSelf )
        {
            DestroyBullet();
            return;
        }

        if(targetTrans.tag == "Item")
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTrans.position + new Vector3(0,0,3), moveSpeed * Time.deltaTime *gameController.gameSpeed);
            transform.LookAt(targetTrans.position + new Vector3(0, 0, 3));
        }
        else if(targetTrans.tag == "Monster")
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, moveSpeed * Time.deltaTime * gameController.gameSpeed);
            transform.LookAt(targetTrans);
        }

        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles += new Vector3(0, 90, 0);
        }
    }

    private void Init()
    {
        targetTrans = null;
    }

    //销毁子弹
    protected virtual void DestroyBullet()
    {
        Init();
        gameController.PushGameObjectToFactory("Tower/ID" + towerID.ToString() + "/Bullect/" + towerLevel.ToString(), gameObject);
    }

    //生成特效
    protected virtual void SpawnEffect()
    {
        GameObject effectGo = gameController.GetGameObjectResource("Tower/ID" + towerID.ToString() + "/Effect/" + towerLevel.ToString());
        effectGo.transform.position = this.transform.position;
        effectGo.transform.SetParent(gameController.transform);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster" || collision.tag == "Item")
        {
            if(collision.gameObject.activeSelf)
            {
                if (collision.transform == targetTrans)
                {
                    collision.SendMessage("TakeDamage", atkValue);
                    SpawnEffect();
                    DestroyBullet();
                }
            }
        }
       
    }
}
