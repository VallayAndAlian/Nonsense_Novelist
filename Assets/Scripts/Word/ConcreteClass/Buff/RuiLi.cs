using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class RuiLi : AbstractBuff
{
    static public string s_description = "<sprite name=\"atk\">+15%";
    static public string s_wordName = "����";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "����";
        description = "<sprite name=\"atk\">+15%";
        book = BookNameEnum.allBooks;  

        upup = 2;

        base.Awake();
       
        chara.atk += 0.15f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.atk -= 0.15f;
    }

}
