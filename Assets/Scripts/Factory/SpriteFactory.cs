/****************************************************
	文件：SpriteFactory.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 20:02   	
	功能：精灵图片资源工厂
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : IBaseResourceFactory<Sprite>
{
    protected Dictionary<string, Sprite> factoryDict = new Dictionary<string, Sprite>();

    protected string loadPath;

    public SpriteFactory()
    {
        loadPath = "Sprites/";
    }

    public Sprite GetSingleResource(string resPath)
    {
        Sprite sprite = null;
        string path = loadPath + resPath;
        if(factoryDict.ContainsKey(resPath))
        {
            sprite = factoryDict[resPath];
        }
        else
        {
            sprite = Resources.Load<Sprite>(path);
            factoryDict.Add(resPath, sprite);
        }
        if(sprite == null)
        {
            Debug.Log("图片资源路径：" + path + "错误");
        }
        return sprite;
    }
}
