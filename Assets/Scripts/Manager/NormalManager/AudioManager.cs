/****************************************************
	文件：AudioManager.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/14 16:35   	
	功能：负责控制音乐、音效的管理者
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    //控制背景音乐播放的AudioSource
    private AudioSource bgMusicAudioSource;
    //控制音效播放的AudioSource
    private AudioSource effectMusicAudioSource;

    private bool isEffectMusicOn = true;


    private bool isBgMusicOn = true;

    public AudioManager()
    {
        AudioSource[] audioSources = GameManager.Instance.GetComponents<AudioSource>();
        bgMusicAudioSource = audioSources[0];
        effectMusicAudioSource = audioSources[1];
    }

    #region BgMusic
    //切换播放背景音乐
    public void PlayBgMusic(AudioClip audioClip)
    {
        if(!bgMusicAudioSource.isPlaying||bgMusicAudioSource.clip != audioClip)
        {
            bgMusicAudioSource.clip = audioClip;
            bgMusicAudioSource.Play();
        }
    }

    //关闭背景音乐
    public void CloseBgMusic()
    {
        bgMusicAudioSource.Stop();
    }

    //播放当前背景音乐
    public void OpenBgMusic()
    {
        bgMusicAudioSource.Play();
    }

    public void CloseOrOpenBGMusic()
    {
        isBgMusicOn = !isBgMusicOn;
        if(isBgMusicOn)
        {
            OpenBgMusic();
        }
        else
        {
            CloseBgMusic();
        }
    }
    #endregion

    #region EffectMusic
    //播放音效
    public void PlayEffectMusic(AudioClip audioClip)
    {
        if(isEffectMusicOn)
        {
            effectMusicAudioSource.PlayOneShot(audioClip);
        }
    }

    public void OpenEffectMusic()
    {
        effectMusicAudioSource.Play();
    }

    public void CloseEffectMusic()
    {
        effectMusicAudioSource.Stop();
    }

    public void CloseOrOpenEffectMusic()
    {
        isEffectMusicOn = !isEffectMusicOn;
        if(isEffectMusicOn)
        {
            OpenEffectMusic();
        }
        else
        {
            CloseEffectMusic();
        }
    }

    //按钮音效
    public void PlayButtonAudioClip()
    {
        PlayEffectMusic(GameManager.Instance.GetAudioClip("Main/Button"));
    }

    //翻书音效
    public void PlayPagingAudioClip()
    {
        PlayEffectMusic(GameManager.Instance.GetAudioClip("Main/Paging"));
    }
    #endregion
}
