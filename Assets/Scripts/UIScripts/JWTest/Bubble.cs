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
    /// �������ײ=>(������ʧ����������ĩβɾ�����ݣ�-logo���Ŷ���������ĩβɾ��logo+������Ŷ���������ĩβɾ�����+����壩��)
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            
            if (GameMgr.instance.eventHappen) return;

            //����������ʧ����
            animator = this.GetComponent<Animator>();
            animator.SetBool("boom", true);


            //�����¼�����
            GameMgr.instance.eventHappen = true;


            Animlogo();
            //������ʧ
            Destroy(collision.gameObject);

            //Σ���¼������趨(���ݰ볡λ��)
            if(this.gameObject.name== "weiji(Clone)"&&this.gameObject.transform.position.x<=-0.15f)
            {//A�������趨

            }

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
