using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������
/// </summary>
class Country : AbstractRole
{
    public void Awake()
    {
        roleID = 3;
        roleName = "������";
        description = "����ũ����ˣ�û��ô��������";
        growHP = 6;
        growATK = 1.3f;
        growDEF = 0.7f;
        restrainRole.Add(7, 0.2f);

    }
}
