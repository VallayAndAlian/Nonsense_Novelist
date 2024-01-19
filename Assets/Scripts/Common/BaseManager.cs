using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.C#�� ���͵�֪ʶ
//2.���ģʽ�� ����ģʽ��֪ʶ
public class BaseManager<T> where T : new()
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}
