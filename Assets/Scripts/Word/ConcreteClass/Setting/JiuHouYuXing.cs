using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����Ƶ���ܡ����ƺ�����
/// </summary>
public class JiuHouYuXing : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "�ƺ�����";
        res_name = "jiuhouyuxing";
        info = "��ɫÿ�ͷ�4�����ʣ����7s��ʫ�顱�͡�������";
        lables = new List<string> { "��Ƶ" };
        hasAdd = false;
 
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.event_UseVerb+= Effect;//ÿ���ͷŶ��ʣ�����Ч��
            
        }
        hasAdd = true;
    }    
    void Effect(AbstractVerbs _av)
    {
        
        
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
