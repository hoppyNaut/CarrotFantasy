using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    private NormalModePanel normalModePanel;

    private void Awake()
    {
        normalModePanel = GetComponentInParent<NormalModePanel>();
    }

    public void ContinueGame()
    {
        GameController.Instance.isGamePause = false;
        normalModePanel.HideMenuPage();
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
