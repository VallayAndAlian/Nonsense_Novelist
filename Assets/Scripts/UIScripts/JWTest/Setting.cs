using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在设定预制体上面
/// </summary>
public class Setting : MonoBehaviour
{
    [Header("品质概率")]
    [Header("平庸")] public int pingYong = 60;
    [Header("巧思")] public int qiaoSi = 30;
    [Header("鬼才")] public int guiCai = 10;
    
    void Start()
    {
        Quality();
    }
    /// <summary>
    /// 随机出一个品质，并随机其中三个具体设定
    /// </summary>
    void Quality()
    {
        //概率抽取                
        int numx = Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;

        //从品质中抽取三个设定（设定写完之后再完成）

    }
    
}
