using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI������
/// UI���̳д��༴�ɻ�ȡMono��֧�ֺͳ��ø��๦��
/// </summary>
/// <typeparam name="T">�������</typeparam>
public abstract class BasePanel : MonoBehaviour 
{


    //�������͸���ȵ����
    CanvasGroup canvasGroup;
    //��ʾ����Ƿ����صı�ʶ
    private bool isShow = true;
    //UI������ʾ/�����ٶ�
    private float showSpeed = 5f;
    //��������Ļص�
    UnityAction hideCallback = null;

    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        //��ʼ��Panel��CanvasGroup���
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }

        //��ò��洢�������
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<RawImage>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
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


    /// <summary>
    /// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        for (int i = 0; i < controls.Length; ++i)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            //����ǰ�ť�ؼ�
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });

            }
            //����ǵ�ѡ����߶�ѡ��
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            //����ǵ�ѡ����߶�ѡ��
            else if (controls[i] is Slider)
            {
                (controls[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }

        }
    }


    public void SetMouseOnEffect(UIBehaviour uiName, float scaleAmount)
    {
        Vector3 _v = new Vector3(scaleAmount, scaleAmount, scaleAmount);
        UIManager.AddCustomEventListener(uiName, EventTriggerType.PointerEnter, (data) =>
        {
            uiName.gameObject.transform.localScale += _v;
        });

        UIManager.AddCustomEventListener(uiName, EventTriggerType.PointerExit, (data) =>
        {
            uiName.gameObject.transform.localScale -= _v;
        });
    }

    /// <summary>
    /// �õ���Ӧ���ֵĶ�Ӧ�ؼ��ű�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; ++i)
            {
                if (controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            }
        }

        return null;
    }
    protected virtual void OnClick(string btnName)
    {

    }


    protected virtual void OnValueChanged(string toggleName, bool value)
    {

    }

    protected virtual void OnValueChanged(string sliderName, float value)
    {

    }
}
