
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ӣ�������
/// </summary>
public class CS_HunYangLong : ServantAbstract
{
    static public string s_description = "���޽����ı�";
    static public string s_wordName = "��ӣ�������";

    AbstractCharacter[] aims;
    Coroutine coroutineAttack = null;


    override public void Awake()
    {
        base.Awake();
        

    }



    /// <summary>
    /// �����ʼ����ֵ
    /// </summary>
    public void SetInitNumber(ServantAbstract _s1)
    {
        atk += _s1.atk;
        def += _s1.def;
        san += _s1.san;
        psy += _s1.psy;
        if (maxHp == 1340.25f)
        {
            //��ʼ��ֵ
            maxHp = _s1.maxHp;
        }
        else
        {
             maxHp += _s1.maxHp; 
        }
        hp = maxHp;   
        
    }

    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<CS_YiZhiWeiShiQi>() != null)
            Destroy(masterNow.GetComponent<CS_YiZhiWeiShiQi>());
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
