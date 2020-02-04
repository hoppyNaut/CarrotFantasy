/****************************************************
	文件：LevelInfo.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/03 21:02   	
	功能：关卡战斗配置信息
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int bigLevelID;
    public int levelID;

    public int bgSprite;
    public int roadSprite;

    public List<GridPoint.GridState> gridPoints;

    public List<GridPoint.GridIndex> monsterPath;

    public List<Round.RoundInfo> roundInfo;
}
