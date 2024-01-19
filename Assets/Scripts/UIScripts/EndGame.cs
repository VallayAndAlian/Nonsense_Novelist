using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [Header("(手动设置)panel1")]
    public Transform panel1;//命名
    private string titleName;
    public Text textInput;

    [Header("(手动设置)panel2")]
    public Transform panel2;//查看书本
    public Text title;
    public GameObject closeBook;
    public GameObject openBook;

    [Header("(手动设置)panel3")]
    public Transform panel3;//收到信件
    public GameObject letterBig;
    private Animator letetrAnim;
    public GameObject letetrScroe;//信件得分
    public Scrollbar scroll;
    private Vector3 letterScroePos;
    private void Awake()
    {
        //初始化，打开panel1关闭其它
        ChangePanel(1);

        //赋值
        letetrAnim = letterBig.GetComponent<Animator>();
    }

    /// <summary>
    /// 换到panel几？_panel=1234
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


    #region 外部点击事件

    public void ChangeText()
    {
        titleName = textInput.text ;
    }
    public void Change1To2()
    {
        ChangePanel(2);
        P2_ClickOpenBook();
        if((titleName==null)||(titleName.Length<1)) title.text = "未命名作品";
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
    //在3的动画中调用

    #endregion
}
