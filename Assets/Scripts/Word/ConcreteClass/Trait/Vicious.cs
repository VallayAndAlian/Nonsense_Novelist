using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ը�
/// </summary>
class Vicious : AbstractTrait
{
    public void Awake()
    {
        traitName = "��";
        traitEnum = TraitEnum.Vicious;
        restrainRole.Add(TraitEnum.Sentimental);
        restrainRole.Add(TraitEnum.Mercy);
    }
}
