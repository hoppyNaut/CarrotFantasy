/****************************************************
	文件：UIManager.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:43   	
	功能：UI管理类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //UI中介
    public UIFacade mUIFacade;
    //当前场景拥有的UI面板的字典
    public Dictionary<string, GameObject> currentScenePanelDict;
    private GameManager mGameManager;

    public UIManager()
    {
        mUIFacade = new UIFacade(this);
        currentScenePanelDict = new Dictionary<string, GameObject>();
        mGameManager = GameManager.Instance;
        mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade);
    }

    //清空字典
    public void ClearDict()
    {
        //将每个不用的UIPanel回收到对象池
        foreach (var item in currentScenePanelDict)
        {
            mUIFacade.PushGameObjectToFactory(FactoryType.UIPanelFactory, item.Key, item.Value);
        }
        currentScenePanelDict.Clear();
    }
}
