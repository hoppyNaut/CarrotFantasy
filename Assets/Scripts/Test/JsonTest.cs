using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonTest : MonoBehaviour
{
    private Phone phone;

    void Start()
    {
        //phone = new Phone()
        //{
        //    appNum = 3,
        //    phoneState = true,
        //    appList = new List<App>()
        //    {
        //        new App()
        //        {
        //            appName = "QQ",
        //            appSize = 200.0,
        //            isFavour = true,
        //            useTimeList = new List<int>()
        //            {
        //                8,12,16
        //            }
        //        },
        //        new App()
        //        {
        //            appName = "WeChat",
        //            appSize = 150.3,
        //            isFavour = true,
        //            useTimeList = new List<int>()
        //            {
        //                9,10,11
        //            }
        //        },
        //    },

        //};
        //SaveByJson();

        phone = LoadByJson();
    }


    //存储Json信息文件
    private void SaveByJson()
    {
        string filePath = Application.dataPath + "/Resources/Json/App.json";
        //使用JsonMapper将信息类对象转换成Json格式的字符串
        string jsonStr = JsonMapper.ToJson(phone);
        //创建文件写入流
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(jsonStr);
        //关闭写入流
        sw.Close();
    }

    //读取Json信息文件
    private Phone LoadByJson()
    {
        Phone phoneGo = new Phone();
        string filePath = Application.dataPath + "/Resources/Json/App.json";
        if(File.Exists(filePath))
        {
            //创建文件读取流
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            sr.Close();

            phoneGo = JsonMapper.ToObject<Phone>(jsonStr);
             return phoneGo;
        }
        else
        {
            Debug.Log("存档失败");
            return null;
        }
    }

    
}
