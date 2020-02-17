using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int totalRound;
    public Round[] roundList;
    public int curRound;

    public Level(int totalRound,List<Round.RoundInfo> roundList)
    {
        this.totalRound = totalRound;
        this.roundList = new Round[roundList.Count];
        for(int i = 0; i < this.roundList.Length; i++)
        {
            this.roundList[i] = new Round(i, roundList[i], this);
        }
        //设置责任链
        for(int i = 0; i < this.roundList.Length; i++)
        {
            if(i == totalRound - 1)
            {
                break;
            }
            this.roundList[i].SetNextRound(this.roundList[i + 1]);
        }
        curRound = 0;
    }

    public void HandleRound()
    {
        if(curRound >= totalRound)
        {
            //游戏胜利
        }
        else if(curRound == totalRound - 1)
        {
            //最后一波怪
            HandleLastRound();
        }
        else
        {
            roundList[curRound].Handle(curRound);
        }
    }

    //最后一回合的Handle方法
    public void HandleLastRound()
    {
        roundList[curRound].Handle(curRound);
    }

    public void AddRoundNum()
    {
        curRound++;
    }
}
