using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：再生
/// </summary>
public class ZaiSheng : AbstractBuff
{
    static public string s_description = "生命恢复+15";
    static public string s_wordName = "再生";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "再生";
        description = "生命恢复+15";
        book = BookNameEnum.allBooks;  

        upup = 2;

        base.Awake();
       
        chara.cure += 15f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.defMul -= 15f;
    }

}
