using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class QingMi : AbstractBuff
{
    static public string s_description = "���Ѷ������ѷ����˺�����;";
    static public string s_wordName = "����";

    bool hasUse = false;
    override protected void Awake()
    {

        buffName = "����";
        description = "���Ѷ������ѷ����˺�����";
        book = BookNameEnum.Salome;
        isBad = true;
        isAll = false;
        maxTime = 100;

        base.Awake();

        chara.teXiao.PlayTeXiao("fuhuo");

        //����ѱ��������ӳ�ʱ�䡣
        var _fuhuos = GetComponents<FuHuo>();
        for (int i = 0; i < _fuhuos.Length; i++)
        {
            if (_fuhuos[i] != this)
            {
                _fuhuos[i].maxTime += this.maxTime;

                Destroy(this);
                return;
            }
        }

        //�˺�����
        hasUse = true;
        chara.attackAmount -= 0.5f;
        chara.hasBetray = true;


    }


    private void OnDestroy()
    {
        if (hasUse)
        {
            chara.attackAmount += 0.5f;
            chara.hasBetray = false;
            base.OnDestroy();

        }



        // if (GetComponent<AbstractCharacter>() == null) return;
        //if(this.GetComponents<FuHuo>().Length<=1)//ֻ���Լ�
        // {
        //     if(GameObject.Find("AllCharacter").GetComponentsInChildren<FuHuo>().Length<=1)
        //         AbstractVerbs.OnAwake -= FuHuoSkill;

        //     AbstractCharacter character = GetComponent<AbstractCharacter>();
        //     if (character.attackA.GetType() == typeof(FuHuoMode))
        //     {
        //         DamageMode newMode = gameObject.AddComponent<DamageMode>();
        //         newMode.attackRange = character.attackA.attackRange;
        //         Destroy(character.attackA);
        //         character.attackA = newMode;
        //     }
        //     AbstractVerbs[] allVerb = GetComponents<AbstractVerbs>();
        //     foreach (AbstractVerbs verb in allVerb)
        //     {
        //         if (verb.skillMode.GetType() == typeof(FuHuoMode))
        //         {
        //             DamageMode newMode = gameObject.AddComponent<DamageMode>();
        //             newMode.attackRange = verb.skillMode.attackRange;
        //             Destroy(verb.skillMode);
        //             verb.skillMode = newMode;
        //         }
        //     }
        // }
    }
}
