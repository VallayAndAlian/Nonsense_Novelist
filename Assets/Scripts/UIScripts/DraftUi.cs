using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���ڲݸ屾ĸ�ڵ��ϡ�
/// </summary>
public class DraftUi : MonoBehaviour
{
    //�ı�����
    public List<string> content=new List<string>();

    // �����еĵĿո��
    public static readonly string NO_BREAKING_SPACE = "\u00A0";//"\u3000";

    private string sentenseAdr= "UI/draftSentence";
    private GameObject sentenseObj;
    private Transform parent;
    bool select = false;

    //īˮ
    bool inkRedOn = false;
    int inkRedCount = 5;
    Image inkRedObj;
    bool inkBlackOn = false;
    int inkBlackCount = 5;
    Image inkBlackObj;
    bool inkBlueOn = false;
    int inkBlueCount = 5;
    Image inkBlueObj;
    Image penObj;

    //�ı��޸�
    int oriTextLen;
    bool textHasChange;

    //ת�м���
    float sizeWidth;
    float sizeFont;

    //ҳ�����
    TextMeshProUGUI textPage;
    public int maxPage=1;
    public int nowPage = 1;
    List<int> pageCount = new List<int>();
    List<Transform> pageChild = new List<Transform>();
    bool readOnly;
    private void Awake()
    {
        sentenseObj = ResMgr.GetInstance().Load<GameObject>(sentenseAdr);
        parent = this.transform.Find("Panel");

        foreach (var _a in GetComponentsInChildren<TMP_InputField>())
        {
            _a.text = "";
            _a.transform.Find("showText").gameObject.SetActive(true);
            _a.transform.Find("editText").gameObject.SetActive(false);
            _a.caretColor -= new Color(0, 0, 0, 1);
        }

        select = false;

        //��ȡīˮ����
        inkRedObj = this.transform.Find("red").GetComponent<Image>();
        inkBlackObj = this.transform.Find("black").GetComponent<Image>();
        inkBlueObj = this.transform.Find("blue").GetComponent<Image>();
        penObj = this.transform.Find("pen").GetComponent<Image>();

        textPage = this.transform.Find("page").GetComponent<TextMeshProUGUI>();

        //��ʼ���趨
        ChangeInkNum();
        ClickInk(-1);


        //Ԥ����Ĵ�С���ֺŴ�С������ת����
        sizeWidth = (sentenseObj.transform.Find("showText").GetComponent<RectTransform>().rect.width);
        sizeFont = (sentenseObj.transform.Find("showText").GetComponent<TextMeshProUGUI>().fontSize);
    }

