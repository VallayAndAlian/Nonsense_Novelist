using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//huaiyizhuyiBOSSר������
//��bossѪ������ʱ�˽ű�����boss�ϡ�
//Ч���ǣ�������Ľ�ɫA�ͷţ����䲻�Ϲ��������ɫB������5�룬�˺��������50%
//�����󣬱�������B���������ɫC����ɫ���Է�����ã���CҲ������A����Ч��һ�£����ݴ�ѭ����ֱ��boss����
//����˵������һ�������޴����ĳ��
//������ʽ�ǣ�bossѪ��Ϊ50%ʱ
public class Caiyilian : AbstractVerbs
{

    //ֻ��һ�Ρ�
    bool isUsed = false;
    
    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "������";
        bookName = BookNameEnum.HongLouMeng;
        description = "�ñ��ͷŴ˼��ܵĽ�ɫ�����Ϲ����ѷ���з������ɫ������5�룬�˺��������50%";
        skillMode = gameObject.AddComponent<DamageMode>();
        skillEffectsTime = 5;
        rarity = 3;
        needCD = 0;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="useCharacter">ʩ����</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
      
        base.UseVerb(useCharacter);
        if (isUsed)
            return;
        print("caiyilian  UseVerb");

        //�����һ��ʩ��Ŀ��
        AbstractCharacter[] _aim= skillMode.CalculateRandom(attackDistance, useCharacter,true);


        //Ϊ���ʩ��Ŀ������CAIYI��buff
        var _b = _aim[0].gameObject.AddComponent<CaiYi>();
        buffs.Add(_b);
        _b.maxTime = skillEffectsTime;

        //��ֻ��һ�εĿ���
        isUsed = true;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "�ּ�ʢ�����һ������Ʈ���ڵء�\n"+character.wordName+"��Ʈ���ڵص��һ���£���ţ����������ᣬΪ�䰧��������л���ɻ����죬����������˭������";

    }
}