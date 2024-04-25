using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �趨�����Ĵ�˯
/// </summary>
public class HaiTangChunShui : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "���Ĵ�˯";
        res_name = "haitangchunshui";
        info = "������ÿӵ��50���꣬��ӵ�ж����е�һ��������1�������ޣ�����Ϊ1";
        lables = new List<string> { "��ɫ", "����" };
        hasAdd = false;
       
    }
    public override void Init()
    {
        if (hasAdd) return;

         chara = CharacterManager.instance.gameObject.GetComponentInChildren<LinDaiYu>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {
        

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
