using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    private GameObjectPool _instance;
    
    public GameObjectPool GetInstance()
    {
        return _instance;
    }

    //管理物体名和存储物体的列表的字典，每一种游戏物体对应一个存储列表
    private static Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        _instance = this;
    }


    public static GameObject Get(string prefabName, Vector3 position, Quaternion rotation)
    {
        //拼接制作字典的key值，因为Instantiate生成出的gameobject都会自动命名为gameobject(Clone)
        string key = prefabName + "(Clone)";
        GameObject go;
        //如果字典里有这个key且key对应的列表中存在元素
        if(pool.ContainsKey(key) && pool[key].Count > 0)
        {
            //取出当前key所对应的列表
            List<GameObject> list = pool[key];
            //取得当前列表的第一个元素
            go = list[0];
            //移除元素
            list.RemoveAt(0);
            //初始化游戏物体
            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;
        }
        else
        {
            go = Instantiate(Resources.Load(prefabName), position, rotation) as GameObject;
        }
        return go;
    }

    public static GameObject Store(GameObject go)
    {
        string key = go.name;
        if(pool.ContainsKey(key))
        {
            pool[key].Add(go);
        }
        else
        {
            pool[key] = new List<GameObject>() { go };
        }
        go.SetActive(false);
        return go;
    }

}
