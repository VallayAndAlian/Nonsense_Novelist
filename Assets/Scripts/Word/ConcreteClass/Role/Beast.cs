using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ұ��
/// </summary>
class Beast : AbstractRole
{
    public void Awake()
    {
        roleID = 7;
        roleName = "Ұ��";
        description = "û�����ǵĶ���";
        growHP = 7;
        growATK = 1.5f;
        growDEF = 0.5f;
    }
}
