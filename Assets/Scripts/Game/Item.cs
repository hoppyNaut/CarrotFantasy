using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //所属的格子
    public GridPoint gridPoint;
    public int itemID;
    private GameController gameController;

    private void Start()
    {
        gameController = GameController.Instance;
    }

    private void OnMouseDown()
    {
        if (gameController.targetTrans == null)
        {
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //转换新目标
        else if (gameController.targetTrans != transform)
        {
            gameController.HideSignal();
            gameController.targetTrans = transform;
            gameController.ShowSignal();
        }
        //两次点击同一个目标
        else if (gameController.targetTrans == transform)
        {
            gameController.HideSignal();
        }
    }
}
