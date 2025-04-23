using System;
using Spine.Unity;
using UnityEngine;

public class UnitModelLayout : MonoBehaviour
{
    protected Animator mAnimator = null;
    protected AnimEventReceiver mAnimEvents = null;
    protected AudioSource mAudioSource = null;
    protected SkeletonAnimation mSpineAnimator = null;
    
    public Animator Animator => mAnimator;
    public AnimEventReceiver AnimEvents => mAnimEvents;
    public AudioSource AudioSource => mAudioSource;

    
    protected Transform mWeaponPart;
    public Transform WeaponPart => mWeaponPart;
    
    // 角色的各个骨骼的transform
    
    public void Setup(UnitViewBase unitObj)
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator != null)
        {
            mAnimator.runtimeAnimatorController = unitObj.Asset.animatorController;
        }
        else
        {
            mSpineAnimator = GetComponent<SkeletonAnimation>();
        }


        mAnimEvents = mAnimator.GetComponent<AnimEventReceiver>();
        mAudioSource = GetComponent<AudioSource>();

        mWeaponPart = transform;
    }

    public void PlayAnimation(string animName, bool isLoop = false)
    {
        if (Animator != null)
        {
            Animator.Play(animName, 0, 0.0f);
        }
        else if (mSpineAnimator != null)
        {
            mSpineAnimator.state.SetAnimation(0, animName, isLoop);
        }
    }

    private void Update()
    {
    }
}