using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GongYi : ServantAbstract
{

    override public void Awake()
    {
        base.Awake();
        characterID = 1;
        wordName = "����";
        bookName = BookNameEnum.PHXTwist;


        hp = maxHp = 30;
        atk = 3;
        def = 10;
        psy = 0;
        san = 10;

        //mainProperty.Add("����", "��T");
        //trait = gameObject.AddComponent<Pride>();
        roleName = "��Ⱥ";

        brief = "�����Ĺ���Խ��Խǿ��";
        description = "�����Ĺ���Խ��Խǿ��";


        //������ڵĹ�����������ʵʱ���¸��ý�ɫ���ϵ����й���
        //�Ƿ�����ظ����⣿
        CS_GongYi[] _yiQun = transform.parent
            .GetComponentsInChildren<CS_GongYi>();
        foreach (var _gongYi in _yiQun)
        {
            _gongYi.ChangeNumber(_yiQun.Length);
        }
        
    }


    /// <summary>
    /// �������У�ÿ������һֻ���ϣ������й��Ϲ���+1������+3
    /// </summary>
    /// <param name="_count">���ϵ���Ŀ</param>
    public void ChangeNumber(float _count)
    {
        if (_count > 3)
            return;
        if (-_count < 0)
            return;

        atk = 3 + _count * 1;
        def = 10 + _count * 3;
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
