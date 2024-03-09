using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ܡ�����Ƶ���
/// </summary>
public class ChaoPinChongJi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Start()
    {
        base.Start();
        level = SettingLevel.PingYong;
        name = "��Ƶ���";
        info = "��ɫÿӵ��1�����㣬��ɵ������˺�+3%";
        lables = new List<string> { "����" };
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<SiYangYuan>();
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
