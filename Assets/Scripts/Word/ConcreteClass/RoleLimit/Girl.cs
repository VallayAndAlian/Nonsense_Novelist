using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ů��
/// </summary>
class Girl : AbstractRoleLimit
{
    public void Awake()
    {
        thisName = "Ů��";
        banGender.Add(GenderEnum.boy);
        banGender.Add(GenderEnum.noGender);
    }

}
