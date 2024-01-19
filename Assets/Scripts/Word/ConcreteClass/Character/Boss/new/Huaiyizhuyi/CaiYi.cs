
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiYi : AbstractBuff
{
    float orgAtk;
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        book = BookNameEnum.allBooks;

        //�˺�Ч������50%
        orgAtk = chara.atk;
        chara.atk = orgAtk*0.5f;

        upup = 1;
        maxTime = 5f;

        
        //���Ŀ�꿪�ش򿪣�����Ч��ʱ�ٹرմ˿���
        chara.SetAimRandom(true);
        AbstractCharacter _ac=chara.myState.GetANewAim(true);
        chara.myState.SetUnchangeAim(_ac);
        print(chara.wordName + "���ڵ�Ŀ����" + chara.myState.aim.wordName);
  
    }
    override public void Update()
    {
        base.Update();
        print(chara.wordName + "Update()���ڵ�Ŀ����" + chara.myState.aim.wordName);
        //���������ɫB������5�룬�˺��������50 %
        //��������B���������ɫC����ɫ���Է�����ã���CҲ������A����Ч��һ�£����ݴ�ѭ����ֱ��boss����


    }

    private void OnDestroy()
    {

        if (chara.myState == null || chara.myState.aim== null) return;
        //Ϊ���ʩ��Ŀ������CAIYI��buff
        var _b = chara.myState.aim.gameObject.AddComponent<CaiYi>();
        _b.maxTime = 10;
    

        //���Ŀ�꿪�عرգ�����Ч��ʱ�ٹرմ˿���
        chara.SetAimRandom(false);
        chara.myState.SetUnchangeAim(null);
        //�����ָ�ԭװ
        chara.atk = orgAtk;
    }
}

