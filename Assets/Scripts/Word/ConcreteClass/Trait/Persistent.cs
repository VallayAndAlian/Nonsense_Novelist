using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ը�
/// </summary>
class Persistent : AbstractTrait
{
    public void Awake()
    {
        traitName = "����";
        traitEnum = TraitEnum.Persistent;
        restrainRole.Add(TraitEnum.Vicious);
        restrainRole.Add(TraitEnum.Possessive);
    }
}
