/****************************************************
	文件：NormalModePanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/17 9:52   	
	功能：游戏场景UI面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalModePanel : BasePanel
{
    private GameObject topPageGo;
    private GameObject menuPageGo;
    private GameObject gameOverPageGo;
    private GameObject gameWinPageGo;
    private GameObject prizePageGo;
    private GameObject startUIGo;
    private GameObject finalWaveGo;

    public TopPage topPage;
    public MenuPage menuPage;
    public GameOverPage gameOverPage;
    public GameWinPage gameWinPage;
    public PrizePage prizePage;

    public int totalRound;

    protected override void Awake()
    {
        base.Awake();

        topPageGo = transform.Find("TopPage").gameObject;
        menuPageGo = transform.Find("MenuPage").gameObject;
        gameOverPageGo = transform.Find("GameOverPage").gameObject;
        gameWinPageGo = transform.Find("GameWinPage").gameObject;
        prizePageGo = transform.Find("PrizePage").gameObject;
        startUIGo = transform.Find("StartUI").gameObject;
        finalWaveGo = transform.Find("FinalWave").gameObject;

        topPage = topPageGo.GetComponent<TopPage>();
        menuPage = menuPageGo.GetComponent<MenuPage>();
        gameOverPage = gameOverPageGo.GetComponent<GameOverPage>();
        gameWinPage = gameWinPageGo.GetComponent<GameWinPage>();
        prizePage = prizePageGo.GetComponent<PrizePage>();
    }

    private void OnEnable()
    {
        startUIGo.SetActive(true);
        //播放倒计时音效
        InvokeRepeating("PlayAudio", 0, 1);
        //倒计时结束后开始游戏
        Invoke("StartGame",3);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        totalRound = GameController.Instance.curStage.mTotalWaves;
        topPageGo.SetActive(true);
    }

    public override void UpdatePanel()
    {
        base.UpdatePanel();
    }

    #region 页面/UI显示/隐藏方法
    public void ShowPrizePage()
    {
        prizePageGo.SetActive(true);
    }

    public void HidePrizePage()
    {
        prizePageGo.SetActive(false);
    }

    public void ShowMenuPage()
    {
        menuPageGo.SetActive(true);
    }

    public void HideMenuPage()
    {
        menuPageGo.SetActive(false);
    }

    public void ShowGameWinPage()
    {
        GameController.Instance.isGameOver = true;
        UpdatePlayerManagerData();
        //获取当前小关卡索引
        int index = 0;
        for (int i = 0; i < GameController.Instance.curStage.mBigLevelID - 1; i++)
        {
            index += GameManager.Instance.playerManager.totalNormalModeLevelNumList[i];
        }
        index += (GameController.Instance.curStage.mLevelID - 1);
        //当前关卡信息更新
        Stage curStage = GameManager.Instance.playerManager.NormalModeLevelInfoList[index];
        if(GameController.Instance.IsClearAllItem())
        {
            curStage.mAllClear = true;
        }
        int carrotState = GameController.Instance.GetCarrotState();
        if(carrotState != 0)
        {
            if(curStage.mCarrotState == 0)
            {
                curStage.mCarrotState = carrotState;
            }
            else if (curStage.mCarrotState > carrotState)
            {
                curStage.mCarrotState = carrotState;
            }
        }
        //解锁下一个关卡
        //不是最后一关且不是隐藏关卡
        if(!GameController.Instance.curStage.mIsHiddenLevel && index < GameManager.Instance.playerManager.NormalModeLevelInfoList.Count)
        {
            GameManager.Instance.playerManager.NormalModeLevelInfoList[index + 1].mUnLocked = true;
        }

        gameWinPageGo.SetActive(true);
        GameManager.Instance.playerManager.adventrueModeNum++;
    }

    public void ShowGameOverPage()
    {
        GameController.Instance.isGameOver = true;
        UpdatePlayerManagerData();
        gameOverPageGo.SetActive(true);
    }

    public void ShowFinalWaveUI()
    {
        finalWaveGo.SetActive(true);
        Invoke("HideFinalWaveUI", 1);
    }

    private void HideFinalWaveUI()
    {
        finalWaveGo.SetActive(false);
    }

    //更新回合的文本显示
    public void ShowRoundInfo(Text roundText)
    {
        int roundNum = GameController.Instance.curLevel.curRound + 1;
        string roundStr = "";
        if(roundNum > totalRound)
        {
            roundNum = totalRound;
        }
        if(roundNum < 10)
        {
            roundStr += "0  " + roundNum.ToString();
        }
        else
        {
            roundStr += (roundNum/10).ToString() + "  "+ (roundNum%10).ToString();
        }
        roundText.text = roundStr;
    }
    #endregion

    #region 关卡处理方法
    public void UpdatePlayerManagerData()
    {
        GameManager.Instance.playerManager.coin += GameController.Instance.coin;
        GameManager.Instance.playerManager.killMonsterNum = GameController.Instance.totalKillMonsterNum;
        GameManager.Instance.playerManager.ClearItemNum = GameController.Instance.destroyItemNum;
    }

    public void Replay()
    {
        //当游戏结束时,更新当局游戏数据
        if(GameController.Instance.isGameOver)
        {
            UpdatePlayerManagerData();
        }
        mUIFacade.ChangeSceneState(new NormalModeSceneState(mUIFacade));
        Invoke("ResetGame", 2.0f);
    }

    private void ResetGame()
    {
        ResetUI();
        SceneManager.LoadScene(Constant.NormalModeSceneIndex);
    }

    private void ResetUI()
    {
        gameOverPageGo.SetActive(false);
        gameWinPageGo.SetActive(false);
        menuPageGo.SetActive(false);
        prizePageGo.SetActive(false);
    }

    public void ChooseOtherLevel()
    {
        //当游戏结束时,更新当局游戏数据
        if (GameController.Instance.isGameOver)
        {
            UpdatePlayerManagerData();
        }
        mUIFacade.ChangeSceneState(new GameNormalOptionSceneState(mUIFacade));
        Invoke("ToGameNormalOptionPanel", 2);        
    }

    //返回关卡选择场景
    public void ToGameNormalOptionPanel()
    {
        ResetUI();
        SceneManager.LoadScene(Constant.GameNormalOptionSceneIndex);
    }
    #endregion

    private void PlayAudio()
    {

    }

    private void StartGame()
    {
        GameController.Instance.StartGame();
        startUIGo.SetActive(false);
        CancelInvoke();
    }
}
