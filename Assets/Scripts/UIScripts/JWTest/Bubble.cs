using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool isKey = false;
    private Animator animator;
    float destroyTime=0;
    public Vector3 pos = Vector3.one;
    [Header("事件消失时间")] public float dTime = 4;
    public static string b_name = "";

    private void Update()
    {

        if (CharacterManager.instance.pause) return;

        destroyTime += Time.deltaTime;
        if (destroyTime > dTime)
        {
            GetComponent<Animator>().SetBool("boom", true);
        }  
    }


    public void Animlogo()
    {
        string _adr = "";
        GameObject obj;
        //播放事件logo动画
        switch (this.gameObject.name)
        {
            case "fangke(Clone)":
                {
                    _adr = "UI/LOGO/FANGKE"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position;
                }
                break;
            case "xiwang(Clone)":
                {
                    _adr = "UI/LOGO/XIWANG"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position;
                }
                break;
            case "yiwai(Clone)":
                {
                    _adr = "UI/LOGO/YIWAI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position;
                }
                break;
            case "weiji(Clone)":
                {
                    _adr = "UI/LOGO/WEIJI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().isKey = isKey;
                }
                break;
            case "jiaoyi(Clone)":
                {
                    _adr = "UI/LOGO/JIAOYI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position;
                }
                break;
            case "changjing(Clone)":
                {
                    _adr = "UI/LOGO/CHANGJING"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position;
                }
                break;
        }

    }



    /// <summary>
    /// 与词球碰撞=>(气泡消失动画（动画末尾删除气泡）-logo播放动画（动画末尾删除logo+横幅播放动画（动画末尾删除横幅+打开面板））)
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            
            if (GameMgr.instance.eventHappen) return;

            //播放气泡消失动画
            animator = this.GetComponent<Animator>();
            animator.SetBool("boom", true);


            //开启事件开关
            GameMgr.instance.eventHappen = true;


            Animlogo();
            //词条消失
            Destroy(collision.gameObject);

            //危机事件触发设定(根据半场位置)
            if(this.gameObject.name== "weiji(Clone)"&&this.gameObject.transform.position.x<=-0.15f)
            {//A队增加设定

            }

        }
    }
    /// <summary>
    /// 事件LOGO播完后，播放事件横幅，事件横幅播完后，打开面板
    /// </summary>
    public void DestroyBubble()
    {
        //删除气泡
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
    /// <summary>
    /// 打开事件面板
    /// </summary>
    public void OpenPanal()
    {


        string _adr = "";
        switch (this.gameObject.name)
        {
            case "FANGKE(Clone)": { _adr = "UI/Event_FangKe"; } break;
            case "XIWANG(Clone)": { _adr = "UI/Event_XiWang"; } break;
            case "YIWAI(Clone)": { _adr = "UI/Event_YiWai"; } break;
            case "WEIJI(Clone)": { _adr = "UI/Event_WeiJi"; } break;
            case "JIAOYI(Clone)": { _adr = "UI/Event_JiaoYi"; } break;
            case "CHANGJING(Clone)": { _adr = "UI/Event_ChangJing"; } break;
        }
       
        var a = ResMgr.GetInstance().Load<GameObject>(_adr);
        if (a == null) print("1null");
        if(a.GetComponent<EventUI>()==null) print("null");
        a.GetComponent<EventUI>().Open(isKey);
        a.GetComponent<EventUI>().eventWorldPos = pos;
        a.transform.parent = GameObject.Find("CharacterCanvas").transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = Vector3.one;

    }
}
