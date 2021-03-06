﻿/****************************************************
	文件：MainPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:42   	
	功能：主面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainPanel : BasePanel
{
    private Animator carrotAnimator;
    private Transform monsterTrans;
    private Transform cloudTrans;

    private Tween[] mainPanelTween;//左移右移动画  0:左移    1:右移
    private Tween exitTween;//退出动画

    protected override void Awake()
    {
        base.Awake();

        //transform.SetSiblingIndex(8);

        carrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
        monsterTrans = transform.Find("Img_Monster");
        cloudTrans = transform.Find("Img_Cloud");

        mainPanelTween = new Tween[2];
        mainPanelTween[0] = transform.DOLocalMoveX(-800, 0.5f);
        mainPanelTween[0].SetAutoKill(false);
        mainPanelTween[0].Pause();
        mainPanelTween[1] = transform.DOLocalMoveX(800, 0.5f);
        mainPanelTween[1].SetAutoKill(false);
        mainPanelTween[1].Pause();

    }


    public override void EnterPanel()
    {
        base.EnterPanel();
        //transform.SetSiblingIndex(8);
        carrotAnimator.Play("CarrotGrow");
        if(exitTween != null)
        {
            exitTween.PlayBackwards();
        }
        cloudTrans.gameObject.SetActive(true);

        PlayUITween();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        exitTween.PlayForward();
        cloudTrans.gameObject.SetActive(false);
    }

    public void MoveToLeft()
    {
        mUIFacade.PlayButtonAudioClip();
        exitTween = mainPanelTween[0];
        ExitPanel();
        //mUIFacade.currentScenePanelDict[Constant.HelpPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.HelpPanel).EnterPanel();
    }

    public void MoveToRight()
    {
        mUIFacade.PlayButtonAudioClip();
        exitTween = mainPanelTween[1];
        ExitPanel();
        //mUIFacade.currentScenePanelDict[Constant.SetPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.SetPanel).EnterPanel();
    }

    //播放UI动画
    private void PlayUITween()
    {
        monsterTrans.DOLocalMoveY(220, 2.0f).SetLoops(-1,LoopType.Yoyo);
        cloudTrans.DOLocalMoveX(600, 8.0f).SetLoops(-1, LoopType.Restart);
    }


    public void OnBtnNormalClick()
    {
        mUIFacade.PlayButtonAudioClip();
        //mUIFacade.currentScenePanelDict[Constant.GameLoadPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.GameLoadPanel).EnterPanel();
        mUIFacade.ChangeSceneState(new GameNormalOptionSceneState(mUIFacade));
    }

    public void OnBtnBossClick()
    {
        mUIFacade.PlayButtonAudioClip();
        //mUIFacade.currentScenePanelDict[Constant.GameLoadPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.GameLoadPanel).EnterPanel();
        mUIFacade.ChangeSceneState(new GameBossOptionSceneState(mUIFacade));
    }

    public void OnBtnMonsterNestClick()
    {
        mUIFacade.PlayButtonAudioClip();
        //mUIFacade.currentScenePanelDict[Constant.GameLoadPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.GameLoadPanel).EnterPanel();
        mUIFacade.ChangeSceneState(new MonsterNestSceneState(mUIFacade));
    }

    public void OnBtnExitClick()
    {
        mUIFacade.PlayButtonAudioClip();
        GameManager.Instance.playerManager.SaveData();
        Application.Quit();
    }
}
