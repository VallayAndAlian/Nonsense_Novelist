using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChongLuan : AbstractBuff
{
    static public string s_description = "(��û��)�ܵ����ƺ󣬷���һֻ�������";
    static public string s_wordName = "����";


    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        description = "�ܵ����ƺ󣬷���һֻ�������";
        book = BookNameEnum.allBooks;
        upup = 3;
        //
      
    }


    public override void Update()
    {
        base.Update();
       
    }
}
