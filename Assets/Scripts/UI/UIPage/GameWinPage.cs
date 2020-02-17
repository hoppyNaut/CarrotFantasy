using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPage : MonoBehaviour
{
    private Text txt_RoundCount;
    private Text txt_TotalRound;
    private Text txt_LevelCount;
    private Image img_Carrot;

    private NormalModePanel normalModePanel;

    public Sprite[] carrotSprites;

    private void Awake()
    {
        txt_RoundCount = transform.Find("Emp_Instruction").Find("Txt_RoundCount").GetComponent<Text>();
        txt_TotalRound = transform.Find("Emp_Instruction").Find("Txt_TotalCount").GetComponent<Text>();
        txt_LevelCount = transform.Find("Emp_Level").Find("Txt_LevelCount").GetComponent<Text>();
        img_Carrot = transform.Find("Img_Carrot").GetComponent<Image>();

        normalModePanel = GetComponentInParent<NormalModePanel>();

        //carrotSprites = new Sprite[3];
        //for(int i = 0; i < 3; i++)
        //{
        //    carrotSprites[i] = GameController.Instance.GetSprite("GameOption/Normal/Level/Carrot_" + (i + 1).ToString());
        //}
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        normalModePanel.ShowRoundInfo(txt_RoundCount);
        txt_TotalRound.text = normalModePanel.totalRound.ToString();
        txt_LevelCount.text = GameController.Instance.curStage.mBigLevelID + "-" + GameController.Instance.curStage.mLevelID.ToString();
        int carrotState = GameController.Instance.GetCarrotState();
        img_Carrot.sprite = carrotSprites[carrotState - 1];
    }

    public void ReplayGame()
    {
        GameController.Instance.isGamePause = false;
        normalModePanel.Replay();
    }

    public void ChooseOtherLevel()
    {
        GameController.Instance.isGamePause = false;
        normalModePanel.ChooseOtherLevel();
    }
}
