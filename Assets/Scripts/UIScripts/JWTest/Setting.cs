using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����趨Ԥ��������
/// </summary>
public class Setting : MonoBehaviour
{
    [Header("Ʒ�ʸ���")]
    [Header("ƽӹ")] public int pingYong = 60;
    [Header("��˼")] public int qiaoSi = 30;
    [Header("���")] public int guiCai = 10;
    
    void Start()
    {
        Quality();
    }
    /// <summary>
    /// �����һ��Ʒ�ʣ�������������������趨
    /// </summary>
    void Quality()
    {
        //���ʳ�ȡ                
        int numx = Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;

        //��Ʒ���г�ȡ�����趨���趨д��֮������ɣ�

    }
    
}
