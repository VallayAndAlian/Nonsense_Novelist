using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：晕眩
/// </summary>
public class Dizzy : AbstractBuff
{
    static public string s_description = "无法行动，能量增长停止";
    static public string s_wordName = "晕眩";
    float record;
    override protected void Awake()
    {       
        
        base.Awake();
        buffName = "晕眩";
        description = "无法行动，能量增长停止";
        book = BookNameEnum.allBooks;
        record = chara.energy;
        chara.dizzyTime = maxTime;
        if (record > 0.9f) record = 0.9f;
       
       
        isBad = true;
    }

   
    

    public override void Update()
    {
        base.Update();
   
        chara.energy = 0;//能量增长停止
    }
}
