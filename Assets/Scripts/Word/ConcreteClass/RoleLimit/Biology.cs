using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ѫ��
/// </summary>
class Biology : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "Ѫ��";
        banRole.Add(gameObject.AddComponent<Bank>());
    }

}
