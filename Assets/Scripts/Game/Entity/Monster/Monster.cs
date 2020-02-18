using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    //怪物属性：
    public int monsterID;
    //总血量
    public int hp;
    //当前血量
    private int curHp;
    public int CurHp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            //更新血条
            UpdateHpSlider();
        }
    }
    //当前速度
    public float moveSpeed;
    //初始速度
    public float initMoveSpeed;
    //运动方向
    private Vector3 moveDirection;
    //奖励
    public int prize;

    private GameController gameController;

    private Animator animator;
    private Slider hpSlider;
    private GameObject shitGo;

    public RuntimeAnimatorController runtimeAnimatorController;
    public AudioClip dieAudipClip;

    private int targetPosIndex ;
    //是否到达终点
    private bool isReachCarrot;

    //是否被减速
    public bool isSlowDown;

    //buff列表
    public List<BaseBuff> buffList;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        hpSlider = transform.Find("Canvas").Find("HpSlider").GetComponent<Slider>();
        hpSlider.gameObject.SetActive(false);
        shitGo = transform.Find("Tshit").gameObject;
        shitGo.SetActive(false);
        gameController = GameController.Instance;
        targetPosIndex = 1;
        isReachCarrot = false;
        isSlowDown = false;
        buffList = new List<BaseBuff>();
    }

    private void OnEnable()
    {
        gameController = GameController.Instance;
        //设置初始朝向
        moveDirection = gameController.mapMaker.monsterPathPosList[1] - gameController.mapMaker.monsterPathPosList[0];
        if (moveDirection.x > 0 )
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (moveDirection.x < 0 )
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        if (gameController.isGamePause || gameController.isGameOver)
        {
            return;
        }

        //更新Buff
        RefreshBuff();

        if(!isReachCarrot)
        {
            Vector3 targetPos = gameController.mapMaker.monsterPathPosList[targetPosIndex];
            transform.position = Vector3.Lerp(transform.position, targetPos, (1/Vector3.Distance(transform.position,targetPos)) *Time.deltaTime * moveSpeed * gameController.gameSpeed);
            if(Mathf.Abs(Vector3.Distance(transform.position,targetPos)) <= 0.01f)
            {
                targetPosIndex++;
                
                if(targetPosIndex >= gameController.mapMaker.monsterPathPosList.Count)
                {
                    isReachCarrot = true;
                }
                else
                {
                    SetMonsterDirection();
                }
            }
        }
        else
        {
            //销毁怪物
            DestroyMonster();
            //萝卜扣血
            gameController.DecreaseCarrotHp();
        }
    }

    #region 怪物处理
    //初始化怪物
    private void InitMonster()
    {
        monsterID = 0;
        hp = 0;
        curHp = 0;
        moveSpeed = 0;
        initMoveSpeed = 0;
        moveDirection = Vector3.zero;
        prize = 0;
        targetPosIndex = 1;
        isReachCarrot = false;
        hpSlider.value = 1;
        hpSlider.gameObject.SetActive(false);
        isSlowDown = false;
        shitGo.SetActive(false);
        buffList.Clear();
    }

    //销毁怪物处理
    private void DestroyMonster()
    {
        if(gameController.targetTrans == this.transform)
        {
            gameController.HideSignal();
        }

        if(!isReachCarrot)//被玩家击杀 
        {
            //生成金币
            GameObject coin = gameController.GetGameObjectResource("CoinCanvas");
            coin.transform.Find("Emp_Coin").GetComponent<CoinMove>().ShowCoin(prize);
            coin.transform.SetParent(gameController.transform);
            coin.transform.position = transform.position;
            //增加玩家的金币
            gameController.AddCoin(prize);
            //奖品随机掉落
            int randomNum = Random.Range(0, 100);
            if(randomNum < 10)
            {
                GameObject prizeGo = gameController.GetGameObjectResource("Prize");
                prizeGo.transform.position = transform.position;
            }
        }
        //产生销毁特效
        GameObject deathEffect = gameController.GetGameObjectResource("DestroyEffect");
        deathEffect.transform.SetParent(gameController.transform);
        deathEffect.transform.position = transform.position;

        gameController.AddKillCount();
        InitMonster();
        gameController.PushGameObjectToFactory("Monster", this.gameObject);
    }

    //怪物受到伤害处理
    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        if(CurHp <= 0)
        {
            //怪物死亡处理
            DestroyMonster();
            return;
        }
    }

    //设置不同怪物的动画状态机
    public void GetMonsterAnimatorController()
    {
        runtimeAnimatorController = gameController.animatorControllerList[monsterID - 1];
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    private void UpdateHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.value = curHp * 1.0f / hp;
    }

    private Vector3 CalculateMoveDir()
    {
        return gameController.mapMaker.monsterPathPosList[targetPosIndex] - transform.position;
    }

    //设置怪物朝向
    private void SetMonsterDirection()
    {
        moveDirection = CalculateMoveDir();
        if(moveDirection.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(moveDirection.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    #endregion

    #region 怪物Buff处理
    public void AddBuff(BaseBuff buff)
    {
        bool hasSameBuff = false;
        int index = -1;
        //遍历buff列表寻找是否已有相同类型的buff
        for(int i = 0; i < buffList.Count; i++)
        {
            if(buffList[i].buffType == buff.buffType)
            {
                hasSameBuff = true;
                index = i;
                break;
            }
        }

        //如果没有相同buff,就添加
        if(!hasSameBuff)
        {
            buffList.Add(buff);
            buff.OnAdd();
        }
        //如果有了,重置已有的buff
        else
        {
            buffList[index].OnInit();
        }
    }

    public void RefreshBuff()
    {
        for(int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].OnUpdate();
        }
    }

    public void RemoveBuff(BaseBuff buff)
    {
        buffList.Remove(buff);
        buff.OnRemove();
    }

    public void SlowDown(BaseBuff buff)
    {
        if(!isSlowDown)
        {
            moveSpeed -= buff.buffValue;
            shitGo.SetActive(true);
        }
        isSlowDown = true;
    }

    public void CancelSlowDown()
    {
        isSlowDown = false;
        moveSpeed = initMoveSpeed;
        shitGo.SetActive(false);
    }
    #endregion

    private void OnMouseDown()
    {
        Debug.Log("点击到了怪物");
        if(gameController.targetTrans == null)
        {
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //转换新目标
        else if(gameController.targetTrans != transform)
        {
            gameController.HideSignal();
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //两次点击同一个目标
        else if(gameController.targetTrans == transform)
        {
            gameController.HideSignal();
        }
    }
}
