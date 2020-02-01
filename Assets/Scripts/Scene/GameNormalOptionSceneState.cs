/****************************************************
	文件：NormalGameOptionSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 16:20   	
	功能：普通关卡选择场景
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNormalOptionSceneState : BaseSceneState
{
    public GameNormalOptionSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void OnEnterScene()
    {
        mUIFacade.AddPanelToDict(Constant.GameNormalOptionPanel);
        mUIFacade.AddPanelToDict(Constant.GameNormalLevelPanel);
        mUIFacade.AddPanelToDict(Constant.GameNormalBigLevelPanel);
        mUIFacade.AddPanelToDict(Constant.HelpPanel);
        mUIFacade.AddPanelToDict(Constant.GameLoadPanel);

        base.OnEnterScene();
    }

    public override void OnExitScene()
    {
        base.OnExitScene();
        SceneManager.LoadScene(Constant.NormalModeSceneIndex);
    }
}
