using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff����ѣ
/// </summary>
public class Dizzy : AbstractBuff
{
    static public string s_description = "�޷��ж�����������ֹͣ";
    static public string s_wordName = "��ѣ";
    float record;
    override protected void Awake()
    {       
        
        base.Awake();
        buffName = "��ѣ";
        description = "�޷��ж�����������ֹͣ";
        book = BookNameEnum.allBooks;
        record = chara.energy;
        chara.dizzyTime = maxTime;
        if (record > 0.9f) record = 0.9f;
       
       
        isBad = true;
    }

   
    

    public override void Update()
    {
        base.Update();
   
        chara.energy = 0;//��������ֹͣ
    }
}
