using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �޵�
/// </summary>
class OldEnemy : AbstractRole
{
    public void Awake()
    {
        roleID = 5;
        roleName = "�޵�";
        description = "������ȴ�����������Ķ���";
        growHP = 5;
        growATK = 1.6f;
        growDEF = 1;
        restrainRole.Add(1, 0.3f);
    }
}
