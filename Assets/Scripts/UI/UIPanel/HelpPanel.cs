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
        transform.SetSiblingIndex(8);

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
        helpPage.SetActive(true);
        monsterPage.SetActive(false);
        towerPage.SetActive(false);
    }

    public void ShowMonsterPage()
    {
        helpPage.SetActive(false);
        monsterPage.SetActive(true);
        towerPage.SetActive(false);
    }

    public void ShowTowerPage()
    {
        helpPage.SetActive(false);
        monsterPage.SetActive(false);
        towerPage.SetActive(true);
    }

    public void BackToMain()
    {
        ExitPanel();
        mUIFacade.currentScenePanelDict[Constant.MainPanel].EnterPanel();
    }
}
