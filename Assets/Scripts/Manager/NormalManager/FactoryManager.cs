/****************************************************
	文件：FactoryManager.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:39   	
	功能：工厂管理者，管理各种类型的工厂和对象池
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager
{
    public Dictionary<FactoryType, IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
    public AudioClipFactory audioClipFactory;
    public SpriteFactory spriteFactory;
    public RuntimeAnimatorControllerFactory runtimeAnimatorControllerFactory;

    public FactoryManager()
    {
        factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
        factoryDict.Add(FactoryType.UIFactory, new UIFactory());
        factoryDict.Add(FactoryType.GameFactory, new GameFactory());
        audioClipFactory = new AudioClipFactory();
        spriteFactory = new SpriteFactory();
        runtimeAnimatorControllerFactory = new RuntimeAnimatorControllerFactory();
    }
}
