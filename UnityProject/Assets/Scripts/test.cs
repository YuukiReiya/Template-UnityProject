using API.Process;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("a")]
    private void A()
    {
        ProcessBuilder process = new ProcessBuilder();
        //process.SetPluginsPath();
        //process.Test();
        string buf = string.Empty;
        //SymbolicLink.MakeSymbolicLink("C:/テスト_リンク", Application.dataPath, buf);

        SymbolicLink.CreatePluginsLink();

        //Debug.Log(":"+System.IO.Directory.GetCurrentDirectory());
        //string str = string.Empty;
        //string str = "username";
        //Debug.Log(":" + Environment.UserName);
        //Debug.LogWarning(str);
        //foreach(var i in System.Environment.GetEnvironmentVariables())
        //{
        //    Debug.LogError(i);
        //}
    }
}
