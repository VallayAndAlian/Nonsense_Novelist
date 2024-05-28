using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��������˰
/// </summary>
public class KeJuanZaShui : AbstractSetting
{

    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.QiaoSi;
        settingName = "������˰";
        res_name = "kejuanzashui";
        info = "ÿ���з��ͷŶ��ʣ�¢�Ϲ�˾�������һ���˺�Ϊ20%����ͨ��������������Ч��";
        lables = new List<string> { "��ɫ"};

        hasAdd = false;

   


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            foreach (var it in CharacterManager.instance.GetEnemy(chara.Camp))//��ȡ�з����н�ɫ
            {
                it.event_UseVerb += Effect;//ÿ���ͷŶ��ʵ���Ϊ��һ��Ч��
            }
        }
        hasAdd = true;
    }
    void Effect(AbstractVerbs buff)
    {
        buff.GetComponent<AbstractCharacter>().BeAttack(AttackType.atk, 0.2f * chara.atk * chara.atkMul, true, 0, chara);
        //�ɶ��ʻ�ȡ����ɫ����������
        //����Ч��δд
    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            foreach (var it in CharacterManager.instance.GetEnemy(chara.Camp))
            {
                it.event_UseVerb -= Effect;
            }
        }
    }
}
