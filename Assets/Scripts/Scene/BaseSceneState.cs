/****************************************************
	文件：BaseSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 15:57   	
	功能：实现了场景状态接口的基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneState : IBaseSceneState
{
    public virtual void OnEnterScene()
    {

    }

    public virtual void OnExitScene()
    {

    }
}
