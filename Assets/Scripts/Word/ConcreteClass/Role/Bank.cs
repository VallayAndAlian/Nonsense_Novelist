using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����
/// </summary>
class Bank : AbstractRole
{
    public void Awake()
    {
        roleID = 6;
        roleName = "����";
        description = "���ǰ����Ž��ں�ծ������";
        growHP = 5;
        growATK = 1;
        growDEF = 0.7f;
        restrainRole.Add(3, 0.3f);
    }
}
