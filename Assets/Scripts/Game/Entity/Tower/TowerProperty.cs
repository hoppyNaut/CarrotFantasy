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

    public int towerLevel;
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
        bulletGo = gameController.GetGameObjectResource("Tower/ID" + tower.towerID.ToString() + "/Bullect/" + towerLevel.ToString());
        bulletGo.transform.position = transform.position;
    }
}
