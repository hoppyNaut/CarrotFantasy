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

    public RoundInfo roundInfo;
}
