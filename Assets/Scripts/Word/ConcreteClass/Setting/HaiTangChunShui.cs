using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaiTangChunShui : AbstractSetting
{
   



    AbstractCharacter chara;

    public override void Start()
    {
        base.Start();

        level = SettingLevel.PingYong;
        name = "���Ĵ�˯";
        info = "������ÿӵ��50���꣬��ӵ�ж����е�һ��������1�������ޣ�����Ϊ1";
        lables = new List<string> { "��ɫ", "����" };

        hasAdd = false;

        Init();


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
