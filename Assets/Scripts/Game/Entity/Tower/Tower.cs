using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int towerID;
    public int towerLevel;

    //是否有攻击目标
    public bool hasTarget;
    //是否需要集火目标
    public bool isConverge;

    //攻击目标
    public Transform atkTargetTrans;

    private CircleCollider2D circleCollider2D;
    private TowerProperty towerProperty;
    //攻击范围渲染
    private SpriteRenderer attackRangeSr;

    private GameController gameController;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    void Update()
    {
        //更换集火目标处理
        if(isConverge)
        {
            if(gameController.targetTrans != atkTargetTrans)
            {
                atkTargetTrans = null;
                isConverge = false;
                hasTarget = false;
            }
        }
        if(hasTarget)
        {
            if(atkTargetTrans.gameObject.activeInHierarchy == false)
            {
                atkTargetTrans = null;
                isConverge = false;
                hasTarget = false;
            }
        }
    }

    private void Init()
    {
        gameController = GameController.Instance;
        circleCollider2D = GetComponent<CircleCollider2D>();
        towerProperty = GetComponent<TowerProperty>();
        towerProperty.tower = this;
        attackRangeSr = transform.Find("attackRange").GetComponent<SpriteRenderer>();
        attackRangeSr.gameObject.SetActive(false);
        circleCollider2D.radius = 5;
        hasTarget = false;
        isConverge = false;
    }

    public void DestroyTower()
    {
        towerProperty.Init();
        Init();
        gameController.PushGameObjectToFactory("Tower/ID" + towerID.ToString() + "/TowerSet/" + towerLevel.ToString(), gameObject);
    }

    public void GetTowerResource()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Monster" && collision.tag != "Item" )
        {
            return;
        }

        //有集火目标
        if(gameController.targetTrans != null)
        {
            //还未找到集火目标
            if(!isConverge)
            {
                //如果进入目标是道具且是集火目标
                if(collision.tag == "Item" && gameController.targetTrans == collision.transform)
                {
                    atkTargetTrans = collision.transform;
                    hasTarget = true;
                    isConverge = true;
                }
                //如果进入目标是怪物
                else if(collision.tag == "Monster")
                {
                    //如果当前进入攻击范围的怪物不是集火目标
                    if(collision.transform != gameController.targetTrans)
                    {
                        if(!hasTarget)
                        {
                            atkTargetTrans = collision.transform;
                            hasTarget = true;
                        }
                    }
                    //如果当前进入攻击范围的怪物是集火目标
                    else
                    {
                        atkTargetTrans = collision.transform;
                        hasTarget = true;
                        isConverge = true;
                    }
                }
            }
        }
        //没有集火目标
        else
        {
            //没有攻击目标
            if(!hasTarget && collision.tag == "Monster")
            {
                atkTargetTrans = collision.transform;
                hasTarget = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Monster" && collision.tag != "Item")
        {
            return;
        }

        //有集火目标
        if (gameController.targetTrans != null)
        {
            //还未找到集火目标
            if (!isConverge)
            {
                //如果进入目标是道具且是集火目标
                if (collision.tag == "Item" && gameController.targetTrans == collision.transform)
                {
                    atkTargetTrans = collision.transform;
                    hasTarget = true;
                    isConverge = true;
                }
                //如果进入目标是怪物
                else if (collision.tag == "Monster")
                {
                    //如果当前进入攻击范围的怪物不是集火目标
                    if (collision.transform != gameController.targetTrans)
                    {
                        if (!hasTarget)
                        {
                            atkTargetTrans = collision.transform;
                            hasTarget = true;
                        }
                    }
                    //如果当前进入攻击范围的怪物是集火目标
                    else
                    {
                        atkTargetTrans = collision.transform;
                        hasTarget = true;
                        isConverge = true;
                    }
                }
            }
        }
        //没有集火目标
        else
        {
            //没有攻击目标
            if (!hasTarget && collision.tag == "Monster")
            {
                atkTargetTrans = collision.transform;
                hasTarget = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(atkTargetTrans == collision.transform)
        {
            atkTargetTrans = null;
            hasTarget = false;
            isConverge = false;
        }
    }

}
