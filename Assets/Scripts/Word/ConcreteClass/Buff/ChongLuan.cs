using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class ChongLuan : AbstractBuff
{
    static public string s_description = "�ܵ����ƺ󣬷���һֻ�������";
    static public string s_wordName = "����";

    float recordHp = 0;
    override protected void Awake()
    {
        
        buffName = "����";
        description = "�ܵ����ƺ󣬷���һֻ�������";
        book = BookNameEnum.allBooks;
        upup = 3;
        //
  
        base.Awake();
        chara.event_BeCure += OnCure;
    }


    public override void Update()
    {
        
        base.Update();

    }
    void OnCure()
    {
        chara.AddServant("CS_GongYi");
        chara.event_BeCure -= OnCure;
        Destroy(this);
    }
}
