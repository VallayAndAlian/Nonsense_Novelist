using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ը�
/// </summary>
class Pride :AbstractTrait
{
    public void Awake()
    {
        traitName = "����";
        traitEnum=TraitEnum.Pride;
        restrainRole.Add(TraitEnum.Persistent);
        restrainRole.Add(TraitEnum.Spicy);
    }
}
