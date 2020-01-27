/****************************************************
	文件：GameNormalLevelPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:39   	
	功能：小关卡选择面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalLevelPanel : BasePanel
{
    public int bigLevelID;
    public int levelID;

    private string resPath;

    private Transform levelContentTrans;
    private GameObject lockBtnGo;
    private Transform emp_TowerTrans;
    private Image img_BGLeft;
    private Image img_BGRight;
    private Image img_Carrot;
    private Image img_AllClear;
    private Text txt_Waves;

    private PlayerManager playerManager;
    private ScrollViewControllerOne sv_Level;

    private List<GameObject> levelContentGoList;
    private List<GameObject> towerContentGoList;

    protected override void Awake()
    {
        base.Awake();
        resPath = "GameOption/Normal/Level/";
        playerManager = mUIFacade.mPlayerManager;
        levelContentGoList = new List<GameObject>();
        towerContentGoList = new List<GameObject>();
        levelContentTrans = transform.Find("Scroll View").Find("Viewport").Find("Content");
        lockBtnGo = transform.Find("Img_LockBtn").gameObject;
        emp_TowerTrans = transform.Find("Emp_Tower");
        img_BGLeft = transform.Find("Img_BgLeft").GetComponent<Image>();
        img_BGRight = transform.Find("Img_BgRight").GetComponent<Image>();
        txt_Waves = transform.Find("Img_TotalWaves").GetComponentInChildren<Text>();
        sv_Level = transform.Find("Scroll View").GetComponent<ScrollViewControllerOne>();
        bigLevelID = 1;
        levelID = 1;
    }

    //初始化不同大关卡对应的小关卡
    public void Init(int bigLevelID)
    {
        this.bigLevelID = bigLevelID;
        EnterPanel();
    }

    //资源预加载
    private void LoadResources()
    {
        mUIFacade.GetSprite(resPath + "AllClear");
        mUIFacade.GetSprite(resPath + "Carrot_1");
        mUIFacade.GetSprite(resPath + "Carrot_2");
        mUIFacade.GetSprite(resPath + "Carrot_3");
        for (int i = 1; i < playerManager.bigLevelNum + 1; i++)
        {
            string spritePath = resPath + i.ToString() + "/";
            mUIFacade.GetSprite(spritePath + "BG_Left");
            mUIFacade.GetSprite(spritePath + "BG_Right");
            for (int j = 1; j < playerManager.totalNormalModeLevelNumList[bigLevelID - 1] + 1; j++)
            {
                mUIFacade.GetSprite(spritePath + "Level_" + j.ToString());
            }
        }
        for (int i = 1; i < playerManager.towerNum + 1; i++)
        {
            mUIFacade.GetSprite(resPath + "Tower/Tower_" + i.ToString());
        }

    }

}
