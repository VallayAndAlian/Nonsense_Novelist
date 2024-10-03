using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
/// <summary>
/// ��ɫ�����������Ҫ��Play�������
/// </summary>
public class CharaAnim : MonoBehaviour
{
    /// <summary>ʵ�ʶ������</summary>
    [HideInInspector]public Animator anim;
    /// <summary>��ǰ������</summary>
    [Header("������ʾ��")]
    public AnimEnum currentAnim;
    
    private SkeletonAnimation spineAnim;


    void Start()
    {
        currentAnim=AnimEnum.idle;
        spineAnim = GetComponentInChildren<SkeletonAnimation>();
        if (spineAnim != null)
        {
            spineAnim.Initialize(true);
        }


        TryGetComponent<Animator>(out anim);

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
        anim.SetBool(currentAnimName, false); 
        anim.SetBool(newAnimName, true);
        currentAnim = newAnimEnum;
    }


    /// <summary>
    /// ���Ŷ��������
    /// </summary>
    /// <param name="paramName">�¶���ö��</param>
    public void Play(string newAnimEnum)
    {
        spineAnim.AnimationState.SetAnimation(0, newAnimEnum, true);
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


    public void SetSpeed(AnimEnum animEnum, float speed)
    {
        anim.speed = 1 * speed;
        if (spineAnim != null)
            spineAnim.timeScale = 1 * speed;
    }
}