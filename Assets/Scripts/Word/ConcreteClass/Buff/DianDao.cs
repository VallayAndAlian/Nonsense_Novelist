using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff���ߵ�
/// </summary>
public class DianDao : AbstractBuff
{
    static public string s_description = "������ǰ��<sprite name=\"atk\">��<sprite name=\"psy\">��������ָ�";
    static public string s_wordName = "�ߵ�";


    bool hasUse = false;
    float record = 0;
    override protected void Awake()
    {
       
        buffName = "�ߵ�";
        description = "������ǰ��<sprite name=\"atk\">��<sprite name=\"psy\">��������ָ�";
        book = BookNameEnum.allBooks;

        upup = 1;
        isBad = true;
        base.Awake();

        //����ѱ��ߵ������ӳ�ʱ�䡣
        var _diandaos = GetComponents<DianDao>();
        for (int i = 0; i < _diandaos.Length; i++)
        {
            if (_diandaos[i] != this)
            {
                _diandaos[i].maxTime += this.maxTime;
                Destroy(this);
                return;
            }
        }

        //�˺�����
      
        record = chara.atk*chara.atkMul; 
     
        chara.atk = (chara.psy *chara.psyMul)/chara.atkMul;
       
        chara.psy = record / chara.psyMul;
     
        hasUse = true;
 

    }


    private void OnDestroy()
    {
        if (hasUse)
        {
            record = chara.atk * chara.atkMul;

            chara.atk = (chara.psy * chara.psyMul) / chara.atkMul;

            chara.psy = record / chara.psyMul;
            base.OnDestroy();
        }

    }
}
