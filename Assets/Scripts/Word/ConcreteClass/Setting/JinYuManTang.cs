using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：金玉满堂
/// </summary>
public class JinYuManTang : AbstractSetting
{
    AbstractCharacter chara;
    public override void Start()
    {
        base.Start();

        level = SettingLevel.PingYong;
        name = "金玉满堂";
        info = "王熙凤获得的名词和形容词，有25%概率视作获得两次";
        lables = new List<string> { "角色"};

        hasAdd = false;

        Init();


    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<WangXiFeng>();
        if (chara != null)
        {
            chara.event_AddAdj += EffectA;
            chara.event_AddNoun += EffectB;
        }
        hasAdd = true;
    }
    void EffectA(AbstractAdjectives _av)
    {
        //她拥有动词中的一个，减少1能量上限，最少为1
        

    }
    void EffectB(AbstractItems _av)
    {
        //她拥有动词中的一个，减少1能量上限，最少为1


    }
    private void OnDestroy()
    {
        if (!hasAdd) return;
        chara.event_AddAdj -= EffectA;
        chara.event_AddNoun -= EffectB;
    }
}
