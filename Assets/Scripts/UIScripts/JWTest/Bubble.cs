using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool isKey = false;
    private Animator animator;
    float destroyTime=0;
    [Header("事件消失时间")] public float dTime = 4;
    [Header("事件LOGO（顺序不可更改）")] public GameObject[] logo_Event;
    private List<GameObject> array = new List<GameObject>();

    private void Update()
    {

        if (CharacterManager.instance.pause) return;

        destroyTime += Time.deltaTime;
        if (destroyTime > dTime)
        {
            GetComponent<Animator>().SetBool("boom", true);
        }  
    }
    /// <summary>
    /// 与词球碰撞
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            //播放气泡消失动画
            animator = this.GetComponent<Animator>();
            animator.SetBool("boom", true);
            //播放事件大logo动画
            switch (this.gameObject.name)
            {
                case "fangke(Clone)": {GameObject a= Instantiate(logo_Event[0], Vector3.zero, Quaternion.identity); array.Add(a);  } break;
                case "xiwang(Clone)": { GameObject a = Instantiate(logo_Event[1], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "yiwai(Clone)": { GameObject a = Instantiate(logo_Event[2], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "weiji(Clone)": { GameObject a = Instantiate(logo_Event[3], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "jiaoyi(Clone)": { GameObject a = Instantiate(logo_Event[4], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            }
            //词条消失
            Destroy(collision.gameObject);
        }
    }
    /// <summary>
    /// 打开事件面板
    /// </summary>
    void OpenPanal()
    {
        string _adr = "" ;
        switch (this.gameObject.name)
        {
            case "fangke(Clone)": { _adr = "UI/Event_FangKe"; } break;
            case "xiwang(Clone)": { _adr = "UI/Event_XiWang"; } break;
            case "yiwai(Clone)": { _adr = "UI/Event_YiWai"; } break;
            case "weiji(Clone)": { _adr = "UI/Event_WeiJi"; } break;
            case "jiaoyi(Clone)": { _adr = "UI/Event_JiaoYi"; } break;
        }
        var a=ResMgr.GetInstance().Load<GameObject>(_adr);
        a.GetComponent<EventUI>().Open(isKey);
        a.transform.parent = GameObject.Find("CharacterCanvas").transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = Vector3.one;
    }
    public void DestroyBubble()
    {
        OpenPanal();
        PoolMgr.GetInstance().PushObj(this.gameObject.name,this.gameObject);
        //这里的打开面板要延迟
    }
    /// <summary>
    /// 事件LOGO播完后，播放事件横幅，事件横幅播完后，打开面板
    /// </summary>
    public void AnimEventBanner()
    {
        //销毁事件LOGO
        Destroy(array[0]);
        array.Clear();
        //播放事件横幅(待定)
        switch (this.gameObject.name)
        {
            case "fangke(Clone)": { GameObject a = Instantiate(logo_Event[0], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            case "xiwang(Clone)": { GameObject a = Instantiate(logo_Event[1], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            case "yiwai(Clone)": { GameObject a = Instantiate(logo_Event[2], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            case "weiji(Clone)": { GameObject a = Instantiate(logo_Event[3], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            case "jiaoyi(Clone)": { GameObject a = Instantiate(logo_Event[4], Vector3.zero, Quaternion.identity); array.Add(a); } break;
        }
    }
}
