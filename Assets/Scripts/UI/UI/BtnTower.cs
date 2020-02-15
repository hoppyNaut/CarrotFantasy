using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnTower : MonoBehaviour
{
    public int towerID;
    public int price;
    private Button button;
    private Image image;
    private Sprite canClickSprite;
    private Sprite cantClickSprite;
    private GameController gameController;

    private void Awake()
    {
        gameController = GameController.Instance;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if(towerID == 0)
        {
            return;
        }
        UpdateIcon();
    }

    private void Start()
    {
        canClickSprite = gameController.GetSprite("NormalMordel/Game/Tower/" + towerID.ToString() + "/CanClick1");
        cantClickSprite = gameController.GetSprite("NormalMordel/Game/Tower/" + towerID.ToString() + "/CanClick0");
        UpdateIcon();
        button.onClick.AddListener(BuildTower);
    }

    private void Update()
    {
        UpdateIcon();
    }

    //建塔
    private void BuildTower()
    {
        gameController.towerBuilder.towerID = towerID;
        gameController.towerBuilder.towerLevel = 1;
        GameObject towerGo = gameController.towerBuilder.GetProduct();
        towerGo.transform.SetParent(gameController.transform);
        towerGo.transform.position = gameController.selectGrid.transform.position;
        towerGo.transform.localEulerAngles = new Vector3(-90, 90, 0);
        //产生建塔特效
        GameObject effectGo = gameController.GetGameObjectResource("BuildEffect");
        effectGo.transform.SetParent(gameController.transform);
        effectGo.transform.position = gameController.selectGrid.transform.position;

        //处理格子状态
        gameController.selectGrid.FinishBuildTower(towerGo);
        gameController.selectGrid.HideGrid();
        gameController.selectGrid.hasTower = true;
        gameController.AddCoin(-price);
        gameController.selectGrid = null;
        gameController.handleTowerCanvas.SetActive(false);
    }

    //更新图标
    private void UpdateIcon()
    {
        if(gameController.coin >= price)
        {
            image.sprite = canClickSprite;
            button.interactable = true;
        }
        else
        {
            image.sprite = cantClickSprite;
            button.interactable = false;
        }
    }

}
