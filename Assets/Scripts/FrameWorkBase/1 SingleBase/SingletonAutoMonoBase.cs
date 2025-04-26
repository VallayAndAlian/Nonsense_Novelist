using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**˵��
 * ��Ϊ������һ��Ϊ������,�л�����ʱ����ʧȥ���ã��������Ѿ����˴���
 * ��Ϊ�ű��Ĺ��������ֶ����ߴ������
 * ������ʹ���˴�����ӽű��ķ�ʽ,���ڳ�������һ����ű�����ͬ�Ķ�����Ϊ�������
 * ʹ��ʱֻ��Ҫ�򵥵ļ̳м���
 * �����߼�����ʵ��,���Զ���֤��Ψһ��
 */

/// <summary>
/// �Զ���Mono��������
/// ��������������Զ���֤Ψһ��
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonAutoMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    
    /// <summary>
    /// ������Ϊ��ʱ,�򴴽���Ϸ����,���򲻴�������֤����
    /// </summary>
    /// <returns>���ص�������</returns>
    public static T GetInstance()
    {
        if (instance == null)
        {
            //�ڳ���������������󲢹��ؽű�
            GameObject obj = new GameObject();
            //���ö�������Ϊ������
            obj.name = typeof(T).ToString();
            //�����������Ƴ�
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}
