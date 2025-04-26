using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����Monoģ�����ģ��
/// ͨ��������װMonoController��:ʵ��֡������ӹ���
/// ���ⲿ�ṩ���֡���º����ķ���
/// ���ⲿ�ṩ���Э�̵ķ���
/// </summary>
public class MonoManager : SingletonBase<MonoManager>
{
    public MonoController controller;

    //������֤��MonoController��Ψһ��
    //��һ�ι���ʱ�ڳ���������һ������MonoController�ű��Ķ���
    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    /// <summary>
    /// ���ⲿ�ṩ�������֡�����¼�
    /// </summary>
    /// <param name="fun">��Ҫ֡���µ��߼�</param>
    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    /// <summary>
    /// �ṩ���ⲿ�����Ƴ�֡�����¼�
    /// </summary>
    /// <param name="fun">��Ҫ֡���µ��߼�</param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    /// <summary>
    /// ����Э�̵ķ���
    /// </summary>
    /// <returns>Э�̺�����������</returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    /// <summary>
    /// ֹͣЭ�̵ķ���
    /// </summary>
    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }
    public void StopCoroutine(string methodName)
    {
        controller.StopCoroutine(methodName);
    }

}
