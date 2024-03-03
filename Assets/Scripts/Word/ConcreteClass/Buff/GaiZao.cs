using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:����
/// </summary>
public class GaiZao : AbstractBuff
{
    static public string s_description = " ����10%<sprite name=\"san\">��ÿ5����һ�֡�׿Խ���ܡ�";
    static public string s_wordName = "����";

    // 3�㣬��������+60��
    //5�㣬��������+130������+5����־���룻
    //7�㣬��������+260������+15����־���Ϊ0
    int count=0;

    float record=0;
    override protected void Awake()
    {

        buffName = "����";
        description = " ����10%<sprite name=\"san\">��ÿ5����һ�֡�׿Խ���ܡ�";
        book = BookNameEnum.ElectronicGoal;


        base.Awake();
        chara.sanMul -= 0.05f;

        count = GetComponents<GaiZao>().Length;
        //ÿ�����-5%��־
        //ÿ5����죬������û������״̬��һ��
        //׿Խ���� - ����
        //׿Խ���� - ����
        //׿Խ���� - ��ʵ
        //׿Խ���� - ����
        if (count % 5 == 0)
        {
            //���һ��׿Խ����
        }

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
