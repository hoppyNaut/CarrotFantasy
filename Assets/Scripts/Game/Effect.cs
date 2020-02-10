using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float animTime;
    public string resName;

    private void OnEnable()
    {
        Invoke("DestroyEffect", animTime);
    }

    private void DestroyEffect()
    {
        GameController.Instance.PushGameObjectToFactory(resName, this.gameObject);
    }
}
