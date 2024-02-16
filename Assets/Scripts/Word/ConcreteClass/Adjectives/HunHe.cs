using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ���ϵ�
/// </summary>
public class HunHe : AbstractAdjectives
{
    static public string s_description = "���1��������;����3����ӣ���ϲ�Ϊһ��<color=#dd7d0e>������</color>";
    static public string s_wordName = "��ϵ�";
    static public int rarity = 2;
    public override void Awake()
    {
        adjID = 4;
        wordName = "��ϵ�";
        bookName = BookNameEnum.ZooManual;
        description = "���1��������;����3����ӣ���ϲ�Ϊһ��<color=#dd7d0e>������</color>";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 12;
        rarity = 2;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        var _obj = this.GetComponent<AbstractCharacter>();
        int count = _obj.servants.Count;
        if (count < 3)
        {
            _obj.AddRandomServant();

        }
        //�����������ϲ�
        else if (count == 3)
        {
            //var _s1 = this.GetComponentsInChildren<ServantAbstract>()[0];
            //var _s2 = this.GetComponentsInChildren<ServantAbstract>()[2];
            //var _s3 = this.GetComponentsInChildren<ServantAbstract>()[1];
            ////ɾ��������Ӳ�������һ��������
            //_obj.DeleteServant(_s1.gameObject);
            //_obj.DeleteServant(_s2.gameObject);
            //_obj.DeleteServant(_s3.gameObject);

            //_obj.AddServant("CS_HunYangLong");
            //this.GetComponent<CS_HunYangLong>().SetInitNumber(_s1, _s2, _s3);
            _obj.ServantMerge();
        }
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {

    }

}

