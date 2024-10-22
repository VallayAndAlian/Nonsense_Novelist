using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI面板基类
/// UI面板继承此类即可获取Mono的支持和常用父类功能
/// </summary>
/// <typeparam name="T">子类面板</typeparam>
public abstract class BasePanel : MonoBehaviour 
{


    //管理面板透明度的组件
    CanvasGroup canvasGroup;
    //表示面板是否隐藏的标识
    private bool isShow = true;
    //UI渐变显示/隐藏速度
    private float showSpeed = 5f;
    //隐藏面板后的回调
    UnityAction hideCallback = null;

    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        //初始化Panel的CanvasGroup组件
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }

        //获得并存储所有组件
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
    /// 初始化函数
    /// 代替子类Start逻辑,建议用于字段初始化和UI事件绑定
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 显示面板
    /// 子类可重写,建议可用于更新,每次刷新页面需要更新的数据
    /// </summary>
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
        isShow = true;
        //先设置面板透明度为0,待渐变为1
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// 隐藏面板
    /// 可传入无参委托,建议可用于销毁面板预制体等
    /// </summary>
    /// <param name="callBack">隐藏面板后的回调</param>
    public virtual void Hide(UnityAction callBack = null)
    {
        isShow = false;
        //先设置面板透明度为1,待渐变为0
        canvasGroup.alpha = 1;
        hideCallback += callBack;
        
    }
    
    /// <summary>
    /// update帧更新方法:
    /// 子类可重写在base()逻辑上提供新功能
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
                //隐藏后调用委托移除面板对象
                hideCallback?.Invoke();
            }
        }
    }


    /// <summary>
    /// 找到子对象的对应控件
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
            //如果是按钮控件
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });

            }
            //如果是单选框或者多选框
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            //如果是单选框或者多选框
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
    /// 得到对应名字的对应控件脚本
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
