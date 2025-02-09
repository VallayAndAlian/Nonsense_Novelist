using System;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// �սӿ�ʹ��,д����
/// </summary>
interface IEventInfo{   }

/// <summary>
/// ���ڴ洢�¼�����
/// �����¼��洢�ص����õ�ί������
/// ʹ�ñ���װ�����
/// </summary>
/// <typeparam name="T">��������</typeparam>
public class EventInfo<T>: IEventInfo
{
    public UnityAction<T> actionos;
    public EventInfo(UnityAction<T> action)
    {
        actionos += action;
    }
}

/// <summary>
/// û�в�����ί����Ϣ
/// </summary>
public class EventInfo: IEventInfo
{
    public UnityAction actionos;
    public EventInfo(UnityAction action)
    {
        actionos += action;
    }
}

/// <summary>
/// �¼�����
/// ��������
/// �۲���ģʽ
/// </summary>
public class EventCenter : SingletonBase<EventCenter>
{
    /// <summary>
    /// �¼��洢���� <�¼�Ψһ��ʶ(/�¼�����),�����¼���ί�к���>
    /// ί�п��Դ洢�������,����ʱ��������Ӧ
    /// UnityAction<object> ��һ��������ί��,�¼��������ڴ�����������
    /// </summary>
    Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// ����¼�����(��һ������)
    /// ע���¼���ʶ�����ظ�
    /// ������Startһ��ĳ�ʼ�������е���
    /// </summary>
    /// <param name="name">�¼�����</param>
    /// <param name="action">��Ҫ�����ʱ��</param>
    public void AddEventListener<T>(string name , UnityAction<T> action)
    {

        //��û�д��¼�ʱ,����һ��
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
        //�������һ��
        else
        {
            //ת�����¼���Ϣ����ӻص�
            (eventDic[name] as EventInfo<T>).actionos += action;
        }
    }

    /// <summary>
    /// ����¼�����(û�в���)
    /// </summary>
    /// <param name="name">�¼�����</param>
    /// <param name="action">�¼��ص�</param>
    public void AddEventListener(string name, UnityAction action)
    {
        //��û�д��¼�ʱ
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo(action));
        }
        //�������һ��
        else
        {
            //ת�����¼���Ϣ����ӻص�
            (eventDic[name] as EventInfo).actionos += action;
        }
    }

    /// <summary>
    /// �¼�����(һ������)
    /// </summary>
    /// <param name="name">�¼�����</param>
    public void EventTrigger<T>(string name,T var)
    {
        if (eventDic.ContainsKey(name))
        {
            //���ֵ���ȡ����ʶ��Ӧ�Ļص�,��Ϊ����ִ��
            (eventDic[name] as EventInfo<T>).actionos?.Invoke(var);
        }
    }

    /// <summary>
    /// �¼�����(û�в���)
    /// </summary>
    /// <param name="name">�¼�����</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            //���ֵ���ȡ����ʶ��Ӧ�Ļص�,��Ϊ����ִ��
            (eventDic[name] as EventInfo).actionos?.Invoke();
        }
    }

    /// <summary>
    /// �Ƴ��¼�����һ�������ļ�������
    /// ������OnDestory�е���
    /// </summary>
    /// <param name="name">�¼���ʶ</param>
    /// <param name="action">�Ѿ���ӵļ�������</param>
    public void RemoveEventListener<T>(string name,UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actionos -= action;
        }
    }

    /// <summary>
    /// �Ƴ��¼���û�в����ļ�������
    /// </summary>
    /// <param name="name">�¼���ʶ</param>
    /// <param name="action">�Ѿ���ӵļ�������</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actionos -= action;
        }
    }

    /// <summary>
    /// ��������¼�
    /// �����л�ʱ��������¼�����
    /// </summary>
    public void ClearEvent()
    {
        eventDic.Clear();
    }
    
}
