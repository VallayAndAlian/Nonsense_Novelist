using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ӱ������ֱ�ߡ����Ρ�Բ�Σ�
/// </summary>
public interface  IAttackRange
{
    /// <summary>
    /// ����Ӱ��������Ŀ��
    /// </summary>
    /// <param name="attackDistance">���</param>
    /// <param name="ownTrans">ʹ����վλ</param>
    /// <param name="ownTrans">������Ӫ</param>
    /// <returns>����������Ŀ������</returns>
    abstract public AbstractCharacter[] CaculateRange(int attackDistance,Situation situation,NeedCampEnum needCamp);
    abstract public AbstractCharacter[] CaculateRange(int attackDistance, Situation situation, bool _ignoreBoss);
}
