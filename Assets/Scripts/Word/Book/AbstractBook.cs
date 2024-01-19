using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AbstractBook : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    protected AbstractCharacter leadingChara;
    /// <summary>
    /// ս���ģ���ʱ��ؼ�
    /// </summary>
    static public string beforeFightText, afterFightText;

    /// <summary>
    /// ���н�ɫ�ĸ�����
    /// </summary>
    protected GameObject fatherObject;

    private void Start()
    {
        fatherObject = GameObject.Find("panel");
        FindLeadingChara();
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    protected void FindLeadingChara()
    {
        if (leadingChara == null)
            leadingChara = fatherObject.transform.Find("SelfCharacter").GetComponentInChildren<AbstractCharacter>();
    }

    abstract public string GetText(int character, int part, int phase);
}
