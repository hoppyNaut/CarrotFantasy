/****************************************************
	文件：AudioClipFactory.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 19:58   	
	功能：音频资源工厂
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipFactory : IBaseResourceFactory<AudioClip>
{
    protected Dictionary<string, AudioClip> factoryClip = new Dictionary<string, AudioClip>();

    protected string loadPath;

    public AudioClipFactory()
    {
        loadPath = "AudioClips/";
    }

    public AudioClip GetSingleResource(string resPath)
    {
        AudioClip item = null;
        string path = loadPath + resPath;
        if(factoryClip.ContainsKey(resPath))
        {
            item = factoryClip[resPath];
        }
        else
        {
            item = Resources.Load<AudioClip>(path);
            factoryClip.Add(resPath, item);
        }

        if(item == null)
        {
            Debug.Log("音频资源路径：" + path + "错误");
        }

        return item;
    }
}
