using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �¼�������
/// </summary>
[System.Serializable]
public enum EventType
{
    XiWang = 0,
    FangKe = 1,
    YiWai = 2,
    WeiJi = 3,
    JiaoYi = 4,
    ChangJing=5
}


public class EventUI : MonoBehaviour
{
    [Header("������һ�������UI")]
    public EventType type;
    bool isKey=false;

    //ȫ������
    public test1ExcelData data;

    //����Ҫ����
    List<test1ExcelItem> needAllDate_nKey = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_nKey = new List<test1ExcelItem>();
    //��Ҫ����
    List<test1ExcelItem> needAllDate_Key = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_Key = new List<test1ExcelItem>();
    //������ʹ�õ�����
    List<test1ExcelItem> tempAllDate = new List<test1ExcelItem>();
    List<test1ExcelItem> tempNowDate = new List<test1ExcelItem>();

    private void Start()
    {
      
    }

   


    #region ����data 
    
    
    void DataInit()
    {
        tempAllDate.Clear();
        tempNowDate.Clear();
        if (isKey)
        {
            tempAllDate = needAllDate_Key;
            tempNowDate = needNowDate_Key;
        }
        else
        {
            tempAllDate = needAllDate_nKey;
            tempNowDate = needNowDate_nKey;
        }
    }


    void DealWithData(EventType _type)
    {
        needAllDate_nKey.Clear();
        needAllDate_Key.Clear();
        needNowDate_nKey.Clear();
        needNowDate_Key.Clear();

        foreach (var _t in data.items)
        {
            if (_t.type == _type)
            {
                if (!_t.isKey)
                {
                    if (!needAllDate_nKey.Contains(_t))
                        needAllDate_nKey.Add(_t);
                }
                else
                {
                    if (!needAllDate_Key.Contains(_t))
                        needAllDate_Key.Add(_t);
                }
            }
        }

        needNowDate_nKey.AddRange(needAllDate_nKey);
        needNowDate_Key.AddRange(needAllDate_Key);
    }



    void RefreshNowList()
    {
        if (needNowDate_nKey.Count <= 0)
        {
            needNowDate_nKey.AddRange(needAllDate_nKey);
        }
        if (needNowDate_Key.Count <= 0)
        {
            needNowDate_Key.AddRange(needAllDate_Key);
        }
    }


    #endregion


    #region �������͵��¼�

    #region ����
    /// <summary>
    /// ���������ʱ�ĳ�ʼˢ��
    /// </summary>
    public void OpenInit_YiWai()
    {
        DataInit();

         Transform cardParent=this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();
      
       

        for (int i = 0; i < 6; i += 2)
        {

            int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
            int loopCount = 0;
            while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
            {//�����������������û����,������һ��
             
                _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
                if (loopCount > 50)
                {
                    print("��ѭ��");
                    return;
                }
            }


            //�л���������
            text[i].text= tempNowDate[_r].name;
            text[i+1].text = tempNowDate[_r].textEvent;

            //ˢ�������¼��б�
            tempNowDate.Remove(tempNowDate[_r]);
            RefreshNowList();
        }

    }

    #endregion


