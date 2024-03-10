using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SettingLevel
{
    PingYong=0,
    QiaoSi=1,
    GuiCai=2,
    DuTe=3,
}
public class AbstractSetting : MonoBehaviour
{

    [HideInInspector]public SettingLevel level = SettingLevel.PingYong;
    [HideInInspector] public string settingName;
    [HideInInspector] public string info;
    [HideInInspector] public List<string> lables;
    [HideInInspector] public CampEnum camp;

    public bool hasAdd = false;
    /// <summary>
    /// �ڼ����ı��������仯��ʱ��ִ�����
    /// </summary>
    /// <returns></returns>
    /// 

    public virtual void Awake()
    {
        
    }
    public virtual void Start()
    {
        
    }
    public virtual void Init()
    {
        
    }
}
