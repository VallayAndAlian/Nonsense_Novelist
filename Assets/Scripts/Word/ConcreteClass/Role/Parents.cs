using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ҳ�
/// </summary>
class Parents : AbstractRole
{
    public void Awake()
    {
        roleID = 4;
        roleName = "�ҳ�";
        description = "�ੲ��ݵļҳ�";
        growHP = 5;
        growATK = 1;
        growDEF = 0.6f;
        restrainRole.Add(1, 0.2f);
        restrainRole.Add(7, 0.2f);
    }
}
