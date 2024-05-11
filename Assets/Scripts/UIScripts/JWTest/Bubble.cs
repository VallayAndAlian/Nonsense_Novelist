using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool isKey = false;
    private Animator animator;
    float destroyTime=0;
    public Vector3 pos = Vector3.one;
    [Header("�¼���ʧʱ��")] public float dTime = 4;
    public static string b_name = "";


    private EventType type;
    private bool fromBubble = false;//��һ���׶�true
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
        //�����¼�logo����
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
    /// �������ײ=>(������ʧ����������ĩβɾ�����ݣ�-logo���Ŷ���������ĩβɾ��logo+������Ŷ���������ĩβɾ�����+����壩��)
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {

            StartEventBefore(type,fromBubble,false);
            //������ʧ
            Destroy(collision.gameObject);
        }
    }



    public void StartEventBefore(EventType _type,bool _fromBubble,bool _waiBu)
    {
        if (GameMgr.instance.eventHappen) return;

        waiBu = _waiBu;

        //����������ʧ����
        animator = this.GetComponent<Animator>();
        animator.SetBool("boom", true);

        //�����¼�����
        GameMgr.instance.eventHappen = true;
        Animlogo();
     
     
       

        //Σ���¼������趨(���ݰ볡λ��)
        if (this.gameObject.name == "weiji(Clone)" && this.gameObject.transform.position.x <= -0.15f)
        {//A�������趨

        }
    }


    /// <summary>
    /// �¼�LOGO����󣬲����¼�������¼��������󣬴����
    /// </summary>
    public void DestroyBubble()
    {
        //ɾ������
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
    /// <summary>
    /// ���¼����
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
        
        if (waiBu)//�ⲿ���ã�ִ��һЩ�������
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
