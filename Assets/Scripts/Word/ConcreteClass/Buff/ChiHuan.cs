using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff���ٻ�
/// </summary>
public class ChiHuan : AbstractBuff
{
    static public string s_description = "�����ٶ�-30%�����3��";
    static public string s_wordName = "�ٻ�";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "�ٻ�";
        description = "�����ٶ�-30%�����3��";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = true;
        upup = 3;

        base.Awake();
       
        chara.attackSpeedPlus -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.attackSpeedPlus += 0.3f;
    }

}
