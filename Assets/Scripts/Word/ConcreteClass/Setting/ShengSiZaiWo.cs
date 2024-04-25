using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����������
/// </summary>
public class ShengSiZaiWo : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.PingYong;
        settingName = "��������";
        res_name = "shengsizaiwo";
        info = "��Ŭ��˹�Ķ��ѻ���봥������ʱ����Ŭ��˹���䶼����30����";
        lables = new List<string> { "��ɫ", "����" };

        hasAdd = false;

    


    }


    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<Anubis>();
        if (chara != null)
        {
            foreach (var it in CharacterManager.instance.GetFriend(chara.camp))//��ȡ�ѷ����н�ɫ�������Լ���
            {
                if (it.name == "��Ŭ��˹") { }
                else
                {
                    //��������
                    var _list = it.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;//�ҵ�it���Ϲ���״̬�����д�����
                    foreach (var _t in _list)
                    {
                        if (_t.GetType() == typeof(AI.NoHealthTrigger))//�жϴ��������ͣ�����
                        { (_t as AI.NoHealthTrigger).event_relife += Effect; }//����һ��Ч��

                    }
                    //��ø���
                    it.event_GetRelife += Effect;
                }
                
            }
        }
        hasAdd = true;
    }
    void Effect(AbstractCharacter c)
    {
        c.BeCure(30, true, 0);
        chara.BeCure(30, true, 0); 
    }

    private void OnDestroy()
    {
        if (hasAdd)
        {
            foreach (var it in CharacterManager.instance.GetFriend(chara.camp))//��ȡ�ѷ����н�ɫ�������Լ���
            {
                if (it.name == "��Ŭ��˹") { }
                else
                {
                    //��������
                    var _list = it.myState.allState.Find(p => p.id == AI.StateID.attack).triggers;//�ҵ�it���Ϲ���״̬�����д�����
                    foreach (var _t in _list)
                    {
                        if (_t.GetType() == typeof(AI.NoHealthTrigger))//�жϴ��������ͣ�����
                        { (_t as AI.NoHealthTrigger).event_relife -= Effect; }//����һ��Ч��

                    }
                    it.event_GetRelife -= Effect;
                }

            }
        }
    }
}
