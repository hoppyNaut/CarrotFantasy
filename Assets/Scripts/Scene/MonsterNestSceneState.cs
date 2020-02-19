using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterNestSceneState : BaseSceneState
{
    public MonsterNestSceneState(UIFacade uiFacade) : base(uiFacade)
    {

    }

    public override void OnEnterScene()
    {
        mUIFacade.AddPanelToDict(Constant.GameLoadPanel);
        mUIFacade.AddPanelToDict(Constant.MonsterNestPanel);

        base.OnEnterScene();
        GameManager.Instance.audioManager.PlayBgMusic(GameManager.Instance.GetAudioClip("MonsterNest/BGMusic02"));
    }

    public override void OnExitScene()
    {
        base.OnExitScene();
        if(mUIFacade.currentSceneState.GetType() == typeof(MainSceneState))
        {
            SceneManager.LoadScene(Constant.MainSceneIndex);
        }
    }
}
