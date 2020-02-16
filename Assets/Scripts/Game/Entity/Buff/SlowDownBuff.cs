/****************************************************
	文件：SlowDownBuff.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/16 12:58   	
	功能：减速Buff
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBuff : BaseBuff
{
    public SlowDownBuff(Monster monster, BuffType buffType, float buffDuration, float buffValue) : base(monster, buffType, buffDuration, buffValue)
    {
    }

    public override void OnAdd()
    {
        base.OnAdd();
        monster.SlowDown(this);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(monster.isSlowDown)
        {
            //Debug.Log(buffTimer);
            buffTimer += Time.deltaTime;
            if(buffTimer >= buffDuration)
            {
                monster.CancelSlowDown();
                monster.RemoveBuff(this);                
            }
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    public override void OnInit()
    {
        base.OnInit();
    }
}
