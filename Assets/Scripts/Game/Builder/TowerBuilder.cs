using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : IBuilder<Tower>
{
    public int towerID;
    public int towerLevel;
    private GameObject towerGo;

    public void GetData(Tower productClass)
    {
        productClass.towerID = towerID;
        productClass.towerLevel = towerLevel;
    }

    public void GetOtherResource(Tower productClass)
    {
        productClass.GetTowerResource();
    }

    public GameObject GetProduct()
    {
        GameObject towerGo = GameController.Instance.GetGameObjectResource("Tower/ID" + towerID.ToString() + "/TowerSet/" + towerLevel.ToString());
        Tower towerClass = GetProductClass(towerGo);
        GetData(towerClass);
        GetOtherResource(towerClass);
        return towerGo;
    }

    public Tower GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<Tower>();
    }

}
