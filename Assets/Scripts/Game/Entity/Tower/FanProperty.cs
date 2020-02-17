﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanProperty : TowerProperty
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (gameController.isGamePause || tower.atkTargetTrans == null)
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