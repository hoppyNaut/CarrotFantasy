/****************************************************
	文件：UIFacade.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/12/09 21:40   	
	功能：UI中介，上层与管理者交互，下层与各UI面板交互
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFacade
{
    //上层管理者
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioManager mAudioManager;
    public PlayerManager mPlayerManager;

    //下层UI面板
    public Dictionary<string, IBasePanel> currentScenePanelDict;

    private GameObject mask;
    private Image maskImage;
    public Transform canvasTrans;

    //场景状态
    public IBaseSceneState currentSceneState;
    public IBaseSceneState lastSceneState;

    public UIFacade(UIManager uiManager)
    {
        mGameManager = GameManager.Instance;
        mUIManager = uiManager;
        mAudioManager = mGameManager.audioManager;
        mPlayerManager = mGameManager.playerManager;

        currentScenePanelDict = new Dictionary<string, IBasePanel>();

        InitMask();
    }

    /// <summary>
    /// 初始化遮罩
    /// </summary>
    public void InitMask()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        //通过工厂加载Mask并获取Mask组件 TODO
        mask = CreateUIAndSetPosition("Img_Mask");
        maskImage = mask.GetComponent<Image>();
    }

    /// <summary>
    /// 实例化当前场景的所有面板并存入字典
    /// </summary>
    public void InitDict()
    {
        foreach(KeyValuePair<string,GameObject> item in mUIManager.currentScenePanelDict)
        {
            item.Value.transform.SetParent(canvasTrans);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if(basePanel == null)
            {
                Debug.Log("获取UI面板上IBasePanel脚本失败");
            }
            basePanel.InitPanel();
            currentScenePanelDict.Add(item.Key, basePanel);
        }
    }

    //改变当前场景的状态
    public void ChangeSceneState(IBaseSceneState baseSceneState)
    {
        lastSceneState = currentSceneState;
        ShowMask();
        currentSceneState = baseSceneState;
    }

    //显示遮罩
    public void ShowMask()
    {
        mask.SetActive(true);
        mask.transform.SetSiblingIndex(10);
        Tween t = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0,0, 1), 2.0f);
        t.OnComplete(ExitSceneComplete);
    }

    private void ExitSceneComplete()
    {
        lastSceneState.OnExitScene();
        currentSceneState.OnEnterScene();
        HideMask();
    }

    public void HideMask()
    {
        mask.transform.SetSiblingIndex(10);
        Tween t = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2.0f);
        t.OnComplete(()=> { mask.SetActive(false); });
    }


    #region GetOperation
    //获取Sprite资源
    public Sprite GetSprite(string path)
    {
        return mGameManager.GetSprite(path);
    }

    //获取AudioClip资源
    public AudioClip GetAudioClip(string path)
    {
        return mGameManager.GetAudioClip(path);
    }

    //获取RuntimeAnimatorController资源
    public RuntimeAnimatorController GetRuntimeAnimatorController(string path)
    {
        return mGameManager.GetRuntimeAnimatorController(path);
    }

    //获取游戏物体资源
    public GameObject GetGameObjectResource(FactoryType factoryType,string name)
    {
        return mGameManager.GetGameObjectResource(factoryType, name);
    }
    #endregion


    //将游戏物体回收到对象池
    public void PushGameObjectToFactory(FactoryType factoryType,string name,GameObject go)
    {
        mGameManager.PushGameObjectToFactory(factoryType, name, go);
    }

    //实例化UI 
    public GameObject CreateUIAndSetPosition(string name)
    {
        GameObject itemUI = GetGameObjectResource(FactoryType.UIFactory, name);
        itemUI.transform.SetParent(canvasTrans);
        itemUI.transform.localPosition = Vector3.zero;
        itemUI.transform.localScale = Vector3.one;
        return itemUI;
    }

    //添加UIPanel到UIManager字典
    public void AddPanelToDict(string panelName)
    {
        mUIManager.currentScenePanelDict.Add(panelName, GetGameObjectResource(FactoryType.UIPanelFactory, panelName));
    }

    //清空UIPanel字典
    public void ClearDict()
    {
        currentScenePanelDict.Clear();
        mUIManager.ClearDict();
    }

    #region AudioOperation
    public void CloseOrOpenBGMusic()
    {
        mAudioManager.CloseOrOpenBGMusic();
    }

    public void CloseOrOpenEffectMusic()
    {
        mAudioManager.CloseOrOpenEffectMusic();
    }
    #endregion

}
