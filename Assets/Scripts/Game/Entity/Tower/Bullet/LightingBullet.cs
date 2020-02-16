using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBullet : Bullet
{
    private float atkTimer;
    private bool canTakeDamage;
    public float atkTime;

    protected override void Update()
    {
        base.Update();
        if(!canTakeDamage)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkTime)
            {
                canTakeDamage = true;
                atkTimer = 0;
                DecreaseHp();
            }
        }
    }

    private void DecreaseHp()
    {
        if(!canTakeDamage || targetTrans == null)
        {
            return;
        }
        if (targetTrans.tag == "Monster" || targetTrans.tag == "Item")
        {
            if (targetTrans.gameObject.activeSelf)
            {
                targetTrans.SendMessage("TakeDamage", atkValue);
                SpawnEffect();
                canTakeDamage = false;
            }
        }
    }

    protected override void SpawnEffect()
    {
        if(targetTrans == null)
        {
            return;
        }
        GameObject effectGo = gameController.GetGameObjectResource("Tower/ID" + towerID.ToString() + "/Effect/" + towerLevel.ToString());
        effectGo.transform.position =targetTrans.position;
    }
}
