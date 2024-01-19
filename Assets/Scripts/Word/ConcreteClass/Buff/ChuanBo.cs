using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����ã�buff������
/// </summary>
public class ChuanBo: AbstractBuff
{
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        book = BookNameEnum.FluStudy;
        upup= 1;
    }

    /// <summary>
    /// �����ھӣ��ôʽű�ȥ��ֵ
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
