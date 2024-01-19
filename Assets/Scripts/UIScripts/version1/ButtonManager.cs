using UnityEngine.UI;
using UnityEngine;

///<summary>
///ȫ�ְ�ť��������������ϣ�
///��Ҫ��������Ϸ�������һҳ��ť����
///</summary>
class ButtonManager : MonoBehaviour
{
    /// <summary>��¥��</summary>
    private GameObject libraryHLM;
    /// <summary>��¥�δʿ�����Ƿ��</summary>
    private bool isLibraryHLM = false;
    /// <summary>�鱾ѡ��</summary>
    public Button selectBook;
    /// <summary>�鿴�ʿ�</summary>
    public Button seeWorLibrary;
    /// <summary>���µ���</summary>
    public Button intoStory;
    /// <summary>��ó�ʼ����</summary>
    public Button getWord;
    /// <summary>��ȡ����ֵ</summary>
    public Button getLuckyValue;
    /// <summary>ѡ���½�</summary>
    public Button selectChapter;
    /// <summary>�ڶ���ѡ���鱾</summary>
    public Button two_selectBook;
    /// <summary>�ڶ��½�����</summary>
    public Button two_nextNext;
    public PanelManager panelManager;
    public Image guide;

    private void Start()
    {
        libraryHLM = GameObject.Find("AllWordPanelF");
    }
    /// <summary>
    /// �ʿⰴť
    /// </summary>
    public void OpenWordLibrary()
    {
        libraryHLM.transform.GetChild(0).gameObject.SetActive(true);
        isLibraryHLM = true;
    }
    /// <summary>
    /// ����ĺ�Ļ��ť
    /// </summary>
    public void BlackPanelButton()
    {
        if (isLibraryHLM)
        {
            libraryHLM.transform.GetChild(0).gameObject.SetActive(false);
            isLibraryHLM = false;
        }
    }
    /// <summary>
    /// �鱾ѡ��ť
    /// </summary>
    public void Next_Select()
    {
        selectBook.interactable = true;
        //���϶���
        RectTransform rectTransform = selectBook.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons1.Length; i++)
        {
            if (panelManager.buttons1[i].name == selectBook.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages1);
                if (panelManager.Pages1[i].gameObject.activeSelf == false)
                    panelManager.Pages1[i].gameObject.SetActive(true);
            }
            //�򿪹��µ������ʱ���򿪵ڶ������
            /*if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
            {
                panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
            }*/
        }
    }
    /// <summary>
    /// �鿴�ʿⰴť
    /// </summary>
    public void Next_SeeBook()
    {
        if (EntryDrawBox.count == 2)//ѡ��������
        {
            seeWorLibrary.interactable = true;
            //���϶���
            RectTransform rectTransform = seeWorLibrary.GetComponent<RectTransform>();
            panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

            //panel
            for (int i = 0; i < panelManager.buttons1.Length; i++)
            {
                if (panelManager.buttons1[i].name == seeWorLibrary.name)
                {
                    panelManager.CloseAllPanels(panelManager.Pages1);
                    if (panelManager.Pages1[i].gameObject.activeSelf == false)
                        panelManager.Pages1[i].gameObject.SetActive(true);
                }
                //�򿪹��µ������ʱ���򿪵ڶ������
                if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
                {
                    panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
                }
            }
        }
    }
    /// <summary>
    /// ���µ��밴ť
    /// </summary>
    public void Next_IntoStory()
    {
        intoStory.interactable = true;
        //���϶���
        RectTransform rectTransform = intoStory.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons1, panelManager.originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons1.Length; i++)
        {
            if (panelManager.buttons1[i].name == intoStory.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages1);
                if (panelManager.Pages1[i].gameObject.activeSelf == false)
                    panelManager.Pages1[i].gameObject.SetActive(true);
            }
            //�򿪹��µ������ʱ���򿪵ڶ������
            if (panelManager.buttons1[i].name == panelManager.buttons1[3].name)
            {
                panelManager.OpenPanel(panelManager.Pages2, panelManager.Pages2[0], panelManager.buttons2[0].GetComponent<RectTransform>(), panelManager.g_btnUpY);
            }
        }
    }
    
    /// <summary>
    /// ��ȡ����ֵ��ť
    /// </summary>
    public void Next_GetLuckyValue()
    {
        getLuckyValue.interactable = true;
        //���϶���
        RectTransform rectTransform = getLuckyValue.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == getLuckyValue.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }            
        }
    }
    /// <summary>
    /// ѡ���½ڰ�ť
    /// </summary>
    public void Next_SelectChapter()
    {
        selectChapter.interactable = true;
        //���϶���
        RectTransform rectTransform = selectChapter.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == selectChapter.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }
            
        }
    }
    /// <summary>
    /// ��ó�ʼ������ť
    /// </summary>
    public void Next_GetInitialWord()
    {
        getWord.interactable = true;
        //���϶���
        RectTransform rectTransform = getWord.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == getWord.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }
        }
    }

    public void DestroyGuide()
    {
        Destroy(guide);
    }
    /// <summary>
    /// ѡ���鱾��ť
    /// </summary>
    public void Next_SelectBook()
    {
        two_selectBook.interactable = true;
        //���϶���
        RectTransform rectTransform = two_selectBook.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == two_selectBook.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }

        }
    }
    /// <summary>
    /// ��������ť
    /// </summary>
    public void Next_NextChapter()
    {
        two_nextNext.interactable = true;
        //���϶���
        RectTransform rectTransform = two_nextNext.GetComponent<RectTransform>();
        panelManager.AllButtonsDown(panelManager.buttons2, panelManager.g_originalY);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, panelManager.g_btnUpY, rectTransform.localPosition.z);

        //panel
        for (int i = 0; i < panelManager.buttons2.Length; i++)
        {
            if (panelManager.buttons2[i].name == two_nextNext.name)
            {
                panelManager.CloseAllPanels(panelManager.Pages2);
                if (panelManager.Pages2[i].gameObject.activeSelf == false)
                    panelManager.Pages2[i].gameObject.SetActive(true);
            }

        }
    }
}
