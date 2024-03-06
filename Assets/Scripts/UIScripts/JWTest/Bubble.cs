using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool isKey = false;
    private Animator animator;
    float destroyTime=0;
    [Header("�¼���ʧʱ��")] public float dTime = 4;
    [Header("�¼�LOGO��˳�򲻿ɸ��ģ�")] public GameObject[] logo_Event;
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
    /// �������ײ
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            //����������ʧ����
            animator = this.GetComponent<Animator>();
            animator.SetBool("boom", true);
            //�����¼���logo����
            switch (this.gameObject.name)
            {
                case "fangke(Clone)": {GameObject a= Instantiate(logo_Event[0], Vector3.zero, Quaternion.identity); array.Add(a);  } break;
                case "xiwang(Clone)": { GameObject a = Instantiate(logo_Event[1], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "yiwai(Clone)": { GameObject a = Instantiate(logo_Event[2], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "weiji(Clone)": { GameObject a = Instantiate(logo_Event[3], Vector3.zero, Quaternion.identity); array.Add(a); } break;
                case "jiaoyi(Clone)": { GameObject a = Instantiate(logo_Event[4], Vector3.zero, Quaternion.identity); array.Add(a); } break;
            }
            //������ʧ
            Destroy(collision.gameObject);
        }
    }
    /// <summary>
    /// ���¼����
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
        //����Ĵ����Ҫ�ӳ�
    }
    /// <summary>
    /// �¼�LOGO����󣬲����¼�������¼��������󣬴����
    /// </summary>
    public void AnimEventBanner()
    {
        //�����¼�LOGO
        Destroy(array[0]);
        array.Clear();
        //�����¼����(����)
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
