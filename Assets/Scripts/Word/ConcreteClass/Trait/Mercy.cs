using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Ȱ��Ը�
/// </summary>
class Mercy :AbstractTrait
{
    public void Awake()
    {
        traitName = "�Ȱ�";
        traitEnum=TraitEnum.Mercy;
        restrainRole.Add(TraitEnum.Spicy);
        restrainRole.Add(TraitEnum.ColdInexorability);
    }
}
