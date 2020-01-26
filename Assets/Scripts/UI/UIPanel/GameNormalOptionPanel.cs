using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNormalOptionPanel : BasePanel
{
    public bool isInBigLevel = true;

    public void BackToLastPanel()
    {
        if(isInBigLevel)
        {
            //返回主界面
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
        //mUIFacade.currentScenePanelDict[Constant.HelpPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.HelpPanel).EnterPanel();
    }
}
