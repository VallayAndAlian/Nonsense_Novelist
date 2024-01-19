using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
///<summary>
///ÌØÐ§
///</summary>
public class TeXiao: MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void PlayTeXiao(string animName)
    {
        animator.Play(animName);
    }
}
