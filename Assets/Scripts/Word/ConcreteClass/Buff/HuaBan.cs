using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:����
/// </summary>
public class HuaBan: AbstractBuff
{   
    static public string s_description = "����1<sprite name=\"psy\">";
    static public string s_wordName = "����";
    override protected void Awake()
    {
        
        buffName = "����";
        description = "����1<sprite name=\"psy\">";
        book = BookNameEnum.allBooks; 
         maxTime = 200;
        base.Awake();

        chara.psy += 1;
       

        
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        chara.psy -= 1;
    }
}
