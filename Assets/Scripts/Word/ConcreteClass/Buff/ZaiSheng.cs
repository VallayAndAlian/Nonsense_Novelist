using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class ZaiSheng : AbstractBuff
{
    static public string s_description = "�����ָ�+10";
    static public string s_wordName = "����";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "����";
        description = "�����ָ�+10";
        book = BookNameEnum.allBooks;  

        upup = 3;

        base.Awake();
       
        chara.cure += 10f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.cure -= 10f;
    }

}
