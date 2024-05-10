using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ʣ���ս
/// </summary>
public class XuanZhan : AbstractVerbs
{
    static public string s_description = "ʹĿ����<color=#dd7d0e>����</color>����ʹ�ҷ�������ӹ����ٶ�����30%������10s";
    static public string s_wordName = "��ս";
    static public int s_rarity = 3;
    public override void Awake()
    {
        base.Awake();
        skillID = 19;
        wordName = "��ս";
        bookName = BookNameEnum.PHXTwist;
        description = "ʹĿ����<color=#dd7d0e>����</color>����ʹ�ҷ�������ӹ����ٶ�����30%������10s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange = new SingleSelector();

        skillEffectsTime = 10;
        rarity = 3;
        needCD = 6;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChaoFeng";
        return _s;
    }

    List<AbstractCharacter> servants = new List<AbstractCharacter>();
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);

        servants.Clear();

        //ʹĿ�����˳���
        if (useCharacter)
        {
            var _aims = skillMode.CalculateAgain(200, useCharacter);
            int x = 0;
            for (int i = 0; (i < _aims.Length) && (x < useCharacter.myState.aimCount); i++)
            {
              
                buffs.Add(_aims[i].gameObject.AddComponent<ChaoFeng>());
                buffs[0].maxTime = skillEffectsTime;
                x++;
            }

            //ʹ�ҷ�������Ӽ���
            if (useCharacter.camp == CampEnum.left)
            {
                AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
                for (int i = 0; i < left.Length; i++)
                {
                    foreach (var _s in left[i].servants)
                    {
                        _s.GetComponent<AbstractCharacter>().attackSpeedPlus += 0.3f;
                        servants.Add(_s.GetComponent<AbstractCharacter>());
                    }
                }

            }
            else if (useCharacter.camp == CampEnum.right)
            {
                AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
                for (int i = 0; i < right.Length; i++)
                {
                    foreach (var _s in right[i].servants)
                    {
                        _s.GetComponent<AbstractCharacter>().attackSpeedPlus += 0.3f;
                        servants.Add(_s.GetComponent<AbstractCharacter>());
                    }
                }
            }

            StartCoroutine(DeleteEffect());
           
            return;


        }

   



    }
    WaitForSeconds waitTen = new WaitForSeconds(10);
    IEnumerator DeleteEffect()
    {
        yield return waitTen;
        foreach (var _s in servants)
        {
            _s.GetComponent<AbstractCharacter>().attackSpeedPlus -= 0.3f;
            servants.Add(_s.GetComponent<AbstractCharacter>());
        }
    }
    public override void BasicAbility(AbstractCharacter useCharacter)
    {
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
        //return null;

        return character.wordName + "����С��������������һ��Сȱ�ڣ�������֬�͹������С��ٽ���֬��������2��ͷ­����ֹͷ���ı��Ρ������������������ڼ����һ���£������Ϳ����������岻�������������ˡ�";

    }
}
