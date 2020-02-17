using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : MonoBehaviour
{
    private Text txt_RoundCount;
    private Text txt_TotalRound;
    private Text txt_LevelCount;

    private NormalModePanel normalModePanel;

    private void Awake()
    {
        txt_RoundCount = transform.Find("Emp_Instruction").Find("Txt_RoundCount").GetComponent<Text>();
        txt_TotalRound = transform.Find("Emp_Instruction").Find("Txt_TotalCount").GetComponent<Text>();
        txt_LevelCount = transform.Find("Emp_Level").Find("Txt_LevelCount").GetComponent<Text>();

        normalModePanel = GetComponentInParent<NormalModePanel>();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        normalModePanel.ShowRoundInfo(txt_RoundCount);
        txt_TotalRound.text = normalModePanel.totalRound.ToString();
        txt_LevelCount.text = GameController.Instance.curStage.mBigLevelID + "-" + GameController.Instance.curStage.mLevelID.ToString();
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
