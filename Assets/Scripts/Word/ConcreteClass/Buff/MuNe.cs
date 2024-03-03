using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£ºÄ¾Ú«
/// </summary>
public class MuNe : AbstractBuff
{
    static public string s_description = "<sprite name=\"psy\">-30%£¬×î¶à3²ã";
    static public string s_wordName = "Ä¾Ú«";
    float record;
    override protected void Awake()
    {       
        
    
        buffName = "Ä¾Ú«";
        description = "<sprite name=\"psy\">-30%£¬×î¶à3²ã";
        book = BookNameEnum.allBooks;  
        isBad = true;
        isAll = true;
        upup = 3;

        base.Awake();
       
        chara.psyMul -= 0.3f;
       
        
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        chara.psyMul += 0.3f;
    }

}
