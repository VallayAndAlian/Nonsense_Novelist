using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [Header("(�ֶ�����)panel1")]
    public Transform panel1;//����
    private string titleName;
    public Text textInput;

    [Header("(�ֶ�����)panel2")]
    public Transform panel2;//�鿴�鱾
    public Text title;
    public GameObject closeBook;
    public GameObject openBook;

    [Header("(�ֶ�����)panel3")]
    public Transform panel3;//�յ��ż�
    public GameObject letterBig;
    private Animator letetrAnim;
    public GameObject letetrScroe;//�ż��÷�
    public Scrollbar scroll;
    private Vector3 letterScroePos;
    private void Awake()
    {
        //��ʼ������panel1�ر�����
        ChangePanel(1);

        //��ֵ
        letetrAnim = letterBig.GetComponent<Animator>();
    }

    /// <summary>
    /// ����panel����_panel=1234
    /// </summary>
    /// <param name="_panel"></param>
    private void ChangePanel(int _panel)
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
      
        switch (_panel)
        {
            case 1:
                {
                    panel1.gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    panel2.gameObject.SetActive(true);
                }
                break;
            case 3:
                {
                    panel3.gameObject.SetActive(true);
                }
                break;
        
        }
    }


    #region �ⲿ����¼�

    public void ChangeText()
    {
        titleName = textInput.text ;
    }
    public void Change1To2()
    {
        ChangePanel(2);
        P2_ClickOpenBook();
        if((titleName==null)||(titleName.Length<1)) title.text = "δ������Ʒ";
        else title.text = titleName;
    }
    public void P2_ClickCloseBook()
    {
        closeBook.SetActive(false);
        openBook.SetActive(true);
    }
    public void P2_ClickOpenBook()
    {
        closeBook.SetActive(true);
        openBook.SetActive(false);
    }
    public void Change2To3()
    {
        ChangePanel(3);

        letterBig.SetActive(false);
        letetrAnim.enabled = true;
        letetrAnim.SetBool("open", false);
        letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
    }
    public void P3_ClickLetter()
    {
        letterBig.SetActive(true);
        letetrAnim.SetBool("open", true);
    }

    Vector3 vector010 = new Vector3(0, 1, 0);
    public void P3_LetterScroll()
    {
        if (letetrAnim != null && (letetrAnim.enabled))
        {
            letetrAnim.enabled=false; letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
        }
       
        print(" scroll.value" + scroll.value);

        letetrScroe.GetComponent<RectTransform>().localPosition = letterScroePos + vector010 * 1200 * scroll.value;
    }
    //��3�Ķ����е���

    #endregion
}
