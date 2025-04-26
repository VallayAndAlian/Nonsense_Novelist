using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**˵��
 * ����ģʽ����������ͬ������,��֤ʵ��Ψһ
 * ���ڵ���ĳ���������Ǹ�Ψһ��ʵ��
 * ������������˵���ģʽ�Ĵ�����
 */

/// <summary>
/// [����ģʽ����]
/// �̳д��༴�ɽ������Ϊ����ģʽ
/// Ҫ����������й��췽��
/// </summary>
/// <typeparam name="T">�����������</typeparam>
public class SingletonBase<T> where T:class,new()
{
    //Ψһ��ʵ������
    private static T instance;
    //�Ǳ����,�����Ա��ϰ�ȫ
    //private SingletonBase() { }

    public static T GetInstance() 
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
