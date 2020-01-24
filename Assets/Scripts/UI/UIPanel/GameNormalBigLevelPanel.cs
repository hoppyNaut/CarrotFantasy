using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
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
