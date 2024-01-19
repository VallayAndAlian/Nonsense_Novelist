using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class HuaiYiZhuYi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 0;
        wordName = "怀疑主义";
        bookName = BookNameEnum.allBooks;
        gender = GenderEnum.noGender;
        camp = CampEnum.stranger;
        hp =maxHp  = 350;
        atk = 10;
        def = 30;
        psy = 15;
        san = 30;
        trait=gameObject.AddComponent<Sentimental>();
        roleName = "思潮";
        attackInterval = 2.2f;
        description = "暂无文案";
    }
    private void Start()
    {
        skillMode = gameObject.AddComponent<DamageMode>();
    }

    AbstractSkillMode skillMode;
    AbstractCharacter[] aims;
    float record;
    public override bool AttackA()
    {
        if (base.AttackA())
        {
            aims = skillMode.CalculateAgain(100, this);
            //夺取场上每个敌人5点意志力，加在自己身上
            foreach (AbstractCharacter aim in aims)
            {
                record = aim.san;
                aim.san -= 5;
                record = record - aim.san;
                san += record;
            }
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
