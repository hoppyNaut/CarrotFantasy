/****************************************************
	文件：BaseBuff.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/16 12:58   	
	功能：Buff基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType
{
    None,
    SlowDownBuff,
}

public abstract class BaseBuff 
{
    //buff类型
    public BuffType buffType;
    //buff计时器
    public float buffTimer;
    //buff持续时间
    public float buffDuration;
    public float buffValue;
    //所属实体
    public Monster monster;

    public BaseBuff(Monster monster,BuffType buffType,float buffDuration,float buffValue)
    {
        this.monster = monster;
        this.buffType = buffType;
        this.buffDuration = buffDuration;
        this.buffValue = buffValue;
        this.buffTimer = 0;
    }

    /// <summary>
    /// 当Buff添加到实体时执行
    /// </summary>
    public virtual void OnAdd()
    {
        //Debug.Log("获得Buff：" + buffType.ToString());
    }

    /// <summary>
    /// Buff跟随实体每帧更新
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    /// <summary>
    /// Buff从实体中移除时执行
    /// </summary>
    public virtual void OnRemove()
    {
        //Debug.Log("移除Buff：" + buffType.ToString());
    }

    /// <summary>
    /// 初始化Buff
    /// </summary>
    public virtual void OnInit()
    {
        this.buffTimer = 0;
    }
}
