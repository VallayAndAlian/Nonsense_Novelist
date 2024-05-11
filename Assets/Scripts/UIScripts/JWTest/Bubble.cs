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


    private EventType type;
    private bool fromBubble = false;//第一个阶段true
    private bool waiBu;
    private void Awake()
    {
        type = EventType.WeiJi;
        switch (this.gameObject.name)
        {

            case "fangke(Clone)":
                { fromBubble = true; type = EventType.FangKe; }
                break;
            case "FANGKE(Clone)":
                { fromBubble = false; type = EventType.FangKe; }
                break;
            case "xiwang(Clone)":
                { fromBubble = true; type = EventType.XiWang; }
                break;
            case "XIWANG(Clone)":
                { fromBubble = false; type = EventType.XiWang; }
                break;
            case "yiwai(Clone)":
                { fromBubble = true; type = EventType.YiWai; }
                break;
            case "YIWAI(Clone)":
                { fromBubble = false; type = EventType.YiWai; }
                break;
            case "weiji(Clone)":
                { fromBubble = true; type = EventType.WeiJi; }
                break;
            case "WEIJI(Clone)":
                { fromBubble = false; type = EventType.WeiJi; }
                break;
            case "jiaoyi(Clone)":
                { fromBubble = true; type = EventType.JiaoYi; }
                break;
            case "JIAOYI(Clone)":
                { fromBubble = false; type = EventType.JiaoYi; }
                break;
            case "changjing(Clone)":
                { fromBubble = true; type = EventType.ChangJing; }
                break;
            case "CHANGJING(Clone)":
                { fromBubble = false; type = EventType.ChangJing; }
                break;

        }
    }


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
        switch (type)
        {
            case EventType.FangKe:
                {
                    _adr = "UI/LOGO/FANGKE"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().waiBu = waiBu;
                }
                break;
            case EventType.XiWang:
                {
                    _adr = "UI/LOGO/XIWANG"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().waiBu = waiBu;
                }
                break;
            case EventType.YiWai:
                {
                    _adr = "UI/LOGO/YIWAI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().waiBu = waiBu; 
                }
                break;
            case EventType.WeiJi:
                {
                    _adr = "UI/LOGO/WEIJI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().waiBu = waiBu;
                }
                break;
            case EventType.JiaoYi:
                {
                    _adr = "UI/LOGO/JIAOYI"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().waiBu = waiBu;
                }
                break;
            case EventType.ChangJing:
                {
                    _adr = "UI/LOGO/CHANGJING"; obj = ResMgr.GetInstance().Load<GameObject>(_adr); obj.transform.localPosition = new Vector3(0, -1.5f, 0);
                    obj.GetComponent<Bubble>().isKey = isKey; obj.GetComponent<Bubble>().pos = this.transform.position; obj.GetComponent<Bubble>().waiBu = waiBu;
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

            StartEventBefore(type,fromBubble,false);
            //词条消失
            Destroy(collision.gameObject);
        }
    }



    public void StartEventBefore(EventType _type,bool _fromBubble,bool _waiBu)
    {
        if (GameMgr.instance.eventHappen) return;

        waiBu = _waiBu;

        //播放气泡消失动画
        animator = this.GetComponent<Animator>();
        animator.SetBool("boom", true);

        //开启事件开关
        GameMgr.instance.eventHappen = true;
        Animlogo();
     
     
       

        //危机事件触发设定(根据半场位置)
        if (this.gameObject.name == "weiji(Clone)" && this.gameObject.transform.position.x <= -0.15f)
        {//A队增加设定

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
        switch (type)
        {
            case EventType.FangKe: { _adr = "UI/Event_FangKe"; } break;
            case EventType.XiWang: { _adr = "UI/Event_XiWang"; } break;
            case EventType.YiWai: { _adr = "UI/Event_YiWai"; } break;
            case EventType.WeiJi: { _adr = "UI/Event_WeiJi"; } break;
            case EventType.JiaoYi: { _adr = "UI/Event_JiaoYi"; } break;
            case EventType.ChangJing: { _adr = "UI/Event_ChangJing"; } break;
        }
       
        var a = ResMgr.GetInstance().Load<GameObject>(_adr);
        if (a == null) print("1null");
        if(a.GetComponent<EventUI>()==null) print("null");
        
        if (waiBu)//外部调用，执行一些特殊操作
        {
            a.GetComponent<EventUI>().WJ_static = true;
        }

        a.GetComponent<EventUI>().Open(isKey);
        a.GetComponent<EventUI>().eventWorldPos = pos;
  

        a.transform.parent = GameObject.Find("CharacterCanvas").transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = Vector3.one;

    }
}
