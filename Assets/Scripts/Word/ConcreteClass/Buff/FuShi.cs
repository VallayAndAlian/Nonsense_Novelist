using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:¸¯Ê´
/// </summary>
public class FuShi: AbstractBuff
{
    static public string s_description = "½µµÍ1 <sprite name=\"def\">£¬¿Éµþ¼Ó";
    static public string s_wordName = "¸¯Ê´";

    override protected void Awake()
    {
        base.Awake();
        buffName = "¸¯Ê´";
        description = "½µµÍ1<sprite name=\"def\">£¬¿Éµþ¼Ó";
        
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
