using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݲ�����int����ֵstring��������
/// </summary>
public static class IntToString 
{
    /// <summary>�����ַ������߸�����Ҫ�ĳ�������ֵ����� ���磺���ף�</summary>
    private static string errorString = "ERROR";
    /// <summary>
    /// ����
    /// </summary>
   public static string SwitchATK(float data)
    {
       if(data>=0&&data<3)
        {
            return "���";
        }
       else if(data>=3&&data<7)
        {
            return "Ѱ��";
        }
       else if(data>=7&&data<12)
        {
            return "����";
        }
       else if(data>=12&&data<20)
        {
            return "�б�";
        }
       else if(data >=20&&data<999)
        {
            return "�Ƿ��켫";
        }
       else  //С��0���ߴ���999���ش����ַ������߸�����Ҫ�ĳ�������ֵ����� ���磺���ף�
        {
            return errorString;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public static string SwitchDEF(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "����";
        }
        else if (data >= 3 && data < 7)
        {
            return "Ѱ��";
        }
        else if (data >= 7 && data < 12)
        {
            return "���";
        }
        else if (data >= 12 && data < 20)
        {
            return "��ʯ��";
        }
        else if (data >= 20 && data < 999)
        {
            return "�������";
        }
        else  //С��0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public static string SwitchPSY(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "�޶�";
        }
        else if (data >= 3 && data < 7)
        {
            return "Ѱ��";
        }
        else if (data >= 7 && data < 12)
        {
            return "����";
        }
        else if (data >= 12 && data < 20)
        {
            return "����";
        }
        else if (data >= 20 && data < 999)
        {
            return "���ǻ���";
        }
        else  //С��0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// ��־
    /// </summary>
    public static string SwitchSAN(float data)
    {
        if (data >= 0 && data < 3)
        {
            return "����";
        }
        else if (data >= 3 && data < 7)
        {
            return "Ѱ��";
        }
        else if (data >= 7 && data < 12)
        {
            return "�ƶ�";
        }
        else if (data >= 12 && data < 20)
        {
            return "����";
        }
        else if (data >= 20 && data < 999)
        {
            return "��������";
        }
        else  //С��0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public static string SwitchCriticalChance(float data)
    {
        if (data== 0)
        {
            return "����";
        }
        else if (data >0 && data <0.2)
        {
            return "ðʧ";
        }
        else if (data >= 0.2 && data <0.5)
        {
            return "Ѱ��";
        }
        else if (data >= 0.5 && data < 0.75)
        {
            return "����";
        }
        else if (data >= 0.75 && data < 0.95)
        {
            return "¯����";
        }
        else if(data >= 0.95 && data <= 1)
        {
            return "�����뻯";
        }
        else  //С��0���ߴ���1
        {
            return errorString;
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public static string SwitchMultipleCriticalStrike(int data)
    {
        if (data >= 0 && data < 2)
        {
            return "Ѱ��";
        }
        else if (data >= 2 && data <4)
        {
            return "����";
        }
        else if (data >= 4 && data < 6)
        {
            return "�б�";
        }
        else if (data >= 6 && data < 10)
        {
            return "�����";
        }
        else if (data >= 10 && data < 999)
        {
            return "��ħ�Ų�";
        }
        else  //С��0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public static string SwitchAttackInterval(int data)
    {
        if (data >= 2.5 && data < 999)
        {
            return "��Ӳ";
        }
        else if (data >= 1.7 && data < 2.5)
        {
            return "����";
        }
        else if (data >= 1.2&& data < 1.7)
        {
            return "Ѱ��";
        }
        else if (data >=0.7 && data < 1.2)
        {
            return "Ѹ��";
        }
        else if (data >0 && data <0.7)
        {
            return "��������";
        }
        else  //С�ڵ���0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// �����ٶ�
    /// </summary>
    public static string SwitchSkillSpeed(int data)
    {
        if (data == 0)
        {
            return "Ѱ��";
        }
        else if (data >0 && data < 0.2)
        {
            return "�鶯";
        }
        else if (data >= 0.2 && data < 0.4)
        {
            return "Ѹ��";
        }
        else if (data >= 0.4 && data <= 0.7)
        {
            return "��������";
        }
        else  //С��0���ߴ���0.7
        {
            return errorString;
        }
    }

    /// <summary>
    /// ������Χ
    /// </summary>
    public static string SwitchAttackDistance(int data)
    {
        if (data == 0)
        {
            return "����";
        }
        else if (data > 0 && data < 2)
        {
            return "���";
        }
        else if (data >=2 && data < 4)
        {
            return "�޳�";
        }
        else if (data >= 4 && data < 7)
        {
            return "û���";
        }
        else if (data >= 7 && data < 999)
        {
            return "���ޱ߼�";
        }
        else  //С��0���ߴ���999
        {
            return errorString;
        }
    }

    /// <summary>
    /// �����ɳ�
    /// </summary>
    public static string SwitchHPGrow(int data)
    {
        if (data >= 0 && data < 3)
        {
            return "����";
        }
        else if (data >= 3 && data < 5)
        {
            return "Ѱ��";
        }
        else if (data >= 5 && data < 8)
        {
            return "ǿ׳";
        }
        else if (data >= 8 && data <= 12)
        {
            return "�쳣";
        }
        else  //С��0���ߴ���12
        {
            return errorString;
        }
    }

    /// <summary>
    /// ħ���ɳ�
    /// </summary>
    public static string SwitchSPGrow(int data)
    {
        if (data >= 0 && data < 3)
        {
            return "����";
        }
        else if (data >= 3 && data < 5)
        {
            return "Ѱ��";
        }
        else if (data >= 5 && data < 8)
        {
            return "�ȷ�";
        }
        else if (data >= 8 && data <= 12)
        {
            return "Ȫӿ��";
        }
        else  //С��0���ߴ���12
        {
            return errorString;
        }
    }

    /// <summary>
    /// �����ɳ�
    /// </summary>
    public static string SwitchATKGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "���";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "Ѱ��";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "����";
        }
        else if (data >= 2 && data <= 4)
        {
            return "�б�";
        }
        else  //С��0���ߴ���4
        {
            return errorString;
        }
    }

    /// <summary>
    /// �����ɳ�
    /// </summary>
    public static string SwitchDEFGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "����";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "Ѱ��";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "���";
        }
        else if (data >= 2 && data <= 3.5)
        {
            return "��ʯ��";
        }
        else  //С��0���ߴ���3.5
        {
            return errorString;
        }
    }

    /// <summary>
    /// ����ɳ�
    /// </summary>
    public static string SwitchPSYGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "�޶�";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "Ѱ��";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "����";
        }
        else if (data >= 2 && data <= 4)
        {
            return "����";
        }
        else  //С��0���ߴ���4
        {
            return errorString;
        }
    }

    /// <summary>
    /// ��־�ɳ�
    /// </summary>
    public static string SwitchSANGrow(int data)
    {
        if (data >= 0 && data < 0.5)
        {
            return "����";
        }
        else if (data >= 0.5 && data < 1.2)
        {
            return "Ѱ��";
        }
        else if (data >= 1.2 && data < 2)
        {
            return "�ƶ�";
        }
        else if (data >= 2 && data <= 3.5)
        {
            return "����";
        }
        else  //С��0���ߴ���3.5
        {
            return errorString;
        }
    }
}
