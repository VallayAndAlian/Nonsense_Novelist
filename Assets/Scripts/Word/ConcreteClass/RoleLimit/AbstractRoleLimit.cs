using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������
/// </summary>
public class AbstractRoleLimit : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    public string thisName;
    /// <summary>
    /// ��ֹ�����
    /// </summary>
    public List<AbstractRole> banRole=new List<AbstractRole>();
    /// <summary>
    /// ��ֹ���Ա�
    /// </summary>
    public List<GenderEnum> banGender=new List<GenderEnum>();
}
