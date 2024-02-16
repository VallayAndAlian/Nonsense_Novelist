using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：隔离的
/// </summary>
public class GeLi : AbstractAdjectives,IChongNeng
{
    static public string s_description = "<color=#dd7d0e>嘲讽</color>，<color=#dd7d0e>沮丧</color>，每受到一次攻击，恢复+1，持续10s";
    static public string s_wordName = "隔离的";
    static public int rarity = 1;

    private float dizzyAdd;

    public override void Awake()
    {        
        base.Awake();
        adjID = 17;
        wordName = "隔离的";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>嘲讽</color>，<color=#dd7d0e>沮丧</color>，每受到一次攻击，恢复+1，持续10s";

        skillMode = gameObject.AddComponent<SelfMode>();

        skillEffectsTime = 10;
        rarity = 1;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ChaoFeng";
        _s[1] = "Upset";
        return _s;
    }

    int times;
    public void ChongNeng(int _times)
    {
        dizzyAdd += 0.5f*_times;
        times = _times;
    }
    AbstractCharacter thisCharacter;
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
      

        //每受到一次攻击回复+1
        thisCharacter = aimCharacter;
        aimCharacter.event_AttackA += AddCure;

        //添加10s的嘲讽和沮丧
        buffs.Add(aimCharacter.gameObject.AddComponent<Upset>());
        buffs[0].maxTime = 10;
        buffs.Add(aimCharacter.gameObject.AddComponent<ChaoFeng>());
        buffs[0].maxTime = 9;

    }

    float countCure = 0;
    public void AddCure()
    {
        countCure += 1;
        thisCharacter.cure += 1;
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }
    

    public override void End()
    {
        aim.cure -= countCure;
        base.End();
    }

    
}
