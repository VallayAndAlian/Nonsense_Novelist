using UnityEngine;

public class EventLOGO : MonoBehaviour
{
    GameObject ab = null;
    [Header("�¼�LOGO��˳�򲻿ɸ��ģ�")] public GameObject[] logo_Event;
    public static bool logo = false;



    public void AnimDestroy()
    {
        //�����¼�LOGO+���
        Destroy(this.gameObject);
    }
}
