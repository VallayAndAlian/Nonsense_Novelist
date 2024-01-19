using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff\״̬������
/// </summary>
/// 

public class HuanBing_x : AbstractBuff
{
    public float effectTime;
    private float nowTime = 0;
    private bool isOpen = false;
    WaitForSeconds wf1second= new WaitForSeconds(1);

    DamageMode damageMode;
    override protected void Awake()
    {
        base.Awake();

        buffName = "����";
        book = BookNameEnum.FluStudy;

        StartCoroutine(Wait());
        upup = 999;//����

        isOpen = true;
        nowTime = 0;
        damageMode.attackRange = new SingleSelector();
    }


    //ÿ���ܵ�2�������˺���
    IEnumerator Wait()
    {
        while (isOpen)
        {
             yield return wf1second;

        }
    }

    public override void Update()
    {
        base.Update();
        if (nowTime > effectTime)
        {
            isOpen = false;
            StopCoroutine(Wait());
            return;
        }

        nowTime += Time.deltaTime;

    }
}
