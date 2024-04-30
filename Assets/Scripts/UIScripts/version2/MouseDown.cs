using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    bool[] downs = new bool[5];
    Type[] res = new Type[5];
    Dictionary<int,Type> word_=new Dictionary<int,Type>();
    
    /// <summary>
    /// ȫ���������
    /// </summary>
    private void MouseDownView()
    {
        
        if (Input.GetMouseButtonDown(1))//�����Ҽ�
        {
            //
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero, Mathf.Infinity, (1 << 10) | (1 << 3));
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "character" && !isShow)
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
                else if (hit.collider.gameObject.tag == "Slider" && hit.collider.gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name != "�Ŷ�")//���������λ
                {
                    print("sh"+Shoot.sumWordCount);
                    //�����������Ͻ���ͼƬ���߼����Ƴ���ǰ����ֱ������ʱadd��
                    GameObject tt = hit.collider.gameObject;
                    GameObject jj = GameObject.Find("combatCanvas");
                    if (tt.name == "Slider0")
                    {
                        if (downs[0] == false)
                        {
                            downs[0] = true;
                            print("SD0");
                            print(Shoot.sumWordCount - 1);
                            //�ݴ�+�Ƴ������е�
                            word_.Add(0, GameMgr.instance.wordGoingUseList[0]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(0);
                            //���ֲ��滻0��Shoot.sumWordCount-1��ͼƬ���ܿ��Ų�λ-1
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;

                            Shoot.sumWordCount--;

                        }
                        else
                        {//����
                            downs[0] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[0]);
                            word_.Remove(0);
                            print("JS");
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider1")
                    {
                        if (downs[1] == false)
                        {
                            downs[1] = true;
                            word_.Add(1, GameMgr.instance.wordGoingUseList[1]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(1);
                            //���ֲ��滻1��Shoot.sumWordCount-1��ͼƬ
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetComponent<Image>().sprite = ss;
                            print("SD1");
                            Shoot.sumWordCount--;

                        }
                        else
                        {//true,����
                            downs[1] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[1]);
                            word_.Remove(1);
                            print("JW1");
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider2")
                    {
                        if (downs[2] == false)
                        {
                            downs[2] = true;
                            word_.Add(2, GameMgr.instance.wordGoingUseList[2]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(2);
                            //���ֲ��滻2��Shoot.sumWordCount-1��ͼƬ
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            print("SD2");
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[2] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[2]);
                            word_.Remove(2);
                            print("JS2");
                            Shoot.sumWordCount++;

                        }

                    }
                    //����������λ������
                    else if (tt.name == "Slider3" && Shoot.sumWordCount >= 4)
                    {
                        if (downs[3] == false)
                        {
                            downs[3] = true;
                            word_.Add(3, GameMgr.instance.wordGoingUseList[3]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(3);
                            //���ֲ��滻2��Shoot.sumWordCount-1��ͼƬ
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[3] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[3]);
                            word_.Remove(3);
                            Shoot.sumWordCount++;

                        }
                    }
                    else if (tt.name == "Slider4" && Shoot.sumWordCount == 5)
                    {
                        if (downs[4] == false)
                        {
                            downs[4] = true;
                            word_.Add(4, GameMgr.instance.wordGoingUseList[4]);
                            GameMgr.instance.wordGoingUseList.RemoveAt(4);
                            //���ֲ��滻2��Shoot.sumWordCount-1��ͼƬ
                            Sprite ss = tt.transform.GetChild(1).GetComponent<Image>().sprite;
                            tt.transform.GetChild(1).GetComponent<Image>().sprite = jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite;
                            jj.transform.Find("ShootTime").transform.GetChild(Shoot.sumWordCount - 1).GetChild(1).GetComponent<Image>().sprite = ss;
                            Shoot.sumWordCount--;

                        }
                        else
                        {
                            downs[4] = false;
                            GameMgr.instance.wordGoingUseList.Add(word_[4]);
                            word_.Remove(4);
                            Shoot.sumWordCount++;

                        }
                    }
                }
            
            }
        }
    }

    public void CloseDetail()
    {
        isShow = false;
    }    
}
