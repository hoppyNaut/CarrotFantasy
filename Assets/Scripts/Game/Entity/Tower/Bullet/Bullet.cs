using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform targetTrans;
    public float moveSpeed;
    public int atkValue;

    public int towerID;
    public int towerLevel;

    protected GameController gameController;

    protected virtual void Start()
    {
        gameController = GameController.Instance;
    }

    protected virtual void Update()
    {
       if(gameController.isGamePause || targetTrans == null)
        {
            return;
        }
       if(!targetTrans.gameObject.activeInHierarchy)
        {
            DestroyBullet();
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, moveSpeed * Time.deltaTime);
        transform.LookAt(targetTrans);

    }

    private void Init()
    {
        targetTrans = null;
    }

    private void DestroyBullet()
    {
        //生成特效
        GameObject effectGo = gameController.GetGameObjectResource("Tower/ID" + towerID.ToString() + "/Effect/" + towerLevel.ToString());
        effectGo.transform.position = this.transform.position;
        effectGo.transform.SetParent(gameController.transform);
        Init();
        gameController.PushGameObjectToFactory("Tower/ID" + towerID.ToString() + "/Bullect/" + towerLevel.ToString(), gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == targetTrans)
        {
            DestroyBullet();
        }
    }
}
