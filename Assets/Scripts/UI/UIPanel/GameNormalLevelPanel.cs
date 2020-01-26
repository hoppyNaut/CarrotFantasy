using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNormalLevelPanel : BasePanel
{
    public int bigLevelID;

    //初始化不同大关卡对应的小关卡
    public void Init(int bigLevelID)
    {
        this.bigLevelID = bigLevelID;
        EnterPanel();
    }

}
