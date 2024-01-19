using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
abstract public class AbstractRole : MonoBehaviour
{
    /// <summary>������ </summary>
    public int roleID;
    /// <summary>������� </summary>
    public string roleName;
    /// <summary>������� </summary>
    public string description;
    /// <summary>Ѫ���ɳ� </summary>
    public int growHP;
    /// <summary>�����ɳ� </summary>
    public float growATK;
    /// <summary>�����ɳ� </summary>
    public float growDEF;
    /// <summary>�ܿ��Ƶ������ţ�����ǿ�ȣ�С���� </summary>
    public Dictionary<int, float> restrainRole=new Dictionary<int, float>();

}
