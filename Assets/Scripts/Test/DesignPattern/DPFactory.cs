using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFactory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FactoryIPhone8 factoryIPhone8 = new FactoryIPhone8();
        factoryIPhone8.ProduceIPhone();
        factoryIPhone8.ProduceIPhoneCharger();
        FactoryIPhoneX factoryIPhoneX = new FactoryIPhoneX();
        factoryIPhoneX.ProduceIPhone();
        factoryIPhoneX.ProduceIPhoneCharger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//工厂模式分为简单工厂模式、工厂方法模式、抽象工厂模式

//简单工厂模式

public class IPhone
{
    public IPhone() { }
}

public class IPhone8 : IPhone
{
    public IPhone8() {
        Debug.Log("Produce IPhone8");
    }
}

public class IPhoneX : IPhone
{
    public IPhoneX() {
        Debug.Log("Produce IPhoneX");
    }
}

public class IPhoneCharger
{
    public IPhoneCharger(){ }
}

public class IPhone8Charger:IPhoneCharger
{
    public IPhone8Charger()
    {
        Debug.Log("Produce IPhone8 Charger");
    }
}

public class IPhoneXCharger:IPhoneCharger
{
    public IPhoneXCharger()
    {
        Debug.Log("Produce IPhoneX Charger");
    }
}

public interface IFactory
{
    IPhone ProduceIPhone();
    IPhoneCharger ProduceIPhoneCharger();
}

public class FactoryIPhone8 : IFactory
{
    public IPhone ProduceIPhone()
    {
        return new IPhone8();
    }

    public IPhoneCharger ProduceIPhoneCharger()
    {
        return new IPhone8Charger();
    }
}

public class FactoryIPhoneX : IFactory
{
    public IPhone ProduceIPhone()
    {
        return new IPhoneX();
    }

    public IPhoneCharger ProduceIPhoneCharger()
    {
        return new IPhoneXCharger();
    }
}


