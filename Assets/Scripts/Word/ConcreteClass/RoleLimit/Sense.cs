using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����
/// </summary>
class Sense : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "����";
        banRole.Add(gameObject.AddComponent<Beast>());

    }

}
