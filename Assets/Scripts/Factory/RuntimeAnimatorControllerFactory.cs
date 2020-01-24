/****************************************************
	文件：RuntimeAnimatorControllerFactory.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 20:36   	
	功能：动画控制器资源工厂
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeAnimatorControllerFactory : IBaseResourceFactory<RuntimeAnimatorController>
{
    protected Dictionary<string, RuntimeAnimatorController> factoryDict = new Dictionary<string, RuntimeAnimatorController>();
    protected string loadPath;

    public RuntimeAnimatorControllerFactory()
    {
        loadPath = "Animator/";
    }

    public RuntimeAnimatorController GetSingleResource(string resPath)
    {
        RuntimeAnimatorController controller = null;
        string path = loadPath + resPath;
        if(factoryDict.ContainsKey(resPath))
        {
            controller = factoryDict[resPath];
        }
        else
        {
            controller = Resources.Load<RuntimeAnimatorController>(path);
            factoryDict.Add(resPath, controller);
        }
        if(controller == null)
        {
            Debug.Log("动画控制器资源路径：" + path + "错误");
        }
        return controller;
    }
}
