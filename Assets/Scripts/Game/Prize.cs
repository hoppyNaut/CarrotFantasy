﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void Update()
    {
        if(GameController.Instance.isGameOver)
        {
            GameController.Instance.PushGameObjectToFactory("Prize", gameObject);
        }
    }

    private void OnMouseDown()
    {
        GameController.Instance.PlayEffectMusic("NormalMordel/GiftGot");
        //游戏暂停
        GameController.Instance.isGamePause = true;
        GameController.Instance.normalModePanel.ShowPrizePage();
        GameController.Instance.PushGameObjectToFactory("Prize", gameObject);
    }
}
