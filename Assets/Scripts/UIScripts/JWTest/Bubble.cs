using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool isKey = false;
    private Animator animator;
    float destroyTime=0;
    [Header("事件消失事件")] public float dTime = 4;
   
    private void Update()
    {

        if (CharacterManager.instance.pause) return;

        destroyTime += Time.deltaTime;
        if (destroyTime > dTime)
        {
            GetComponent<Animator>().SetBool("boom", true);
        }  
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            //播放消失动画
            animator = this.GetComponent<Animator>();
            animator.SetBool("boom", true);
            //词条消失
            Destroy(collision.gameObject);
        }
    }
    public void DestroyBubble()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name,this.gameObject);       
    }
}
