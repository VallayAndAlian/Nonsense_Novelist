using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ը�
/// </summary>
class Spicy : AbstractTrait
{
    public void Awake()
    {
        traitName = "����";
        traitEnum=TraitEnum.Spicy;
        restrainRole.Add(TraitEnum.Possessive);
        restrainRole.Add(TraitEnum.Vicious);
    }
}
