using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void OnMouseDown()
    {
        //游戏暂停
        GameController.Instance.isGamePause = true;
        GameController.Instance.normalModePanel.ShowPrizePage();
        GameController.Instance.PushGameObjectToFactory("Prize", gameObject);
    }
}
