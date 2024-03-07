using UnityEngine;

public class EventLOGO : MonoBehaviour
{
    GameObject ab = null;
    [Header("事件LOGO（顺序不可更改）")] public GameObject[] logo_Event;
    public static bool logo = false;



    public void AnimDestroy()
    {
        //销毁事件LOGO+横幅
        Destroy(this.gameObject);
    }
}
