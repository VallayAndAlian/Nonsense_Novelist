using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>

public class HanLen : AbstractBuff
{
    static public string s_description = "�����ٶ�-30%";
    static public string s_wordName = "����";
    float record;
    override protected void Awake()
    {


        buffName = "����";
        description = "�����ٶ�-30%";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = false;
        upup = 2;

        base.Awake();

        chara.attackSpeedPlus -= 0.3f;



    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.attackSpeedPlus += 0.3f;
    }
}
