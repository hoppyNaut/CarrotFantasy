/****************************************************
	文件：MapMaker.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2020/02/03 21:23   	
	功能：关卡地图制作工具类
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;

[System.Serializable]
public class BigLevelItemInfo
{
    public int itemNum_Total;
    //不同大小道具的最大索引号
    public int itemIndex_2x2;
    public int itemIndex_1x2;
    public int itemIndex_1x1;
}

public class MapMaker : MonoBehaviour
{

#if Tool
    public bool isDrawLine;//是否绘制线条
    public GameObject gridGo;

    private static MapMaker _instance;

    public static MapMaker Instance
    {
        get { return _instance; }
    }


#endif
    //地图有关属性
    private float mapWidth;//地图宽度
    private float mapHeight;//地图高度

    [HideInInspector]
    public float gridWidth;//格子宽度
    [HideInInspector]
    public float gridHeight;//格子高度

    public const int yRow = 8;
    public const int xColumn = 12;

    //当前关卡索引
    public int bigLevelID;
    public int levelID;
    //背景图和关卡图
    public int bgSprite;
    public int roadSprite;

    //怪物路点索引列表
    [HideInInspector]
    public List<GridPoint.GridIndex> monsterPathList;
    //怪物路径点位置
    [HideInInspector]
    public List<Vector3> monsterPathPosList;

    //所有格子对象信息
    public GridPoint[,] gridPoints;

    //不同关卡的背景图与路线图
    private SpriteRenderer sr_Bg;
    private SpriteRenderer sr_Road;

    //怪物波次信息列表
    [HideInInspector]
    public List<Round.RoundInfo> roundInfoList;

    [HideInInspector]
    public Carrot carrot;

    //大关卡道具信息
    public List<BigLevelItemInfo> bigLevelInfoItemList;


    private void Awake()
    {

#if Tool
        _instance = this;
#endif

        //Init();

    }

    public BigLevelItemInfo GetCurBigLevelItemInfo()
    {
        return bigLevelInfoItemList[bigLevelID - 1];
    }

    //初始化地图
    public void Init()
    {
        sr_Bg = transform.Find("BG").GetComponent<SpriteRenderer>();
        sr_Road = transform.Find("Road").GetComponent<SpriteRenderer>();
        CalculateSize();
        monsterPathList = new List<GridPoint.GridIndex>();
        monsterPathPosList = new List<Vector3>();
        roundInfoList = new List<Round.RoundInfo>();
        gridPoints = new GridPoint[xColumn, yRow];
        for (int x = 0; x < xColumn; x++)
        {
            for(int y = 0; y < yRow; y++)
            {
#if Tool
                GameObject itemGo = Instantiate(gridGo, transform.position, transform.rotation);
#endif

#if Game
                GameObject itemGo = GameController.Instance.GetGameObjectResource("Grid");
#endif
                itemGo.transform.position = CorrectPosition(new Vector3(x * gridWidth, y * gridHeight, 0));
                itemGo.transform.SetParent(transform);
                //设置格子索引
                itemGo.GetComponent<GridPoint>().SetGridIndex(x, y);
                //将当前格子加入所有格子二维数组
                gridPoints[x, y] = itemGo.GetComponent<GridPoint>();
            }
        }
    }

    private Vector3 CorrectPosition(Vector3 pos)
    {
        return new Vector3(pos.x - mapWidth / 2 + gridWidth / 2,pos.y - mapHeight / 2 + gridHeight / 2, 0);
    }

    //计算地图格子宽高
    private void CalculateSize()
    {
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);
        //视口坐标转化为世界坐标
        leftDown = Camera.main.ViewportToWorldPoint(leftDown);
        rightUp = Camera.main.ViewportToWorldPoint(rightUp);

        mapWidth = rightUp.x - leftDown.x;
        mapHeight = rightUp.y - leftDown.y;

        gridWidth = mapWidth / xColumn;
        gridHeight = mapHeight / yRow;
        //Debug.Log(string.Format("地图宽度:{0} 地图高度:{1} 格子宽度:{2} 格子高度:{3}", mapWidth, mapHeight, gridWidth, gridHeight));
    }

#if Tool
    //绘制格子(辅助设计)
    private void OnDrawGizmos()
    {
        if(isDrawLine)
        {
            CalculateSize();
            Gizmos.color = Color.red;
            //画行
            for(int y = 0; y <= yRow; y++)
            {
                Vector3 startPos = new Vector3(-mapWidth / 2, -mapHeight / 2 + y * gridHeight, -9);
                Vector3 endPos = new Vector3(mapWidth / 2, -mapHeight / 2 + y * gridHeight, -9);
                Gizmos.DrawLine(startPos, endPos);
            }
            //画列
            for(int x = 0; x <= xColumn; x++)
            {
                Vector3 startPos = new Vector3(-mapWidth / 2 + x * gridWidth, -mapHeight / 2, -9);
                Vector3 endPos = new Vector3(-mapWidth / 2 + x * gridWidth, mapHeight / 2, -9);
                Gizmos.DrawLine(startPos,endPos);
            }
        }
    }
#endif

#region 地图编辑器有关方法
    //清除怪物路点
    public void ClearMonsterPath()
    {
        foreach (var monsterPoint in monsterPathList)
        {
            int x = monsterPoint.xIndex;
            int y = monsterPoint.yIndex;
            gridPoints[x, y].InitGrid();
        }
        monsterPathList.Clear();
    }

    //恢复地图编辑器状态
    public void RecoverTowerPoint()
    {
        ClearMonsterPath();
        for(int x = 0; x < xColumn; x++)
            for(int y = 0; y < yRow; y++)
            {
                gridPoints[x, y].InitGrid();
            }
    }

    //初始化地图
    public void InitMap()
    {
        bigLevelID = 0;
        levelID = 0;
        bgSprite = -1;
        roadSprite = -1;
        RecoverTowerPoint();
        roundInfoList.Clear();
        sr_Bg.sprite = null;
        sr_Road.sprite = null;
    }

#if Game
    //加载地图
    public void LoadMap(int bigLevelID,int levelID)
    {
        this.bigLevelID = bigLevelID;
        this.levelID = levelID;
        InitLevelInfo("Level" + bigLevelID.ToString() + "_" + levelID.ToString() + ".json");
        for(int i = 0; i < monsterPathList.Count; i++)
        {
            monsterPathPosList.Add(gridPoints[monsterPathList[i].xIndex, monsterPathList[i].yIndex].transform.position);
        }
        //生成起始点和终止点
        GameObject startPointGo = GameController.Instance.GetGameObjectResource("StartPoint");
        startPointGo.transform.position = monsterPathPosList[0];
        startPointGo.transform.SetParent(this.transform);

        GameObject endPointGo = GameController.Instance.GetGameObjectResource("Carrot");
        endPointGo.transform.position = monsterPathPosList[monsterPathPosList.Count - 1];
        endPointGo.transform.SetParent(this.transform);
    }
#endif

#if Tool
    //生成LevelInfo类来保存文件
    private LevelInfo CreateLevelInfo()
    {
        LevelInfo levelInfo = new LevelInfo
        {
            bigLevelID = bigLevelID,
            levelID = levelID,
            bgSprite = bgSprite,
            roadSprite = roadSprite,
        };
        levelInfo.gridPoints = new List<GridPoint.GridState>();
        for(int x = 0; x < xColumn; x++)
        {
            for(int y = 0; y < yRow; y++)
            {
                levelInfo.gridPoints.Add(gridPoints[x, y].gridState);
            }
        }
        levelInfo.monsterPath = new List<GridPoint.GridIndex>();
        for(int i = 0; i < monsterPathList.Count; i++)
        {
            levelInfo.monsterPath.Add(monsterPathList[i]);
        }
        levelInfo.roundInfo = new List<Round.RoundInfo>();
        for(int i = 0; i < roundInfoList.Count; i++)
        {
            levelInfo.roundInfo.Add(roundInfoList[i]);
        }
        Debug.Log("保存成功");
        return levelInfo;
    }

    //保存当前关卡数据文件
    public void SaveLevelInfoByJson()
    {
        LevelInfo levelInfo = CreateLevelInfo();
        string filePath = Application.dataPath + "/Resources/Json/Level/" + "Level" + bigLevelID.ToString() + "_" + levelID.ToString() + ".json";
        string jsonStr = JsonMapper.ToJson(levelInfo);
        StreamWriter writer = new StreamWriter(filePath);
        writer.Write(jsonStr);
        writer.Close();
    }
#endif

    //读取指定关卡数据文件
    public LevelInfo LoadLevelInfoByJson(string fileName)
    {
        LevelInfo levelInfo = new LevelInfo();
        string filePath = Application.dataPath + "/Resources/Json/Level/" + fileName;
        if(File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            string jsonStr = reader.ReadToEnd();
            reader.Close();

            levelInfo = JsonMapper.ToObject<LevelInfo>(jsonStr);
           
            return levelInfo;  
        }
        else
        {
            Debug.Log("目标文件不存在");
            return null;
        }
    }

    //根据读取的关卡数据文件进行初始化
    public void InitLevelInfo(string fileName)
    {
        LevelInfo levelInfo = LoadLevelInfoByJson(fileName);
        bigLevelID = levelInfo.bigLevelID;
        levelID = levelInfo.levelID;
        bgSprite = levelInfo.bgSprite;
        roadSprite = levelInfo.roadSprite;
        for(int x = 0; x < xColumn; x++)
            for(int y = 0; y < yRow; y++)
            {
                gridPoints[x, y].gridState = levelInfo.gridPoints[y + x * yRow];
                //更新格子状态
#if Tool
                gridPoints[x, y].UpdateGrid();
#endif

#if Game
                gridPoints[x, y].UpdateGrid();
#endif
            }
        monsterPathList.Clear();
        monsterPathList = levelInfo.monsterPath;
        roundInfoList.Clear();
        roundInfoList = levelInfo.roundInfo;

        sr_Bg.sprite = Resources.Load<Sprite>("Sprites/NormalMordel/Game/" + bigLevelID.ToString() + "/" + "BG" + bgSprite.ToString());
        sr_Road.sprite = Resources.Load<Sprite>("Sprites/NormalMordel/Game/" + bigLevelID.ToString() + "/" + "Road" + roadSprite.ToString());
    }
#endregion
}
