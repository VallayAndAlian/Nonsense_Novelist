using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class SeeWordDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOpen = false;
    private string adr_detail= "UI/WordInformation";
    private GameObject go;
    private Vector3 detailPos = Vector3.zero;
    private Vector3 detailScale = Vector3.one;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ShootCombat"|| SceneManager.GetActiveScene().name == "NewGame4")
        {
            detailPos = Vector3.zero-new Vector3(200,0,0);
            detailScale = Vector3.one*1.1f;
        }
        if (SceneManager.GetActiveScene().name == "BookDesk3")
        {
            detailPos = Vector3.zero ;
            detailScale = Vector3.one*1.3f;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
        PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
         {
             go = obj;
       
            obj.GetComponentInChildren<WordInformation>().SetIsDetail(false);
             if (this.gameObject.GetComponent<AbstractWord0>() == null) print("this.gameObject.GetComponent<AbstractWord0>()");
             else
                obj.GetComponentInChildren<WordInformation>().ChangeInformation(this.gameObject.GetComponent<AbstractWord0>());
             obj.transform.parent = this.transform;
             obj.transform.localPosition = new Vector3(0,0,3);
             obj.transform.localScale = detailScale;
             obj.transform.GetChild(0).localPosition = detailPos;
             obj.GetComponent<Canvas>().overrideSorting = true;
         });

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;
        PoolMgr.GetInstance().PushObj(adr_detail, go);
    }
}
