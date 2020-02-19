/****************************************************
	文件：SetPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:40   	
	功能：设置面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SetPanel : BasePanel
{
    private GameObject optionPage;
    private GameObject statisticsPage;
    private GameObject producePage;
    private GameObject resetPage;
    private Tween setPanelTween;

    private bool playBGMusic = true;
    private bool playEffectMusic = true;

    public Text[] statisticsTexts;

    public Sprite[] btnSprites;//0：音效开  1：音效关   2：背景音乐开   3：背景音乐关
    private Image img_Btn_EffectMusic;
    private Image img_Btn_BGMusic;

    protected override void Awake()
    {
        base.Awake();

        optionPage = transform.Find("OptionPage").gameObject;
        statisticsPage = transform.Find("StatisticsPage").gameObject;
        producePage = transform.Find("ProducePage").gameObject;
        resetPage = transform.Find("ResetPage").gameObject;

        img_Btn_BGMusic = optionPage.transform.Find("Btn_BgMusic").GetComponent<Image>();
        img_Btn_EffectMusic = optionPage.transform.Find("Btn_EffectMusic").GetComponent<Image>();

        setPanelTween = transform.DOLocalMoveX(0, 0.5f);
        setPanelTween.SetAutoKill(false);
        setPanelTween.Pause();
    }

    public override void InitPanel()
    {
        base.InitPanel();
        transform.localPosition = new Vector3(-800, 0, 0);
        //transform.SetSiblingIndex(8);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();

        InitPanel();

        ShowOptionPage();
        MoveToCenter();       
    }

    public override void ExitPanel()
    {
        base.ExitPanel();

        setPanelTween.PlayBackwards();
    }

    public void MoveToCenter()
    {
        setPanelTween.PlayForward();
    }

    public void ShowOptionPage()
    {
        if(!optionPage.activeSelf)
        {
            mUIFacade.PlayButtonAudioClip();
            optionPage.SetActive(true);
        }              
        statisticsPage.SetActive(false);
        producePage.SetActive(false);
    }

    public void ShowStatisticsPage()
    {
        mUIFacade.PlayButtonAudioClip();
        optionPage.SetActive(false);
        statisticsPage.SetActive(true);
        producePage.SetActive(false);
        ShowStatistics();
    }

    public void ShowProducePage()
    {
        mUIFacade.PlayButtonAudioClip();
        optionPage.SetActive(false);
        statisticsPage.SetActive(false);
        producePage.SetActive(true);
    }

    public void OpenResetPage()
    {
        mUIFacade.PlayButtonAudioClip();
        resetPage.SetActive(true);
    }

    public void CloseResetPage()
    {
        mUIFacade.PlayButtonAudioClip();
        resetPage.SetActive(false);
    }

    //重置游戏
    public void ResetGame()
    {
        mUIFacade.PlayButtonAudioClip();
        GameManager.Instance.initPlayerManager = true;
        GameManager.Instance.playerManager.ReadData();
        ShowStatistics();
        CloseResetPage();
    }

    //显示数据
    public void ShowStatistics()
    {
        PlayerManager playerManager = mUIFacade.mPlayerManager;
        statisticsTexts[0].text = playerManager.adventrueModeNum.ToString();
        statisticsTexts[1].text = playerManager.burriedLevelNum.ToString();
        statisticsTexts[2].text = playerManager.bossModeNum.ToString();
        statisticsTexts[3].text = playerManager.coin.ToString();
        statisticsTexts[4].text = playerManager.killMonsterNum.ToString();
        statisticsTexts[5].text = playerManager.killBossNum.ToString();
        statisticsTexts[6].text = playerManager.ClearItemNum.ToString();
    }

    public void BackMainPanel()
    {
        mUIFacade.PlayButtonAudioClip();
        ExitPanel();
        //mUIFacade.currentScenePanelDict[Constant.MainPanel].EnterPanel();
        mUIFacade.GetCurScenePanel(Constant.MainPanel).EnterPanel();
    }

    public void ControlBGMusic()
    {
        mUIFacade.PlayButtonAudioClip();
        playBGMusic = !playBGMusic;
        mUIFacade.CloseOrOpenBGMusic();
        if(playBGMusic)
        {
            img_Btn_BGMusic.sprite = btnSprites[2];
        }
        else
        {
            img_Btn_BGMusic.sprite = btnSprites[3];
        }
    }

    public void ControlEffectMusic()
    {
        mUIFacade.PlayButtonAudioClip();
        playEffectMusic = !playEffectMusic;
        mUIFacade.CloseOrOpenEffectMusic();
        if(playEffectMusic)
        {
            img_Btn_EffectMusic.sprite = btnSprites[0];
        }
        else
        {
            img_Btn_EffectMusic.sprite = btnSprites[1];
        }
    }
}
