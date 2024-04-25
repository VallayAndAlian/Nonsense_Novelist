//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��������ù
/// </summary>
public class JiNiTaiMei : AbstractSetting
{

    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "������ù";
        res_name = "jinitaimei";
        info = "���弧������ÿ���ͷŶ��ʣ�����һ���ѷ�0.2*��־";
        lables = new List<string> { "��ɫ", "��Ƶ" };
        hasAdd = false;

    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<BeiLuoJi>();
        if (chara != null)
        {
            chara.event_UseVerb += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractVerbs verb)
    {
        //��ȡ�ѷ����н�ɫ�������Լ���
        List<AbstractCharacter> a = CharacterManager.instance.GetFriend(chara.camp);
        int i = Random.Range(0, a.Count);
        int loopCount = 0;
        while (a[i].wordName == "���弧������"&&loopCount<50)
        {
            int j = Random.Range(0, a.Count);
            i = j;
            loopCount++;
        }
        //��ֻ�б��弧�Լ���������ѭ��Ĭ�ϻظ��Լ�Ѫ��
        a[i].BeCure(02 * a[i].san * a[i].sanMul, true, 0);
    }
    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_UseVerb -= Effect;
    }
}
