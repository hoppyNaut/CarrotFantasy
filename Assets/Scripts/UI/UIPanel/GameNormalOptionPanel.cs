/****************************************************
	文件：GameNormalOptionPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:41   	
	功能：关卡选择共用面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNormalOptionPanel : BasePanel
{
    public bool isInBigLevel = true;

    public void BackToLastPanel()
    {
        mUIFacade.PlayButtonAudioClip();
        if (isInBigLevel)
        {
            //返回主界面
            mUIFacade.GetCurScenePanel(Constant.GameLoadPanel).EnterPanel();
            mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
        }
        else
        {
            //返回大关卡选择面板
            // mUIFacade.currentScenePanelDict[Constant.GameNormalLevelPanel].ExitPanel();
            mUIFacade.GetCurScenePanel(Constant.GameNormalLevelPanel).ExitPanel();
            //mUIFacade.currentScenePanelDict[Constant.GameNormalBigLevelPanel].EnterPanel();
            mUIFacade.GetCurScenePanel(Constant.GameNormalBigLevelPanel).EnterPanel();
        }
        isInBigLevel = true;
    }

    public void ShowHelpPanel()
    {
        mUIFacade.PlayButtonAudioClip();
        //mUIFacade.currentScenePanelDict[Constant.HelpPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.HelpPanel).EnterPanel();
    }
}
