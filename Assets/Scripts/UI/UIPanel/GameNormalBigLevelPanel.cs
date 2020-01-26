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
        }
    }

    public void ShowBigLevelState(bool unLocked,int unLockedLevelNum,int totalLevelNum,Transform theBigLevelTrans,int bigLevelID)
    {
        //解锁状态
        if(unLocked)
        {
            theBigLevelTrans.Find("Img_Lock").gameObject.SetActive(false);
            theBigLevelTrans.Find("Img_Page").gameObject.SetActive(true);
            theBigLevelTrans.Find("Img_Page").Find("Txt_Page").GetComponent<Text>().text 
                = unLockedLevelNum + "/" + totalLevelNum;
            Button theBigLevelButton = theBigLevelTrans.GetComponent<Button>();
            theBigLevelButton.interactable = true;
            theBigLevelButton.onClick.AddListener(() =>
            {
                //离开大关卡页面
                //mUIFacade.currentScenePanelDict[Constant.GameNormalBigLevelPanel].ExitPanel();
                mUIFacade.GetCurScenePanel(Constant.GameNormalBigLevelPanel).ExitPanel();
                //初始化并进入小关卡页面
                //mUIFacade.currentScenePanelDict[Constant.GameNormalLevelPanel].EnterPanel();
                GameNormalLevelPanel gameNormalLevelPanel = mUIFacade.GetCurScenePanel(Constant.GameNormalLevelPanel) as GameNormalLevelPanel;
                gameNormalLevelPanel.Init(bigLevelID);
                GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.GetCurScenePanel(Constant.GameNormalOptionPanel) as GameNormalOptionPanel;
                gameNormalOptionPanel.isInBigLevel = false;
            });
        }
        else//未解锁状态
        {
            theBigLevelTrans.Find("Img_Lock").gameObject.SetActive(true);
            theBigLevelTrans.Find("Img_Page").gameObject.SetActive(false);
            theBigLevelTrans.GetComponent<Button>().interactable = false;
        }
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        transform.SetSiblingIndex(8);
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
