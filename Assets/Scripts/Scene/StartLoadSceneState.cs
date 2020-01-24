/****************************************************
	文件：StartLoadSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 15:58   	
	功能：开始加载场景基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadSceneState : BaseSceneState
{
    public StartLoadSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void OnEnterScene()
    {
        mUIFacade.AddPanelToDict(Constant.StartLoadPanel);
        base.OnEnterScene();
    }

    public override void OnExitScene()
    {
        base.OnExitScene();
        SceneManager.LoadScene(Constant.MainSceneIndex);
    }
}
