using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ǿ���Ը�
/// </summary>
class Possessive: AbstractTrait
{
    public void Awake()
    {
        traitName = "ǿ��";
        traitEnum = TraitEnum.Possessive;
        restrainRole.Add(TraitEnum.ColdInexorability);
        restrainRole.Add(TraitEnum.Pride);
    }
}