    #region button��
    /// <summary>�����ݸ屾���� </summary>
    public void openDraft()
    {
       // print(content.Count);
        CharacterManager.instance.pause = true;
        this.gameObject.SetActive(true);
        //this.transform.Find("Panal").
        var _all = this.GetComponentsInChildren<DragDraftText>();
        if (_all != null)
        {
            // ExecuteEvents.Execute(_all[0].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            //_all[0].OnBeginDrag(new PointerEventData(EventSystem.current));
        }
 
        InitDraft();
    }
    /// <summary>�رղݸ屾���� </summary>
    public void closeDraft()
    {
        CharacterManager.instance.pause = false;
      
        for (int i = parent.childCount-1; i >=0; i--)
        {
            if(parent.GetChild(i).GetComponent<DragDraftText>().hasDelete)
            {
                content.RemoveAt(parent.GetChild(i).GetComponent<DragDraftText>().index);
                
            }
            PoolMgr.GetInstance().PushObj(parent.GetChild(i).name, parent.GetChild(i).gameObject);
        } 
        this.gameObject.SetActive(false);
        //InitDraft();
    }
    /// <summary>�ݸ屾�����ʼ�� </summary>
    void InitDraft()
    {
        ChangeInkNum();
        ClickInk(-1);

        //�������еľ��ӣ��ҵ���Ӧ������
        maxPage = 1;
        pageCount.Clear();
        pageCount.Add(0);
        pageChild.Clear();

        //���ɾ���,�����
        for (int i = 0; i < content.Count; i++)
        {
            PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
            {
                obj.transform.parent = parent;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<DragDraftText>().index = i;
         
                var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                var _inputField = obj.GetComponent<TMP_InputField>();

                _showText.text = content[i];
                _inputField.onValueChanged.AddListener((obj) => { CheckContent(_inputField); });
                _inputField.onSelect.AddListener((obj)=> { OpenEditText(_inputField); }); 
                _inputField.onDeselect.AddListener((obj) => { CloseEditText(_inputField); });

                //ת��            
                var count = Mathf.Floor((sizeFont * (content[i].Length))/ (sizeWidth - _showText.margin.x));
                for (int x = 0; x < count - 1; x++)
                {
                    _inputField.text += "\n";
                }
                _inputField.text += "\n";

                //ˢ��ҳ�沢�Ҽ���ҳ��
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
                //print((RectTransformUtility.WorldToScreenPoint(Camera.main, obj.GetComponent<RectTransform>().position)).y);
                if ((RectTransformUtility.WorldToScreenPoint(Camera.main,obj.GetComponent<RectTransform>().position)).y <= 50f)
                {
                    maxPage += 1;
                    pageCount.Add(i);
                }
                pageChild.Add(obj.transform);
            });

            //�����ڵ�ҳ����Ϊ���ҳ������������ҳ���ľ��ӡ�
            nowPage = maxPage;

            ShowPageSentences(nowPage);

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
    }
    /// <summary>չʾ_pageҳ�ľ��� </summary>
    public void ShowPageSentences(int _page)
    {
        textPage.text =nowPage+ " / " +maxPage;
        print("�����ǵ�" + nowPage + "ҳ");
        //nowpage\_page\maxpage���Ǵ�1��ʼ�ģ�
        if (_page < 0 || _page > maxPage)
        {
            print("����ҳ�����");
            return;
        }

        if (_page == maxPage)
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if (x < pageCount[nowPage - 1])
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
        else if (_page == 1)
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if (x >= pageCount[nowPage])
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
        else
        {
            for (int x = 0; x < pageChild.Count; x++)
            {
                if ((x <= pageCount[nowPage-1])||(x >= pageCount[nowPage]))
                {
                    pageChild[x].gameObject.SetActive(false);
                }
                else
                { pageChild[x].gameObject.SetActive(true); }
            }
        }
    }
    public void NextPage()
    {
        if (nowPage == maxPage) return;
        nowPage++;
        ShowPageSentences(nowPage);
    }
    public void LastPage()
    {
        if (nowPage == 1) return;
        nowPage--;
        ShowPageSentences(nowPage);
    }
    public void RefreshIndex()
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            parent.GetChild(i).GetComponent<DragDraftText>().RefreshIndex();
        }
    }
    #endregion

    #region īˮ�¼�
    /// <summary>
    /// �ⲿ���īˮ�¼����á�0��1��2��-1˭Ҳûѡ
    /// </summary>
    /// <param name="_ink"></param>
    public void ClickInk(int _ink)
    {
        switch (_ink)
        {
            case 0://black
                {//ֻ�ǡ�����״̬�����Ƿ����޸Ļ���һ������˴˴����������֡�
                    if (inkBlackOn)
                    { 
                        DefualtState(); 
                    }
                    else if (IsInkEnough(_ink))
                    {
                        inkRedOn = false;
                        inkBlackOn = true;
                        inkBlueOn = false;
                        inkBlackObj.color = Color.white;
                        inkBlueObj.color = Color.white;
                        inkRedObj.color = Color.white;
                        penObj.transform.localScale = Vector3.one * 0.065f;
                        penObj.GetComponent<RectTransform>().anchoredPosition = blackpen;
                        //���ò㼶
                        penObj.transform.SetSiblingIndex(penObj.transform.parent.childCount - 1);
                        int _index = inkBlackObj.transform.parent.Find(inkBlackObj.gameObject.name).GetSiblingIndex();
                        penObj.transform.SetSiblingIndex(_index);
                        //
                        OpenAllDrag();
                        CloseAllDelete();
                    }
                }
                break;
            case 1://red
                {
                    if (inkRedOn)
                    {
                        DefualtState();
                    }
                    else if (IsInkEnough(_ink))
                    {
                        inkRedOn = true;
                        inkBlackOn = false;
                        inkBlueOn = false;
                        inkBlackObj.color = Color.white;
                        inkBlueObj.color = Color.white;
                        inkRedObj.color = Color.white;
                        penObj.transform.localScale = Vector3.one * 0.065f;
                        penObj.GetComponent<RectTransform>().anchoredPosition = redpen;
                        //���ò㼶
                        penObj.transform.SetSiblingIndex(penObj.transform.parent.childCount-1);
                        int _index = inkRedObj.transform.parent.Find(inkRedObj.gameObject.name).GetSiblingIndex();
                        penObj.transform.SetSiblingIndex(_index);
                        //
                        CloseAllDrag();
                        OpenAllDelete();
                    }
                }
                break;
            case 2://blue
                {
                    if (inkBlueOn)
                    {
                        DefualtState();
                    }
                    else if (IsInkEnough(_ink))
                    {
                        inkRedOn = false;
                        inkBlackOn = false;
                        inkBlueOn = true;
                        inkBlackObj.color = Color.white;
                        inkBlueObj.color = Color.white;
                        inkRedObj.color = Color.white;
                        penObj.transform.localScale = Vector3.one * 0.065f;
                        penObj.GetComponent<RectTransform>().anchoredPosition = bluepen;
                        //���ò㼶
                        penObj.transform.SetSiblingIndex(penObj.transform.parent.childCount - 1);
                        int _index = inkBlueObj.transform.parent.Find(inkBlueObj.gameObject.name).GetSiblingIndex();
                        penObj.transform.SetSiblingIndex(_index);
                        //
                        CloseAllDelete();
                        CloseAllDrag();
                    }
                }
                break;
            case -1:
                {
                    DefualtState();
                }
                break;
        }
        ChangeInkNum();
    }
    Vector3 blackpen = new Vector3(839, 234,0);
    Vector3 redpen = new Vector3(839, 34,0);
    Vector3 bluepen = new Vector3(839, -166,0);
    void DefualtState()
    {
        inkRedOn = false;
        inkBlackOn = false;
        inkBlueOn = false;
        inkBlackObj.color = Color.white;
        inkBlueObj.color = Color.white;
        inkRedObj.color = Color.white;
        penObj.gameObject.transform.localScale = Vector3.zero;

        //
        CloseAllDrag();
        CloseAllDelete();
    }

    /// <summary>
    /// �ⲿ���ֱ仯
    /// </summary>
    public void ChangeInkNum()
    {
        inkRedObj.gameObject.GetComponentInChildren<Text>().color = Color.white;
        inkBlackObj.gameObject.GetComponentInChildren<Text>().color = Color.white;
        inkBlueObj.gameObject.GetComponentInChildren<Text>().color = Color.white;
        inkRedObj.gameObject.GetComponentInChildren<Text>().text = inkRedCount.ToString();
        inkBlackObj.gameObject.GetComponentInChildren<Text>().text = inkBlackCount.ToString();
        inkBlueObj.gameObject.GetComponentInChildren<Text>().text = inkBlueCount.ToString();
    }

    /// <summary>
    /// �����е���ק�¼�
    /// </summary>
    void OpenAllDrag()
    {
        var _all = this.GetComponentsInChildren<DragDraftText>();
        foreach (var _a in _all)
        {
            _a.canDrag = true;
        }
    }
    /// <summary>
    /// �ر����е���ק�¼�
    /// </summary>
    void CloseAllDrag()
    {
        var _all = this.GetComponentsInChildren<DragDraftText>();
        foreach (var _a in _all)
        {
            _a.canDrag = false;
        }
    }
    void OpenAllDelete()
    {
        var _all = this.GetComponentsInChildren<DragDraftText>();
        foreach (var _a in _all)
        {
            _a.canDelete = true;
        }
    }
    void CloseAllDelete()
    {
        var _all = this.GetComponentsInChildren<DragDraftText>();
        foreach (var _a in _all)
        {
            _a.canDelete = false;
        }
    }
    /// <summary>
    /// �ж�īˮ�����Ƿ��㹻.0��1��2��
    /// </summary>
    /// <param name="_ink"></param>
    public bool IsInkEnough(int _ink)
    {
        switch (_ink)
        {
            case 0:
                if (inkBlackCount > 0) return true;
                else { InkNotEnough(0);return false; }
                
            case 1:
                if (inkRedCount > 0) return true;
                else { InkNotEnough(1); return false; }
            case 2:
                if (inkBlueCount > 0) return true;
                else { InkNotEnough(2); return false; }
        }
        return false;
    }
    /// <summary>
    /// 0��1��2��
    /// </summary>
    /// <param name="_ink"></param>
    public void UseInkOnce(int _ink)
    {
        switch (_ink)
        {
            case 0:
                { /*inkBlackCount -= 1;*/ }
                break;
            case 1:
                { /*inkRedCount -= 1;*/ }
                break;
            case 2:
                {/* inkBlueCount -= 1;*/ }
                break;
        }
        ChangeInkNum();
    }


    /// <summary>
    /// īˮ����ʱ���ⲿ��ʾ.��0��1��2��-1˭Ҳûѡ
    /// </summary>
    /// <param name="_ink"></param>
    public void InkNotEnough(int _ink)
    {
        switch (_ink)
        {
            case 0:
                {
                    inkBlackObj.gameObject.GetComponentInChildren<Text>().color = Color.red;                
                }
                break;
            case 1:
                {
                    inkRedObj.gameObject.GetComponentInChildren<Text>().color = Color.red;
                }
                break;
            case 2:
                {
                    inkBlueObj.gameObject.GetComponentInChildren<Text>().color = Color.red;
                }
                break;
        }

    }




    #endregion

    #region �ı��༭����īˮ��
    /// <summary>
    /// ʵ���ı��༭��
    /// </summary>
    public void OpenEditText(TMP_InputField _inputField)
    {


       
        if (select) return;
        if (!inkBlueOn) return; 
        if (!IsInkEnough(2))  return;
        if (_inputField.GetComponent<DragDraftText>().hasDelete) return;


     


        _inputField.transform.Find("editText").gameObject.SetActive(true);
        _inputField.caretColor+=new Color(0,0,0,1);
        TextMeshProUGUI _text = _inputField.transform.Find("showText").GetComponent<TextMeshProUGUI>();
        _inputField.text = _text.text;

        //�ҵ��������ڴ��������һ��
        //nowEditIndex = content.FindIndex(item => item.Contains(_text.text));
      


        oriTextLen = _text.text.Length;

        if(!_inputField.GetComponent<DragDraftText>().hasChange)
            textHasChange = false;

        _text.gameObject.SetActive(false);
        select = true;
    }

    /// <summary>
    /// �ص��ı��༭��
    /// </summary>
    public void CloseEditText(TMP_InputField _inputField)
    {
        if (!select) return;
        if (!inkBlueOn) return;
        if (!IsInkEnough(2)) return;
        if (_inputField.GetComponent<DragDraftText>().hasDelete) return;


        TextMeshProUGUI _text = _inputField.transform.Find("showText").GetComponent<TextMeshProUGUI>();
        _text.text = HandleTextContentFormat(_inputField.text);
      
        _text.gameObject.SetActive(true);
        select = false;

        //�޸Ŀ�����ı�
        content[_inputField.GetComponent<DragDraftText>().index] = _inputField.text;
        

        //ת��
        _inputField.text = "";
        var count =Mathf.Floor( (sizeFont * (_text.text.Length)) / sizeWidth);
        var countN = _text.text.Split("\n").Length;
        for (int x = 0; x < count+ countN-1; x++)
        {
            _inputField.text += "\n";
        }


        if (textHasChange && (!_inputField.GetComponent<DragDraftText>().hasChange))
        {
            UseInkOnce(2);
            _inputField.GetComponent<DragDraftText>().hasChange = true;
        }

     
        

        _inputField.transform.Find("editText").gameObject.SetActive(false);
        _inputField.caretColor -= new Color(0, 0, 0, 1);
    }



    /// <summary>
    /// �ı�ת��������unity����Ӣ�ַ�ת��ʶ������
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string HandleTextContentFormat(string content)
    {
        if (content.Contains(" "))
        {
            content = content.Replace(" ", NO_BREAKING_SPACE);
        }

        return content;
    }


    /// <summary>
    /// �ⲿ��inputfield��on value change �е��á�
    /// </summary>
    public void CheckContent(TMP_InputField _inputField)
    {
        if (!IsInkEnough(2)) return;
        if (!select) return;
        if (!inkBlueOn) return;
        if (_inputField.GetComponent<DragDraftText>().hasDelete) return;

        _inputField.text = HandleTextContentFormat(_inputField.text);
        if (!textHasChange)
        {
            if (_inputField.text.Length != oriTextLen)
                textHasChange = true;
        }
            
          
    }

    #endregion

    #region �ı�����

    public void InitContent()
    {
        content.Clear(); 
    }

    public void AddContent(string _new)
    {
      
        content.Add(_new);
    }

    public string MergeContent_A()
    {
        
        string Merge = "";
        foreach (var _content in content)
        {
            Merge+=_content;
            Merge += "\n";
        }

        return Merge;
    }
    public string[] MergeContent_B()
    {
        return content.ToArray();
    }
    public void ChangeIndexContent(int oldIndex,int newIndex)
    {
        if (oldIndex == newIndex) return;

        string _new= content[oldIndex];
        List<string> newList = new List<string>();

        if (oldIndex < newIndex)
        {
            for (int x = oldIndex+1; x < content.Count; x++)
            {
               
                newList.Add(content[x]);
                if(x== newIndex)
                    newList.Add(_new);
            }
            content.RemoveRange(oldIndex, content.Count - oldIndex);
            content.AddRange(newList);
        }
        else
        {
            for (int x = newIndex ; x < content.Count; x++)
            {
                if (x != oldIndex)
                    newList.Add(content[x]);
            }
            content.RemoveRange(newIndex, content.Count - newIndex);
            content.Add(_new);
            content.AddRange(newList);
        }
       
     

    }
    #endregion
}



