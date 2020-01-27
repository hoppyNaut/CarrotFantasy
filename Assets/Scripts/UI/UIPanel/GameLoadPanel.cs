/****************************************************
	文件：GameLoadPanel.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/01/27 15:40   	
	功能：游戏加载面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadPanel : BasePanel
{
    protected override void Awake()
    {
        gameObject.SetActive(false);
        base.Awake();
    }


    public override void EnterPanel()
    {
        base.EnterPanel();
        gameObject.SetActive(true);
        transform.SetSiblingIndex(8);
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

}
