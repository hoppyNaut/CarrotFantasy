using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPet : MonoBehaviour
{
    public MonsterPetData monsterPetData;

    private GameObject[] monsterLevelGoList;

    public Sprite[] buttonSprites;//0:可用milk 1:不可用mik 2:可用cookies 3:不可用cookies

    //Egg
    private GameObject img_InstructionGo;

    //Baby
    private GameObject emp_FeedGo;
    private Text txt_Milk;
    private Text txt_Cookie;
    private Button btn_Milk;
    private Button btn_Cookie;
    private Image img_Btn_Milk;
    private Image img_Btn_Cookies;

    //Normal
    private GameObject img_TalkRightGo;
    private GameObject img_TalkLeftGo;

    public MonsterNestPanel monsterNestPanel;

    private void Awake()
    {
        monsterLevelGoList = new GameObject[3];
        monsterLevelGoList[0] = transform.Find("Emp_Egg").gameObject;
        monsterLevelGoList[1] = transform.Find("Emp_Baby").gameObject;
        monsterLevelGoList[2] = transform.Find("Emp_Normal").gameObject;

        img_InstructionGo = monsterLevelGoList[0].transform.Find("Img_Instruction").gameObject;

        emp_FeedGo = monsterLevelGoList[1].transform.Find("Emp_Feed").gameObject;
        btn_Milk = emp_FeedGo.transform.Find("Btn_Milk").GetComponent<Button>();
        btn_Cookie = emp_FeedGo.transform.Find("Btn_Cookie").GetComponent<Button>();
        txt_Milk = emp_FeedGo.transform.Find("Btn_Milk").Find("Txt_Milk").GetComponent<Text>();
        txt_Cookie = emp_FeedGo.transform.Find("Btn_Cookie").Find("Txt_Cookie").GetComponent<Text>();
        img_Btn_Milk = emp_FeedGo.transform.Find("Btn_Milk").GetComponent<Image>();
        img_Btn_Cookies = emp_FeedGo.transform.Find("Btn_Cookie").GetComponent<Image>();

        img_TalkRightGo = transform.Find("Img_TalkRight").gameObject;
        img_TalkLeftGo = transform.Find("Img_TalkLeft").gameObject;

    }

    private void OnEnable()
    {
        InitMonsterPet();
    }

    public void InitMonsterPet()
    {
        if(monsterPetData.remainMilk == 0)
        {
            monsterPetData.remainMilk = monsterPetData.monsterID * 60;
        }
        if(monsterPetData.remainCookies == 0)
        {
            monsterPetData.remainCookies = monsterPetData.monsterID * 30;
        }
        ShowMonster();
    }

    public void ClickPet()
    {
        GameManager.Instance.audioManager.PlayEffectMusic(GameManager.Instance.GetAudioClip("MonsterNest/PetSound" + monsterPetData.monsterLevel.ToString()));
        switch(monsterPetData.monsterLevel)
        {
            case 1:
                if(GameManager.Instance.playerManager.nest >= 1)
                {
                    GameManager.Instance.playerManager.nest--;
                    //升级并更新显示
                    monsterPetData.monsterLevel++;
                    ShowMonster();
                    monsterNestPanel.UpdateText();
                }
                else
                {
                    img_InstructionGo.SetActive(true);
                    Invoke("CloseTalkUI", 2);
                }
                break;
            case 2:
                if(emp_FeedGo.activeSelf)
                {
                    emp_FeedGo.SetActive(false);
                }
                else
                {
                    emp_FeedGo.SetActive(true);
                    if(GameManager.Instance.playerManager.milk < monsterPetData.remainMilk)
                    {
                        img_Btn_Milk.sprite = buttonSprites[1];
                        btn_Milk.interactable = false;
                    }
                    else
                    {
                        img_Btn_Milk.sprite = buttonSprites[0];
                        btn_Milk.interactable = true;
                        txt_Milk.text = monsterPetData.remainMilk.ToString();
                        if (monsterPetData.remainMilk == 0)
                        {
                            btn_Milk.gameObject.SetActive(false);
                        }
                        else
                        {                          
                            btn_Milk.gameObject.SetActive(true);
                        }         
                    }
                    if (GameManager.Instance.playerManager.cookies < monsterPetData.remainCookies)
                    {
                        img_Btn_Cookies.sprite = buttonSprites[3];
                        btn_Cookie.interactable = false;
                    }
                    else
                    {
                        img_Btn_Cookies.sprite = buttonSprites[2];
                        btn_Cookie.interactable = true;
                        txt_Cookie.text = monsterPetData.remainCookies.ToString();
                        if (monsterPetData.remainCookies == 0)
                        {
                            btn_Cookie.gameObject.SetActive(false);
                        }
                        else
                        {
                            btn_Cookie.gameObject.SetActive(true);
                        }                       
                    }
                }
                break;
            case 3:
                int randomNum = Random.Range(0, 2);
                if(randomNum == 1)
                {
                    img_TalkRightGo.SetActive(true);
                    Invoke("CloseTalkUI", 2);
                }
                else
                {
                    img_TalkLeftGo.SetActive(true);
                    Invoke("CloseTalkUI", 2);
                }
                break;
            default:
                break;
        }
    }

    private void CloseTalkUI()
    {
        switch(monsterPetData.monsterLevel)
        {
            case 1:
                img_InstructionGo.SetActive(false);
                break;
            case 2:
                break;
            case 3:
                img_TalkLeftGo.SetActive(false);
                img_TalkRightGo.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void FeedCookie()
    {
        GameManager.Instance.audioManager.PlayEffectMusic(GameManager.Instance.GetAudioClip("MonsterNest/Feed02"));
        GameManager.Instance.playerManager.cookies -= monsterPetData.remainCookies;
        monsterPetData.remainCookies = 0;
        emp_FeedGo.SetActive(false);
        monsterNestPanel.UpdateText();
        GrowUp();
    }

    public void FeedMilk()
    {
        GameManager.Instance.audioManager.PlayEffectMusic(GameManager.Instance.GetAudioClip("MonsterNest/Feed01"));
        GameManager.Instance.playerManager.milk -= monsterPetData.remainMilk;
        monsterPetData.remainMilk = 0;
        emp_FeedGo.SetActive(false);
        monsterNestPanel.UpdateText();
        Invoke("Grow", 0.5f);
    }

    private void GrowUp()
    {
        if(monsterPetData.remainMilk == 0 && monsterPetData.remainCookies == 0)
        {
            GameManager.Instance.audioManager.PlayEffectMusic(GameManager.Instance.GetAudioClip("MonsterNest/PetChange"));
            monsterPetData.monsterLevel++;
            if(monsterPetData.monsterLevel >= 3)
            {
                int index = 0;
                for(int i = 0; i < monsterPetData.monsterID; i++)
                {
                    index += GameManager.Instance.playerManager.totalNormalModeLevelNumList[i];
                }
                GameManager.Instance.playerManager.NormalModeLevelInfoList[index - 1].mUnLocked = true;
                GameManager.Instance.playerManager.burriedLevelNum++;
                ShowMonster();
            }
            else
            {
                ShowMonster();
            }
            SaveMonsterData();
        }
    }

    //保存怪物数据
    private void SaveMonsterData()
    {
        for(int i = 0; i < GameManager.Instance.playerManager.monsterPetDataList.Count;i++)
        {
            if(GameManager.Instance.playerManager.monsterPetDataList[i].monsterID == monsterPetData.monsterID)
            {
                GameManager.Instance.playerManager.monsterPetDataList[i] = monsterPetData;
            }
        }
    }

    //显示怪物外观
    private void ShowMonster()
    {
        for(int i = 0; i < monsterLevelGoList.Length; i++)
        {
            monsterLevelGoList[i].SetActive(false);
            if((i + 1) == monsterPetData.monsterLevel)
            {
                monsterLevelGoList[i].SetActive(true);
                Sprite petSprite = null;
                switch(monsterPetData.monsterLevel)
                {
                    case 1:
                        petSprite = GameManager.Instance.GetSprite("MonsterNest/Monster/Egg/" + monsterPetData.monsterID.ToString());
                        break;
                    case 2:
                        petSprite = GameManager.Instance.GetSprite("MonsterNest/Monster/Baby/" + monsterPetData.monsterID.ToString());
                        break;
                    case 3:
                        petSprite = GameManager.Instance.GetSprite("MonsterNest/Monster/Normal/" + monsterPetData.monsterID.ToString());
                        break;
                }
                Image img_Monster = monsterLevelGoList[i].transform.Find("Img_Pet").GetComponent<Image>();
                img_Monster.sprite = petSprite;
                img_Monster.SetNativeSize();
            }
           
        }
    }
}

[System.Serializable]
public struct MonsterPetData
{
    public int monsterID;
    public int monsterLevel;
    public int remainCookies;
    public int remainMilk;
}
