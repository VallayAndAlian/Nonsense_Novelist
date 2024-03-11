using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ܡ���ı����
/// </summary>
public class MouDingHouDong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ı����";
        info = "��ɫ���еĶ����У�������ߵĶ��ʣ�����+4���ͷź�׷��һ����ͬ����";
        lables = new List<string> { "����" };
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_AddVerb += Effect;
        }
        hasAdd = true;
    }    
    void Effect(AbstractVerbs _av)
    {
        AbstractVerbs absv=null;
        foreach (var it in _av.GetComponent<AbstractCharacter>().GetComponents<AbstractVerbs>())//��ɫÿ������
        {
            int cd = 0;
            if (it.needCD > cd)
            {
                cd = it.needCD;
                absv = it;
            }
        }
        absv.CD += 4;
        absv.UseVerb(_av.GetComponent<AbstractCharacter>());//�����ͷţ��ڶ����ͷŵ�ʱ����׷����
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return; 
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_AddVerb += Effect;
        }
    }
}
