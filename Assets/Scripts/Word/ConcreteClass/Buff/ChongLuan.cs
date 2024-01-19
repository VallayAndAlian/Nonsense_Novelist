using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChongLuan : AbstractBuff
{
    static public string s_description = "(还没做)受到治疗后，孵化一只工蚁随从";
    static public string s_wordName = "虫卵";


    override protected void Awake()
    {
        base.Awake();
        buffName = "虫卵";
        description = "受到治疗后，孵化一只工蚁随从";
        book = BookNameEnum.allBooks;
        upup = 3;
        //
      
    }


    public override void Update()
    {
        base.Update();
       
    }
}
