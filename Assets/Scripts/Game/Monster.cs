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
    public int curHp;
    //当前速度
    public float moveSpeed;
    //初始速度
    public float initMoveSpeed;
    //奖励
    public int prize;

    private Animator animator;
    private Slider hpSlider;
    private GameObject shitGo;

    public RuntimeAnimatorController runtimeAnimatorController;
    public AudioClip dieAudipClip;

    private int targetPosIndex ;
    //是否到达终点
    private bool isReachCarrot;
    //是否被减速
    private bool hasDecreaseSpeed;
    //减速计时器
    private float decreaseSpeedTimer;
    //减速持续时间
    private float decreaseSpeedTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hpSlider = transform.Find("Canvas").Find("HpSlider").GetComponent<Slider>();
        hpSlider.gameObject.SetActive(false);
        targetPosIndex = 1;
        isReachCarrot = false;
        hasDecreaseSpeed = false;
    }

    private void Update()
    {
        if (GameController.Instance.isGamePause)
        {
            return;
        }
        if(!isReachCarrot)
        {
            transform.position = Vector3.Lerp(transform.position, GameController.Instance.mapMaker.monsterPathPosList[targetPosIndex], 0.1f *Time.deltaTime * moveSpeed * GameController.Instance.gameSpeed);

        }
        else
        {
            //销毁怪物
            DestroyMonster();
            //萝卜扣血TODO 
        }
    }

    private void DestroyMonster()
    {
        gameObject.SetActive(false);
    }

    //设置不同怪物的动画状态机
    public void GetMonsterAnimatorController()
    {
        runtimeAnimatorController = GameController.Instance.animatorControllerList[monsterID - 1];
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }
}
