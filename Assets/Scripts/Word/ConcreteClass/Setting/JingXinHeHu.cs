using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ĺǻ�
/// </summary>
public class JingXinHeHu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "���ĺǻ�";
        res_name = "jingxinhehu";
        info = "�ɱ��弧�����ݷ������Ĺ��ϣ���������+30";
        lables = new List<string> { "��ɫ" };
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<BeiLuoJi>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
