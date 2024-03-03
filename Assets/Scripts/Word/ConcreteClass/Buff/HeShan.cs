using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class HeShan : AbstractBuff
{
    static public string s_description = "<sprite name=\"san\">+20%";
    static public string s_wordName = "����";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "����";
        description = "<sprite name=\"san\">+20%";
        book = BookNameEnum.allBooks;  

        upup = 2;

        base.Awake();
       
        chara.sanMul += 0.2f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.sanMul -= 0.2f;
    }

}
