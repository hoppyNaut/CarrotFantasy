/****************************************************
	文件：HelpPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:42   	
	功能：帮助面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HelpPanel : BasePanel
{
    private GameObject helpPage;
    private GameObject monsterPage;
    private GameObject towerPage;

    private Tween helpPanelTween;

    private ScrollViewControllerOne sv_HelpPage;
    private ScrollViewControllerOne sv_TowerPage;

    protected override void Awake()
    {
        base.Awake();

        helpPage = transform.Find("HelpPage").gameObject;
        monsterPage = transform.Find("MonsterPage").gameObject;
        towerPage = transform.Find("TowerPage").gameObject;

        sv_HelpPage = helpPage.transform.Find("Scroll View").GetComponentInChildren<ScrollViewControllerOne>();
        sv_TowerPage = towerPage.transform.Find("Scroll View").GetComponentInChildren<ScrollViewControllerOne>();

        helpPanelTween = transform.DOLocalMoveX(0, 0.5f);
        helpPanelTween.SetAutoKill(false);
        helpPanelTween.Pause();
    }

    public override void InitPanel()
    {
        base.InitPanel();
        transform.localPosition = new Vector3(800, 0, 0);

        sv_HelpPage.Init();
        sv_TowerPage.Init();
    }

    public override void EnterPanel()
    {
        base.EnterPanel();

        InitPanel();

        MoveToCenter();
        ShowHelpPage();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        helpPanelTween.PlayBackwards();
    }


    public void MoveToCenter()
    {
        helpPanelTween.PlayForward();
    }

    public void ShowHelpPage()
    {
        if(!helpPage.activeSelf)
        {
            mUIFacade.PlayButtonAudioClip();
            helpPage.SetActive(true);
        }
       
        monsterPage.SetActive(false);
        towerPage.SetActive(false);
    }

    public void ShowMonsterPage()
    {
        mUIFacade.PlayButtonAudioClip();
        helpPage.SetActive(false);
        monsterPage.SetActive(true);
        towerPage.SetActive(false);
    }

    public void ShowTowerPage()
    {
        mUIFacade.PlayButtonAudioClip();
        helpPage.SetActive(false);
        monsterPage.SetActive(false);
        towerPage.SetActive(true);
    }

    public void BackToMain()
    {
        mUIFacade.PlayButtonAudioClip();
        ExitPanel();
        //mUIFacade.currentScenePanelDict[Constant.MainPanel].EnterPanel();
        if(mUIFacade.currentSceneState.GetType() == typeof(MainSceneState))
        {
            mUIFacade.GetCurScenePanel(Constant.MainPanel).EnterPanel();
        }
        else if(mUIFacade.currentSceneState.GetType() == typeof(GameNormalOptionSceneState))
        {
            GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.GetCurScenePanel(Constant.GameNormalOptionPanel) as GameNormalOptionPanel;
            if (gameNormalOptionPanel.isInBigLevel)
            {
                mUIFacade.GetCurScenePanel(Constant.GameNormalBigLevelPanel).EnterPanel();
            }
            else
            {
                mUIFacade.GetCurScenePanel(Constant.GameNormalLevelPanel).EnterPanel();
            }
            
        }
    }
}
