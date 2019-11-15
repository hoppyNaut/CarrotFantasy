using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass:IResetable,IInitable
{
    public MyClass() {
        Debug.Log("生成了一个MyClass的对象");
    }

    public void Init()
    {
        Debug.Log("MyClass Init.....");
    }

    public void Reset()
    {
        Debug.Log("MyClass Reset.....");
    }
}

public class Test : MonoBehaviour
{
   
    void Start()
    {
        ObjectPool<MyClass> myClassObjectPool = new ObjectPool<MyClass>(10);
        MyClass mc = myClassObjectPool.GetNew();
        myClassObjectPool.Store(mc);
        mc = myClassObjectPool.GetNew();
    }

}
