/****************************************************
	文件：GameManager.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:32   	
	功能：游戏总管理者，管理所有的子管理者
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public FactoryManager factoryManager;
    public AudioManager audioManager;
    public UIManager uiManager;

    public Stage curStage;

    public bool initPlayerManager;//是否重置playerManager

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        playerManager = new PlayerManager();
        //playerManager.SaveData();
        playerManager.ReadData();
        factoryManager = new FactoryManager();
        audioManager = new AudioManager() ;
        uiManager = new UIManager();
        //进入开始游戏场景
        uiManager.mUIFacade.currentSceneState.OnEnterScene();
    }

    //获取精灵资源
    public Sprite GetSprite(string path)
    {
        return factoryManager.spriteFactory.GetSingleResource(path);
    }

    //获取AudioClip资源
    public AudioClip GetAudioClip(string path)
    {
        return factoryManager.audioClipFactory.GetSingleResource(path);
    }

    //获取RuntimeAnimatorController资源
    public RuntimeAnimatorController GetRuntimeAnimatorController(string path)
    {
        return factoryManager.runtimeAnimatorControllerFactory.GetSingleResource(path);
    }

    //获取游戏物体资源
    public GameObject GetGameObjectResource(FactoryType factoryType,string name)
    {
        return factoryManager.factoryDict[factoryType].GetItem(name);
    }

    //回收游戏物体到对象池
    public void PushGameObjectToFactory(FactoryType factoryType,string name,GameObject go)
    {
        factoryManager.factoryDict[factoryType].PushItem(name, go);
    }

}
