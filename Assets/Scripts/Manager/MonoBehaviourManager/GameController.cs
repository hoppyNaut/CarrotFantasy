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

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance
    {
        get { return _instance; }
    }

    public Level curLevel;
    public Stage curStage;
    public NormalModePanel normalModePanel;
    public MapMaker mapMaker;

    private GameManager gameManager;

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
#endif
    }


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
