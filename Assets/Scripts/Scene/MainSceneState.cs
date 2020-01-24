/****************************************************
	文件：MainSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 15:59   	
	功能：主场景状态
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneState : BaseSceneState
{
    public MainSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void OnEnterScene()
    {
        //添加面板
        mUIFacade.AddPanelToDict(Constant.MainPanel);
        mUIFacade.AddPanelToDict(Constant.SetPanel);
        mUIFacade.AddPanelToDict(Constant.HelpPanel);
        mUIFacade.AddPanelToDict(Constant.GameLoadPanel);
        base.OnEnterScene();
        mUIFacade.currentScenePanelDict[Constant.MainPanel].EnterPanel();
    }

    public override void OnExitScene()
    {
        base.OnExitScene();
        if(mUIFacade.currentSceneState.GetType() == typeof(GameNormalOptionSceneState))
        {
            SceneManager.LoadScene(Constant.GameNormalOptionSceneIndex);
        }
        else if(mUIFacade.currentSceneState.GetType() == typeof(GameBossOptionSceneState))
        {
            SceneManager.LoadScene(Constant.GameBossOptionSceneIndex);
        }
        else if(mUIFacade.currentSceneState.GetType() == typeof(MonsterNestSceneState))
        {
            SceneManager.LoadScene(Constant.MonsterNestSceneIndex);
        }
    }
}
