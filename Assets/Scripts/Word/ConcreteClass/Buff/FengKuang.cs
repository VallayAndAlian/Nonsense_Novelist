using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff�����
/// </summary>
public class FengKuang : AbstractBuff
{
    static public string s_description = "����ʱ���ֵ��ң��޷������Լ���\n��������Ҳ�������������";
    static public string s_wordName = "���";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {

        buffName = "���";
        description = "����ʱ���ֵ��ң��޷������Լ���\n��������Ҳ�������������";
        book = BookNameEnum.ZooManual;
        maxTime = 2;
        upup = 1;
        isBad = true;
        isAll = false;
        base.Awake();

        //���˽�ɫ��Ŀ������Ϊ���
        chara.SetAimRandom(true);
    }
    public override void Update()
    {
        base.Update();
        
    }
    private void OnDestroy()
    {
      
        chara.SetAimRandom(true);
        base.OnDestroy();

    }
}
