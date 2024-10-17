using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//huaiyizhuyiBOSSר������
//Ч���ǣ������������ɫ��Ч��10���ڣ������뾫��ÿ������5%;����ʱ���������2*������+����-4*��־�ľ����˺����˺����Ϊ20��


public class Renzhiguhua_buff : AbstractBuff
{
    float record;
    float remaindTime = 10;//����Ч����ά��ʱ��
    AbstractCharacter usingChara;

    float timePer = 0;
    float time = 0;


    override protected void Awake()
    {
       
        buffName = "��֪�̻�";
        book = BookNameEnum.allBooks;

        usingChara=this.GetComponent<AbstractCharacter>();

        StartCoroutine(DoPerSecond());
        StartCoroutine(EndBuff());


        upup = 1;
        base.Awake();
    }


    /// <summary>
    /// ÿ��ִ��
    /// </summary>
    /// <returns></returns>
    IEnumerator DoPerSecond()
    {
        while (true)
        {
            AddEffect();
            yield return new WaitForSeconds(1);
        }
       

    }

    IEnumerator EndBuff()
    {
      
        yield return new WaitForSeconds(remaindTime);

        EndAndAttack();
        StopCoroutine(DoPerSecond());
    }

    /// <summary>
    ///����ʱ��һ��������˺�s
    /// </summary>
    public void EndAndAttack()
    {

        //2*������+����-4*��־�ľ����˺����˺����Ϊ20��
        float _endAttack=2*(usingChara.atk+usingChara.psy)-4*usingChara.san*usingChara.sanMul;
        if (_endAttack >= 20)
        { 
           
            //usingChara.BeAttack(AttackType.dir, 20, true, 0, this.GetComponent<AbstractCharacter>());
            DealDamageCalc _temp = new DealDamageCalc();
            _temp.mInstigator = this.GetComponent<AbstractCharacter>();
            _temp.mTarget = usingChara;
            _temp.mMinAttack = 20;
            _temp.mMaxAttack = 20;

            DamageHelper.ProcessDamage(_temp);
        }
        else
        {
            //usingChara.BeAttack(AttackType.dir, _endAttack, true, 0, this.GetComponent<AbstractCharacter>());

            DealDamageCalc _temp = new DealDamageCalc();
            _temp.mInstigator = this.GetComponent<AbstractCharacter>();
            _temp.mTarget = usingChara;
            _temp.mMinAttack = _endAttack;
            _temp.mMaxAttack = _endAttack;

            DamageHelper.ProcessDamage(_temp);
        }
    }

    /// <summary>
    /// �����뾫��ÿ������5%;
    /// </summary>
    public void AddEffect()
    {
        //print(usingChara.wordName + "�Ĺ����뾫������5%;");
        usingChara.hp *= 1.05f;
        usingChara. psyMul+= 0.05f;
    }
}
