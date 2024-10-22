using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ʣ�����
/// </summary>
public class XuanHua : AbstractVerbs
{
    static public string s_description = "ʹ�Լ����<color=#dd7d0e>����</color>������5s";
    static public string s_wordName = "����";
    static public int s_rarity = 1;
    public override void Awake()
    {
        base.Awake();
        skillID = 20;
        wordName = "����";
        bookName = BookNameEnum.allBooks;
        description = "ʹ�Լ����<color=#dd7d0e>����</color>������5s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange = new SingleSelector();

        skillEffectsTime = 5;
        rarity = 1;
        needCD = 3;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChaoFeng";
        return _s;
    }

 
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);

        buffs.Add(useCharacter.gameObject.AddComponent<ChaoFeng>());
        buffs[0].maxTime = skillEffectsTime;




    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (other == null || aimState==null)
        //return null;

        return character.wordName + "����С��������������һ��Сȱ�ڣ�������֬�͹������С��ٽ���֬��������2��ͷ­����ֹͷ���ı��Ρ������������������ڼ����һ���£������Ϳ����������岻�������������ˡ�";

    }
}
