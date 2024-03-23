using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingList : MonoBehaviour
{
    [Header("�����������壨�ֶ����ã�")]
    public Transform groupL;
    public Transform groupR;
    [Header("�趨Ԥ���壨�ֶ����ã�")]
    public GameObject pingYong;
    public GameObject guiCai;
    public GameObject qiaoSi;

    public void RefreshList()
    {
        //�Ȱ����еĻ���ȥ
        for(int i= groupL.childCount-1; i>0;i--)
        {
            PoolMgr.GetInstance().PushObj(groupL.GetChild(i).gameObject.name, groupL.GetChild(i).gameObject); 
        }
        for (int i = groupR.childCount - 1; i > 0; i--)
        {
            PoolMgr.GetInstance().PushObj(groupR.GetChild(i).gameObject.name, groupR.GetChild(i).gameObject);
        }
        //�ٰ����б�ˢ��
        foreach (var set in GameMgr.instance.settingL)
        {
            GameObject obj = pingYong;
            switch (set.level)
            {
                case SettingLevel.PingYong: { obj = pingYong; } break;
                case SettingLevel.GuiCai: { obj = guiCai; } break;
                case SettingLevel.QiaoSi: { obj = qiaoSi; } break;
              
            }
            PoolMgr.GetInstance().GetObj(obj, (_obj)=>
            {
                _obj.transform.parent = groupL;
                _obj.transform.localPosition = Vector3.zero;
                _obj.transform.localScale = Vector3.one;
                _obj.GetComponentInChildren<Text>().text = set.settingName;
            });
        }

        foreach (var set in GameMgr.instance.settingR)
        {
            GameObject obj = pingYong;
            switch (set.level)
            {
                case SettingLevel.PingYong: { obj = pingYong; } break;
                case SettingLevel.GuiCai: { obj = guiCai; } break;
                case SettingLevel.QiaoSi: { obj = qiaoSi; } break;

            }
            PoolMgr.GetInstance().GetObj(obj, (_obj) =>
            {
                _obj.transform.parent = groupR;
                _obj.transform.localPosition = Vector3.zero;
                _obj.transform.localScale = Vector3.one;
                _obj.GetComponentInChildren<Text>().text = set.settingName;
            });
        }
    }
}
