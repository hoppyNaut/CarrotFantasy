using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnUpLevel : MonoBehaviour
{
    public int price;

    private Button button;
    private Image image;
    private Text txt_Price;
    private Sprite canUpLevelSprite;
    private Sprite cantUpLevelSprite;
    private Sprite reachHighestLevelSprite;
    private GameController gameController;

    private void Awake()
    {
        gameController = GameController.Instance;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        txt_Price = transform.Find("Txt_UpLevel").GetComponent<Text>();
    }

    private void OnEnable()
    {
#if Game
        if(txt_Price == null)
        {
            return;
        }
        UpdateIcon();
#endif
    }

    void Start()
    {
#if Game
        canUpLevelSprite = gameController.GetSprite("NormalMordel/Game/Tower/Btn_CanUpLevel");
        cantUpLevelSprite = gameController.GetSprite("NormalMordel/Game/Tower/Btn_CantUpLevel");
        reachHighestLevelSprite = gameController.GetSprite("NormalMordel/Game/Tower/Btn_ReachHighestLevel");
        UpdateIcon();
        button.onClick.AddListener(UpLevel);
#endif
        }

    private void Update()
    {
#if Game
        UpdateIcon();
#endif
    }

    private void UpLevel()
    {
        //赋值建造者需要的属性
        gameController.towerBuilder.towerID = gameController.selectGrid.tower.towerID;
        gameController.towerBuilder.towerLevel = gameController.selectGrid.tower.towerLevel + 1;
        //销毁原有的塔
        gameController.selectGrid.towerProperty.UpLevelTower();
        //产生升级后的塔
        GameObject towerGo = gameController.towerBuilder.GetProduct();
        towerGo.transform.SetParent(gameController.transform);
        towerGo.transform.position = gameController.selectGrid.transform.position;
        towerGo.transform.localEulerAngles = new Vector3(-90, 90, 0);
        //后续处理
#if Game
        gameController.selectGrid.FinishBuildTower(towerGo);
        gameController.selectGrid.HideGrid();
#endif
    gameController.selectGrid = null;

    }

    private void UpdateIcon()
    {
        if(gameController.selectGrid == null || !gameController.selectGrid.hasTower)
        {
            return;
        }

        if(gameController.selectGrid.tower.towerLevel >= 3)
        {
            image.sprite = reachHighestLevelSprite;
            txt_Price.enabled = false;
            button.interactable = false;
        }
        else
        {
            txt_Price.enabled = true;
            price = gameController.selectGrid.towerProperty.upLevelPrice;
            txt_Price.text = price.ToString();
            if(gameController.coin >= price)
            {
                image.sprite = canUpLevelSprite;
                button.interactable = true;
            }
            else
            {
                image.sprite = cantUpLevelSprite;
                button.interactable = false;
            }
        }
    }
}
