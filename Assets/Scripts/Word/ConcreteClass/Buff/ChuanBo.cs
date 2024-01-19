using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// （弃用）buff：传播
/// </summary>
public class ChuanBo: AbstractBuff
{
    override protected void Awake()
    {
        base.Awake();
        buffName = "传播";
        book = BookNameEnum.FluStudy;
        upup= 1;
    }

    /// <summary>
    /// 返回邻居，让词脚本去赋值
    /// </summary>
    public AbstractCharacter[] GetNeighbor(AbstractCharacter center)
    {
       Situation[] situation= CollectionHelper.FindAll<Situation>(Situation.allSituation, p => Situation.Distance(center.situation, p) <= 1);
        List<AbstractCharacter> result=new List<AbstractCharacter>();
        foreach (Situation s in situation)
        {
            result.Add(s.GetComponentInChildren<AbstractCharacter>());
        }
        return result.ToArray();
    }
}
