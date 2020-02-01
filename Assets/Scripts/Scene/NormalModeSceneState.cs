/****************************************************
	文件：NormalModelSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 16:27   	
	功能：普通关卡游戏场景
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalModeSceneState : BaseSceneState
{
    public NormalModeSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void OnEnterScene()
    {
        mUIFacade.AddPanelToDict(Constant.GameLoadPanel);
        mUIFacade.AddPanelToDict(Constant.NormalModePanel);

        base.OnEnterScene();
    }

    public override void OnExitScene()
    {
        base.OnExitScene();
    }
}
