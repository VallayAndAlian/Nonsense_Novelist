using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class ZaiSheng : AbstractBuff
{
    static public string s_description = "�����ָ�+15";
    static public string s_wordName = "����";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "����";
        description = "�����ָ�+15";
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
