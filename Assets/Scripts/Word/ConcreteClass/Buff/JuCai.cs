using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff���۲ƣ�αbuff��
/// </summary>
public class JuCai : AbstractBuff
{
    static public string s_description = "ʹ��ɫ��õ���һ�����ʷ���";
    static public string s_wordName = "�۲Ƶ�";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {

        buffName = "�۲Ƶ�";
        description = "ʹ��ɫ��õ���һ�����ʷ���";
        book = BookNameEnum.CrystalEnergy;
        maxTime = Mathf.Infinity;
        upup = 1;
        isBad = false;
        isAll = false;

        //awake���ݣ�ɾ���˻��buffƮ��
        chara = GetComponent<AbstractCharacter>();
        int num = 0;
        AbstractBuff[] buffs = GetComponents<AbstractBuff>();

        for (int i = buffs.Length - 1; i > -1; i--)
        {//����,�����buff�ȱ�ɾ��
            if (buffs[i].buffName == buffName)
            {
                num++;
                if (num > upup)
                {

                    Destroy(buffs[i]);
                }
            }
        }
        chara.AddBuff(this);



        //����ɫ����������

        chara.event_AddNoun += WhenGetWord;
    }

    public void WhenGetWord(AbstractItems _item)
    {
        chara.event_AddNoun -= WhenGetWord;
        chara.gameObject.AddComponent(_item.GetType());
        chara.AddNoun(_item);
        Destroy(this);
    }

    public override void Update()
    {
        base.Update();
    }

     private void OnDestroy()
    {

        base.OnDestroy();

    }
}
