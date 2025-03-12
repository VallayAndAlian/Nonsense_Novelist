using System;
using UnityEngine;

public class UnitModelLayout : MonoBehaviour
{
    protected Animator mAnimator = null;
    protected AnimEventReceiver mAnimEvents = null;
    protected AudioSource mAudioSource = null;
    
    public Animator Animator => mAnimator;
    public AnimEventReceiver AnimEvents => mAnimEvents;
    public AudioSource AudioSource => mAudioSource;

    
    protected Transform mWeaponPart;
    public Transform WeaponPart => mWeaponPart;
    
    // 角色的各个骨骼的transform
    
    public void Setup(UnitViewBase unitObj)
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
            mAnimator = transform.GetComponentInChildren<Animator>();

        mAnimEvents = mAnimator.GetComponent<AnimEventReceiver>();
        mAudioSource = GetComponent<AudioSource>();

        mWeaponPart = transform;
    }

    private void Update()
    {
    }
}