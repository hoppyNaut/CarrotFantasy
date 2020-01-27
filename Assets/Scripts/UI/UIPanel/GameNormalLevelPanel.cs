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
        levelID = 1;
        EnterPanel();
    }

    //更新动态UI
    public void UpdateDynamicUI(string spritePath)
    {
        //设置左右背景板
        img_BGLeft.sprite = mUIFacade.GetSprite(spritePath + "BG_Left");
        img_BGRight.sprite = mUIFacade.GetSprite(spritePath + "BG_Right");
        for(int i = 1; i < playerManager.totalNormalModeLevelNumList[bigLevelID - 1] + 1; i++)
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
            //获取当前小关卡信息索引
            int index = 0;
            for(int j = 0; j < bigLevelID - 1; j++)
            {
                index += playerManager.totalNormalModeLevelNumList[j];
            }
            index += (i - 1);
            Stage curStage = playerManager.NormalModeLevelInfoList[index];
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
        int childCount = levelContentTrans.childCount;
        sv_Level.InitContentSize(childCount);

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

}
