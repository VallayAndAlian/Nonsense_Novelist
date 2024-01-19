using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 阿努比斯
/// </summary>
class Anubis : AbstractCharacter
{
    float hpadd = 0;
    float timer = 0;
    override public void Awake()
    {
        base.Awake();
        characterID = 7;
        wordName = "阿努比斯";
        bookName = BookNameEnum.EgyptMyth;
        gender = GenderEnum.boy;
        hp =maxHp  = 250;
        atk = 3;
        def = 4;
        psy = 3;
        san = 5;
        mainProperty.Add("防御","肉T");
        trait=gameObject.AddComponent<Pride>();
        roleName = "死神";
        attackInterval = 2.2f;
        attackDistance = 200;
        brief = "";
        description = "暂无文案";

    }

    /// <summary>
    /// 每10秒，恢复5%最大生命值
    /// </summary>
    /// <returns></returns>
    void FixedUpdate()
    {
        if (CharacterManager.instance.pause) return;

        timer += Time.deltaTime;


        if (timer > 10)
        {
            timer = 0;

            hpadd = (maxHp * 0.05f * maxHpMul);
            hp += hpadd;
            CreateFloatWord(hpadd, FloatWordColor.heal, false);
        }
            
        
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
