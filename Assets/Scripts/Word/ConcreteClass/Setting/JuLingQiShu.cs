using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����������
/// </summary>
public class JuLingQiShu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "��������";
        res_name = "julingqishu";
        info = "ľ����ÿ�θ������+3%������+6%";
        lables = new List<string> { "��ɫ", "����" };

        hasAdd = false;

        Init();


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<MuNaiYi>();
        if (chara != null)
        {
        // ÿ�θ���� + 3%���� + 6%��
            var _list =chara.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
            foreach (var _t in _list)
            {
                if (_t.GetType() == typeof(AI.NoHealthTrigger))
                { (_t as AI.NoHealthTrigger).event_relife += Effect;  }

            }
        }
        hasAdd = true;
    }
    void Effect(AbstractCharacter ac)
    {
        chara.atkMul += 0.03f;
        chara.defMul += 0.06f;

    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            var _list = chara.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
            foreach (var _t in _list)
            {
                if (_t.GetType() == typeof(AI.NoHealthTrigger))
                { (_t as AI.NoHealthTrigger).event_relife -= Effect; }

            }
        }        
    }
}
