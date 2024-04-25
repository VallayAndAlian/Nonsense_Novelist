using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨������ר��
/// </summary>
public class DongWuZhuanJia : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "����ר��";
        res_name = "dongwuzhuanjia";
        info = "����Ա����ͨ��������ʱ����ӻ�ѪЧ������";
        lables = new List<string> { "��ɫ", "���" };
        hasAdd = false;
       
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
