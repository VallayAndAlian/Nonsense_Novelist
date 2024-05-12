using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// ���ʣ�����
/// </summary>
public class LiWu : AbstractItems, IJiHuo
{
    static public string s_description = "<sprite name=\"atk\">+1";
    static public string s_wordName = "����";
    static public int s_rarity = 1;
    static public int s_useTimes = 6;
    public override void Awake()
    {
        base.Awake();
        itemID = 24;
        wordName = "����";
        useTimes = 6;
        bookName = BookNameEnum.Salome;
        description = "����һ����������������������һ���������ʡ�δ�������40��;�������20��";
        //Ч����Ҫ�ڽ�ɫ����ʱ�ſ�ʼ���㣬�Ҷ�������������
        //�������ʰ���3����4������
        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 1;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<JiHuo>();
    }

    int attackTimes = 0;
    bool hasUsed = false;
    bool hasAdd= false;
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "JiHuo";
        return _s;
    }
    /// <summary>�Ƿ񼤻�� </summary>
    private bool jiHuo;
    public void JiHuo(bool value)
    {
        jiHuo = value;
    }


    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);

        if (hasUsed) return;

        if (jiHuo) attackTimes = 20;
        else attackTimes = 2;

        //���ӹ�����ί���¼�
        chara.event_AttackA += AttackOnce;
        hasUsed = true;

    }
    void AttackOnce()
    {
        if (hasAdd) return;

        attackTimes -= 1;
        if (attackTimes <= 0)
        {
            List<Type> _list = new List<Type>();
            _list.AddRange(AllSkills.Rare_3); _list.AddRange(AllSkills.Rare_4);
            int _randomR = UnityEngine.Random.Range(0, _list.Count);
           
            int _loop = 0;
            while ((!(AllSkills.list_noun.Contains(_list[_randomR])))&&(_loop<100))
            {
                _randomR = UnityEngine.Random.Range(0, _list.Count);
                _loop++;
            }
            this.gameObject.AddComponent(_list[_randomR]);
            hasAdd = true; 
            Destroy(this);
        }
    }





    public override void End()
    {
        if(hasUsed) aim.event_AttackA -= AttackOnce;
        base.End();
    }
}
