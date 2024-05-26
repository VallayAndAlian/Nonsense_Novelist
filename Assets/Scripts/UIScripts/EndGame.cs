using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public Text contentL;
    public Text contentR;
    public int indexPage=0;

    private string[] content;
    private string content_string;
    public int lineWords = 18;
    public int lineCount = 13;
    private Dictionary<int, int> pageContent = new Dictionary<int, int>();

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


        //
        this.GetComponent<Canvas>().worldCamera = Camera.main;
      
        content = GameMgr.instance.draftUi.MergeContent_B();
        content_string = GameMgr.instance.draftUi.MergeContent_A();
       // CalculateAllPageIndex();
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
                    //��ǰҳ��Ϊ���ҳ
                    InitContent();
                    //RefreshPanal2();
                }
                break;
            case 3:
                {
                    panel3.gameObject.SetActive(true);
                }
                break;
        
        }
    }


    #region draftUI��ֲ
    public GameObject sentenseObj;
    public Transform parentL;
    public Transform parentR;
    List<string> _content = new List<string>();
    int maxPage = 0;//���ҳ����ʵ�ʣ�
     int nowPage = 0;//����ҳ����0��ʼ��
     List<int> pageCountL = new List<int>();//���ʼ����0
     List<int> pageCountR = new List<int>();
    List<Transform> pageChildL = new List<Transform>();
    List<Transform> pageChildR= new List<Transform>();

    public TextMeshProUGUI textPagel;

    public TextMeshProUGUI textPager;
    //ת�м���
    float sizeWidth;
    float sizeFont;
    bool isleftNOW = true;
    private void InitContent()
    {
        sizeWidth = (sentenseObj.transform.Find("showText").GetComponent<RectTransform>().rect.width);
        sizeFont = (sentenseObj.transform.Find("showText").GetComponent<TextMeshProUGUI>().fontSize);
        maxPage = 0;
        _content = GameMgr.instance.draftUi.content;

        //�������еľ��ӣ��ҵ���Ӧ������
        pageCountL.Clear(); pageCountR.Clear();
        pageChildL.Clear(); pageChildR.Clear();

        int _l = 0;
        int _r = 0;
        //���ɾ���,�����
        for (int i = 0; i < _content.Count; i++)
        {
            if (isleftNOW)//����ߵĽ������
            {
              
                PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
                {
                    obj.transform.parent = parentL;
                    obj.transform.localScale = Vector3.one;

                    var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                    var _inputField = obj.GetComponent<TMP_InputField>();
                    _showText.text = _content[i];

                    //ת��            
                    var count = Mathf.Floor((sizeFont * (content[i].Length)) / (sizeWidth - _showText.margin.x));
                    for (int x = 0; x < count - 1; x++)
                    {
                        _inputField.text += "\n";
                    }
                    _inputField.text += "\n";

                    //ˢ��ҳ�沢�Ҽ���ҳ��
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
                    pageChildL.Add(obj.transform);
                    if ((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y <= 350f)
                    {
                        maxPage += 1;
                        pageCountL.Add(_l);
                        isleftNOW = false;
                    
                    }
                    _l++;
                });
                nowPage = maxPage - (maxPage % 2);
                ShowPageSentences(nowPage);
            }
            else//�ұ�
            {
                PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
                {
                    obj.transform.parent = parentR;
                    obj.transform.localScale = Vector3.one;

                    var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                    var _inputField = obj.GetComponent<TMP_InputField>();
                    _showText.text = _content[i];

                    //ת��            
                    var count = Mathf.Floor((sizeFont * (content[i].Length)) / (sizeWidth - _showText.margin.x));
                    for (int x = 0; x < count - 1; x++)
                    {
                        _inputField.text += "\n";
                    }
                    _inputField.text += "\n";

                    //ˢ��ҳ�沢�Ҽ���ҳ��
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
                    pageChildR.Add(obj.transform);
                    if ((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y <= 250f)
                    {
                        maxPage += 1;
                        pageCountR.Add(_r);
                        isleftNOW = true;
                    }
                    _r++;
                });
                nowPage = maxPage - (maxPage % 2);
                ShowPageSentences(nowPage);
            }
        }
            //�����ڵ�ҳ����Ϊ���ҳ������������ҳ���ľ��ӡ�
        //nowPage = maxPage-(maxPage%2);
        //ShowPageSentences(nowPage);
      
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentL);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentR);
    }

    private void ShowPageSentences(int _page)
    {
        //textPagel.text = (nowPage+1).ToString() + " / " + (maxPage).ToString();
        textPagel.text = (nowPage + 1).ToString();
        textPager.text = (nowPage + 2).ToString();
        if ((_page < 0) || (_page >= maxPage))//nowpage��0��ʼ��������ָ��;maxPage��Count����1��ʼ
        {
            print("����ҳ�淭ҳ����"+_page);
            return;
        }

        int _maxLpage= maxPage - (maxPage % 2); //���ҳ�������ҳ���ʵ�ʶ�Ӧnowpage
     
        if ((nowPage != 0) && (nowPage < _maxLpage))//�м�ҳ��
        {
      
            for (int x = 0; x < pageChildL.Count; x++)//l
            {
                if ((x >= pageCountL[nowPage/2 - 1])&& (x <pageCountL[nowPage  / 2]))
                {
                    pageChildL[x].gameObject.SetActive(true);
                }
                else
                { pageChildL[x].gameObject.SetActive(false); }
            }
            //r
            for (int x = 0; x < pageChildR.Count; x++)
            {
                if ((x >= pageCountR[nowPage  / 2 - 1]) && (x < pageCountR[nowPage  / 2]))
                {
                    pageChildR[x].gameObject.SetActive(true);
                }
                else
                { pageChildR[x].gameObject.SetActive(false); }
            }
        }
        else//��һҳ�������һҳ
        {
            if (nowPage == 0)//��һҳ
            {
                if (maxPage == 0)
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)
                    {
                        pageChildR[x].gameObject.SetActive(false);
                    }
                }
                else if (maxPage == 1)
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)//R
                    {
                        pageChildR[x].gameObject.SetActive(true);
                    }
                }
                else
                {

                    for (int x = 0; x < pageChildL.Count; x++)//L
                    {
                        if (x >= pageCountL[0])//pageCountL[0]���������һҳ
                        {
                            pageChildL[x].gameObject.SetActive(false);
                        }
                        else
                            pageChildL[x].gameObject.SetActive(true);
                    }
                    for (int x = 0; x < pageChildR.Count; x++)//R
                    {
                        if (x >= pageCountR[0])//pageCountL[0]���������һҳ
                        {
                            pageChildR[x].gameObject.SetActive(false);
                        }
                        else
                            pageChildR[x].gameObject.SetActive(true);
                    }
                }

            }
          
            else if ((nowPage == _maxLpage) && (maxPage == _maxLpage))//���һҳ���
            {

                for (int x = 0; x < pageChildL.Count; x++)
                {
                    if ((x < pageCountL[(nowPage/ 2 )- 1]))//pageCountL[nowPage / 2]���������һҳ
                    {
                        pageChildL[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildL[x].gameObject.SetActive(true); }
                }
                for (int x = 0; x < pageChildR.Count; x++)
                {
                    pageChildR[x].gameObject.SetActive(false);
                }
            }
            else if ((nowPage == _maxLpage) && (maxPage != _maxLpage))//���һҳ�ұ�
            {
   
                for (int x = 0; x < pageChildL.Count; x++)//L
                {
                    if ((x < pageCountL[(nowPage / 2)-1]))//pageCountL[nowPage / 2]���������һҳ
                    {
                        pageChildL[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildL[x].gameObject.SetActive(true); }
                }
                for (int x = 0; x < pageChildR.Count; x++)//R
                {
                    if ((x < pageCountR[(nowPage/ 2)-1 ]))//pageCountL[nowPage / 2]���������һҳ
                    {
                        pageChildR[x].gameObject.SetActive(false);
                    }
                    else
                    { pageChildR[x].gameObject.SetActive(true); }
                }
            }
            else
            {
                print("δ֪���");
            }

        }

    }


    public void ClickNextPage()
    {
        if (nowPage == (maxPage - (maxPage % 2))) return;
        nowPage+=2;
        ShowPageSentences(nowPage);
    }
    public void ClickLastPage()
    {
        if (nowPage == 0) return;
        nowPage -= 2;
        ShowPageSentences(nowPage);
    }



    #endregion


    #region ������ʾ����
    //private void RefreshPanal2()
    //{
    //    //ҳ��=0ʱ����1��2.���ֵ����Ϊ0Ҳ����Ϊ1
    //    if (indexPage + 1 <= pageContent.Count)   //��-��(1)
    //    {
    //        if (!pageContent.ContainsKey(indexPage + 1))
    //        {
    //            contentL.text = content_string.Substring(pageContent[indexPage]);
    //        }
    //        else
    //        {
    //            contentL.text = content_string.Substring(pageContent[indexPage],
    //                pageContent[indexPage + 1] - pageContent[indexPage]);
    //        }
    //    }
    //    else
    //    {
    //        contentL.text = "";
    //    }

    //    if (indexPage +2 <= pageContent.Count) //��-˫(2)
    //    {
    //        if (!pageContent.ContainsKey(indexPage + 2))
    //        {
    //            contentR.text = content_string.Substring(pageContent[indexPage + 1]);
    //        }
    //        else
    //        {
    //            contentR.text = content_string.Substring(pageContent[indexPage + 1],
    //                pageContent[indexPage + 2] - pageContent[indexPage + 1]);
    //        }
    //    }
    //    else
    //    {
    //        contentR.text = "";
    //    }
    //}
    //private void CalculateAllPageIndex()
    //{
    //    print(content.Length+ "Length");
    //    int _INDEX = 0;
    //    int _wordIndex = 0;
    //    int o = 0;
    //    pageContent.Add(0, 0); _INDEX++;
    //    foreach (var _c in content)
    //    {
    //        o += Mathf.CeilToInt(_c.Length / (lineWords))+1;
    //        print("o" + o);
    //        if (o >= (lineCount))
    //        {
    //            int i = o - lineCount;//����������
    //            int j = ((_c.Length % (lineWords)) == 0 ? lineWords : (_c.Length % (lineWords))) + Mathf.Clamp((i - 1), 0, lineCount + 1) * lineWords;
    //            int x = _c.Length - j;
    //            _wordIndex += x;

    //            pageContent.Add(_INDEX, _wordIndex);

    //            _INDEX++;
    //            o = 0;
    //            o +=i;
    //            _wordIndex += j; _wordIndex++;
    //        }
    //        else
    //        {
    //            _wordIndex += _c.Length;
    //            _wordIndex++;
    //        }


    //    }

    //    return;
    //}
    //public void LastPage()
    //{
    //    if (indexPage <= 0) return;
    //    indexPage -= 2;
    //    RefreshPanal2();
    //}

    //public void NextPage()
    //{
    //    if (indexPage + 2 >= pageContent.Count) return;
    //    indexPage += 2;
    //    RefreshPanal2();
    //}
    #endregion

    #region �ⲿ����¼�



    public void BackToStudyScene()
    {
        RecordMgr.instance.AddRecord(titleName, GameMgr.instance.draftUi.content, 2);
        gameObject.GetComponent<LoadingScene>().EnterNextScene();
    }
    public void ChangeText()
    {
        titleName = textInput.text ;
        
    }
    public void Change1To2()
    { 
        ChangeText();
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
