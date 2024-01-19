using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：颠倒
/// </summary>
public class DianDao : AbstractBuff
{
    static public string s_description = "交换当前的<sprite name=\"atk\">和<sprite name=\"psy\">，结束后恢复";
    static public string s_wordName = "颠倒";

    override protected void Awake()
    {
        base.Awake();
        buffName = "颠倒";
        description = "交换当前的<sprite name=\"atk\">和<sprite name=\"psy\">，结束后恢复";
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
