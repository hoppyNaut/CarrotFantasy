/****************************************************
	文件：TowerProperty.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/12 10:25   	
	功能：塔的特性脚本
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProperty : MonoBehaviour
{
    [HideInInspector]
    public Tower tower;

 
    //攻击间隔
    public float atkTime;
    //攻击计时器
    protected float atkTimer;

    [HideInInspector]
    //升级价格
    public int upLevelPrice;
    [HideInInspector]
    //出售价格
    public int sellPrice;
    //当前塔的价格
    public int towerPrice;

    protected GameObject bulletGo;

    public Animator animator;
    private GameController gameController;

    protected virtual void Start()
    {
        gameController = GameController.Instance;
        upLevelPrice = (int)(towerPrice * 1.5f);
        sellPrice = towerPrice / 2;
        animator = transform.Find("tower").GetComponent<Animator>();
        atkTimer = atkTime;
    }

    protected virtual void Update()
    {
        if(gameController.isGamePause || tower.atkTargetTrans == null)
        {
            return;
        }
        if(!tower.atkTargetTrans.gameObject.activeInHierarchy)
        {
            tower.atkTargetTrans = null;
            return;
        }
        //旋转
        if(tower.atkTargetTrans.gameObject.tag == "Item")
        {
            transform.LookAt(tower.atkTargetTrans.position+ new Vector3(0,0,3));
        }
        else
        {
            transform.LookAt(tower.atkTargetTrans.position);
        }
        
        if(transform.eulerAngles.y == 0)
        {
            transform.eulerAngles += new Vector3(0, 90, 0);
        }

        //攻击
        atkTimer += Time.deltaTime;
        if(atkTimer >= atkTime / gameController.gameSpeed)
        {
            Attack();
            atkTimer = 0;
        }
    }

    public void Init()
    {
        tower = null;
    }

    public void SellTower()
    {
        gameController.AddCoin(sellPrice);
        //产生销毁特效       
        GameObject effectGo = gameController.GetGameObjectResource("BuildEffect");
        effectGo.transform.position = transform.position;
        effectGo.transform.SetParent(gameController.transform);
        tower.DestroyTower();
    }

    public void UpLevelTower()
    {
        gameController.AddCoin(-upLevelPrice);
        GameObject effectGo = gameController.GetGameObjectResource("UpLevelEffect");
        effectGo.transform.position = transform.position;
        effectGo.transform.SetParent(gameController.transform);
        //销毁当前塔
        tower.DestroyTower();
    }

    protected virtual void Attack()
    {
        if(tower.atkTargetTrans == null)
        {
            return;
        }
        animator.Play("Attack");
        bulletGo = gameController.GetGameObjectResource("Tower/ID" + tower.towerID.ToString() + "/Bullect/" + tower.towerLevel.ToString());
        bulletGo.transform.position = transform.position;
        bulletGo.transform.localEulerAngles = new Vector3(-90,90,0);
        Bullet bulletClass = bulletGo.GetComponent<Bullet>();
        bulletClass.towerID = tower.towerID;
        bulletClass.towerLevel = tower.towerLevel;
        //为子弹设置目标
        bulletClass.targetTrans = tower.atkTargetTrans;
    }
}
