using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizePage : MonoBehaviour
{
    private Image img_Prize;
    private Text txt_PrizeName;
    private Image img_Instruction;
    private Animator animator;

    private NormalModePanel normalModePanel;

    public Sprite[] prizeSprites;
    public Sprite[] instructionSprites;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        img_Prize = transform.Find("Img_Prize").GetComponent<Image>();
        txt_PrizeName = transform.Find("Img_Prize").Find("Txt_PrizeName").GetComponent<Text>();
        img_Instruction = transform.Find("Img_Instruction").GetComponent<Image>();

        normalModePanel = GetComponentInParent<NormalModePanel>();

        //prizeSprites = new Sprite[4];
        //for(int i = 0; i < prizeSprites.Length; i++)
        //{
        //    prizeSprites[i] = GameController.Instance.GetSprite("MonsterNest/Prize/Prize" + (i + 1).ToString());
        //}
        //instructionSprites = new Sprite[4];
        //for(int i = 0; i < instructionSprites.Length; i++)
        //{
        //    instructionSprites[i] = GameController.Instance.GetSprite("MonsterNest/Prize/Instruction" + (i + 1).ToString());
        //}
    }

    private void OnEnable()
    {
        int randomNum = Random.Range(0, 4);
        string prizeName = "";
        if (randomNum >= 3 && GameManager.Instance.playerManager.monsterPetDataList.Count < 3)
        {
            int randomEggNum = 0;
            do
            {
                randomEggNum = Random.Range(1, 4);
            } while (HasThePet(randomEggNum));
            MonsterPetData monsterPetData = new MonsterPetData
            {
                monsterID = randomEggNum,
                monsterLevel = 1,
                remainCookies = 0,
                remainMilk = 0
            };
            GameManager.Instance.playerManager.monsterPetDataList.Add(monsterPetData);
            prizeName = "宠物蛋";
        }
        else
        {
            switch(randomNum)
            {
                case 0:
                    prizeName = "牛奶";
                    GameManager.Instance.playerManager.milk += 20;
                    break;
                case 1:
                    prizeName = "饼干";
                    GameManager.Instance.playerManager.cookies += 20;
                    break;
                case 2:
                    prizeName = "窝";
                    GameManager.Instance.playerManager.nest += 1;
                    break;
                default:
                    break;
            }
        }
        txt_PrizeName.text = prizeName;
        img_Instruction.sprite = instructionSprites[randomNum];
        img_Prize.sprite = prizeSprites[randomNum];
        animator.Play("Enter");
    }

    private bool HasThePet(int monsterID)
    {
        List<MonsterPetData> monsterPetDataList = GameManager.Instance.playerManager.monsterPetDataList;
        for(int  i = 0; i < monsterPetDataList.Count; i++)
        {
            if(monsterPetDataList[i].monsterID == monsterID)
            {
                return true;
            }
        }
        return false;
    }

    public void ClosePrizePage()
    {
        if(!normalModePanel.topPage.isPause)
        {
            GameController.Instance.isGamePause = false;
        }
        normalModePanel.HidePrizePage();
    }
}
