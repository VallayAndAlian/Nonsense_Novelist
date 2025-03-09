using System;
using UnityEngine;


public class NnProjectile : MonoBehaviour
{
    public enum State
    {
        None = 0,
        Route,
        Hit,
        Expired,
    }

    protected State mState = State.None;
    
    protected EmitMeta mMeta;
    protected Vector3 mEmitPos;
    protected Vector3 mTargetPos;
    protected float mTimer = 0;
    protected float mExpiredTime = 0;
    protected bool mExpired = false;
    
    public void Emit(UnitViewBase emitter, EmitMeta meta)
    {
        mMeta = meta;
        
        mEmitPos = emitter.ModelLayout.WeaponPart.position;
        mTargetPos = meta.mTarget.UnitView.transform.position;
        
        Vector3 offset = mTargetPos - mEmitPos;
        var transform1 = transform;
        transform1.position = mEmitPos;
        transform1.forward = offset;
        
        if (meta.mDelay <= 0)
            meta.mDelay = 1.0f;
        
        mState = State.Route;
        mExpiredTime = meta.mDelay * 1.5f + 2;
    }

    private void Update()
    {
        mTimer += Time.deltaTime;
        
        switch (mState)
        {
            case State.Route:
            {
                float percent = (mMeta.mDelay - mTimer) / mMeta.mDelay;
                transform.position = (1 - percent) * mEmitPos + percent * mTargetPos;

                if (percent >= 1.0f)
                {
                    mState = State.Hit;
                    OnEnterHit();
                }
                
                break;
            }

            default:
                break;
        }

        if (mTimer > mExpiredTime)
        {
            mState = State.Expired;
            Expired();
        }
    }

    protected void OnEnterHit()
    {
        if (mMeta.mTarget != null)
        {
            // play hit effect

            Expired();
        }
    }
    
    protected void Expired()
    {
        if (mExpired)
            return;
        
        mExpired = true;
        Destroy(this);
    }
}