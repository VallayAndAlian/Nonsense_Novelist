using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// �趨��˶��˶��
/// </summary>
public class ShuoShuShuoShu :AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "˶��˶��";
        res_name = "shuoshushuoshu";
        info = "�������˫��������֮�������ɲ�ֵ*5%�˺�";
        lables = new List<string> { "��ɫ" };

        hasAdd = false;

      


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<Rat>();
        if (chara != null)
        {
            chara.event_AttackA += Effect;
        }
        hasAdd = true;
    }
    void Effect()
    {
        int chara_num= chara.GetComponents<AbstractItems>().Length;
        foreach(var it in chara.myState.aim)
        {
            int a = math.abs(it.GetComponents<AbstractItems>().Length - chara_num);
            it.BeAttack(AttackType.atk, a*0.05f * chara.atk * chara.atkMul, true, 0, chara);

        }

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AttackA -= Effect;
    }
}
