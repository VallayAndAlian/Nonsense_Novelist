using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��Ҫ�����
/// </summary>
public class YaoHaiZhenCe : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "Ҫ�����";
        res_name = "yaohaizhence";
        info = "�ҿ���ÿ�ι����Ĺ�����Ч����30%���ʷ�����5%���ʱ�Ϊ4��";
        lables = new List<string> { "��ɫ", "������Ч" };
        hasAdd = false;
        
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<DiKaDe>();
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
