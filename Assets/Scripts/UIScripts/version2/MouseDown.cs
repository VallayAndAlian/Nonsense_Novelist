using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///������main camera�ϣ���������� ��ʾ��Ϣ
///</summary>
class MouseDown : MonoBehaviour
{
    /// <summary>�������Ԥ����</summary>
    public GameObject characterDetailPrefab;
    /// <summary>UIcanvas</summary>
    public Canvas otherCanvas;
    /// <summary>Ԥ����Ŀ�¡��</summary>
    private static GameObject a;
    /// <summary>Ԥ�����е�����</summary>
    private Text[] texts=new Text[2];
    /// <summary>�����ɫ</summary>
    [HideInInspector]
    public AbstractCharacter abschara;
    /// <summary>�����ɫ���</summary>
    [HideInInspector]
    public AbstractRole absRole;
    /// <summary>�������ݴ�</summary>
    [HideInInspector]
    public AbstractAdjectives absAdj;
    /// <summary>���󶯴�</summary>
    [HideInInspector]
    public AbstractVerbs absVerbs;
    /// <summary>�����Ը�</summary>
    [HideInInspector]
    public AbstractTrait absTrait;
    /// <summary>�жϵ�ǰ�Ƿ��н�ɫ��Ϣչʾ���</summary>
    public bool isShow = false;
    /// <summary>��Ч</summary>
    public AudioSource audioSource;
    /// <summary>������</summary>
    public GameObject shoot;
    
    private void Update()
    {
        if (CharacterManager.instance.pause) return;
        if (isShow) return;
        MouseDownView();
    }  
    /// <summary>
    /// ȫ���������
    /// </summary>
    private void MouseDownView()
    {
        
        if (Input.GetMouseButtonDown(1))//�����Ҽ�
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            
            if(hit.collider!=null&&hit.collider.gameObject.tag=="character"&& !isShow)
            {
                //���ŵ����Ч
                //audioSource.Play();

                //���ɽ�ɫ��Ϣ���
                a = Instantiate(characterDetailPrefab, otherCanvas.transform);

                isShow = true;
                
                //��ȡ�����ɫ�Ľű���Ϣ
                abschara = hit.collider.GetComponent<AbstractCharacter>();

                a.GetComponentInChildren<CharacterDetail_t>().SetCharacter(hit.collider.gameObject);

                //if (hit.collider.GetComponent<AbstractAdjectives>())
                //{
                //    absAdj = hit.collider.GetComponent<AbstractAdjectives>();
                //}
                //else if (hit.collider.GetComponent<AbstractVerbs>())
                //{
                //    absVerbs = hit.collider.GetComponent<AbstractVerbs>();
                //}
                
            }
            else if (hit.collider != null && hit.collider.gameObject.tag == "Slider" && hit.collider.gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name != "�Ŷ�")//���������λ
            {
                //����
                Shoot.sunWordCount--;
                GameObject tt = hit.collider.gameObject;
                GameObject jj = GameObject.Find("combatCanvas");
                if (tt.name == "Slider0")
                {
                    //�Ƴ������е�
                    GameMgr.instance.wordGoingUseList.RemoveAt(0);
                    //���ֲ��滻0��2��ͼƬ������ֱ���滻�����ܿ��Ų�λ-1
                    Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                    tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite;
                    jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite = ss;
                }
                else if(tt.name == "Slider1")
                {
                    GameMgr.instance.wordGoingUseList.RemoveAt(1);
                    //���ֲ��滻1��2��ͼƬ
                    Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                    tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite;
                    jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite = ss;

                }
                else if (tt.name == "Slider2")
                {
                    GameMgr.instance.wordGoingUseList.RemoveAt(2);
                    //���ֲ��滻2��2��ͼƬ
                    Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                    tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite;
                    jj.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Image>().sprite = ss;

                }

            }
        }
    }

    public void CloseDetail()
    {
        isShow = false;
    }    
}
