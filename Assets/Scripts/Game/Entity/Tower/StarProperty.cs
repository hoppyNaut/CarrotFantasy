using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarProperty : TowerProperty
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (gameController.isGamePause || tower.atkTargetTrans == null || gameController.isGameOver)
        {
            return;
        }
        if (!tower.atkTargetTrans.gameObject.activeSelf)
        {
            tower.atkTargetTrans = null;
            return;
        }
        //攻击
        atkTimer += Time.deltaTime;
        if (atkTimer >= atkTime / gameController.gameSpeed)
        {
            Attack();
            atkTimer = 0;
        }
    }
}
