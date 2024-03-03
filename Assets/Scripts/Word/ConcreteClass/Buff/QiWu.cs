using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// buff:����
/// </summary>
public class QiWu : AbstractBuff
{
    static public string s_description = "��ͨ�����������е��ˣ��˺�����70%����������Ч��";
    static public string s_wordName = "����";

    private int recordAimCount;
    bool hasUse = false;

    override protected void Awake()
    {
       
        buffName = "����";
        description = "��ͨ�����������е��ˣ��˺�����70%����������Ч��";
        book = BookNameEnum.allBooks;

        base.Awake();

        var _qiwus = GetComponents<QiWu>();
        for (int i=0;i< _qiwus.Length;i++)
        {
            if (_qiwus[i] != this)//�����ɫ���ϻ���������������
            {
                _qiwus[i].maxTime += this.maxTime;
                Destroy(this);
                return;
            }
        }

        //�����ɫ����û�й�������������

        recordAimCount = chara.myState.aimCount;
        chara.myState.aimCount = 10;

        chara.attackAmount -= 0.7f;
        hasUse = true;

    }

    private void Update()
    {
        
        base.Update();
    }
    private void OnDestroy()
    {
        if (hasUse)//��������ʱ���ʧȥʱ����ײ��ʱ����ܻ���bug����δ���
        { 
            chara.myState.aimCount = recordAimCount;
            chara.attackAmount += 0.7f;
            base.OnDestroy();
        }
        
    }
}
