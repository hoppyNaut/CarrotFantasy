/****************************************************
	文件：Round.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/03 20:12   	
	功能：怪物波次信息类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    [System.Serializable]
    public struct RoundInfo
    {
        public int[] mMonsterIDList;
    }

    protected int roundID;
    protected Round nextRound;
    //当前怪物波次所对应的关卡
    protected Level level;
    public RoundInfo roundInfo;
    

    public Round(int roundID, RoundInfo roundInfo,Level level)
    {
        this.roundID = roundID;
        this.level = level;
        this.roundInfo = roundInfo;
    }

    public Round SetNextRound(Round nextRound)
    {
        this.nextRound = nextRound;
        return this.nextRound;
    }

    public void Handle(int curRoundID)
    {
        if(roundID < curRoundID)
        {
            nextRound.Handle(curRoundID);
            level.AddRoundNum();
        }
        else
        {
            //产生怪物
            GameController.Instance.monsterIDList = roundInfo.mMonsterIDList;
            GameController.Instance.CreateMonster();
            //GameController.Instance.creatingMonster = true;
        }

    }
}
