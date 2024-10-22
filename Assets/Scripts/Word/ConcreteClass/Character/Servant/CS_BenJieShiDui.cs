
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BenJieShiDui : ServantAbstract
{
    static public string s_description = "��ͨ�������ƶ���";
    static public string s_wordName = "��ӣ�����ʿ��";

    override public void Awake()
    {
        base.Awake();
        
        Destroy(attackA);
        attackA = gameObject.AddComponent<CureMode>();
    }

    AbstractCharacter[] aims;
    public override bool AttackA()
    {
   
        //����ƽA
        if (myState.aim != null)
        {
            print("���"+wordName+"��Ŀ����"+ myState.aim[0].name);
            for (int x = 0; x < myState.aim.Count; x++)
            {
                    myState.character.CreateBullet(myState.aim[x].gameObject);
                attackA.UseMode(AttackType.heal, san * sanMul * 1f, myState.character, myState.aim[x], true, 0);
            }
        
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            //��ͨ����Ŀ��ΪѪ���ٷֱ���͵Ķ��ѣ��ָ�10Ѫ����������
            //myState.aim.CreateFloatWord(
            //attackA.UseMode(myState.other, san * sanMul * 1f, myState.aim)
            //, FloatWordColor.heal, false);
           
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<ShiWuFengRong>() != null)
            Destroy(masterNow.GetComponent<ShiWuFengRong>());
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

