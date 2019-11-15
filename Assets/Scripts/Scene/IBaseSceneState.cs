/****************************************************
	文件：IBaseSceneState.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/15 15:53   	
	功能：场景状态接口
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseSceneState
{
    //进入场景处理
    void OnEnterScene();
    //退出场景处理
    void OnExitScene();
}
