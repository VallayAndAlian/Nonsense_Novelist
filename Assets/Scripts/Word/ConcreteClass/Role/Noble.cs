using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���
/// </summary>
class Noble : AbstractRole
{
    public void Awake()
    {
        roleID = 1;
        roleName = "���";
        description = "���ֺ��е��ӵ�";
        growHP = 3;
        growATK = 0.7f;
        growDEF = 0.4f;
        restrainRole.Add(3, 0.1f);
        restrainRole.Add(6, 0.3f);
    }
}
