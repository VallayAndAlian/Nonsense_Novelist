using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff����Į
/// </summary>
public class LengMo : AbstractBuff
{
    static public string s_description = "��־-30%�����3��";
    static public string s_wordName = "��Į";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "��Į";
        description = "��־-30%�����3��";
        book = BookNameEnum.allBooks;  
        isBad = true;
        upup = 2;

        base.Awake();
       
        chara.sanMul -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.sanMul += 0.3f;
    }

}
