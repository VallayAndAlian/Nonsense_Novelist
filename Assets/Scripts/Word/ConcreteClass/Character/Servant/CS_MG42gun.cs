
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MG42gun : ServantAbstract
{


    //���������Ĵ���
    private int attackCountMax=0;
    private int attackCountNow = 0;
    private int attackWait = 0;
    private float attackWaitTime = 0;
    override public void Awake()
    {
        base.Awake();
        characterID = 4;
        wordName = "MG-42��ǹ";
        bookName = BookNameEnum.allBooks;
        //gender = GenderEnum.boy;

        hp = maxHp = 40;
        atk = 5;
        def = 00;
        psy = 0;
        san = 0;

        //mainProperty.Add("����", "��T");
        //trait = gameObject.AddComponent<Pride>();
        roleName = "����";

        brief = "���ٸ��죬��Ҫ����";
        description = "���ٸ��죬��Ҫ����";

        //�������Ϊ1s��ÿ����8�ι�����ֹͣ����5s
        attackInterval = 1f;
        attackCountMax = 8;
        attackWaitTime = 5f;

        Destroy(attackA);
        attackA = gameObject.AddComponent<DamageMode>();
    }



    //���ƽA
    public override bool AttackA()
    {

        if (attackWait >= attackWaitTime)
        {
            attackCountNow = 0;
            attackWait = 0;
            return true;
        }

        if (attackCountNow >= attackCountMax)
        {
            attackWait += 1;
            return true;
        }
        //base:
        if (myState.aim != null)
        {
            myState.character.CreateBullet(myState.aim.gameObject);
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            myState.aim.CreateFloatWord(
                attackA.UseMode(myState.character, myState.character.atk * (1 - myState.aim.def / (myState.aim.def + 20)), myState.aim)
                , FloatWordColor.physics, false);
            attackCountNow += 1;
            return true;
        }
        return false;
    }






    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "���ѿ�������һ�����ã�ϸ�����ݣ�ֻ������㣬����΢΢���о�ʱ��毻���ˮ���ж������������磬" + otherChara.wordName + "Ц������������ã����������ġ�";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "���Ҿ�֪�������˲���ʣ�µ�Ҳ�����ҡ�������������һ�仨�꣬��" + otherChara.wordName + "��ȥ";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "�������Ů��Ϣ���������ϻ���ա����㽫һ�����ӣ�һ��ʫ��پ��ڻ����С�";
    }
    public override string DieText()
    {
        return "�����񡭱�����á���������û˵��������˫�ۡ�";
    }


}
