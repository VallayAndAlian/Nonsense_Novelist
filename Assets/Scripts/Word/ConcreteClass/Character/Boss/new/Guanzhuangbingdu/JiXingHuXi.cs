using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiXingHuXi : AbstractVerbs
{ 
   
    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "���Ժ����ۺ���";
        bookName = BookNameEnum.FluStudy;
        
        description = "������Ľ�ɫ�ͷţ�ʹ���á�������������8s�����ڵ�6��ʱ������Χ�ڽ������н�ɫ��ɢ��Ч����" +
            "��ʹ������ѣ��2s" +
            "����ɢ�Ľ�ɫ��ͬ�����8s��������2s��ѣ�����ٴ���ɢ" +
            "PS�����������ǿ��Ե��ӵ�";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;

        needCD = 3;//������3����
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="useCharacter">ʩ����</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {

        base.UseVerb(useCharacter);
  
        print("���Ժ���  UseVerb");

        //�����һ��ʩ��Ŀ��
        AbstractCharacter[] _aim = skillMode.CalculateRandom(attackDistance, useCharacter, true);


        //Ϊ���ʩ��Ŀ�����ӻ�����buff
        HuanBing_x _hb = gameObject.AddComponent<HuanBing_x>();
        buffs.Add(_hb);
        _hb.maxTime = 9f;
        _hb.effectTime = 8f;

        //�ۼ�ʹ�û����Ĵ���
        float _count = character.GetComponent<Boss_Guanzhuangbingdu>().HuanbingBuffCount;
        _count += 1;
        print("�ۼ�" + _count + "�λ���Ч���ˣ�");

        //����8s��6s�ļ�ʱ

    }

    #region 8s��6s����ʱ


    /// <summary>
    /// 8s����ʱ
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFor8Seconds()
    {
        
            yield return new WaitForSeconds(8);
          
        
    }


    /// <summary>
    /// 6s����ʱ
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFor6Seconds(AbstractCharacter _aim)
    {
        
            yield return new WaitForSeconds(8);
        //����Χ�ڽ������н�ɫ��ɢ��Ч����������ѣ��2s"
        Dizzy _hb = _aim.gameObject.AddComponent<Dizzy>();
        buffs.Add(_hb);
        _hb.maxTime = 2f;

    }


    #endregion

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "�ּ�ʢ�����һ������Ʈ���ڵء�\n" + character.wordName + "��Ʈ���ڵص��һ���£���ţ����������ᣬΪ�䰧��������л���ɻ����죬����������˭������";

    }
}
