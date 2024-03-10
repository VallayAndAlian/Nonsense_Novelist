using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��������֮Ů
/// </summary>
public class SuoDuoMaZhiNv : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "������֮Ů";
        info = "ɯ����������־����20�Ľ�ɫ����10%�������2s����";
        lables = new List<string> { "��ɫ", "����" };
        hasAdd = false;
        Init();
    }

    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<ShaLeMei>();
        if (chara != null)
        {
            chara.event_AttackA += Effect;//��������
        }
        hasAdd = true;
    }
    void Effect()
    {
        foreach (var it in chara.myState.aim)
        {
            int num = Random.Range(1, 11);
            if (it.san < 20 && num == 1)
            {
                //��buff�Ļ�������
                var buff= it.gameObject.AddComponent<FuHuo>();
                buff.maxTime = 2;
            }
        }

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AttackA -= Effect;
    }
}
