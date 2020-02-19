using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //所属的格子
    public GridPoint gridPoint;
    public int itemID;

    public int hp;
    private int curHp;
    public int CurHp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            //更新血条
#if Game
            UpdateHpSlider();
#endif
        }
    }
    //销毁时金币奖励
    public int prize;

    private Slider hpSlider;

    private GameController gameController;
    //隐藏血条计时器
    private float hpHideTimer;
    private bool showHp;

    private void OnEnable()
    {
        if(hpSlider == null)
        {
            return;
        }
#if Game
        InitItem();
#endif
    }

    private void Start()
    {
#if Tool
        GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("Mask").GetComponent<BoxCollider>().enabled = false;
#endif
        gameController = GameController.Instance;
        hpSlider = transform.Find("ItemCanvas").Find("HpSlider").GetComponent<Slider>();
        hpSlider.gameObject.SetActive(false);
#if Game
        InitItem();
#endif
     }

    private void Update()
    {
#if Game
        if(gameController.isGamePause)
        {
            return;
        }

        if(showHp)
        {
            hpHideTimer -= Time.deltaTime;
            if(hpHideTimer <= 0)
            {
                hpSlider.gameObject.SetActive(false);
                showHp = false;
                hpHideTimer = 3;
            }
        }
#endif
    }

#if Game
    private void InitItem()
    {
        gameController = GameController.Instance;
        prize = 1000 - 100 * itemID;
        hp = 1500 - 100 * itemID;
        CurHp = hp;
        hpHideTimer = 3;
        showHp = false;
    }

    private void UpdateHpSlider()
    {
        showHp = true;
        hpHideTimer = 3;
        hpSlider.gameObject.SetActive(true);
        hpSlider.value = curHp * 1.0f / hp;
    }

    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        if (CurHp <= 0)
        {
            //道具销毁处理
            DestroyItem();
            return;
        }
    }

    private void DestroyItem()
    {
        gameController.PlayEffectMusic("NormalMordel/Item");
        //产生销毁特效
        GameObject deathEffect = gameController.GetGameObjectResource("DestroyEffect");
        deathEffect.transform.SetParent(gameController.transform);
        deathEffect.transform.position = transform.position;

        //生成金币
        GameObject coin = gameController.GetGameObjectResource("CoinCanvas");
        coin.transform.Find("Emp_Coin").GetComponent<CoinMove>().ShowCoin(prize);
        coin.transform.SetParent(gameController.transform);
        coin.transform.position = transform.position;
        gameController.AddCoin(prize);

        gameController.AddDestroyItemCount();

        gameController.PushGameObjectToFactory(gameController.mapMaker.bigLevelID.ToString() + "/Item/" + itemID.ToString(), gameObject);

        gridPoint.gridState.hasItem = false;
        gridPoint.gridState.itemID = -1;

        InitItem();
    }

    private void OnMouseDown()
    {
        //如果点击的是UI不交互
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("点击到了UI");
            return;
        }

        if (gameController.targetTrans == null)
        {
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //转换新目标
        else if (gameController.targetTrans != transform)
        {
            gameController.HideSignal();
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //两次点击同一个目标
        else if (gameController.targetTrans == transform)
        {
            gameController.HideSignal();
        }
    }
#endif
}
