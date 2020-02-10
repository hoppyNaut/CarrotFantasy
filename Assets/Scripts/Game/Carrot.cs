using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrot : MonoBehaviour
{
    public Text txt_Hp;

    private void Awake()
    {
        txt_Hp = transform.Find("HpCanvas").Find("Image").Find("txt_Hp").GetComponent<Text>();
    }

    public void SetHp(int hp)
    {
        txt_Hp.text = hp.ToString();
    }
}
