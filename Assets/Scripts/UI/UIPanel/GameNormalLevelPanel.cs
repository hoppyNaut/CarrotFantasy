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

        LoadResources();
    }

    //初始化不同大关卡对应的小关卡
    public void ToLevelPanel(int bigLevelID)
    {
        this.bigLevelID = bigLevelID;
        levelID = 1;
        EnterPanel();
    }

    public override void InitPanel()
    {
        base.InitPanel();
        gameObject.SetActive(false);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        gameObject.SetActive(true);
        string spritePath = resPath + bigLevelID.ToString() + "/";
        UpdateDynamicUI(spritePath);
        string towerPath = resPath + "Tower/";
        UpdateStaticUI(towerPath);
        sv_Level.Init();
    }

    public override void UpdatePanel()
    {
        base.UpdatePanel();
        string towerPath = resPath + "Tower/";
        UpdateStaticUI(towerPath);
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

    public void ToGamePanel()
    {
        GameManager.Instance.curStage = GetCurStage(bigLevelID, levelID);
        mUIFacade.GetCurScenePanel(Constant.GameLoadPanel).EnterPanel();
        mUIFacade.ChangeSceneState(new NormalModeSceneState(mUIFacade));
    }


    //更新动态UI(关卡UI)
    public void UpdateDynamicUI(string spritePath)
    {
        if(levelContentGoList.Count != 0)
        {
            DestroyMapUI();
        }
        //设置左右背景板
        img_BGLeft.sprite = mUIFacade.GetSprite(spritePath + "BG_Left");
        img_BGRight.sprite = mUIFacade.GetSprite(spritePath + "BG_Right");
        for (int i = 1; i < playerManager.totalNormalModeLevelNumList[bigLevelID - 1] + 1; i++)
        {
            GameObject itemGo = CreateUIAndSetPosition("Img_Level", levelContentTrans);
            levelContentGoList.Add(itemGo);
            
            //设置背景UI
            itemGo.transform.GetComponent<Image>().sprite = mUIFacade.GetSprite(spritePath + "Level_" + i.ToString());
            
            //获取子物体的Transform
            Transform img_BgTrans = itemGo.transform.Find("Img_Bg");
            Transform img_LockTrans = itemGo.transform.Find("Img_Lock");
            Transform img_CarrotTrans = itemGo.transform.Find("Img_Carrot");
            Transform img_AllClearTrans = itemGo.transform.Find("Img_AllClear");
            img_CarrotTrans.gameObject.SetActive(false);
            img_AllClearTrans.gameObject.SetActive(false);
            
            //获取当前小关卡信息
            Stage curStage = GetCurStage(bigLevelID,i);

            if(curStage.mUnLocked)
            {
                //关卡已解锁
               if(curStage.mAllClear)
                {
                    img_AllClearTrans.gameObject.SetActive(true);
                }
               if(curStage.mCarrotState != 0)
                {
                    img_CarrotTrans.gameObject.SetActive(true);
                    img_CarrotTrans.GetComponent<Image>().sprite = mUIFacade.GetSprite(resPath + "Carrot_" + curStage.mCarrotState.ToString());
                }
                img_BgTrans.gameObject.SetActive(false);
                img_LockTrans.gameObject.SetActive(false);
            }
            else
            {
                //关卡未解锁
                if(curStage.mIsHiddenLevel)
                {
                    img_LockTrans.gameObject.SetActive(false);
                    img_BgTrans.gameObject.SetActive(true);
                    //设置解锁怪物图片
                    Image img_Monster = img_BgTrans.Find("Img_Monster").GetComponent<Image>();
                    img_Monster.sprite = mUIFacade.GetSprite("MonsterNest/Monster/Baby/" + bigLevelID.ToString());
                    img_Monster.SetNativeSize();
                }
                else
                {
                    img_LockTrans.gameObject.SetActive(true);
                    img_BgTrans.gameObject.SetActive(false);
                }
            }

        }
        //设置滚动视图Content大小
        sv_Level.Init();

    }

    //更新静态UI(炮台、波次、是否锁定等)
    public void UpdateStaticUI(string spritePath)
    {
        //每次更新需要先清除原有的炮塔
        if(towerContentGoList.Count != 0)
        {
            DestroyTowerUI();
        }
        //获取当前小关卡信息
        Stage curStage = GetCurStage(bigLevelID,levelID);

        lockBtnGo.SetActive(false);
        if (!curStage.mUnLocked)
        {
            lockBtnGo.SetActive(true);
        }
        //显示波次数
        txt_Waves.text = curStage.mTotalWaves.ToString();
        //显示炮塔信息
        for(int i = 1; i < curStage.mTowerIDListLength + 1; i++)
        {
            //实例化UI
            GameObject towerGo = CreateUIAndSetPosition("Img_Tower", emp_TowerTrans);
            towerContentGoList.Add(towerGo);
            towerGo.GetComponent<Image>().sprite = mUIFacade.GetSprite(spritePath + "Tower_" + curStage.mTowerIDList[i - 1].ToString());
        }
    }


    private Stage GetCurStage(int bigLevelID, int levelID)
    {
        //获取当前小关卡索引
        int index = 0;
        for(int i = 0; i < bigLevelID - 1; i++)
        {
            index += playerManager.totalNormalModeLevelNumList[i];
        }
        index += (levelID - 1);

        Stage curStage = playerManager.NormalModeLevelInfoList[index];
        return curStage;
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

    //实例化UI到Content下
    private GameObject CreateUIAndSetPosition(string name,Transform parentTrans)
    {
        GameObject itemGo = mUIFacade.GetGameObjectResource(FactoryType.UIFactory, name);
        itemGo.transform.SetParent(parentTrans);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }

    //销毁Content下的UI
    private void DestroyMapUI()
    {
        foreach(var item in levelContentGoList)
        {
            mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, item.name, item);
        }
        levelContentGoList.Clear();
    }

    private void DestroyTowerUI()
    {
        foreach(var item in towerContentGoList)
        {
            item.GetComponent<Image>().sprite = null;
            mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, item.name, item);
        }
        towerContentGoList.Clear();
    }


    //更新当前滑动关卡的信息
    public void UpdateCurLevelInfo(int levelID)
    {
        this.levelID = levelID;
        UpdatePanel();
    }


}
