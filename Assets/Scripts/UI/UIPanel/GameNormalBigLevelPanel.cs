/****************************************************
	文件：GameNormalBigLevelPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:39   	
	功能：大关卡选择面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalBigLevelPanel : BasePanel
{
    public Transform bigLevelContentTrans;
    public int bigLevelPageCount;
    private ScrollViewControllerOne sv_bigLevel;
    private PlayerManager playerManager;
    private Transform[] bigLevelPages;

    private bool hasRigisterEvent;

    protected override void Awake()
    {
        base.Awake();
        Transform scrollViewTrans = transform.Find("Scroll View");
        bigLevelContentTrans = scrollViewTrans.Find("Viewport").Find("Content");
        bigLevelPageCount = bigLevelContentTrans.childCount;
        sv_bigLevel = scrollViewTrans.GetComponent<ScrollViewControllerOne>();
        playerManager = mUIFacade.mPlayerManager;
        bigLevelPages = new Transform[bigLevelPageCount];
        for(int i = 0; i < bigLevelPageCount; i++)
        {
            Transform trans = bigLevelContentTrans.GetChild(i);
            bigLevelPages[i] = trans;
            //显示大关卡信息
            ShowBigLevelState(playerManager.unLockedNormalModeBigLevelList[i], playerManager.unLockedNormalModeLevelNumList[i],
                playerManager.totalNormalModeLevelNumList[i], bigLevelPages[i], i + 1);
        }
        hasRigisterEvent = true;
    }

    private void OnEnable()
    {
        for(int i = 0; i < bigLevelPageCount; i++)
        {
            ShowBigLevelState(playerManager.unLockedNormalModeBigLevelList[i], playerManager.unLockedNormalModeLevelNumList[i],
                playerManager.totalNormalModeLevelNumList[i], bigLevelPages[i], i + 1);
        }
    }

    public void ShowBigLevelState(bool unLocked,int unLockedLevelNum,int totalLevelNum,Transform theBigLevelTrans,int bigLevelID)
    {
        //解锁状态
        if(unLocked)
        {
            Debug.Log("Enter UnLock State");
            theBigLevelTrans.Find("Img_Lock").gameObject.SetActive(false);
            theBigLevelTrans.Find("Img_Page").gameObject.SetActive(true);
            theBigLevelTrans.Find("Img_Page").Find("Txt_Page").GetComponent<Text>().text 
                = unLockedLevelNum + "/" + totalLevelNum;
            Button theBigLevelButton = theBigLevelTrans.GetComponent<Button>();
            theBigLevelButton.interactable = true;
            if(!hasRigisterEvent)
            {
                theBigLevelButton.onClick.AddListener(() =>
                {
                    //离开大关卡页面
                    //mUIFacade.currentScenePanelDict[Constant.GameNormalBigLevelPanel].ExitPanel();
                    mUIFacade.GetCurScenePanel(Constant.GameNormalBigLevelPanel).ExitPanel();
                    //初始化并进入小关卡页面
                    //mUIFacade.currentScenePanelDict[Constant.GameNormalLevelPanel].EnterPanel();
                    GameNormalLevelPanel gameNormalLevelPanel = mUIFacade.GetCurScenePanel(Constant.GameNormalLevelPanel) as GameNormalLevelPanel;
                    gameNormalLevelPanel.ToLevelPanel(bigLevelID);
                    //设置所在页面
                    GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.GetCurScenePanel(Constant.GameNormalOptionPanel) as GameNormalOptionPanel;
                    gameNormalOptionPanel.isInBigLevel = false;
                });
            }

        }
        else//未解锁状态
        {
            theBigLevelTrans.Find("Img_Lock").gameObject.SetActive(true);
            theBigLevelTrans.Find("Img_Page").gameObject.SetActive(false);
            theBigLevelTrans.GetComponent<Button>().interactable = false;
        }
    }
    public override void InitPanel()
    {
        base.InitPanel();
        gameObject.SetActive(true);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        sv_bigLevel.Init();
        gameObject.SetActive(true);
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

    public void MoveNext()
    {
        sv_bigLevel.MoveNext();
    }

    public void MovePrev()
    {
        sv_bigLevel.MovePrev();
    }
}
