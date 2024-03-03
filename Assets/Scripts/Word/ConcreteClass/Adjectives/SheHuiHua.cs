using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ữ��
/// </summary>
public class SheHuiHua : AbstractAdjectives,IJiHuo
{

    /// <summary>�Ƿ񼤻�� </summary>
    private bool jiHuo;
    static public string s_description = "δ��ʾ���ٻ�2ֻ<color=#dd7d0e>����</color>����ʾ���ٻ�2ֻ<color=#dd7d0e>����</color>��1��������";
    static public string s_wordName = "��ữ��";
    static public int rarity = 3;
    public override void Awake()
    {
        adjID = 18;
        wordName = "��ữ��";
        bookName = BookNameEnum.FluStudy;
        description = "δ��ʾ���ٻ�2ֻ<color=#dd7d0e>����</color>����ʾ���ٻ�2ֻ<color=#dd7d0e>����</color>��1��������";
        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 20;
        rarity = 3;
        
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
        
      
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "JiHuo";
        _s[1] = "CS_GongYi";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo = value;
    }
   
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        aimCharacter.AddServant("CS_GongYi");
        aimCharacter.AddServant("CS_GongYi");
        if (jiHuo)
        {
            aimCharacter.AddRandomServant();
        }


    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
    }

}
