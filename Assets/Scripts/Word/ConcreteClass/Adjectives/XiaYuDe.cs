using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ������
/// </summary>

public class XiaYuDe : AbstractAdjectives, IJiHuo
{
    static public string s_description = "δ���<color=#dd7d0e>ľګ</color>������10s���������ȫ��1�����Ч��";
    static public string s_wordName = "�����";
    static public int s_rarity = 1;
    /// <summary>�Ƿ񼤻�� </summary>
    private bool jiHuo;

    public override void Awake()
    {

        adjID = 31;
        wordName = "�����";
        bookName = BookNameEnum.ElectronicGoal;
        description = "δ���<color=#dd7d0e>ľګ</color>������10s���������ȫ��1�����Ч��";


        skillEffectsTime = 10;

        rarity = 1;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();

    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "MuNe";
        _s[1] = "JiHuo";
        return _s;
    }

    public void JiHuo(bool value)
    {
        jiHuo = value;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        if (jiHuo)
        {
            if (aimCharacter.camp == CampEnum.left)
            {
                AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
                for (int i = 0; i < left.Length; i++)
                {
                    left[i].DeleteBadBuff(1);
                }

            }
            else if (aimCharacter.camp == CampEnum.right)
            {
                AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
                for (int i = 0; i < right.Length; i++)
                {
                    right[i].DeleteBadBuff(1);
                }
            }
         
        }
        else
        {
            buffs.Add(aimCharacter.gameObject.AddComponent<MuNe>());
            buffs[0].maxTime = skillEffectsTime;
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
