using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:��ʴ
/// </summary>
public class FuShi: AbstractBuff
{
    static public string s_description = "����1 <sprite name=\"def\">���ɵ���";
    static public string s_wordName = "��ʴ";

    override protected void Awake()
    {
        base.Awake();
        buffName = "��ʴ";
        description = "����1<sprite name=\"def\">���ɵ���";
        
        book = BookNameEnum.allBooks;
        chara.def -= 1;
        isBad = true;
    }

    private void OnDestroy()
    {
        base.OnDestroy();
        chara.def += 1;
    }

}
