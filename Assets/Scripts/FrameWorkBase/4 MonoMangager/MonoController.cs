
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono����ģ��Ĺ���ģ��
/// ʵ�����ⲿ�ṩ���̳�Mono��ļ�������
/// ���̳�Mono���������ṩ��Э�̽ӿں���
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;

    void Update()
    {
        if (updateEvent != null) 
        {
            //ִ���¼�
            updateEvent(); 
        }
    }

    /// <summary>
    /// �ṩ���ⲿ�������֡�����¼�
    /// </summary>
    /// <param name="fun">��Ҫ֡���µ��߼�����</param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// �ṩ���ⲿ�����Ƴ�֡�����¼�
    /// </summary>
    /// <param name="fun">��Ҫ֡���µ��߼�����</param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

}
