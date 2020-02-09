using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LitJson;

//格子种类
public enum GridType {
    Normal,     //普通格
    Tower,  //建塔格
    Item,       //道具格
    MonsterPath, //怪物路径格
}




public class GridPoint : MonoBehaviour
{
    //格子状态
    public struct GridState
    {
        public bool canBuild;       //是否能建塔
        public bool isMonsterPoint;     //是否是怪物路径
        public bool hasItem;    //是否有道具
        //public GridType gridType;
        public int itemID;      //道具ID 
    }

    //格子索引 
    [System.Serializable]
    public struct GridIndex
    {
        public int xIndex;
        public int yIndex;
    }

    private SpriteRenderer spriteRenderer;

    private Sprite gridSprite;
    public GridState gridState;
    public GridIndex gridIndex;

#if Tool
    private Sprite monsterPathSprite;

    public GameObject[] itemPrefabs;    //道具游戏物体数组
    public GameObject curItem;  //当前格子持有道具
#endif

    private void Awake()
    {
#if Tool

        gridSprite = Resources.Load<Sprite>("Sprites/NormalMordel/Game/Grid");
        monsterPathSprite = Resources.Load<Sprite>("Sprites/NormalMordel/Game/monsterPath");
        int itemNum = MapMaker.Instance.bigLevelInfoItemList[MapMaker.Instance.bigLevelID - 1].itemNum_Total;
        itemPrefabs = new GameObject[itemNum];
        string prefabPath = "Prefabs/Game/" + MapMaker.Instance.bigLevelID.ToString() + "/Items/";
        for(int i = 0; i < itemPrefabs.Length; i++)
        {
            itemPrefabs[i] = Resources.Load<GameObject>(prefabPath + i.ToString());
            if(!itemPrefabs[i])
            {
                Debug.Log("加载失败,失败路径：" + prefabPath + i.ToString());
            }
        }

#endif

        spriteRenderer = GetComponent<SpriteRenderer>();
        //初始化格子状态
        InitGrid();
    }

    public void InitGrid()
    {
        spriteRenderer.enabled = true;
        gridState.canBuild = true;
        gridState.isMonsterPoint = false;
        gridState.hasItem = false;
        gridState.itemID = -1;

#if Tool

        spriteRenderer.sprite = gridSprite;
        Destroy(curItem);

#endif
        }

    public void SetGridIndex(int x,int y)
    {
        gridIndex.xIndex = x;
        gridIndex.yIndex = y;
    }

#if Game
    //更新格子状态
    public void UpdateGrid()
    {
        if(gridState.canBuild)
        {
            spriteRenderer.enabled = true;
            if(gridState.hasItem)
            {
                CreateItem();
            }
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    //创建道具
    private void CreateItem()
    {
        GameObject itemGo = GameController.Instance.GetGameObjectResource
            (GameController.Instance.mapMaker.bigLevelID.ToString() + "/Items/" + gridState.itemID.ToString());
        itemGo.transform.SetParent(GameController.Instance.transform);

        Vector3 createPos = transform.position - new Vector3(0, 0, 3);
        BigLevelItemInfo curItemInfo = GameController.Instance.mapMaker.GetCurBigLevelItemInfo();

        if (gridState.itemID >= 0 && gridState.itemID <= curItemInfo.itemIndex_2x2)
        {
            createPos += new Vector3(GameController.Instance.mapMaker.gridWidth / 2, -GameController.Instance.mapMaker.gridHeight / 2);
        }
        else if (gridState.itemID > curItemInfo.itemIndex_2x2 && gridState.itemID <= curItemInfo.itemIndex_1x2)
        {
            createPos += new Vector3(GameController.Instance.mapMaker.gridWidth / 2, 0);
        }
        itemGo.transform.position = createPos;
        itemGo.GetComponent<Item>().gridPoint = this;
    }
#endif


#if Tool
    //OnMouseXXXX类型API只作用于鼠标左键
    private void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.P))
        {      
            spriteRenderer.enabled = true;
            gridState.canBuild = false;
            //点击一下设置,再点一下取消
            gridState.isMonsterPoint = !gridState.isMonsterPoint;
            gridState.hasItem = false;
            gridState.itemID = -1;

            if (gridState.isMonsterPoint)        //是怪物路点
            {
                Debug.Log("创建怪物路点");
                //改变显示
                spriteRenderer.sprite = monsterPathSprite;
                //加入怪物路点列表
                MapMaker.Instance.monsterPathList.Add(gridIndex);
            }
            else        //不是怪物路点
            {
                Debug.Log("取消怪物路点");
                spriteRenderer.sprite = gridSprite;
                //从怪物路点列表中移除
                MapMaker.Instance.monsterPathList.Remove(gridIndex);
                gridState.canBuild = true;
            }
        }
        else if(Input.GetKey(KeyCode.I))
        {
            gridState.itemID++;
            if(gridState.itemID == itemPrefabs.Length)
            {
                //spriteRenderer.enabled = true;
                gridState.hasItem = false;
                gridState.itemID = -1;
                Destroy(curItem);
                return;
            }
            if(curItem == null)
            {
                //产生道具
                CreateItem();
            }
            else//本身就有道具
            {
                Destroy(curItem);
                //产生道具
                CreateItem();
            }
            gridState.hasItem = true;
            gridState.canBuild = true;
            //spriteRenderer.enabled = false;
        }
        else if(!gridState.isMonsterPoint)
        {
            gridState.isMonsterPoint = false;
            gridState.canBuild = !gridState.canBuild;
            if(gridState.canBuild)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }



    //生成道具
    private void CreateItem()
    {
        Vector3 createPos = transform.position;
        BigLevelItemInfo curItemInfo = MapMaker.Instance.GetCurBigLevelItemInfo();
        Debug.Log(curItemInfo.itemIndex_2x2);
        if (gridState.itemID >= 0 && gridState.itemID <= curItemInfo.itemIndex_2x2)
        {
            createPos += new Vector3(MapMaker.Instance.gridWidth / 2, -MapMaker.Instance.gridHeight / 2);
        }
        else if(gridState.itemID > curItemInfo.itemIndex_2x2 &&gridState.itemID <= curItemInfo.itemIndex_1x2)
        {
            createPos += new Vector3(MapMaker.Instance.gridWidth / 2, 0);
        }
        GameObject item = Instantiate(itemPrefabs[gridState.itemID], createPos, Quaternion.identity);
        item.transform.SetParent(this.transform);
        curItem = item;
    }

    //更新格子状态
    public void UpdateGrid()
    {
        if(gridState.canBuild)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = gridSprite;
            if (gridState.hasItem)
            {
                CreateItem();
            }
        }
        else
        {
            if(gridState.isMonsterPoint)
            {
                spriteRenderer.sprite = monsterPathSprite;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }
#endif
}
