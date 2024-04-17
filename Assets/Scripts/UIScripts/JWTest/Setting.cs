using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �����趨SettingԤ��������
/// </summary>
public class Setting : MonoBehaviour
{
    [Header("Ʒ�ʸ���")]
    [Header("ƽӹ")] public int pingYong = 60;
    public GameObject py;
    [Header("��˼")] public int qiaoSi = 30;
    public GameObject qs;
    [Header("���")] public int guiCai = 10;
    public GameObject gc;

    public GameObject logo;
    private PointerEventData ed;
    private BaseEventData baseEventData;

    //����һ�߼����趨����true��false
    bool isLeft = true;
    void Start()
    {

        if (!CharacterManager.instance.pause)
            CharacterManager.instance.pause = true;
        Quality();
    }
    /// <summary>
    /// �����һ��Ʒ�ʣ�������������������趨
    /// </summary>
    void Quality()
    {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text= "ѡ��һ���趨";
        this.transform.GetChild(3).GetComponent<Button>().interactable = false;
        //���ʳ�ȡ                
        int numx = UnityEngine.Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;

        //��Ʒ���г�ȡ�����趨���趨д��֮������ɣ�
        switch (numx)//���ȷ����ť��ʱ��push�����
        {
            case 0: //ƽӹ
                for(int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(py, (a) => {
                        Type py0 = AllSkills.RandomPY();
                        var ppy= a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //��ȡ�����ƽӹ��������
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/pingyong/"+ppy.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ppy.settingName;//����
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ppy.info;//����
                                                                                                //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj) => { PointerClick(a); });                                                                                                                                             //���ɱ�ǩ
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < ppy.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                        
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + ppy.lables[i]);
                            lg.GetComponent<Image>().SetNativeSize();
                            lg.transform.localScale = Vector3.one;
                        }
                    });
                }
                return;
            case 1:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(qs, (a) => {
                        Type qs0 = AllSkills.RandomQS();
                        var qss= a.AddComponent(qs0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //��ȡ�������˼��������
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/qiaosi/" + qss.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qss.settingName;//����
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = qss.info;//����
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, (obj)=> { PointerClick(a); });                                                                                                                                             //���ɱ�ǩ

                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < qss.lables.Count; i++)//���ɱ�ǩ
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + qss.lables[i]);
                        }
                    });
                }
                return;
            case 2:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(gc, (a) => {
                        Type gc0 = AllSkills.RandomGC();
                        var gcc= a.AddComponent(gc0)as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //��ȡ����Ĺ�ſ�������
                        a.GetComponent<Image>().sprite = Resources.Load<Sprite>("settingSprite/guicai/" + gcc.res_name);
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gcc.settingName;//����
                        a.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gcc.info;//����
                        AddPointerEvent(a.transform.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, ( data) => { PointerClick( a); });                                                                                                                                             //���ɱ�ǩ
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < gcc.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            lg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + gcc.lables[i]);
                        }
                    });
                }
                return;
        }
    }
  
    /// <param name="eventTrigger">��Ҫ����¼���EventTrigger</param>
    /// <param name="eventTriggerType">�¼�����</param>
    /// <param name="callback">�ص�����</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }
    private GameObject click = null;


    #region �ⲿ����¼�
    /// <summary>
    /// ѡ������һ���趨
    /// </summary>
    /// <param name="a">a�ǵ�������壨����eventTrigger��</param>
    void PointerClick(GameObject a)
    {
        if (click != a)//click���ǵ�ǰѡ�е�����
        {
            if (click != null)//click���ǿ�
                click.GetComponent<Image>().color = Color.white;
            a.GetComponent<Image>().color = Color.black;
            click = a;
        }
        else
        {
            a.GetComponent<Image>().color = Color.white;
            click = null;
        }
        if (click == null)//clickûѡ������
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "ѡ��һ���趨";
            this.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }
        else//clickѡ������
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "ȷ��";
            this.transform.GetChild(3).GetComponent<Button>().interactable = true;
        }
    }




    /// <summary>
    /// ���ѡ�а�ť
    /// </summary>
    public void ClickCheck()
    {
        if (CharacterManager.instance.pause)
            CharacterManager.instance.pause = false;
        ///��֪������һ�Ӽ����趨�G������д����
        if (isLeft)
        {
            GameMgr.instance.settingL.Add(click.GetComponent<AbstractSetting>());
            GameMgr.instance.settingPanel.RefreshList();
            Destroy(gameObject);
        }
        else
        {
            GameMgr.instance.settingR.Add(click.GetComponent<AbstractSetting>());
            GameMgr.instance.settingPanel.RefreshList();
            Destroy(gameObject);
        }
    }
    #endregion
}
