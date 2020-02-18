/****************************************************
	文件：Stage.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/24 16:05   	
	功能：关卡信息类（玩家）
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int mBigLevelID;
    public int mLevelID;
    //本关卡允许使用的炮塔id
    public int[] mTowerIDList;
    public int mTowerIDListLength;
    //是否全部清除
    public bool mAllClear;
    //萝卜状态
    public int mCarrotState;
    //关卡是否已解锁
    public bool mUnLocked;
    //是否是隐藏关卡
    public bool mIsHiddenLevel;
    //关卡怪物波次
    public int mTotalWaves;

    public Stage() { }

    public Stage(int totalWaves,int towerIDListLength,int [] towerIDList,bool allClear,
        int carrotState,int levelID,int bigLevelID,bool unLocked,bool isHiddenLevel)
    {
        mTotalWaves = totalWaves;
        mTowerIDListLength = towerIDListLength;
        mTowerIDList = towerIDList;
        mAllClear = allClear;
        mCarrotState = carrotState;
        mLevelID = levelID;
        mBigLevelID = bigLevelID;
        mUnLocked = unLocked;
        mIsHiddenLevel = isHiddenLevel;
    }
}
