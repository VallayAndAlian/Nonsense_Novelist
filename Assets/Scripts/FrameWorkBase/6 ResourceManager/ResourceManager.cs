using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��Դ����ģ�� [����Resources]
/// </summary>
public class ResourceManager : SingletonBase<ResourceManager>
{
    /// <summary>
    /// ͬ��������Դ
    /// ������ΪGameObject������ʵ����������
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="name">��Դ��</param>
    /// <returns></returns>
    public T LoadResource<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    /// <summary>
    /// �첽������Դ
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="name">��Դ��</param>
    /// <param name="collback">�ص�����,����Ϊ���ص���Դ,������ΪGameObject������ʵ��������Ϊ����</param>
    public void LoadResourceAsync<T>(string name,UnityAction<T> collback) where T : Object
    {
        MonoManager.GetInstance().StartCoroutine(LoadReourceByCoroutine(name, collback));
    }

    /// <summary>
    /// �첽������Դ��Э��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="collback"></param>
    /// <returns></returns>
     private IEnumerator LoadReourceByCoroutine<T>(string name, UnityAction<T> collback) where T : Object
    {
        ResourceRequest resReq = Resources.LoadAsync<T>(name);
        yield return resReq;
        if (resReq.asset is GameObject)
            collback(GameObject.Instantiate(resReq.asset) as T);
        else
            collback(resReq.asset as T);
    }
}
