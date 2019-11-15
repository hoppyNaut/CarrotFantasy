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
        factoryManager = new FactoryManager();
        audioManager = new AudioManager() ;
        uiManager = new UIManager();
    }

}
