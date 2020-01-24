/****************************************************
	文件：BaseFactory.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 15:11   	
	功能：游戏物体工厂基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : IBaseFactory
{
    //当前拥有的gameobject类型资源
    protected Dictionary<string, GameObject> factoryDict = new Dictionary<string, GameObject>();
    //对象池字典
    protected Dictionary<string, List<GameObject>> objectPoolDict = new Dictionary<string, List<GameObject>>();
    //预制体路径
    protected string loadPath;

    public BaseFactory()
    {
        loadPath = "Prefabs/";
    }

    public GameObject GetItem(string itemName)
    {
        GameObject item = null;
        if(objectPoolDict.ContainsKey(itemName))
        {
            if(objectPoolDict[itemName].Count > 0)
            {
                item = objectPoolDict[itemName][0];
                objectPoolDict[itemName].RemoveAt(0);
                item.SetActive(true);
                item.transform.position = Vector3.zero;
                item.transform.rotation = Quaternion.identity;
                return item;
            }
        }

        GameObject prefab = null;
        if(factoryDict.ContainsKey(itemName))
        {
            prefab = factoryDict[itemName];
        }
        else
        {
            prefab = Resources.Load<GameObject>(loadPath + itemName);
            factoryDict.Add(itemName, prefab);
        }

        if(prefab == null)
        {
            Debug.Log(itemName + "资源获取失败 ");
            Debug.Log("失败路径:" + loadPath + itemName);
        }

        item = GameObject.Instantiate(prefab);
        item.transform.position = Vector3.zero;
        item.transform.rotation = Quaternion.identity;
        //改名(去除(Clone))
        item.name = itemName;
        return item;
    }

    public void PushItem(string itemName, GameObject item)
    {
        item.SetActive(false);
        //设置同一个父物体 便于管理 
        item.transform.SetParent(GameManager.Instance.transform);
        if (objectPoolDict.ContainsKey(itemName))
        {
            objectPoolDict[itemName].Add(item);
        }
        else
        {
            objectPoolDict.Add(itemName, new List<GameObject>() { item});
        }
    }
}
