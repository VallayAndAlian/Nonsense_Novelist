using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����ɫ����ʳ���Ż�
/// </summary>
public class ShiPuYouHua : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ʳ���Ż�";
        info = "����Ա��ͨ�������������ܡ��ĸ��ʣ�ÿ�ι�������20%���ɹ�������ָ�Ϊ20%";
        lables = new List<string> { "��ɫ","����"};
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
}
