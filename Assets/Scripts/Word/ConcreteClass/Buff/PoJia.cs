using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff���Ƽ�
/// </summary>
public class PoJia : AbstractBuff
{
    static public string s_description = "<sprite name=\"def\">-20%�����3��";
    static public string s_wordName = "�Ƽ�";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "�Ƽ�";
        description = "<sprite name=\"def\">-20%�����3��";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = true;
        upup = 3;

        base.Awake();
       
        chara.defMul -= 0.2f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.defMul += 0.2f;
    }

}
