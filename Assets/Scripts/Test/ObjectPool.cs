using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    void Reset();
}

public interface IInitable
{
    void Init();
}

public class ObjectPool<T> where T:class, IResetable, IInitable, new()
{
    private Stack<T> m_objectStack;
    //资源被重用时reset的委托
    private Action<T> m_resetAction;
    //资源生成后调用的委托
    private Action<T> m_onetimeInitAction;

    public ObjectPool(int initialBufferSize,Action<T> resetAction = null,Action<T> onetimeInitAction = null)
    {
        m_objectStack = new Stack<T>(initialBufferSize);
        m_resetAction = resetAction;
        m_onetimeInitAction = onetimeInitAction;
    }

    public T GetNew()
    {
        if(m_objectStack.Count > 0)
        {
            T t = m_objectStack.Pop();
            t.Reset();

            if(m_resetAction != null)
            {
                m_resetAction(t);
            }

            return t;
        }
        else
        {
            T t = new T();

            t.Init();
            if(m_onetimeInitAction != null)
            {
                m_onetimeInitAction(t);
            }

            return t;
        }
    }

    public void Store(T obj)
    {
        m_objectStack.Push(obj);
    }
}


/// <summary>
/// 集体重置池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPoolWithCollectiveReset<T> where T: class, new()
{
    private List<T> m_objectList;
    private int m_nextAvailableIndex = 0;

    private Action<T> m_resetAction;
    private Action<T> m_onetimeInitAction;

    public ObjectPoolWithCollectiveReset(int initialBufferSize,Action<T> resetAction = null, Action<T> onetimeInitAction = null)
    {
        m_objectList = new List<T>(initialBufferSize);
        m_resetAction = resetAction;
        m_onetimeInitAction = onetimeInitAction;
    }

    public T GetNew()
    {
        if(m_nextAvailableIndex < m_objectList.Count)
        {
            T t = m_objectList[m_nextAvailableIndex];
            m_nextAvailableIndex++;

            if(m_resetAction != null)
            {
                m_resetAction(t);
            }
            return t;
        }
        else
        {
            T t = new T();
            m_objectList.Add(t);
            m_nextAvailableIndex++;

            if(m_onetimeInitAction != null)
            {
                m_onetimeInitAction(t);
            }
            return t;
        }
    }

    public void ResetAll()
    {
        //重置索引
        m_nextAvailableIndex = 0;
    }
}
