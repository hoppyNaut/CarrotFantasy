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

}
