using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LitJson;
using DG.Tweening;

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
    [System.Serializable]
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

    private GameController gameController;
    //建塔列表
    private GameObject towerListGo;
    public GameObject handleTowerCanvasGo;
    private Transform btnUpLevelTrans;
    private Transform btnSellTrans;
    private Vector3 btnUpLevelInitPos;
    private Vector3 btnSellInitPos;

    private SpriteRenderer spriteRenderer;

    private Sprite gridSprite;
    private Sprite startSprite;//开始时格子显示的图片
    private Sprite cantBuildSprite;

    public GridState gridState;
    public GridIndex gridIndex;

    public bool hasTower;

    //有塔之后的属性
    public GameObject towerGo;
    [HideInInspector]
    //当前格子上的炮塔
    public Tower tower;
    public TowerProperty towerProperty;
    private GameObject upLevelSignal;

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
        string prefabPath = "Prefabs/Game/" + MapMaker.Instance.bigLevelID.ToString() + "/Item/";
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

#if Game
        hasTower = false;

        gameController = GameController.Instance;
        towerListGo = gameController.towerList;
        handleTowerCanvasGo = gameController.handleTowerCanvas;
        btnUpLevelTrans = handleTowerCanvasGo.transform.Find("Btn_UpLevel");
        btnSellTrans = handleTowerCanvasGo.transform.Find("Btn_Sell");
        btnUpLevelInitPos = btnUpLevelTrans.localPosition;
        btnSellInitPos = btnSellTrans.localPosition;

        upLevelSignal = transform.Find("LevelUpSignal").gameObject;

        gridSprite = gameController.GetSprite("NormalMordel/Game/Grid");
        startSprite = gameController.GetSprite("NormalMordel/Game/StartSprite");
        cantBuildSprite = gameController.GetSprite("NormalMordel/Game/cantBuild");
        spriteRenderer.sprite = startSprite;
        Tween tween = DOTween.To(() => spriteRenderer.color, toColor => spriteRenderer.color = toColor, new Color(1, 1, 1, 0), 2);
        tween.OnComplete(ChangeSpriteToGrid);

#endif
    }

    private void Update()
    {
        if(upLevelSignal != null)
        {
            if(hasTower)
            {
                if(towerProperty.upLevelPrice <= gameController.coin && tower.towerLevel < 3)
                {
                    upLevelSignal.SetActive(true);
                }
                else
                {
                    upLevelSignal.SetActive(false);
                }
            }
            else
            {
                if(upLevelSignal.activeSelf)
                {
                    upLevelSignal.SetActive(false);
                }
            }
        }
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

#if Game
        towerGo = null;
        towerProperty = null;
        hasTower = false;
#endif
    }

    public void SetGridIndex(int x,int y)
    {
        gridIndex.xIndex = x;
        gridIndex.yIndex = y;
    }

