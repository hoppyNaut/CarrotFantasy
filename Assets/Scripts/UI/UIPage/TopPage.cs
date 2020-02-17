using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopPage : MonoBehaviour
{
    private Text txt_Coin;
    private Text txt_RoundCount;
    private Text txt_TotalCount;
    private Image img_Btn_gameSpeed;
    private Image img_Btn_Pause;

    private GameObject emp_Pause;
    private GameObject emp_Playing;

    public Sprite[] btn_gameSpeedSprite;
    public Sprite[] btn_PauseSprite;//0:暂停 1:开始

    private bool isNormalSpeed;
    private bool isPause;

    private NormalModePanel normalModePanel;

    private void Awake()
    {
        txt_Coin = transform.Find("Txt_Coin").GetComponent<Text>();
        txt_RoundCount = transform.Find("Emp_Playing").Find("Txt_RoundText").GetComponent<Text>();
        txt_TotalCount = transform.Find("Emp_Playing").Find("Txt_TotalCount").GetComponent<Text>();
        img_Btn_gameSpeed = transform.Find("Btn_Speed").GetComponent<Image>();
        img_Btn_Pause = transform.Find("Btn_Pause").GetComponent<Image>();
        emp_Pause = transform.Find("Emp_Pause").gameObject;
        emp_Playing = transform.Find("Emp_Playing").gameObject;

        normalModePanel = GetComponentInParent<NormalModePanel>();

        isNormalSpeed = true;
        isPause = false;

        //btn_gameSpeedSprite = new Sprite[2];
        //btn_gameSpeedSprite[0] = GameController.Instance.GetSprite("NormalMordel/GameSpeed_1");
        //btn_gameSpeedSprite[1] = GameController.Instance.GetSprite("NormalMordel/GameSpeed_2");
        //btn_PauseSprite = new Sprite[2];
        //btn_PauseSprite[0] = GameController.Instance.GetSprite("NormalMordel/Pause");
        //btn_PauseSprite[1] = GameController.Instance.GetSprite("NormalMordel/Play");
    }

    private void OnEnable()
    {
        UpdateCoinText();
        txt_TotalCount.text = normalModePanel.totalRound.ToString();
        img_Btn_Pause.sprite = btn_PauseSprite[1];
        img_Btn_gameSpeed.sprite = btn_gameSpeedSprite[0];
        isPause = false;
        isNormalSpeed = true;
        emp_Pause.SetActive(false);
        emp_Playing.SetActive(true);
    }

    public void UpdateCoinText()
    {
        txt_Coin.text = GameController.Instance.coin.ToString();
    }

    public void UpdateRoundText()
    {
        normalModePanel.ShowRoundInfo(txt_RoundCount);
    }

    public void ChangeGameSpeed()
    {
        isNormalSpeed = !isNormalSpeed;
        if(isNormalSpeed)
        {
            GameController.Instance.gameSpeed = 1;
            img_Btn_gameSpeed.sprite = btn_gameSpeedSprite[0];
        }
        else
        {
            GameController.Instance.gameSpeed = 2;
            img_Btn_gameSpeed.sprite = btn_gameSpeedSprite[1];
        }
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if(isPause)
        {
            GameController.Instance.isGamePause = true;
            img_Btn_Pause.sprite = btn_PauseSprite[0];
            emp_Pause.SetActive(true);
            emp_Playing.SetActive(false);
        }
        else
        {
            GameController.Instance.isGamePause = false;
            img_Btn_Pause.sprite = btn_PauseSprite[1];
            emp_Pause.SetActive(false);
            emp_Playing.SetActive(true);
        }
    }

    public void ShowMenu()
    {
        GameController.Instance.isGamePause = true;
        normalModePanel.ShowMenuPage();
    }
}
