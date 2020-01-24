using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        transform.SetSiblingIndex(8);
    }

}
