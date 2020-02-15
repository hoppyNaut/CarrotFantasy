﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSell : MonoBehaviour
{
    public int price;

    private Button button;
    private Image image;
    private Text txt_Price;
    private GameController gameController;

    private void OnEnable()
    {
        if (txt_Price == null)
        {
            return;
        }
        UpdateIcon();
    }


    private void Start()
    {
        gameController = GameController.Instance;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        txt_Price = transform.Find("Txt_Sell").GetComponent<Text>();
        UpdateIcon();
        button.onClick.AddListener(SellTower);
    }

    private void SellTower()
    {
        //出售塔处理
        gameController.selectGrid.towerProperty.SellTower();
        //后续对格子的处理
        gameController.selectGrid.InitGrid();
        gameController.selectGrid.handleTowerCanvasGo.SetActive(false);
        gameController.selectGrid.HideGrid();
        gameController.selectGrid = null;
    }


    private void UpdateIcon()
    {
        if (gameController.selectGrid == null)
        {
            return;
        }

        price = gameController.selectGrid.towerProperty.sellPrice;
        txt_Price.text = price.ToString();
    }


}
