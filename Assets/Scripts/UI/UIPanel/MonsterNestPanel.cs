using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterNestPanel : BasePanel
{
    private GameObject shopPageGo;
    private Text txt_Milk;
    private Text txt_Cookie;
    private Text txt_Nest;
    private Text txt_Diamonds;
    private List<GameObject> monsterPetGoList;
    private Transform emp_MonsterGroup;

    protected override void Awake()
    {
        base.Awake();
        shopPageGo = transform.Find("ShopPage").gameObject;
        txt_Milk = transform.Find("Img_TopPage").Find("Txt_Milk").GetComponent<Text>();
        txt_Cookie = transform.Find("Img_TopPage").Find("Txt_Cookie").GetComponent<Text>();
        txt_Nest = transform.Find("Img_TopPage").Find("Txt_Nest").GetComponent<Text>();
        txt_Diamonds = shopPageGo.transform.Find("Img_Diamonds/Txt_Diamonds").GetComponent<Text>();
        emp_MonsterGroup = transform.Find("Emp_MonsterGroup");
        //预加载资源
        for(int i = 1; i < 4; i++)
        {
            mUIFacade.GetSprite("MonsterNest/Monster/Egg/" + i.ToString());
            mUIFacade.GetSprite("MonsterNest/Monster/Baby/" + i.ToString());
            mUIFacade.GetSprite("MonsterNest/Monster/Normal/" + i.ToString());
        }
        monsterPetGoList = new List<GameObject>();
    }


    private void OnEnable()
    {
        InitPanel();
        UpdateText();
    }

    public override void InitPanel()
    {
        base.InitPanel();
        for(int i = 0; i < monsterPetGoList.Count; i++)
        {
            mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Emp_Monsters", monsterPetGoList[i]);
        }
        monsterPetGoList.Clear();

        List<MonsterPetData> monsterPetDataList = GameManager.Instance.playerManager.monsterPetDataList;
        for (int i = 0; i < monsterPetDataList.Count; i++)
        {
            if(monsterPetDataList[i].monsterID != 0)
            {
                GameObject monsterPetGo = GameManager.Instance.GetGameObjectResource(FactoryType.UIFactory, "Emp_Monsters");
                monsterPetGo.transform.SetParent(emp_MonsterGroup);
                monsterPetGo.transform.localScale = Vector3.one;
                monsterPetGoList.Add(monsterPetGo);

                MonsterPet monsterPetClass = monsterPetGo.GetComponent<MonsterPet>();
                monsterPetClass.monsterPetData = monsterPetDataList[i];
                monsterPetClass.monsterNestPanel = this;
                monsterPetClass.InitMonsterPet();
            }
        }
    }

    public void ShowShopPanel()
    {
        shopPageGo.SetActive(true);
    }

    public void CloseShopPanel()
    {
        shopPageGo.SetActive(false);
    }

    public void ReturnToMain()
    {
        mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
    }

    public void UpdateText()
    {
        txt_Cookie.text = GameManager.Instance.playerManager.cookies.ToString();
        txt_Milk.text = GameManager.Instance.playerManager.milk.ToString();
        txt_Nest.text = GameManager.Instance.playerManager.nest.ToString();
        txt_Diamonds.text = GameManager.Instance.playerManager.diamonds.ToString();
    }

    public void BuyNest()
    {
        if(GameManager.Instance.playerManager.diamonds >= 60)
        {
            GameManager.Instance.playerManager.diamonds -= 60;
            GameManager.Instance.playerManager.nest++;
            UpdateText();
        }
    }

    public void BuyMilk()
    {
        if(GameManager.Instance.playerManager.diamonds >= 1)
        {
            GameManager.Instance.playerManager.diamonds--;
            GameManager.Instance.playerManager.milk += 10;
            UpdateText();
        }
    }

    public void BuyCookies()
    {
        if(GameManager.Instance.playerManager.diamonds >= 10)
        {
            GameManager.Instance.playerManager.diamonds -= 10;
            GameManager.Instance.playerManager.cookies += 15;
            UpdateText();
        }
    }

}
