using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using TMPro;
using UnityEngine;
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
    void Start()
    {
        Quality();
    }
    /// <summary>
    /// �����һ��Ʒ�ʣ�������������������趨
    /// </summary>
    void Quality()
    {
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
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ppy.settingName;//����
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ppy.info;//����
                        //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //���ɱ�ǩ
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < ppy.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + ppy.lables[i]);
                        }
                    });
                }
                return;
            case 1:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(qs, (a) => {
                        Type qs0 = AllSkills.RandomQS();
                        a.AddComponent(qs0);
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //��ȡ�����ƽӹ��������
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().settingName;//����
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().info;//����
                                                                                                                   //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //���ɱ�ǩ
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < a.GetComponent<AbstractSetting>().lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + a.GetComponent<AbstractSetting>().lables[i]);
                        }
                    });
                }
                return;
            case 2:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(gc, (a) => {
                        Type gc0 = AllSkills.RandomQS();
                        a.AddComponent(gc0);
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //��ȡ�����ƽӹ��������
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().settingName;//����
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().info;//����
                                                                                                                   //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //���ɱ�ǩ
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < a.GetComponent<AbstractSetting>().lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + a.GetComponent<AbstractSetting>().lables[i]);
                        }
                    });
                }
                return;
        }
    }
    private Var button;
    public void ButtonDown()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
    }
    public void EndButton()
    {
        
    }
}
