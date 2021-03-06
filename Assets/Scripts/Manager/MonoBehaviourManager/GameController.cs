﻿/****************************************************
	文件：GameController.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:34   	
	功能：游戏控制管理，负责控制游戏的整个逻辑
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance
    {
        get { return _instance; }
    }

    private GameManager gameManager;

    public Level curLevel;
    public Stage curStage;
    public NormalModePanel normalModePanel;
    public MapMaker mapMaker;


    //当前波次产怪列表
    public int[] monsterIDList;
    //怪物序号
    public int monsterIDIndex;

    //怪物动画控制器列表
    public RuntimeAnimatorController[] animatorControllerList;

    //当前波次杀怪数
    public int curRoundKillMonsterNum;
    //杀怪总数
    public int totalKillMonsterNum;
    //销毁道具数量
    public int destroyItemNum;

    public int carrotHp;
    public int coin;
    //当前游戏倍速
    public int gameSpeed;
    public bool isGamePause;
    //集火目标
    public Transform targetTrans;
    //集火标志
    public GameObject targetSignal;
    //当前选择的格子
    public GridPoint selectGrid;
    //是否继续产怪
    public bool creatingMonster;
    public bool isGameOver;

    //建造者
    public MonsterBuilder monsterBuilder;
    public TowerBuilder towerBuilder;

    //建塔价格表
    public Dictionary<int, int> towerPriceDict;
    //建塔按钮列表
    public GameObject towerList;
    //处理塔相关操作的画布
    public GameObject handleTowerCanvas;


    private void Awake()
    {
#if Game
        _instance = this;
        gameManager = GameManager.Instance;
        curStage = gameManager.curStage;
        normalModePanel = gameManager.uiManager.mUIFacade.GetCurScenePanel(Constant.NormalModePanel) as NormalModePanel;
        normalModePanel.EnterPanel();

        mapMaker = transform.GetComponent<MapMaker>();
        mapMaker.Init();
        mapMaker.LoadMap(curStage.mBigLevelID,curStage.mLevelID);
        monsterBuilder = new MonsterBuilder();
        towerBuilder = new TowerBuilder();

        animatorControllerList = new RuntimeAnimatorController[12];
        for(int i = 0; i < animatorControllerList.Length; i++)
        {
            animatorControllerList[i] = GetRuntimeAnimatorController("AnimatorController/Monster/" + curStage.mBigLevelID.ToString() + "/" + (i +1).ToString());
        }

        //初始化属性
        curRoundKillMonsterNum = 0;
        totalKillMonsterNum = 0;
        destroyItemNum = 0;
        carrotHp = 10;
        coin = 500;
        gameSpeed = 1;
        isGamePause = false;
        creatingMonster = false;
        isGameOver = false;



        //建塔价格初始化
        towerPriceDict = new Dictionary<int, int>
        {
            { 1,100 },
            { 2,120 },
            { 3,140 },
            { 4,160 },
            { 5,160 }
        };

        //建塔列表处理

        for (int i = 0; i < curStage.mTowerIDList.Length; i++)
        {
            GameObject towerGo = gameManager.GetGameObjectResource(FactoryType.UIFactory, "Btn_BuildTower");
            towerGo.GetComponent<BtnTower>().towerID = curStage.mTowerIDList[i];
            towerGo.GetComponent<BtnTower>().price = towerPriceDict[curStage.mTowerIDList[i]];
            towerGo.transform.SetParent(towerList.transform);
            towerGo.transform.localPosition = Vector3.zero;
            towerGo.transform.localScale = Vector3.one;
        }
     
        curLevel = new Level(mapMaker.roundInfoList.Count, mapMaker.roundInfoList);
        isGamePause = true;

        normalModePanel.topPage.UpdateCoinText();
        normalModePanel.topPage.UpdateRoundText();
#endif
    }

    private void Update()
    {
#if Game

        if (!isGamePause)
        {
            //产怪
            if(curRoundKillMonsterNum >= monsterIDList.Length)
            {
                if(curLevel.curRound >= curLevel.totalRound)
                {
                    return;
                }
                //当前回合数加一
                AddRoundNum();
            }
            else
            {
                if (!creatingMonster)
                {
                    CreateMonster();
                }
            }
        }
        else
        {
            //暂停
            StopCreateMonster();
            creatingMonster = false;
        }
#endif
    }

    #region 游戏进程控制方法
    public void StartGame()
    {
        isGamePause = false;
        curLevel.HandleRound();
    }

    public void HandleLastRound()
    {

    }

    #endregion

    #region 集火目标处理方法
    public void ShowSignal()
    {
        PlayEffectMusic("NormalMordel/Tower/ShootSelect");
        targetSignal.transform.position = targetTrans.position + new Vector3(0,mapMaker.gridHeight / 2 ,0);
        targetSignal.transform.SetParent(targetTrans);
        targetSignal.SetActive(true);
    }

    public void HideSignal()
    {
        targetSignal.SetActive(false);
        targetTrans = null;
    }

    public void SetTargetTrans(Transform trans)
    {

    }
    #endregion

    #region 数据处理方法
    public void AddKillCount()
    {
        curRoundKillMonsterNum++;
        totalKillMonsterNum++;
        //更新UI显示TODO
    }

    public void AddDestroyItemCount()
    {
        destroyItemNum++;
    }

    public void AddCoin(int prize)
    {
        coin += prize;
        //更新UI显示
        normalModePanel.topPage.UpdateCoinText();
    }

    public void DecreaseCarrotHp()
    {
        PlayEffectMusic("NormalMordel/Carrot/Crash");
        carrotHp -= 1;
        mapMaker.carrot.SetHp(carrotHp);
    }

    public bool IsClearAllItem()
    {
        for(int x = 0; x < MapMaker.xColumn; x++)
        {
            for(int y = 0; y < MapMaker.yRow; y++)
            {
                if(mapMaker.gridPoints[x,y].gridState.hasItem == true)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int GetCarrotState()
    {
        int carrotState = 0;
        if(carrotHp >= 10)
        {
            carrotState = 1;
        }
        else if(carrotHp >= 5)
        {
            carrotState = 2;
        }
        else if(carrotHp >= 1)
        {
            carrotState = 3;
        }
        return carrotState;
    }
    #endregion

    #region 建造者方法
    public void CreateMonster()
    {
        creatingMonster = true;
        InvokeRepeating("InstantiateMonster", 1.0f/gameSpeed,1.0f/gameSpeed);
    }

    private void InstantiateMonster()
    {
        PlayEffectMusic("NormalMordel/Monster/Create");
        if (monsterIDIndex >= monsterIDList.Length)
        {
            StopCreateMonster();
            return;
        }
        //产生特效
        GameObject effectGo = GetGameObjectResource("CreateEffect");
        effectGo.transform.SetParent(transform);
        effectGo.transform.position = mapMaker.monsterPathPosList[0];
        //产生怪物
        if(monsterIDIndex < monsterIDList.Length)
        {
            monsterBuilder.monsterID = monsterIDList[monsterIDIndex];
            GameObject monsterGo = monsterBuilder.GetProduct();
            monsterGo.transform.SetParent(transform);
            monsterGo.transform.position = mapMaker.monsterPathPosList[0];
            monsterIDIndex++;
            
        }
    }

    public void StopCreateMonster()
    {
        CancelInvoke();
    }

    //增加当前回合数并执行下一个关卡
    public void AddRoundNum()
    {
        monsterIDIndex = 0;
        curRoundKillMonsterNum = 0;
        curLevel.AddRoundNum();
        curLevel.HandleRound();
        //更新UI
        normalModePanel.topPage.UpdateRoundText();
    }
    #endregion

    #region 资源获取有关方法
    public Sprite GetSprite(string path)
    {
        return gameManager.GetSprite(path);
    }

    public AudioClip GetAudioClip(string path)
    {
        return gameManager.GetAudioClip(path);
    }

    public RuntimeAnimatorController GetRuntimeAnimatorController(string path)
    {
        return gameManager.GetRuntimeAnimatorController(path);
    }

    public GameObject GetGameObjectResource( string name)
    {
        return gameManager.GetGameObjectResource(FactoryType.GameFactory, name);
    }

    public void PushGameObjectToFactory(string name, GameObject go)
    {
        gameManager.PushGameObjectToFactory(FactoryType.GameFactory, name, go);
    }
    #endregion

    public void PlayEffectMusic(string audioClip)
    {
        gameManager.audioManager.PlayEffectMusic(GetAudioClip(audioClip));
    }
}
