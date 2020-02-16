using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBuilder : IBuilder<Monster>
{
    public int monsterID;
    private GameObject monsterGo;


    public void GetData(Monster productClass)
    {
        productClass.monsterID = monsterID;
        productClass.hp = monsterID * 100;
        productClass.CurHp = productClass.hp;
        productClass.initMoveSpeed = monsterID * 0.5f;
        productClass.moveSpeed = monsterID * 0.5f;
        productClass.prize = monsterID * 50;
    }

    public void GetOtherResource(Monster productClass)
    {
        productClass.GetMonsterAnimatorController();
    }

    public GameObject GetProduct()
    {
        GameObject itemGo = GameController.Instance.GetGameObjectResource("Monster");
        Monster monster = GetProductClass(itemGo);
        GetData(monster);
        GetOtherResource(monster);
        return itemGo;
    }

    public Monster GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<Monster>();
    }
}