    #region �ÿ�
    public void OpenInit_FangKe()
    {
        DataInit();
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//�����������������û����,������һ��

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("��ѭ��");
                return;
            }
        }

        //
        Image sprite = this.transform.Find("Sprite").GetComponent<Image>();
        Image bubble= this.transform.Find("bubble").GetComponent<Image>();
        TextMeshProUGUI words = bubble.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();


        info.text = tempNowDate[_r].name;
        words.text = tempNowDate[_r].textEvent;
        RefreshNowList();
    }

    void Close_FangKe()
    {
        //GameMgr.instance.BookCanvasClickYes();
        Destroy(this.gameObject);
    }
    #endregion


    #region ϣ��
    public void OpenInit_XiWang()
    {
        DataInit();
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//�����������������û����,������һ��

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("��ѭ��");
                return;
            }
        }
   
        TextMeshProUGUI info = this.transform.Find("info").GetComponentInChildren<TextMeshProUGUI>();
        info.text = tempNowDate[_r].name + "\n\n" + tempNowDate[_r].textEvent;


        RefreshNowList();

    }


    public void Close_XiWang()
    {
        
    }
    #endregion


    #region ����
    GameObject cardPanal;
    private AbstractWord0 JY_chooseWord=null;
    Transform choose=null;
    Vector3 chooseScale = new Vector3(0.341f,0.32f,0.341f);
    public void OpenInit_JiaoYi()
    {
        JY_chooseWord = null;
        choose = this.transform.Find("choose");
        cardPanal = this.transform.Find("cardRes").gameObject;

        cardPanal.SetActive(false);
        cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();

        string adr_detail = "UI/WordInformation";

        //��ȡ��ӵ�е����У�δ��õĿ���������ͬ
        TextMeshProUGUI titleText= this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        Transform CardGroup = this.transform.Find("CardGroup");


        //��ѡ�������Ϊ������
        choose.transform.localScale = Vector3.zero;


        //  ѡ��
        BookNameEnum[] _rBook= new BookNameEnum[3];
        Type[] _rWord = new Type[3];
        int x = 0;
        bool _b = false;
        int bookCount = GameMgr.instance.GetBookList().Count;
        for (int i = 0; i < 3;i++)
        {
            if(_b) _rBook[i] = BookNameEnum.BookNull;
            while (GameMgr.instance.IsBookWordAllGet(GameMgr.instance.GetBookList()[x])&&(!_b))
            {
                x++;
                if (x > bookCount)
                {//���е��鶼�Ѿ���ȡ������Ϊnull
                    _rBook[i] = BookNameEnum.BookNull;
                    _b = true;
                } 
            }

            _rBook[i] = GameMgr.instance.GetBookList()[x];
            x++;
            if (x >= bookCount) x = 0;

            
        }

        //���ɿ���
        for (int i = 0; i < 3; i++)
        {
            //ѡ����
            if (_rBook[i] == BookNameEnum.BookNull)
            {
                _rWord[i] = null;
            }
            else
            {
                _rWord[i]=GameMgr.instance.GetBookListNeedWordOne(_rBook[i]) ;
                if (i == 0) { }
                else if (i == 1) {
                    while (_rWord[i] == _rWord[0]) { _rWord[i] = GameMgr.instance.GetBookListNeedWordOne(_rBook[i]); }
                        }
                else if (i == 2) { 
                    while (_rWord[i] == _rWord[0]|| _rWord[i] == _rWord[1]) { _rWord[i] = GameMgr.instance.GetBookListNeedWordOne(_rBook[i]); } }
                
            

                //���ɿ���
                PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
                {
                    var _word=obj.AddComponent(_rWord[i])as AbstractWord0;
                    obj.GetComponentInChildren<WordInformation>().SetIsDetail(false);

                    if (_word == null) print("this.gameObject.GetComponent<AbstractWord0>()");
                    else
                        obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);


                    obj.transform.parent = CardGroup;
                    obj.transform.localPosition = new Vector3(0, 0, 3);
                    obj.transform.localScale = Vector3.one*1.2f;

                    //���Ӱ�ť���
                    var _botton=obj.AddComponent<Button>();
                    _botton.transition = Selectable.Transition.None;
                    _botton.onClick .AddListener(()=>ClickWord(_botton));
                  // obj.GetComponent<Canvas>().overrideSorting = true;
                });
            }
        }

        //��ȡ�¼���Ϣ
        tempAllDate.Clear();
        tempNowDate.Clear();
        if (isKey)
        {
            tempAllDate = needAllDate_Key;
            tempNowDate = needNowDate_Key;
        }
        else
        {
            tempAllDate = needAllDate_nKey;
            tempNowDate = needNowDate_nKey;
        }
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//�����������������û����,������һ��

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("��ѭ��");
                return;
            }
        }

        //�л���������
        titleText.text = tempNowDate[_r].name+"\n"+ tempNowDate[_r].textEvent;


        //ˢ�������¼��б�
        tempNowDate.Remove(tempNowDate[_r]);
        RefreshNowList();
    }


    void ClickWord(Button which)
    {
        if(choose==null) choose = this.transform.Find("choose");
  
        if (JY_chooseWord == which.gameObject.GetComponent<AbstractWord0>())
        {
            choose.SetParent(which.gameObject.transform.parent.parent);
            choose.localScale = Vector3.zero;
            JY_chooseWord = null;
        }
        else
        {
            JY_chooseWord = which.gameObject.GetComponent<AbstractWord0>();
            choose.SetParent(which.gameObject.transform);
            choose.localScale = chooseScale;
            choose.localPosition = Vector3.zero;

            cardPanal.SetActive(true);
        }
  
        
        
    }


    void Close_JiaoYi()
    { 
        Transform CardGroup = this.transform.Find("CardGroup");
        for (int i = 0; i < CardGroup.childCount; i++)
        {
            Button _button=null;
            if (CardGroup.GetChild(i).TryGetComponent<Button>(out _button))
            {
                _button.onClick.RemoveAllListeners();
                Destroy(_button);
            }
            PoolMgr.GetInstance().PushObj(CardGroup.GetChild(i).gameObject.name, CardGroup.GetChild(i).gameObject);
        } 
    }


    public void JY_ClickExchange()
    {
        if(cardPanal==null) cardPanal = this.transform.Find("cardRes").gameObject;

        GameMgr.instance.AddCardList(JY_chooseWord);
        GameMgr.instance.DeleteCardList(cardPanal.GetComponent<ShooterWordCheck>().chooseWord);
    }
    public void JY_ClickCancel()
    {
        cardPanal.SetActive(false);
    }
    #endregion


    #region Σ��
    public void OpenInit_WeiJi()
    {
        DataInit();
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//�����������������û����,������һ��

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("��ѭ��");
                return;
            }
        }

        //
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();
        info.text = tempNowDate[_r].name + "\n\n" + tempNowDate[_r].textEvent;

        RefreshNowList();

    }
    #endregion


    #region ����
    public void OpenInit_ChangJing()
    {
        
    }
    #endregion

    #endregion



    #region �ⲿ���/�����¼�


    public void Open(bool _isKey)
    {
        GameMgr.instance.AddBookList(BookNameEnum.HongLouMeng);
        GameMgr.instance.AddBookList(BookNameEnum.ElectronicGoal);
        //GameMgr.instance.AddBookList(BookNameEnum.EgyptMyth);
       CharacterManager.instance.pause = true;
        isKey = _isKey;
        
        DealWithData(type);
        switch (type)
        {
            case EventType.XiWang:
                {
                    OpenInit_XiWang();
                }
                break;
            case EventType.FangKe:
                {
                    OpenInit_FangKe();
                }
                break;
            case EventType.YiWai:
                {
                    OpenInit_YiWai();
                }
                break;
            case EventType.JiaoYi:
                {
                    OpenInit_JiaoYi();
                }
                break;
            case EventType.WeiJi:
                {
                    OpenInit_WeiJi();
                }
                break;
            case EventType.ChangJing:
                {
                    OpenInit_ChangJing();
                }
                break;
        }
    }

    

    public void ClickNext()
    {
        switch (type)
        {
            case EventType.XiWang: 
                {
                    Close_XiWang();
                }break;
            case EventType.FangKe:
                {
                    Close_FangKe();
                }
                break;
            case EventType.YiWai:
                {

                }
                break;
            case EventType.JiaoYi:
                {

                }
                break;
            case EventType.WeiJi:
                {

                }
                break;
        }
    }

    #endregion
}
