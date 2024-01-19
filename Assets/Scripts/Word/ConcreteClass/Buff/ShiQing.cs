using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:ʫ��
/// </summary>
public class ShiQing : AbstractBuff
{
    static public string s_description = "����20%<sprite name=\"psy\">";
    static public string s_wordName = "ʫ��";


    override protected void Awake()
    {
        base.Awake();
        buffName = "ʫ��";
        description = "����20%<sprite name=\"psy\">";
        book = BookNameEnum.HongLouMeng;
        chara.psyMul += 0.2f;

    }


    private void OnDestroy()
    {
        base.OnDestroy();
        chara.psyMul -= 0.2f;
    }
}
