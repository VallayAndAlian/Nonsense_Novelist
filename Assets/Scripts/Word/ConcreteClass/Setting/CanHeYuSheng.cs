using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定：残荷雨声
/// </summary>
public class CanHeYuSheng : AbstractSetting
{
    AbstractCharacter chara;
    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "残荷雨声";
        info = "林黛玉拥有的动词释放所需能量+2，其中能量最高的动词，获得能量的速度翻倍";
        lables = new List<string> { "角色", "蓄能" };
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
        int cd = 0;
        AbstractVerbs highestverb;//能量最高的动词
        foreach (var verb in chara.GetComponents<AbstractVerbs>())
        {
            verb.needCD += 2;
            if (verb.needCD > cd) highestverb=verb;
        }
        //获得能量的速度翻倍？？？

    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
