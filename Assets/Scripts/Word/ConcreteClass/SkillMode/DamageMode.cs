using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害技能
/// </summary>
class DamageMode : AbstractSkillMode
{
    /// <summary>是否为物理伤害（仅用于展示） </summary>
    public bool isPhysics=true;

    private AbstractCharacter _useChara=null;
    public void Awake()
    {
        skillModeID = 1;
        skillModeName = "伤害";
    }
    Coroutine cor;
    /// <summary>
    /// 对目标实际影响
    /// </summary>
    /// <param name="value">实际伤害</param>
    /// <param name="character">目标（来自目标数组）</param>
    public override float UseMode(AbstractCharacter useCharacter, float value, AbstractCharacter aimCharacter)
    {
        if (useCharacter != null)//角色使用
        {
            //_useChara = useCharacter;
            //float a = Random.Range(0, 100);//暴击抽奖
            //if (a <= useCharacter.criticalChance * 100)//暴击
            //{
            //    value *= useCharacter.multipleCriticalStrike;
            //    aimCharacter.teXiao.PlayTeXiao("BaoJi");
            //    AbstractBook.afterFightText += useCharacter.CriticalText(aimCharacter);
            //}
           // print(useCharacter.wordName);
            if (aimCharacter.servants.Count > 0)
            {
                
                StartCoroutine(DelayAttack(aimCharacter.servants[0].GetComponent<AbstractCharacter>(), (int)value));
                //aimCharacter.servants[0].GetComponent<AbstractCharacter>().hp -= (int)value;
                print(this.GetComponent<AbstractCharacter>().wordName + "攻击了随从" + aimCharacter.servants[0].name);

            }
            else
            {
              
                if (cor != null) StopCoroutine(cor);
                cor=StartCoroutine(DelayAttack(aimCharacter, (int)value));
                //aimCharacter.hp -= (int)value;
            }
         
        }
        else//玩家使用（形容词）
        {
            if (cor != null) StopCoroutine(cor);
            cor = StartCoroutine(DelayAttack(aimCharacter, (int)value));
            //aimCharacter.hp -= (int)value;
        }
        return value;
    }


    #region 偷懒模拟子弹集中后再减血的效果，有空修改
    WaitForSeconds delayAttack =new WaitForSeconds(0.1f); 
    IEnumerator DelayAttack(AbstractCharacter _ac,int _value)
    {
        yield return delayAttack;
        _ac.hp -= _value;
    }
    #endregion


    /// <summary>
    /// 再次计算锁定的目标
    /// </summary>
    /// <param name="character">施法者</param>
    /// <returns></returns>
    override public AbstractCharacter[] CalculateAgain(int attackDistance, AbstractCharacter character)
    {
        
        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, NeedCampEnum.enemy);
        if (a == null)
        {
            if(_useChara==null)
             print("a == null&&_useChara==null");
            else
            print(_useChara.wordName + "fuckyou");
        } 
        return a;
    }
    override public AbstractCharacter[] CalculateRandom(int attackDistance, AbstractCharacter character, bool _ignoreBoss)
    {

        AbstractCharacter[] a = attackRange.CaculateRange(attackDistance, character.situation, _ignoreBoss);
        return a;
    }
}
