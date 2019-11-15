using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHero
{
    void SkillQ();
    void SkillW();
    void SkillE();
    void SkillR();
}

/// <summary>
/// 使用virtual & override实现英雄技能的继承
/// </summary>
public class BaseHero : IHero
{
    public virtual void SkillQ()
    {
        Debug.Log("Skill Q");
    }

    public virtual void SkillW()
    {
        Debug.Log("Skill W");
    }

    public virtual void SkillE()
    {
        Debug.Log("Skill E");
    }


    public virtual void SkillR()
    {
        Debug.Log("Skill R");
    }
}

public class Zed : BaseHero
{
    public int hp = 10;

    public override void SkillQ()
    {
        Debug.Log("Zed Q");
    }
    public override void SkillW()
    {
        Debug.Log("Zed W");
    }
    public override void SkillE()
    {
        Debug.Log("Zed E");
    }
    public override void SkillR()
    {
        Debug.Log("Zed R");
    }
}

public class BaseHeroVersionTwo : IHero
{


    public void SkillE()
    {
        Debug.Log("Skill E");
    }

    public void SkillQ()
    {
        Debug.Log("Skill Q");
    }

    public void SkillR()
    {
        Debug.Log("Skill R");
    }

    public void SkillW()
    {
        Debug.Log("Skill W");
    }
}

/// <summary>
/// 使用new关键字隐藏英雄基类的技能方法
/// </summary>
public class Lebanc : BaseHeroVersionTwo
{
    public new void SkillQ()
    {
        Debug.Log("Lebanc Q");
    }

    public new void SkillW()
    {
        Debug.Log("Lebanc W");
    }

    public new void SkillE()
    {
        Debug.Log("Lebanc E");
    }

    public new void SkillR()
    {
        Debug.Log("Lebanc R");
    }
}



public class IOP : MonoBehaviour
{
    // Start is called before the first frame update

    public void PlaySkill(BaseHero bh)
    {
        bh.SkillQ();
        bh.SkillW();
        bh.SkillE();
        bh.SkillR();
    }

    public void PlaySkill2(BaseHeroVersionTwo bh)
    {
        bh.SkillQ();
        bh.SkillW();
        bh.SkillE();
        bh.SkillR();
    }

    void Start()
    {
        BaseHero zed = new Zed();
        Debug.Log( zed.GetType());
        PlaySkill(zed);
        Debug.Log("---------------------------");
        BaseHeroVersionTwo lebanc = new Lebanc();
       PlaySkill2(lebanc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
