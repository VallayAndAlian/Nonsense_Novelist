using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 莎乐美
/// </summary>
class ShaLeMei : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 8;
        wordName = "莎乐美";
        bookName = BookNameEnum.Salome;
        gender = GenderEnum.girl;
        hp =maxHp  = 130;
        atk = 3;
        def = 4;
        psy = 5;
        san = 3;
        mainProperty.Add("精神","中法dps");
        trait=gameObject.AddComponent<Possessive>();
        roleName = "舞女";
        attackInterval = 2.2f;
        attackDistance = 200;
        brief = "《红楼梦》中一位性格敏感脆弱，却又极有灵性的少女。";
        description = "林黛玉，中国古典名著《红楼梦》的女主角，金陵十二钗正册双首之一，西方灵河岸绛珠仙草转世，最后于贾宝玉、薛宝钗大婚之夜泪尽而逝。她生得容貌清丽，兼有诗才，是古代文学作品中极富灵气的经典女性形象。" +
            "\n道是：" +
            "\n可叹停机德，堪怜咏絮才。" +
            "\n玉带林中挂，金簪雪里埋。";
    }

    public override bool AttackA()
    {
        if (base.AttackA())
        {
            //普通攻击降低敌人20%意志，不可叠加，持续10s
            ShaLeMeiBuff record = myState.aim.GetComponent<ShaLeMeiBuff>();
            if (record == null)
                myState.aim.gameObject.AddComponent<ShaLeMeiBuff>();
            else
                record.nowTime = 10;
            return true;
        }
        return false;
    }



    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "早已看见多了一个妹妹，细看形容，只见泪光点点，娇喘微微，闲静时如姣花照水，行动处似弱柳扶风，" + otherChara.wordName + "笑道：“这个妹妹，我曾见过的”";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "“我就知道，别人不挑剩下的也不给我。”林黛玉轻捻一朵花瓣，向" + otherChara.wordName + "飞去";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "黛玉对侍女喘息道：“笼上火盆罢。”便将一对帕子，一叠诗稿焚尽于火盆中。";
    }
    public override string DieText()
    {
        return "“宝玉…宝玉…你好……”黛玉没说完便合上了双眼。";
    }

}
