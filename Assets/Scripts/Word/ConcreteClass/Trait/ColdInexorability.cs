using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ը�
/// </summary>
class ColdInexorability : AbstractTrait
{
    public void Awake()
    {
        traitName = "����";
        traitEnum = TraitEnum.ColdInexorability;
        restrainRole.Add(TraitEnum.Mercy);
        restrainRole.Add(TraitEnum.Sentimental);
    }
}
