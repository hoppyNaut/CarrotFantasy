using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public bool isDrawLine;//是否绘制线条
    public GameObject gridGo;

    //地图有关属性
    private float mapWidth;//地图宽度
    private float mapHeight;//地图高度

    private float gridWidth;//格子宽度
    private float gridHeight;//格子高度

    public const int yRow = 8;
    public const int xColumn = 12;

    private static MapMaker _instance;

    public static MapMaker Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        InitMap();
    }

    //初始化地图
    public void InitMap()
    {
        CalculateSize();
        for(int x = 0; x < xColumn; x++)
        {
            for(int y = 0; y < yRow; y++)
            {
                GameObject itemGo = Instantiate(gridGo, transform.position, transform.rotation);
                itemGo.transform.position = CorrectPosition(new Vector3(x * gridWidth,  y * gridHeight , 0));
                itemGo.transform.SetParent(transform);
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
        Debug.Log(string.Format("地图宽度:{0} 地图高度:{1} 格子宽度:{2} 格子高度:{3}", mapWidth, mapHeight, gridWidth, gridHeight));
    }

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
}
