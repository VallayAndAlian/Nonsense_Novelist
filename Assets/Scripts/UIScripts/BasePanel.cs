using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI������
/// UI���̳д��༴�ɻ�ȡMono��֧�ֺͳ��ø��๦��
/// </summary>
/// <typeparam name="T">�������</typeparam>
public abstract class BasePanel<T> : MonoBehaviour where T:class
{
    /// <summary>
    /// ��������,��������ĵ��ú���Ϣ����
    /// </summary>
    public static T Instance;

    //�������͸���ȵ����
    CanvasGroup canvasGroup;
    //��ʾ����Ƿ����صı�ʶ
    private bool isShow = true;
    //UI������ʾ/�����ٶ�
    private float showSpeed = 5f;
    //��������Ļص�
    UnityAction hideCallback = null;

    private void Awake()
    {
        Instance = this as T;
        //��ʼ��Panel��CanvasGroup���
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ��ʼ������
    /// ��������Start�߼�,���������ֶγ�ʼ����UI�¼���
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// ��ʾ���
    /// �������д,��������ڸ���,ÿ��ˢ��ҳ����Ҫ���µ�����
    /// </summary>
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
        isShow = true;
        //���������͸����Ϊ0,������Ϊ1
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// �������
    /// �ɴ����޲�ί��,����������������Ԥ�����
    /// </summary>
    /// <param name="callBack">��������Ļص�</param>
    public virtual void Hide(UnityAction callBack = null)
    {
        isShow = false;
        //���������͸����Ϊ1,������Ϊ0
        canvasGroup.alpha = 1;
        hideCallback += callBack;
    }
    
    /// <summary>
    /// update֡���·���:
    /// �������д��base()�߼����ṩ�¹���
    /// </summary>
    protected virtual void Update()
    {
        if (isShow == true && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += showSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if (isShow == false && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= showSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                this.gameObject.SetActive(false);   
                //���غ����ί���Ƴ�������
                hideCallback?.Invoke();
            }
        }
    }
    
}
