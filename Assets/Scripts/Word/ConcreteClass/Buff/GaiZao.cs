using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:����
/// </summary>
public class GaiZao : AbstractBuff
{
    static public string s_description = "3�㣬<sprite name=\"hpmax\">+60��\n5�㣬<sprite name=\"hpmax\">+ 130��<sprite name=\"atk\"> + 5��<sprite name=\"san\"> ���룻" +
            "\n7�㣬<sprite name=\"hpmax\"> + 260��<sprite name=\"atk\">  + 15��<sprite name=\"san\">���Ϊ0";
    static public string s_wordName = "����";

    // 3�㣬��������+60��
    //5�㣬��������+130������+5����־���룻
    //7�㣬��������+260������+15����־���Ϊ0
    int count=0;

    float record=0;
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        description= "3�㣬<sprite name=\"hpmax\">+60��\n5�㣬<sprite name=\"hpmax\">+ 130��<sprite name=\"atk\"> + 5��<sprite name=\"san\"> ���룻" +
            "\n7�㣬<sprite name=\"hpmax\"> + 260��<sprite name=\"atk\">  + 15��<sprite name=\"san\">���Ϊ0";
        book = BookNameEnum.ElectronicGoal;


        count = GetComponents<GaiZao>().Length;
        if (count >= 7)
        {
            chara.maxHp += 260;
            chara.CreateFloatWord(250, FloatWordColor.healMax, false);
            chara.atk += 15;
            record = chara.san;
            if (record >= 0)
                chara.san = 0;
            
        }
        else if (count >= 5)
        {   
            chara.maxHp += 130;
            chara.CreateFloatWord(130, FloatWordColor.healMax, false);
            chara.atk += 5;
            record = chara.san;
            chara.san = record / 2;
        }
        else if (count >= 3)
        {
            chara.maxHp += 60;
            chara.CreateFloatWord(60, FloatWordColor.healMax, false);

        }
        else 
        { }

    }

    private void OnDestroy()
    {
        base.OnDestroy();
        count = GetComponents<GaiZao>().Length;
        if (count >= 7)
        {
            chara.maxHp -= 260;
            chara.atk -= 15;

            chara.san = record; 

        }
        else if (count >= 5)
        {
            chara.maxHp -= 130;
            chara.atk -= 5;
            chara.san += record / 2;
        }
        else if (count >= 3)
        {
            chara.maxHp -= 60;

        }
        else
        { }
    }

}
