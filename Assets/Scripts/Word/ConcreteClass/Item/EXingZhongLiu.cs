using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：恶性肿瘤
/// </summary>
class EXingZhongLiu : AbstractItems,IChongNeng
{
    static public string s_description = "每次弹射<sprite name=\"hpmax\">-30";
    static public string s_wordName = "恶性肿瘤";
    static public int rarity = 1;
    public override void Awake()
    {
        base.Awake();
        itemID = 17;
        wordName = "恶性肿瘤";
        bookName = BookNameEnum.FluStudy;
        description = "每次弹射<sprite name=\"hpmax\">-30";

        VoiceEnum = MaterialVoiceEnum.Ceram;
        useTimes = 6;
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
    int count;
    float record;
    public void ChongNeng(int times)
    {
        count=times;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.maxHp -= count*30;

        record = count * 30;
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.maxHp += record;
    }

    
}
