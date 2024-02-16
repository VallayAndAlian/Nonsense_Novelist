
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ӣ�è
/// </summary>
/// 

public class CS_Mao : ServantAbstract
{
    static public string s_description = "��";
    static public string s_wordName = "��ӣ�è";

    override public void Awake()
    {
        base.Awake();
        characterID = 6;
        wordName = "è";
        bookName = BookNameEnum.EgyptMyth;

        hp = maxHp = 40;
        atk = 5;
        def = 10;
        psy = 5;
        san = 10;

        //mainProperty.Add("����", "��T");
        //trait = gameObject.AddComponent<Pride>();
        roleName = "��";

        brief = "��";
        description = "��";


     
    }

   


    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);

    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "���ѿ�������һ�����ã�ϸ�����ݣ�ֻ������㣬����΢΢���о�ʱ��毻���ˮ���ж������������磬" + otherChara.wordName + "Ц������������ã����������ġ�";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "���Ҿ�֪�������˲���ʣ�µ�Ҳ�����ҡ�������������һ�仨�꣬��" + otherChara.wordName + "��ȥ";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "�������Ů��Ϣ���������ϻ���ա����㽫һ�����ӣ�һ��ʫ��پ��ڻ����С�";
    }
    public override string DieText()
    {
        return "�����񡭱�����á���������û˵��������˫�ۡ�";
    }


}

