using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPMediator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ColleagueMediator mediator = new ColleagueMediator();

        ConcreateColleague1 colleague1 = new ConcreateColleague1(mediator);
        ConcreateColleague2 colleague2 = new ConcreateColleague2(mediator);

        mediator.Colleague1 = colleague1;
        mediator.Colleague2 = colleague2;

        colleague1.SendMsg("1");
        colleague2.SendMsg("2");
    }

}

//中介者基类
public abstract class Mediator
{
    //抽象方法，向Colleague发送消息
    public abstract void SendMessage(Colleague colleague, string msg);
}

public class ColleagueMediator : Mediator
{
    private ConcreateColleague1 colleague1;
    private ConcreateColleague2 colleague2;

    public ConcreateColleague1 Colleague1
    {
        set { colleague1 = value; }
    }

    public ConcreateColleague2 Colleague2
    {
        set { colleague2 = value; }
    }

    public override void SendMessage(Colleague colleague, string msg)
    {
        if(colleague == colleague1)
        {
            colleague2.Receive(msg);
        }
        else if(colleague == colleague2)
        {
            colleague1.Receive(msg);
        }
    }
}


public abstract class Colleague
{
    protected Mediator m_Mediator;

    public Colleague(Mediator mediator)
    {
        m_Mediator = mediator;
    }

    public abstract void SendMsg(string msg);
    public abstract void Receive(string msg);
}

public class ConcreateColleague1 : Colleague
{
    public ConcreateColleague1(Mediator mediator):base(mediator)
    {

    }

    public override void SendMsg(string msg)
    {
        m_Mediator.SendMessage(this, "ConcreateColleague1发出消息：" + msg);
    }

    public override void Receive(string msg)
    {
        Debug.Log("ConcreateColleague1收到消息：" + msg);
    }
}
public class ConcreateColleague2 : Colleague
{
    public ConcreateColleague2(Mediator mediator) : base(mediator)
    {

    }

    public override void SendMsg(string msg)
    {
        m_Mediator.SendMessage(this, "ConcreateColleague2发出消息：" + msg);
    }

    public override void Receive(string msg)
    {
        Debug.Log("ConcreateColleague2收到消息：" + msg);
    }
}
