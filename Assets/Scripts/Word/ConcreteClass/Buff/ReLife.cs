using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:����
/// </summary>
public class ReLife : AbstractBuff
{
    static public string s_description = "���ܵ������˺�ʱ�ָ�����������ħ����������и���״̬";
    static public string s_wordName = "����";
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        description = "���ܵ������˺�ʱ�ָ�����������ħ����������и���״̬";
        book = BookNameEnum.EgyptMyth;
        chara.reLifes ++;
        //������и���״̬
    }

    private void OnDestroy()
    {
        chara.reLifes --;
        base.OnDestroy();
    }
}
