using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ�������
/// </summary>
public class QingXi : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>����ʫ��</color>������10s�������󾻻�3�����Ч��";
    static public string s_wordName = "������";
    static public int s_rarity = 2;
    public override void Awake()
    {
        adjID = 12;
        wordName = "������";
        bookName = BookNameEnum.CrystalEnergy;
        description = "<color=#dd7d0e>����ʫ��</color>������10s�������󾻻�3�����Ч��";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 10;
        rarity = 2;
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[3];
        _s[0] = "GongZhen";
        _s[1] = "ShiQing";
        _s[2] = "XuWu_YunSu";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        buffs.Add(aimCharacter.gameObject.AddComponent<GongZhen>());
        buffs[0].maxTime = Mathf.Infinity;
        buffs.Add(aimCharacter.gameObject.AddComponent<ShiQing>());
        buffs[1].maxTime = skillEffectsTime;
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(skillEffectsTime);
        if (this.GetComponent<AbstractCharacter>() != null)
        {
            this.GetComponent<AbstractCharacter>().DeleteBadBuff(3);
        }
    }

    protected override void Update()
    {
        base.Update();

    }

    public override void End()
    {
        base.End();
        
    }

}
