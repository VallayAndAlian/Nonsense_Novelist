using UnityEngine;

///<summary>
///������Ϊ��
///</summary>
class AnimationActor
{
    private Animation anim;
    public AnimationActor(Animation anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="animName">����Ƭ������</param>
    public void Play(string animName)
    {
        anim.Play(animName);
    }
    /// <summary>
    /// �ж϶����Ƿ񲥷�
    /// </summary>
    /// <param name="animName">����Ƭ������</param>
    /// <returns></returns>
    public bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }
}
