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
       
        buffName = "����";
        description = "���ܵ������˺�ʱ�ָ�����������ħ����������и���״̬";
        book = BookNameEnum.EgyptMyth;
        maxTime = 100;

        base.Awake();

     
        chara.reLifes ++;;
   
 
    }

    private void OnDestroy()
    {
       
        chara.reLifes --;     chara.DeleteBadBuff(100);
        base.OnDestroy();
    }
}
