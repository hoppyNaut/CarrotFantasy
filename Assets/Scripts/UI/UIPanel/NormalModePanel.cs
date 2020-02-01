using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalModePanel : BasePanel
{
    private GameObject img_TopPage;
    private GameObject menuPage;
    private GameObject gameOverPage;
    private GameObject gameWinPage;
    private GameObject prizePage;
    private GameObject startUI;

    protected override void Awake()
    {
        base.Awake();

        img_TopPage = transform.Find("Img_TopPage").gameObject;
        menuPage = transform.Find("MenuPage").gameObject;
        gameOverPage = transform.Find("GameOverPage").gameObject;
        gameWinPage = transform.Find("GameWinPage").gameObject;
        prizePage = transform.Find("PrizePage").gameObject;
        startUI = transform.Find("StartUI").gameObject;
    }
}
