/****************************************************
	文件：PlayerManager.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:40   	
	功能：玩家的管理，负责保存以及加载玩家信息
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager 
{
    //冒险模式解锁地图个数
    public int adventrueModeNum;
    //隐藏关卡解锁地图个数
    public int burriedLevelNum;
    //Boss模式解锁地图个数
    public int bossModeNum;
    //金币数
    public int coin;
    //击败怪物总数
    public int killMonsterNum;
    //击败Boss总数
    public int killBossNum;
    //清除道具总数
    public int ClearItemNum;


    //怪物窝
    public int cookies;
    public int milk;
    public int nest;
    public int diamonds;

    //大关卡数量
    public int bigLevelNum;
    //炮塔数量
    public int towerNum;
    //大关卡是否解锁信息列表
    public List<bool> unLockedNormalModeBigLevelList;
    //小关卡信息列表
    public List<Stage> NormalModeLevelInfoList;
    //大关卡对应解锁小关卡数量列表
    public List<int> unLockedNormalModeLevelNumList;
    //大关卡对应所有小关卡数量
    public List<int> totalNormalModeLevelNumList;

    public PlayerManager()
    {
        adventrueModeNum = 100;
        burriedLevelNum = 100;
        bossModeNum = 100;
        coin = 1000;
        killMonsterNum = 100;
        killBossNum = 100;
        ClearItemNum = 100;

        bigLevelNum = 3;
        towerNum = 12;

        unLockedNormalModeBigLevelList = new List<bool>()
        {
            true,true,true
        };
        unLockedNormalModeLevelNumList = new List<int>()
        {
            4,4,4
        };
        totalNormalModeLevelNumList = new List<int>()
        {
            5,5,5
        };
        NormalModeLevelInfoList = new List<Stage>()
        {
            new Stage(10,2,new int[]{ 1,2},false,0,1,1,true,false),
            new Stage(10,2,new int[]{ 3,2},false,0,2,1,true,false),
            new Stage(10,2,new int[]{ 4,2},false,0,3,1,true,false),
            new Stage(10,2,new int[]{ 5,2},false,0,4,1,true,false),
            new Stage(10,2,new int[]{ 6,2},false,0,5,1,false,true),
            new Stage(10,3,new int[]{ 7,2,1},false,0,1,2,true,false),
            new Stage(10,2,new int[]{ 8,2},false,0,2,2,true,false),
            new Stage(10,2,new int[]{ 9,2},false,0,3,2,true,false),
            new Stage(10,2,new int[]{ 10,2},false,0,4,2,true,false),
            new Stage(10,2,new int[]{ 11,2},false,0,5,2,false,false),
            new Stage(10,3,new int[]{ 7,2,1},false,0,1,3,true,false),
            new Stage(10,2,new int[]{ 8,2},false,0,2,3,true,false),
            new Stage(10,2,new int[]{ 9,2},false,0,3,3,true,false),
            new Stage(10,2,new int[]{ 10,2},false,0,4,3,true,false),
            new Stage(10,2,new int[]{ 11,2},false,0,5,3,false,false),
        };



    }

}
