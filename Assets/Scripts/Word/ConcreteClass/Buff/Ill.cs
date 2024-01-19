using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff：患病
/// </summary>
public class Ill : AbstractBuff
{
    static public string s_description = "每秒受到2点物理伤害，连续患病30s后变得<color=#dd7d0e>虚弱</color>";
    static public string s_wordName = "患病";

    /// <summary>外部赋值使用者</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
        base.Awake();
        buffName = "患病";
        description = "每秒受到2点物理伤害，连续患病30s后变得<color=#dd7d0e>虚弱</color>";
        book = BookNameEnum.FluStudy;
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
        isAll = true;
        nowTime = 0;

        StartCoroutine(MakeAttack());
    }


    public override void Update()
    {
        base.Update();
        //nowTime += Time.deltaTime;
        //if(nowTime>1)
        //{
        //    nowTime= 0;
        //    damageMode.UseMode(useCharacter!=null?useCharacter:chara, 2 * (1 - chara.def / (chara.san + 20)), chara);
        //}
    }

    IEnumerator MakeAttack() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            nowTime += 1;
            
             damageMode.UseMode(useCharacter != null ? useCharacter : chara, 2 * (1 - chara.def / (chara.def + 20)), chara);
            if (nowTime >= 30)
            {
                //buffs.Add(gameObject.AddComponent<>());
                //buffs[0].maxTime = Mathf.Infinity;
                nowTime = -9999;
            }
        }
    }

    private void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}
