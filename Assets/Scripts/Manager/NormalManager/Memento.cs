using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Memento
{
    public void SaveByJson()
    {
        PlayerManager playerManager = GameManager.Instance.playerManager;
        //Test
        //string filePath = Application.streamingAssetsPath + "/Json/Player/TestPlayerManager.json";
        string filePath = Application.streamingAssetsPath + "/Json/Player/PlayerManager.json";
        string jsonStr = JsonMapper.ToJson(playerManager);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(jsonStr);
        sw.Close();
    }

    public PlayerManager LoadByJson()
    {
        PlayerManager playerManager = new PlayerManager();
        string filePath = "";

        if(GameManager.Instance.initPlayerManager)
        {
            filePath = Application.streamingAssetsPath + "/Json/Player/playerManagerInitData.json";
        }
        else
        {
            //Test
           //filePath = Application.streamingAssetsPath + "/Json/Player/TestPlayerManager.json";
            filePath = Application.streamingAssetsPath + "/Json/Player/PlayerManager.json";
        }

        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            sr.Close();

            playerManager = JsonMapper.ToObject<PlayerManager>(jsonStr);
        }
        else
        {
            Debug.Log(filePath + "读取失败");
        }
        return playerManager;
    }
}
