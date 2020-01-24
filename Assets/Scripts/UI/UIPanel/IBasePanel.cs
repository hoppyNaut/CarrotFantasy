using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasePanel
{
    //初始化面板
    void InitPanel();
    //进入面板
    void EnterPanel();
    //更新面板
    void UpdatePanel();
    //退出面板
    void ExitPanel();
}
