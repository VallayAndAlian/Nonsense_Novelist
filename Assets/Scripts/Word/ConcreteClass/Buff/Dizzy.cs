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
        
    
        buffName = "��ѣ";
        description = "�޷��ж�����������ֹͣ";
        book = BookNameEnum.allBooks;  
        isBad = true;
        maxTime = 2;

        base.Awake();

        record = chara.energy;
        chara.dizzyTime = maxTime;
        if (record > 0.9f) record = 0.9f;

        chara.teXiao.PlayTeXiao("dizzy");


    }

   
    

    public override void Update()
    {
        base.Update();
   
        chara.energy = 0;//��������ֹͣ
      
    }
}
