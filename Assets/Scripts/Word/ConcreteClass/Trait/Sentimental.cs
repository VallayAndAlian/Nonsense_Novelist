using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ը�
/// </summary>
class Sentimental :AbstractTrait
{
    public void Awake()
    {
        traitName = "����";
        traitEnum=TraitEnum.Sentimental;
        restrainRole.Add(TraitEnum.Persistent);
        restrainRole.Add(TraitEnum.Pride);
    }
}
