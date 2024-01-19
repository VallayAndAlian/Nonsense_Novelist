using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff：亢奋
/// </summary>
public class KangFen : AbstractBuff
{
    static public string s_description = "为缺少3能量及以上的动词提供1能量，消除自身";
    static public string s_wordName = "亢奋";

    List<AbstractVerbs> skills;
    override protected void Awake()
    {
        base.Awake();
        buffName = "亢奋";
        description = "为缺少3能量及以上的动词提供1能量，消除自身";
        book = BookNameEnum.ZooManual;

        //缺少能量的补充
        skills = this.GetComponent<AbstractCharacter>().skills;
  

 
    }

    public override void Update()
    {
        base.Update();
        
        foreach (var _skill in skills)
        {
            if (_skill.needCD - _skill.CD >= 3)
            {
                print("_skill" + _skill.wordName);
                _skill.CD += 1;
                Destroy(this);
            }
        }
    }
    private void OnDestroy()
    {
        base.OnDestroy();
    }
}
