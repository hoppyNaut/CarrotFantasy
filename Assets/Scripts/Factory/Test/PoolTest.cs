using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public Transform trans;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = GameObjectPool.Get("bullet", trans.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(trans.forward * 30, ForceMode.Impulse);
        }
    }
}
