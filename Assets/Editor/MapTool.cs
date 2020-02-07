using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if Tool

[CustomEditor(typeof(MapMaker))]
public class MapTool : Editor
{
    private MapMaker mapMaker;

    //关卡文件列表
    private List<FileInfo> fileList = new List<FileInfo>();
    //关卡文件名列表
    private string[] fileNameList;

    //当前编辑的关卡索引
    private int selectIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
   
        if(Application.isPlaying)
        {
            mapMaker = MapMaker.Instance;

            EditorGUILayout.BeginHorizontal();
            //下拉列表
            if(fileNameList != null)
            {
                int currentIndex = EditorGUILayout.Popup(selectIndex, fileNameList);
                if (currentIndex != selectIndex)
                {
                    selectIndex = currentIndex;

                    //实例化地图
                    mapMaker.InitMap();
                    //加载当前的选择文件
                    mapMaker.InitLevelInfo(fileNameList[selectIndex]);
                }
            }
            if (GUILayout.Button("读取关卡文件列表"))
            {
                //读取关卡列表
                LoadLevelFiles();
                foreach (string s in fileNameList)
                {
                    Debug.Log(s);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("恢复地图编辑器默认状态"))
            {
                mapMaker.RecoverTowerPoint();
            }

            if(GUILayout.Button("清除怪物路点"))
            {
                mapMaker.ClearMonsterPath();
            }
            EditorGUILayout.EndHorizontal();

            if(GUILayout.Button("保存当前关卡数据文件"))
            {
                mapMaker.SaveLevelInfoByJson();
            }
        }
    }

    //加载关卡数据文件
    private void LoadLevelFiles()
    {
        ClearFileList();
        fileList = GetFileList();
        fileNameList = GetFileNames(fileList);
    }

    //清除文件列表
    private void ClearFileList()
    {
        fileList.Clear();
        selectIndex = -1;
    }

    //读取关卡列表
    private List<FileInfo> GetFileList()
    {
        string[] files = Directory.GetFiles(Application.dataPath + "/Resources/Json/Level/","*.json");

        List<FileInfo> list = new List<FileInfo>();
        for(int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            list.Add(file);
        }
        return list;
    }

    //获取关卡文件名
    private string[] GetFileNames(List<FileInfo> files)
    {
        List<string> names = new List<string>();
        foreach (FileInfo file in files)
        {
            names.Add(file.Name);
        }
        return names.ToArray();
    }
}

#endif