using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenZhiGuHua : AbstractVerbs
{
    static public string s_description = "����ü�������һ��ʱ���������棬�����������ʱ�˺�ʩ���ߡ�";
    static public string s_wordName = "��֪�̻�";

    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "��֪�̻�";
        bookName = BookNameEnum.HongLouMeng;
        description = "����ü�������һ��ʱ���������棬�����������ʱ�˺�ʩ���ߡ�";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 3;
        needCD = 4;
        attackDistance = 100;
      
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="useCharacter">ʩ����</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
      

        //���_aimCount��ʩ��Ŀ��
        int _aimCount = 3;
        AbstractCharacter[] _randomCharacter= skillMode.CalculateRandom(attackDistance, useCharacter,true);

        if (_aimCount <_randomCharacter.Length)
        {//���Ͻ�ɫ����3��ʱ�������ȡ
            //ȷ��ʩ��Ŀ�겻�ظ�
            List<int> _array = new List<int>();
            for (int i = 0; i < _aimCount; i++)
            {
                int number = Random.Range(0, _randomCharacter.Length);
                while (_array.Contains(number))
                {
                    number = Random.Range(0, _randomCharacter.Length);
                }
                _array.Add(number);
                var _b = _randomCharacter[_array[i]].gameObject.AddComponent<Renzhiguhua_buff>();
                buffs.Add(_b);
                _b.maxTime = 10;
            }
        }
        else 
        { //���Ͻ�ɫС������ʱ��ȫ�����衣
         
            for (int i = 0; i < _randomCharacter.Length; i++)
            {
                var _b = _randomCharacter[i].gameObject.AddComponent<Renzhiguhua_buff>();
                buffs.Add(_b);
                _b.maxTime = 10;
            }
        }
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "�ּ�ʢ�����һ������Ʈ���ڵء�\n" + character.wordName + "��Ʈ���ڵص��һ���£���ţ����������ᣬΪ�䰧��������л���ɻ����죬����������˭������";

    }
}
