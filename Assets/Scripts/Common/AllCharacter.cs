using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// ���н�ɫ��̬��
/// </summary>
public static class AllCharacter
{
    /// <summary>ȫ����ɫ</summary>
    public static List<Type> list_allchara = new List<Type>();

    static AllCharacter()
    {
        list_allchara.AddRange(new Type[] { typeof(Anubis),typeof(BeiLuoJi),typeof(DiKaDe), typeof(LinDaiYu), typeof(LiuGrandma),
            typeof(MuNaiYi),typeof(Rat),typeof(ShiLian), typeof(ShuiShou), typeof(SiYangYuan), typeof(ShaLeMei),typeof(WangXiFeng),
        
        });
    }
    /// <summary>
    /// ��̬������ɽ�ɫ
    /// </summary>
    public static Type CreateCharacter()
    {
        int number = UnityEngine.Random.Range(0, list_allchara.Count);
        return list_allchara[number];
    }
    /// <summary>
    /// ����ȫ����ɫ
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Type AllCharacters(int i)
    {
        return list_allchara[i];
    }
}
