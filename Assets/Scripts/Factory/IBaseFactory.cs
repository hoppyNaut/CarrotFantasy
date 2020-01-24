/****************************************************
	文件：IBaseFactory.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 10:42   	
	功能：游戏物体工厂接口
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseFactory
{
    GameObject GetItem(string itemName);
    void PushItem(string itemName, GameObject item);
}
