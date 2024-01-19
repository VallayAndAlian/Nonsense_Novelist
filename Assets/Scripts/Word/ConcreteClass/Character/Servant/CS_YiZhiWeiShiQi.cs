
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_YiZhiWeiShiQi : ServantAbstract
{
    static public string s_description = "���ڸ������ṩ�����ܡ�";
    static public string s_wordName = "��ӣ�����ιʳ��";

    AbstractCharacter[] aims;
    Coroutine coroutineAttack = null;


    override public void Awake()
    {
        base.Awake();
        characterID = 3;
        wordName = "����ιʳ��";
        bookName = BookNameEnum.ZooManual;


        hp = maxHp = 30;
        atk = 0;
        def = 10;
        psy = 5;
        san = 0;

        //mainProperty.Add("����", "��T");
        //trait = gameObject.AddComponent<Pride>();
        roleName = "����";

        brief = "���ڸ������ṩ�����ܡ�";
        description = "���ڸ������ṩ�����ܡ�";



        Destroy(attackA);
        attackA = gameObject.AddComponent<CureMode>();
        if (coroutineAttack != null) StopCoroutine(coroutineAttack);
        coroutineAttack = StartCoroutine(GiveBuffs());
    }
    WaitForSeconds wait = new WaitForSeconds(10);
    IEnumerator GiveBuffs()
    {
        while (true)
        {
             yield return wait;
            useBuff();
        }
      
    }


    public void useBuff()
    {
        print("useBuff");
        //ÿ10s����һ�������ṩ15s�ġ����ܡ�
        //����ƽA
        if (myState.aim != null)
        {
            print("myState.aim != null"+myState.aim.wordName);
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            var buff=  myState.aim.gameObject.AddComponent<KangFen>();
            buff.maxTime = 15;
         
        }
        print("myState.aim ==== null");
    }

    public override bool AttackA()
    {


        return false;
    }



    private void OnDestroy()
    {
        masterNow.DeleteServant(this.gameObject);
        if (masterNow.GetComponent<CS_YiZhiWeiShiQi>() != null)
            Destroy(masterNow.GetComponent<CS_YiZhiWeiShiQi>());
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
