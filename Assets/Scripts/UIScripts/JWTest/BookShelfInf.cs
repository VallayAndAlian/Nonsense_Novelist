using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using static UnityEngine.EventSystems.EventTrigger;

public class BookShelfInf : MonoBehaviour
{
    private string characterDetailPrefab = "UI/CharacterDetail";
    public GameObject wordDetailPrefab;
    private bool checkBook = false;
    public static bool isfirst = true;
    private string spriteAdr = "WordImage/Character/";
    private AbstractWord0 nowWord;
    public GameObject detailInfoPrefab;
    public Transform detailParent;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BookShelf")
        {
            checkBook = true;
            //titleBg = title.transform.parent.GetComponent<Image>();
        }
        else
        {
            checkBook= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseRight();
    }
    /// <summary>
    /// ���
    /// </summary>
    private void MouseRight()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return;
        //��ɫ���
        //sr.color = colorOnMouseOver;

        //�������Ҽ�
        if (isfirst&&checkBook&&Input.GetMouseButtonDown(1))
        {
            isfirst = false;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "charaCard")
                { 
                    CharacterDetail bb= hit.collider.gameObject.GetComponent<CharacterDetail>();
                    var a = ResMgr.GetInstance().Load<GameObject>(characterDetailPrefab);
                    a.transform.parent = GameObject.Find("Canvas").transform;
                    a.transform.localPosition = Vector3.zero;
                    a.transform.localScale = Vector3.one;
                    //��ȡ�����ɫ�Ľű���Ϣ
                    //a.GetComponent<CharacterDetail>().Open(this.GetComponent<AbstractCharacter>());
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + bb.nameText.text);
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
                    a.GetComponent<CharacterDetail>().sprite.sprite = _s2;

                    a.GetComponent<CharacterDetail>().nameText.text = bb.nameText.text;
                    a.GetComponent<CharacterDetail>().roleName.text = bb.roleName.text;
                    a.GetComponent<CharacterDetail>().roleInfo.text = bb.roleInfo.text;

                    a.GetComponent<CharacterDetail>().atk.text = bb.atk.text;
                    a.GetComponent<CharacterDetail>().def.text = bb.def.text;
                    a.GetComponent<CharacterDetail>().psy.text = bb.psy.text;
                    a.GetComponent<CharacterDetail>().san.text = bb.san.text;
                }
                else if (hit.collider.gameObject.tag == "wordInf")
                {
                    var a = Instantiate(wordDetailPrefab);
                    var b = Instantiate(hit.collider.gameObject);
                    a.transform.parent = GameObject.Find("Canvas").transform;
                    a.transform.localPosition = Vector3.zero;
                    a.transform.localScale = Vector3.one; 
                    
                    b.transform.parent = a.transform;
                    b.transform.localPosition = Vector3.zero;
                    b.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
                    //
                    nowWord = hit.collider.gameObject.GetComponent<AbstractWord0>();
                    ChangeDetailInfo(a);
                }
            }
                    
        }
    }
    string[] info = new string[2];

    void ChangeDetailInfo(GameObject tt)
    {
        //�Ӵ�����ȡ����lable������
        var _s = nowWord.DetailLable();
        if (_s == null) return;
        for (int i = 0; i < _s.Length; i++)
        {
            //����ȡ�������� ��ȡ��̬��Ա������ֵ
            System.Type wordType = System.Type.GetType(_s[i]);
            if (wordType != null)
            {
                if (wordType.GetField("s_wordName") == null) print("��" + wordType.ToString() + "��û�ж��徲̬��Աs_wordName/s_description");

                info[0] = (string)wordType.GetField("s_wordName").GetValue(null);
                info[1] = (string)wordType.GetField("s_description").GetValue(null);
            }
            else
            {
                print(nowWord.name + "��" + _s + "���ͻ�ȡʧ��");
                info[0] = null; info[1] = null;
            }

            if ((info[0] != null) && (info[1] != null))
            {
                if (i == 0)
                {
                    PoolMgr.GetInstance().GetObj(detailInfoPrefab, (obj) =>
                    {
                        //��������λ�������û�ã�ֱ���޸���ƷDetailWordInfo��λ��
                        obj.transform.parent = tt.transform;
                        obj.transform.localPosition = new Vector3(360, 30, 0);
                        obj.transform.localScale = Vector3.one;
                        obj.GetComponent<DetailWordInfo>().ChangeInformation(info[0], info[1]);
                    });
                }
                else if (i == 1)
                {
                    PoolMgr.GetInstance().GetObj(detailInfoPrefab, (obj) =>
                    {
                        //��������λ�������û�ã�ֱ���޸���ƷDetailWordInfo��λ��
                        obj.transform.parent = tt.transform;
                        obj.transform.localPosition = new Vector3(360, -100, 0);
                        obj.transform.localScale = Vector3.one;
                        obj.GetComponent<DetailWordInfo>().ChangeInformation(info[0], info[1]);
                    });
                }
            }
            
            else
            {
                print(_s + "���ֶλ�ȡʧ��");
            }
        }
        if (detailParent != null)
        {
            if (detailParent.GetComponent<RectTransform>() != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(detailParent.GetComponent<RectTransform>());
        }
    }
    }

