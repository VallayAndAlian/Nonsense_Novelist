using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ɫ�����������Ҫ��Play�������
/// </summary>
public class CharaAnim : MonoBehaviour
{
    /// <summary>ʵ�ʶ������</summary>
    public Animator anim;
    /// <summary>��ǰ������</summary>
    public AnimEnum currentAnim;
    void Start()
    {
        currentAnim=AnimEnum.idle;
    }

    private string newAnimName;//�¶����ַ���(�����ڡ���
    private string currentAnimName;//��ǰ�����ַ���(�����ڡ���
    /// <summary>
    /// ���Ŷ��������
    /// </summary>
    /// <param name="paramName">�¶���ö��</param>
    public void Play(AnimEnum newAnimEnum)
    {
        newAnimName = Enum.GetName(typeof(AnimEnum),newAnimEnum);
        currentAnimName = Enum.GetName(typeof(AnimEnum), currentAnim);
        anim.SetBool(currentAnimName, false);        anim.SetBool(newAnimName, true);
        currentAnim = newAnimEnum;
    }
    /// <summary>
    /// �ж��Ƿ񲥷����
    /// </summary>
    /// <param name="animEnum"></param>
    /// <returns></returns>
    public bool IsEnd(AnimEnum animEnum)
    {
        AnimatorStateInfo animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        //���ŵĲ��Ǹö���
        if(!animatorInfo.IsName(Enum.GetName(typeof(AnimEnum), animEnum)))
        {
            return true;
        }
        //���ų���1��
        else if ((animatorInfo.normalizedTime > 1.0f) )
        {
            return true;
        }
        else
            return false;
    }
}