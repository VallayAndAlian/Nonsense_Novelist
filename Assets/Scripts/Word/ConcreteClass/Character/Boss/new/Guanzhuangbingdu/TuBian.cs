using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuBian : AbstractVerbs
{
    //ֻ��һ�Ρ�
    bool isUsed = false;

    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "ͻ��";
        bookName = BookNameEnum.HongLouMeng;
        description = "";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 3;
        needCD = 0;

        //�������м��ܵ�һ���Կ���
        for (int _i = 0; _i < hasUsed.Length; _i++)
        {
            hasUsed[_i] = false;
        }
    }

    ///������������е�һ�֣�boss����������������еĲ��ظ�����
    #region Boss�����ּ���

    /// <summary>0��������-1��������-2���ذ���֢-3���������� </summary>
    bool[] hasUsed = new bool[4];
    bool ignoredDEF = false;
    bool chuanranTwice = false;
    bool dizzyDelay = false;

    /// <summary>
    /// 0�������ݣ���״������ɵ������˺������ӷ�����
    /// </summary>
    private void E_MianYiTaoYi()
    {
        if (hasUsed[0])
        {
            return;
        }
        ignoredDEF = true;
        hasUsed[0] = true;
    }


    /// <summary>
    /// 1�������������Ժ����ۺ�֢�����еڶ�����ɢ�������ֱ���ɢ�Ľ�ɫ�Ż᲻������ɢ���ҹ����п��Է�����Ⱦ
    /// </summary>
    private void E_ChaoJiChuanBo()
    {
        if (hasUsed[1])
        {
            return;
        }
        chuanranTwice = true;
        hasUsed[1] = true;
    }


    /// <summary>
    /// 2���ذ���֢�������ۺϺ���֢�����ӵ�4s��ʼ��ѣ����������8s��������2s
    /// </summary>
    private void E_YanZhongBanSuiZheng()
    {
        if (hasUsed[2])
        {
            return;
        }
        dizzyDelay = true;
        hasUsed[2] = true;
    }


    /// <summary>
    /// 3�����������������С��������Ľ�ɫ��ÿ3s��10%�����ܵ�10�������˺�*/
    /// </summary>
    private void E_ZhiSiLvTiSheng()
    {

    }

    #endregion


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
        AbstractCharacter[] _aim = skillMode.CalculateRandom(attackDistance, useCharacter, true);


        //Ϊ���ʩ��Ŀ������CAIYI��buff
        buffs.Add(_aim[0].gameObject.AddComponent<CaiYi>());
        buffs[0].maxTime = skillEffectsTime;

        //��ֻ��һ�εĿ���
        isUsed = true;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "�ּ�ʢ�����һ������Ʈ���ڵء�\n" + character.wordName + "��Ʈ���ڵص��һ���£���ţ����������ᣬΪ�䰧��������л���ɻ����죬����������˭������";

    }
}
