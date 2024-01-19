using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff���ߵ�
/// </summary>
public class DianDao : AbstractBuff
{
    static public string s_description = "������ǰ��<sprite name=\"atk\">��<sprite name=\"psy\">��������ָ�";
    static public string s_wordName = "�ߵ�";

    override protected void Awake()
    {
        base.Awake();
        buffName = "�ߵ�";
        description = "������ǰ��<sprite name=\"atk\">��<sprite name=\"psy\">��������ָ�";
        book = BookNameEnum.allBooks;
        float record = chara.atk;
        chara.atk = chara.psy;
        chara.psy = record;
        upup = 1;
        isBad = true;
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        float record = chara.atk;
        chara.atk = chara.psy;
        chara.psy = record;
    }
}
