using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingList : MonoBehaviour
{
    [Header("左右组别基物体（手动设置）")]
    public Transform groupL;
    public Transform groupR;
    [Header("设定预制体（手动设置）")]
    public GameObject pingYong;
    public GameObject guiCai;
    public GameObject qiaoSi;

    public void RefreshList()
    {
        //先把所有的还回去
        for(int i= groupL.childCount-1; i>0;i--)
        {
            PoolMgr.GetInstance().PushObj(groupL.GetChild(i).gameObject.name, groupL.GetChild(i).gameObject); 
        }
        for (int i = groupR.childCount - 1; i > 0; i--)
        {
            PoolMgr.GetInstance().PushObj(groupR.GetChild(i).gameObject.name, groupR.GetChild(i).gameObject);
        }
        //再按照列表刷新
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
