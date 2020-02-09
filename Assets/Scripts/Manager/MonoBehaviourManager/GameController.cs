/****************************************************
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

    //建塔价格表
    public Dictionary<int, int> towerPriceDict;
    //建塔按钮列表
    public List<GameObject> towerList;
    //处理塔相关操作的画布
    public GameObject handleTowerCanvas;


    private void Awake()
    {
#if Game
        _instance = this;
        gameManager = GameManager.Instance;
        curStage = gameManager.curStage;
        normalModePanel = gameManager.uiManager.mUIFacade.GetCurScenePanel(Constant.NormalModePanel) as NormalModePanel;
        mapMaker = transform.GetComponent<MapMaker>();
        mapMaker.Init();
        mapMaker.LoadMap(curStage.mBigLevelID,curStage.mLevelID);
        monsterBuilder = new MonsterBuilder();

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
        coin = 0;
        gameSpeed = 1;
        isGamePause = false;
        creatingMonster = false;
        isGameOver = false;


        curLevel = new Level(mapMaker.roundInfoList.Count, mapMaker.roundInfoList);
        curLevel.HandleRound();
#endif
    }

    private void Update()
    {
#if Game
        Debug.Log(mapMaker.monsterPathPosList.Count);
        if (!isGamePause)
        {
            //产怪
            if(curRoundKillMonsterNum >= monsterIDList.Length)
            {
                //当前回合数加一
                AddRoundNum();
            }
            else
            {
                if(!creatingMonster)
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

    #region 建造者方法
    public void CreateMonster()
    {
        creatingMonster = true;
        InvokeRepeating("InstantiateMonster", 1.0f/gameSpeed,1.0f/gameSpeed);
    }

    private void InstantiateMonster()
    {
       
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
            if(monsterIDIndex >= monsterIDList.Length)
            {
                StopCreateMonster();
            }
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
}
