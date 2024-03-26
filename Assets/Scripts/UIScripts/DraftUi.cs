using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 挂在草稿本母节点上。
/// </summary>
public class DraftUi : MonoBehaviour
{
    //文本内容
    List<string> content=new List<string>();


 
    
    // 不换行的的空格符
    public static readonly string NO_BREAKING_SPACE = "\u00A0";//"\u3000";

    private string sentenseAdr= "UI/draftSentence";
    private GameObject sentenseObj;
    private Transform parent;
    bool select = false;

    //墨水
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
    //文本修改
    int oriTextLen;
    bool textHasChange;

    //转行计算
    float sizeWidth;
    float sizeFont;



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
        //获取墨水本身
        inkRedObj = this.transform.Find("red").GetComponent<Image>();
        inkBlackObj = this.transform.Find("black").GetComponent<Image>();
        inkBlueObj = this.transform.Find("blue").GetComponent<Image>();
        penObj = this.transform.Find("pen").GetComponent<Image>();

        //初始化设定
        ChangeInkNum();
        ClickInk(-1);


        //预制体的大小和字号大小，计算转行用
        sizeWidth = (sentenseObj.transform.Find("showText").GetComponent<RectTransform>().rect.width);
        sizeFont = (sentenseObj.transform.Find("showText").GetComponent<TextMeshProUGUI>().fontSize);
    }

    #region 开启草稿本的外部界面
    public void openDraft()
    {
        print(content.Count);
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
    public void closeDraft()
    {
        CharacterManager.instance.pause = false;
        for (int i = parent.childCount-1; i >=0; i--)
        {
            PoolMgr.GetInstance().PushObj(parent.GetChild(i).name, parent.GetChild(i).gameObject);
        } 
        this.gameObject.SetActive(false);
        //InitDraft();
    }

 
    void InitDraft()
    {
        ChangeInkNum();
        ClickInk(-1);


        //生成句子,绑定组件
        for (int i = 0; i < content.Count; i++)
        {
            PoolMgr.GetInstance().GetObj(sentenseObj, (obj) =>
             {
                 
                 obj.transform.parent = parent;
                 obj.transform.localScale = Vector3.one;
                 var _showText = obj.transform.Find("showText").GetComponent<TextMeshProUGUI>();
                 var _inputField = obj.GetComponent<TMP_InputField>();

                 _showText.text = content[i];
                 _inputField.onValueChanged.AddListener((obj) => { CheckContent(_inputField); });
                 _inputField.onSelect.AddListener((obj)=> { OpenEditText(_inputField); }); 
                 _inputField.onDeselect.AddListener((obj) => { CloseEditText(_inputField); });

                 //转行            
                 var count = Mathf.Floor((sizeFont * (content[i].Length))/ (sizeWidth - _showText.margin.x));
                 for (int x = 0; x < count - 1; x++)
                 {
                     _inputField.text += "\n";
                 }
                 _inputField.text += "\n";
                 
             });
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
    }
    #endregion


    #region 墨水事件


    /// <summary>
    /// 外部点击墨水事件调用。0黑1红2蓝-1谁也没选
    /// </summary>
    /// <param name="_ink"></param>
    public void ClickInk(int _ink)
    {
        switch (_ink)
        {
            case 0://black
                {//只是【进入状态】，是否做修改还不一定。因此此处不操作数字。
                    if (inkBlackOn)
                    { 
                        DefualtState(); 
                    }
                    else if (IsInkEnough(_ink))
                    {
                        inkRedOn = false;
                        inkBlackOn = true;
                        inkBlueOn = false;
                        inkBlackObj.color = Color.black;
                        inkBlueObj.color = Color.blue;
                        inkRedObj.color = Color.red;
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
                        inkBlueObj.color = Color.blue;
                        inkRedObj.color = Color.black;
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
                        inkBlueObj.color = Color.black;
                        inkRedObj.color = Color.red;
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

    void DefualtState()
    {
        inkRedOn = false;
        inkBlackOn = false;
        inkBlueOn = false;
        inkBlackObj.color = Color.white;
        inkBlueObj.color = Color.blue;
        inkRedObj.color = Color.red;
        penObj.gameObject.transform.localScale = Vector3.zero;

        //
        CloseAllDrag();
        CloseAllDelete();
    }

    /// <summary>
    /// 外部表现变化
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
    /// 打开所有的拖拽事件
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
    /// 关闭所有的拖拽事件
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
    /// 判断墨水数量是否足够.0黑1红2蓝
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
    /// 0黑1红2蓝
    /// </summary>
    /// <param name="_ink"></param>
    public void UseInkOnce(int _ink)
    {
        switch (_ink)
        {
            case 0:
                { inkBlackCount -= 1; }
                break;
            case 1:
                { inkRedCount -= 1; }
                break;
            case 2:
                { inkBlueCount -= 1; }
                break;
        }
        ChangeInkNum();
    }


    /// <summary>
    /// 墨水不足时的外部提示.。0黑1红2蓝-1谁也没选
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


    #region 文本编辑蓝墨水
    /// <summary>
    /// 实现文本编辑框
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

        oriTextLen = _text.text.Length;

        if(!_inputField.GetComponent<DragDraftText>().hasChange)
            textHasChange = false;

        _text.gameObject.SetActive(false);
        select = true;
    }

    /// <summary>
    /// 关掉文本编辑框
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


        //转行
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
    /// 文本转换，处理unity的中英字符转行识别问题
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
    /// 外部：inputfield的on value change 中调用。
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

    #region 文本生成

    public void InitContent()
    {
        content.Clear(); 
    }
    public void AddContent(string _new)
    {
        print(_new);
        content.Add(_new);
    }
    #endregion
}



