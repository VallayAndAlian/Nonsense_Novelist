using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�����ٶ�+10%

/// <summary>
/// buff������
/// </summary>
public class HuoRe : AbstractBuff
{
    static public string s_description = "�����ٶ�+10%";
    static public string s_wordName = "����";


    override protected void Awake()
    {

        buffName = "����";
        description = "�����ٶ�+10%";
        book = BookNameEnum.HongLouMeng;

        base.Awake();
        if (TryGetComponent < AbstractCharacter>(out var _c))
        {
            _c.attackSpeedPlus += 0.1f;
        }
    }


    private void OnDestroy()
    {
        if (TryGetComponent<AbstractCharacter>(out var _c))
        {
            _c.attackSpeedPlus -= 0.1f;
        }
        base.OnDestroy();
     
    }
}
