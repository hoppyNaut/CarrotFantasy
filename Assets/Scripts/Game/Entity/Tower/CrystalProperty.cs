using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalProperty : TowerProperty
{
    private float distance;
    private float bulletWidth;
    private float bulletLength;

    private AudioSource audioSource;

    protected override void OnEnable()
    {
        base.OnEnable();
        if(animator == null)
        {
            return;
        }
        bulletGo = gameController.GetGameObjectResource("Tower/ID" + tower.towerID.ToString() + "/Bullect/" + tower.towerLevel.ToString());
        bulletGo.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        bulletGo = gameController.GetGameObjectResource("Tower/ID" + tower.towerID.ToString() + "/Bullect/" + tower.towerLevel.ToString());
        bulletGo.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gameController.GetAudioClip("NormalMordel/Tower/Attack/" + tower.towerID.ToString());
    }

    protected override void Update()
    {
        if (gameController.isGamePause || tower.atkTargetTrans == null || gameController.isGameOver)
        {
            if(tower.atkTargetTrans == null)
            {
                bulletGo.SetActive(false);
            }
            return;
        }
        if (!tower.atkTargetTrans.gameObject.activeSelf)
        {
            tower.atkTargetTrans = null;
            return;
        }
        //攻击
        Attack();
    }

    protected override void Attack()
    {
        if (tower.atkTargetTrans == null)
        {
            return;
        }
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        animator.Play("Attack");
        if(tower.atkTargetTrans.gameObject.tag == "Item")
        {
            distance = Vector3.Distance(transform.position, tower.atkTargetTrans.position + new Vector3(0,0,3));
        }
        else if(tower.atkTargetTrans.gameObject.tag == "Monster")
        {
            distance = Vector3.Distance(transform.position, tower.atkTargetTrans.position);
        }
        bulletWidth = 3 / distance;
        bulletLength = distance / 2;
        if(bulletWidth <= 0.5f)
        {
            bulletWidth = 0.5f;
        }
        else if(bulletWidth >= 1)
        {
            bulletWidth = 1;
        }
        bulletGo.transform.position = new Vector3((tower.atkTargetTrans.position.x + transform.position.x) / 2, (tower.atkTargetTrans.position.y + transform.position.y) / 2);
        bulletGo.transform.localScale = new Vector3(1, bulletWidth, bulletLength);
        bulletGo.SetActive(true);
        Bullet bulletClass = bulletGo.GetComponent<Bullet>();
        bulletClass.towerID = tower.towerID;
        bulletClass.towerLevel = tower.towerLevel;
        //为子弹设置目标
        bulletClass.targetTrans = tower.atkTargetTrans;
    }

    protected override void DestroyTower()
    {
        bulletGo.SetActive(false);
        gameController.PushGameObjectToFactory("Tower/ID" + tower.towerID.ToString() + "/Bullect/" + tower.towerLevel.ToString(), bulletGo);
        bulletGo = null;
        base.DestroyTower();
    }
}
