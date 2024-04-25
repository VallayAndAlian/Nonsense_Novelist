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
        level = SettingLevel.GuiCai;
        settingName = "�к�����";
        res_name = "canheyusheng";
        info = "������ӵ�еĶ����ͷ���������+2������������ߵĶ��ʣ�����������ٶȷ���";
        lables = new List<string> { "��ɫ", "����" };
        hasAdd = false;
        
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            chara.event_AddVerb += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractVerbs highestverb)
    {
        int cd = 0;
        foreach (var verb in chara.GetComponents<AbstractVerbs>())
        {
            verb.needCD += 2;
            if (verb.needCD > cd) {
                highestverb = verb;//������ߵĶ���
                cd = verb.needCD;
            }
            
        }
        //����������ٶȷ���������

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddVerb -= Effect;
    }
}
