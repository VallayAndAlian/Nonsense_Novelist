using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨���к�����
/// </summary>
public class CanHeYuSheng : AbstractSetting
{
    AbstractCharacter chara;
    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "�к�����";
        info = "������ӵ�еĶ����ͷ���������+2������������ߵĶ��ʣ�����������ٶȷ���";
        lables = new List<string> { "��ɫ", "����" };
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {
        int cd = 0;
        AbstractVerbs highestverb;//������ߵĶ���
        foreach (var verb in chara.GetComponents<AbstractVerbs>())
        {
            verb.needCD += 2;
            if (verb.needCD > cd) highestverb=verb;
        }
        //����������ٶȷ���������

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
