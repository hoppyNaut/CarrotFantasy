using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPMediator1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MatchMaker man = new Man(35, 100000, 99999, 0);
        MatchMaker woman = new Women(26, 5000, 1000, 0);
        //man.GetInformation(woman);
        //woman.GetInformation(man);
        //Debug.Log("1");
        Introducer introducer = new Introducer(man, woman);
        //introducer.GetManInformation();
        //introducer.GetWomanInformation();
        man.GetInormationFromIntroducer(introducer);
        woman.GetInormationFromIntroducer(introducer);
        Debug.Log($"男方对女方好感度：{man.m_favor}  女方对男方好感度：{woman.m_favor}");
    }
}

public abstract class MatchMaker
{
    public int m_age;
    public int m_money;
    public int m_familyBg;
    public int m_favor;

    public MatchMaker(int age,int money,int familyBg,int favor)
    {
        m_age = age;
        m_money = money;
        m_familyBg = familyBg;
        m_favor = favor;
    }

    public abstract void GetInformation(MatchMaker otherMatchMaker);
    public abstract void GetInormationFromIntroducer(Introducer introducer);
}

public class Man : MatchMaker
{
    public Man(int age,int money,int familyBg,int favor) : base(age, money, familyBg, favor)
    {

    }

    public override void GetInformation(MatchMaker otherMatchMaker)
    {
        m_favor += -otherMatchMaker.m_age * 3 + otherMatchMaker.m_money + otherMatchMaker.m_familyBg * 2;
    }

    public override void GetInormationFromIntroducer(Introducer introducer)
    {
        introducer.GetWomanInformation();
    }
}

public class Women : MatchMaker
{
    public Women(int age, int money, int familyBg, int favor) : base(age, money, familyBg, favor)
    {

    }

    public override void GetInformation(MatchMaker otherMatchMaker)
    {
        m_favor += -otherMatchMaker.m_age * 3 + otherMatchMaker.m_money + otherMatchMaker.m_familyBg * 2;
    }

    public override void GetInormationFromIntroducer(Introducer introducer)
    {
        introducer.GetManInformation();
    }
}

public class Introducer
{
    public MatchMaker m_man;
    public MatchMaker m_woman;

    public Introducer(MatchMaker man,MatchMaker woman)
    {
        m_man = man;
        m_woman = woman;
    }

    public void GetManInformation()
    {
        m_woman.m_favor += -m_man.m_age * 3 + m_man.m_money + m_man.m_familyBg * 2;
    }

    public void GetWomanInformation()
    {
        m_man.m_favor += -m_woman.m_age * 3 + m_woman.m_money + m_woman.m_familyBg * 2;
    }
}