#if Game

    private void ChangeSpriteToGrid()
    {
        spriteRenderer.enabled = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        if (gridState.canBuild)
        {
            spriteRenderer.sprite = gridSprite;
        }
        else
        {
            spriteRenderer.sprite = cantBuildSprite;
        }
    }

    #region 格子处理方法
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

    //显示格子
    public void ShowGrid()
    {
        if(!hasTower)
        {
            spriteRenderer.enabled = true;
            //显示建塔列表
            towerListGo.transform.position = CorrectTowerListGoPos();
            towerListGo.SetActive(true);
           
        }
        else
        {
            handleTowerCanvasGo.transform.position = transform.position;
            CorrectHandleTowerCanvas();
            handleTowerCanvasGo.SetActive(true);
            //显示塔的攻击范围
            towerGo.transform.Find("attackRange").gameObject.SetActive(true);
        }
    }

    //隐藏格子
    public void HideGrid()
    {
        if(!hasTower)
        {
            //隐藏建塔列表
            towerListGo.SetActive(false);
        }
        else
        {
            handleTowerCanvasGo.SetActive(false);
            //隐藏塔的攻击范围
            towerGo.transform.Find("attackRange").gameObject.SetActive(false);
        }
        spriteRenderer.enabled = false;
    }

    private void ShowCantBuild()
    {
        spriteRenderer.enabled = true;
        Tween tween = DOTween.To(() => spriteRenderer.color, toColor => spriteRenderer.color = toColor, new Color(1, 1, 1, 0), 2);
        tween.OnComplete(() => { spriteRenderer.enabled = false; spriteRenderer.color = new Color(1, 1, 1, 1); });
    }

    public void FinishBuildTower(GameObject towerGo)
    {
        spriteRenderer.enabled = false;
        //对塔的后续处理
        this.towerGo = towerGo;
        tower = towerGo.GetComponent<Tower>();
        towerProperty = towerGo.GetComponent<TowerProperty>();
    }

    #endregion

    //创建道具
    private void CreateItem()
    {
        GameObject itemGo = GameController.Instance.GetGameObjectResource
            (GameController.Instance.mapMaker.bigLevelID.ToString() + "/Item/" + gridState.itemID.ToString());
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
        itemGo.GetComponent<Item>().itemID = gridState.itemID;
        itemGo.GetComponent<Item>().gridPoint = this;
    }

    private void OnMouseDown()
    {
        //如果点击的是UI不交互
        if(EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("点击到了UI");
            return;
        }
        //Debug.Log("点击到了格子");
        
        if(gridState.canBuild)
        {
            if(gameController.selectGrid == null)
            {
                GameController.Instance.PlayEffectMusic("NormalMordel/Grid/GridSelect");
                gameController.selectGrid = this;
                ShowGrid();
            }
            else if (gameController.selectGrid == this)//如果点击已被选中的格子
            {
                GameController.Instance.PlayEffectMusic("NormalMordel/Grid/GridDeselect");
                gameController.selectGrid.HideGrid();
                gameController.selectGrid = null;
            }
            else if(gameController.selectGrid != this)
            {
                GameController.Instance.PlayEffectMusic("NormalMordel/Grid/GridSelect");
                gameController.selectGrid.HideGrid();
                gameController.selectGrid = this;
                ShowGrid();
            }

        }
        else
        {
            GameController.Instance.PlayEffectMusic("NormalMordel/Grid/SelectFault");
            if (gameController.selectGrid != null)
            {
                gameController.selectGrid.HideGrid();
            }
            gameController.selectGrid = null;
            ShowCantBuild();
        }
    }

    //设置建塔列表的位置
    private Vector3 CorrectTowerListGoPos()
    {
        Vector3 correctPos = Vector3.zero;
        if(gridIndex.xIndex <= 3 && gridIndex.xIndex >= 0)
        {
            correctPos += new Vector3(gameController.mapMaker.gridWidth, 0, 0);
        }
        else if (gridIndex.xIndex <= 11 && gridIndex.xIndex >= 8)
        {
            correctPos -= new Vector3(gameController.mapMaker.gridWidth, 0, 0);
        }
        if (gridIndex.yIndex <= 3 && gridIndex.yIndex >= 0)
        {
            correctPos += new Vector3(0, gameController.mapMaker.gridHeight, 0);
        }
        else if (gridIndex.yIndex <= 7 && gridIndex.yIndex >= 4)
        {
            correctPos -= new Vector3(0, gameController.mapMaker.gridHeight, 0);
        }
        correctPos += transform.position;
        return correctPos;
    }

    //设置升级画布按钮位置
    private void CorrectHandleTowerCanvas()
    {
        btnUpLevelTrans.localPosition = Vector3.zero;
        btnSellTrans.localPosition = Vector3.zero;
        if(gridIndex.yIndex <= 0)
        {
            if(gridIndex.xIndex >=0 && gridIndex.xIndex <= 5)
            {
                btnSellTrans.position += new Vector3(gameController.mapMaker.gridWidth * 3/4, 0, 0);
            }
            else if(gridIndex.xIndex >= 6 && gridIndex.xIndex <= 11)
            {
                btnSellTrans.position -= new Vector3(gameController.mapMaker.gridWidth * 3 / 4, 0, 0);
            }
            btnUpLevelTrans.localPosition = btnUpLevelInitPos;
        }
        else if(gridIndex.yIndex >= 6)
        {
            if (gridIndex.xIndex >= 0 && gridIndex.xIndex <= 5)
            {
                btnUpLevelTrans.position += new Vector3(gameController.mapMaker.gridWidth * 3 / 4, 0, 0);
            }
            else if (gridIndex.xIndex >= 6 && gridIndex.xIndex <= 11)
            {
                btnUpLevelTrans.position -= new Vector3(gameController.mapMaker.gridWidth * 3 / 4, 0, 0);
            }
            btnSellTrans.localPosition = btnSellInitPos;
        }
        else
        {
            btnUpLevelTrans.localPosition = btnUpLevelInitPos;
            btnSellTrans.localPosition = btnSellInitPos;
        }
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
