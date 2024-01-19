using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ը�
/// </summary>
abstract public class AbstractTrait : MonoBehaviour
{
    /// <summary>�Ը��� </summary>
    public string traitName;
    /// <summary>�Ը�ö�٣������ڿ���</summary>
    public TraitEnum traitEnum;
    /// <summary>�ܿ��Ƶ��Ը�(�������30%�˺�)</summary>
    public List<TraitEnum> restrainRole=new List<TraitEnum>();
}

/// <summary>
/// �Ը�ö�٣������ڿ���
/// </summary>
public enum TraitEnum
{
    /// <summary>����</summary>
    Sentimental,
    /// <summary>����</summary>
    Spicy,
    /// <summary>����</summary>
    ColdInexorability,
    /// <summary>��</summary>
    Vicious,
    /// <summary>ǿ��</summary>
    Possessive,
    /// <summary>����</summary>
    Persistent,
    /// <summary>�Ȱ�</summary>
    Mercy,
    /// <summary>����</summary>
    Pride,
}

