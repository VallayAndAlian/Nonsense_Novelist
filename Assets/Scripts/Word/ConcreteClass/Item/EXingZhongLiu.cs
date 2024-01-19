using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：恶性肿瘤
/// </summary>
class EXingZhongLiu : AbstractItems,IChongNeng
{
    static public string s_description = "每次弹射<sprite name=\"hpmax\">-5%";
    static public string s_wordName = "恶性肿瘤";
    public override void Awake()
    {
        base.Awake();
        itemID = 17;
        wordName = "恶性肿瘤";
        bookName = BookNameEnum.FluStudy;
        description = "每次弹射<sprite name=\"hpmax\">-5%";

        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 1;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChongNeng>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChongNeng";
        return _s;
    }
    public float maxHPAdd;

    public void ChongNeng(int times)
    {
        maxHPAdd += 0.05f*times;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);

        chara.maxHpMul -= maxHPAdd;
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.maxHpMul += maxHPAdd;
    }

    
}
