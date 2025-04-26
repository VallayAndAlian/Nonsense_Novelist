using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �����л�����
/// </summary>
public class SceneChangeManager : SingletonBase<SceneChangeManager>
{
    /// <summary>
    /// ͬ�����س���
    /// ����MonoManagerΪ����Monoģ��������ڿ���Э�̵�
    /// </summary>
    /// <param name="name">��������</param>
    /// <param name="completed">�������������Ҫ��������</param>
    public void LoadScene(string name,UnityAction completed)
    {
        //���س���
        SceneManager.LoadScene(name);
        //ִ�г������������������
        completed.Invoke();
    }

    /// <summary>
    /// �첽���س�������
    /// ����ͨ���¼�������EventCenter ������Ϊ"����������"���¼�
    /// ���к��м��ؽ������ڸ��½�����
    /// </summary>
    /// <param name="name">��������</param>
    /// <param name="completed">��������������</param>
    private void LoadSceneAsync(string name, UnityAction completed)
    {
        MonoManager.GetInstance().controller.StartCoroutine(LoadSceneByCoroutine(name,completed));
    } 

    //Э�̼��س����ķ���
    private IEnumerator LoadSceneByCoroutine(string name, UnityAction completed)
    {
        AsyncOperation  ao = SceneManager.LoadSceneAsync(name);
       
        //ʹ���첽���ز������ж�,������ɲ���������
        while (!ao.isDone)
        {
            //�ַ����½��������¼�,�ⲿ���Բο�����,����Ϊ��ǰ����
            //�ַ�"����������"�¼�,������ع���
            EventCenter.GetInstance().EventTrigger("����������",ao.progress);
            //ao.progress���ؽ��� 0-1
            yield return ao.progress;
        }
        //������ɺ�ִ�еķ���
        completed();
    }
}
