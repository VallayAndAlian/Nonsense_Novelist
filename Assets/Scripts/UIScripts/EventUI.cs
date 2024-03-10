using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 事件的类型
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
    [Header("这是哪一个界面的UI")]
    public EventType type;
    bool isKey=false;



    //不重要数据
    List<test1ExcelItem> needAllDate_nKey = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_nKey = new List<test1ExcelItem>();
    //重要数据
    List<test1ExcelItem> needAllDate_Key = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_Key = new List<test1ExcelItem>();
    //函数中使用的数据
    List<test1ExcelItem> tempAllDate = new List<test1ExcelItem>();
    List<test1ExcelItem> tempNowDate = new List<test1ExcelItem>();


    [Header("牌库预制物(手动)")]
    public GameObject word_adj;
    public GameObject word_verb;
    public GameObject word_item;

    private void Start()
    {
      
    }

   


    #region 处理data 
    
    /// <summary>
    /// 在初始化后,处理isKEY和数据
    /// </summary>
    /// <param name="_isKey"></param>
    void DataInit(bool _isKey)
    {
        tempAllDate=null;
        tempNowDate=null;
        
        if (_isKey && (needAllDate_Key.Count != 0))
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

    /// <summary>
    /// 在面板最初打开的时候初始化
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData(EventType _type)
    {
        //清空所有
        needAllDate_nKey.Clear();
        needAllDate_Key.Clear();
        needNowDate_nKey.Clear();
        needNowDate_Key.Clear();

        //获取该类型的当前所有可以发生的data
        foreach (var _t in GameMgr.instance.canHappenData_Key)
        {
            if (_t.type == _type)
            {
                if(!needAllDate_Key.Contains(_t))
                    needAllDate_Key.Add(_t);
                
                
            }
        }
        foreach (var _t in GameMgr.instance.canHappenData_nKey)
        {
            if (_t.type == _type)
            {
                if (!needAllDate_nKey.Contains(_t))
                    needAllDate_nKey.Add(_t);

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

    private string nowEvent;

    #region 五种类型的事件

    #region 意外
    /// <summary>
    /// 打开意外面板时的初始刷新
    /// </summary>
    public void OpenInit_YiWai()
    {
        DataInit(false);

         Transform cardParent=this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();
      
       

        for (int i = 0; i < 6; i += 2)
        {
            //if ((i == 1)&&isKey&& (needNowDate_Key.Count>0)&&(GameMgr.instance.happenEvent.Contains(needNowDate_Key())))
            //{
            //   // needNowDate_Key.
            //}
            int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
            int loopCount = 0;
            while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
            {//如果有条件并且条件没满足,就重找一个
             
                _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
                if (loopCount > 50)
                {
                    print("死循环");
                    return;
                }
            }


            //切换文字内容
            text[i].text= tempNowDate[_r].name;
            text[i+1].text = tempNowDate[_r].textEvent;

            //刷新已用事件列表
            nowEvent = tempNowDate[_r].name;
            tempNowDate.Remove(tempNowDate[_r]);
            RefreshNowList();
        }

    }

    public void Close_YiWai()
    {
        GameMgr.instance.happenEvent.Add(nowEvent);
        Destroy(this.gameObject);
    }

    #endregion


    #region 访客

    private int KeyCharacter = -1;
    public void OpenInit_FangKe()
    {
        DataInit(isKey);
        KeyCharacter = -1;
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//如果有条件并且条件没满足,就重找一个

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("死循环");
                return;
            }
        }

        //
        Image sprite = this.transform.Find("Sprite").GetComponent<Image>();
        Image bubble= this.transform.Find("bubble").GetComponent<Image>();
        TextMeshProUGUI words = bubble.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();

        if ((tempNowDate[_r].happen != null) && (tempNowDate[_r].happen != ""))
            KeyCharacter = int.Parse(tempNowDate[_r].happen);
        nowEvent = tempNowDate[_r].name;
        print(tempNowDate[_r].happen);
        info.text = tempNowDate[_r].name;
        words.text = tempNowDate[_r].textEvent;
        RefreshNowList();
    }

    void Close_FangKe()
    {
        if (isKey&& KeyCharacter!=-1)
            GameMgr.instance.CreateTheCharacterPut(KeyCharacter);
        else
            GameMgr.instance.CreateCharacterPut(1);
        GameMgr.instance.happenEvent.Add(nowEvent);
        Destroy(this.gameObject);
    }
    #endregion


    #region 希望
    Button checkBotton;
    private List<int> showBook=new List<int>();
    private float rate_1 = 20;
    private float rate_2 = 30;
    private float rate_3 = 30;
    private float rate_4 = 20;
    public void OpenInit_XiWang()
    {
        DataInit(isKey);

        //随机抽书
        showBook.Clear();
        Transform cardParent = this.transform.Find("CardGroup");
        checkBotton = this.transform.Find("CheckButton").GetComponent<Button>();
        checkBotton.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {


            //随机抽书
            int _lC = 0;
            int _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
            while (_lC < 50 && showBook.Contains(_rb))
            {
                _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
                _lC++;
                if (_lC > 40) print("死循环");
            }
            //如果是第一本书，有一半概率是剧本的书
            if (i == 0)
            {
                int _rbbb = UnityEngine.Random.Range(0, 2);
                if (_rbbb == 1) _rb = (int)(GameMgr.instance.nowBook);
            }

            showBook.Add(_rb);


            //显示书本图画
            var book = (BookNameEnum)int.Parse(_rb.ToString());
            cardParent.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book.ToString());

            if (i == 1) ClickWord_XW(cardParent.GetChild(i).gameObject);

            //随机抽取词组
            List<Type> _typeList = GameMgr.instance.GetBookList(book);
            _typeList.AddRange(GameMgr.instance.GetBookList(BookNameEnum.allBooks));
            List<Type> _typeListRare = new List<Type>();
            for (int j = 0; j < 3; j++)
            {
                //随机抽取级别
                int _rb1 = UnityEngine.Random.Range(0, 100);
                if (_rb1 < rate_1)//稀有度1
                {
                    _typeListRare = AllSkills.Rare_1;
                }
                else if (_rb1 < rate_2)//稀有度2
                {
                    _typeListRare = AllSkills.Rare_2;
                }
                else if (_rb1 < rate_3)//稀有度3
                {
                    _typeListRare = AllSkills.Rare_3;
                }
                else if (_rb1 < rate_4)//稀有度4
                {
                    _typeListRare = AllSkills.Rare_4;
                }

                //在书本和通用随机抽词语
                int _rbb = UnityEngine.Random.Range(0, _typeList.Count);
                int _loopCount = 0;
                while ((!_typeListRare.Contains(_typeList[_rbb])) && _loopCount < 50)
                {
                    _rbb = UnityEngine.Random.Range(0, _typeList.Count);
                    _loopCount++;
                }

                PoolMgr.GetInstance().GetObj(word_adj, (obj) =>
                {
                    var _word = obj.AddComponent(_typeList[_rbb]) as AbstractWord0;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                    obj.GetComponentInChildren<Image>().SetNativeSize();
                    obj.transform.parent = cardParent.GetChild(i).GetChild(0);
                    obj.transform.localScale = Vector3.one;
                    obj.GetComponent<Button>().interactable = false;

                });
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
        }

        //在当前的所有数据中随机抽一个
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);

        _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        TextMeshProUGUI info = this.transform.Find("info").GetComponentInChildren<TextMeshProUGUI>();
        info.text = tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent;
        nowEvent = tempNowDate[_r].name;

        RefreshNowList();

    }

    private GameObject chooseBook =null;
    public void ClickWord_XW(GameObject _botton)
    {
        if (chooseBook != _botton)
        {
            if(chooseBook!=null)
                chooseBook.GetComponent<Image>().color = Color.white;
            chooseBook = _botton;
            chooseBook.GetComponent<Image>().color = Color.grey;
        }

        else
        {
            chooseBook.GetComponent<Image>().color = Color.white;
            chooseBook = null;
        }
            


        if (chooseBook == null)
        {
            checkBotton.gameObject.SetActive(false);
        }

        else
        {
            checkBotton.gameObject.SetActive(true);
        }
            

    }
    

    public void Close_XiWang()
    {
        foreach (var _w in chooseBook.gameObject.GetComponentsInChildren<AbstractWord0>())
        {
            GameMgr.instance.AddCardList(_w);
        }
        GameMgr.instance.happenEvent.Add(nowEvent);
        Destroy(this.gameObject);
    }
    #endregion


    #region 交易
    GameObject cardPanal;
    [HideInInspector]public AbstractWord0 JY_chooseWord=null;
    Transform choose=null;
    Vector3 chooseScale = new Vector3(0.341f,0.32f,0.341f);
    public void OpenInit_JiaoYi()
    {
        DataInit(isKey);
        JY_chooseWord = null;
        choose = this.transform.Find("choose");
        cardPanal = this.transform.Find("cardRes").gameObject;

        cardPanal.SetActive(false);
   

        string adr_detail = "UI/WordInformation";

        //读取已拥有的书中，未获得的卡，概率相同
        TextMeshProUGUI titleText= this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        Transform CardGroup = this.transform.Find("CardGroup");


        //把选择框设置为看不见
        choose.transform.localScale = Vector3.zero;

        #region 抽取卡牌
        int[] _rWord =new int[3];
        
        int _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        int _loop = 0;
        while (GameMgr.instance.wordList.Contains(AllSkills.list_all[_random]) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[0] = _random;

        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0]== _random )|| GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[1] =_random;


        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0] == _random) || (_rWord[1] == _random) || GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("死循环");
        }
        _rWord[2] = _random;
        #endregion



        //生成卡牌
        for (int i = 0; i < 3; i++)
        {
                //生成卡牌
        PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
            {
                var _word=obj.AddComponent(AllSkills.list_all[_rWord[i]])as AbstractWord0;
                obj.GetComponentInChildren<WordInformation>().SetIsDetail(true);
                obj.GetComponent<Canvas>().overrideSorting = false;

                if (_word == null) print("this.gameObject.GetComponent<AbstractWord0>()");
                else
                    obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);


                obj.transform.parent = CardGroup;
                obj.transform.localPosition = new Vector3(0, 0, 3);
                obj.transform.localScale = Vector3.one*1.2f;

                //增加按钮组件
                var _botton=obj.AddComponent<Button>();
                _botton.transition = Selectable.Transition.None;
                _botton.onClick .AddListener(()=>ClickWord_YJ(_botton));
                // obj.GetComponent<Canvas>().overrideSorting = true;
            });
            
        }

        //抽取事件信息

        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);

        //切换文字内容
        titleText.text = tempNowDate[_r].name+"\n"+ tempNowDate[_r].textEvent;
        nowEvent = tempNowDate[_r].name;

        //刷新已用事件列表
        tempNowDate.Remove(tempNowDate[_r]);
        RefreshNowList();
    }


    void ClickWord_YJ(Button which)
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

          
            cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();
        }
  
        
        
    }


    void Close_JiaoYi()
    {
        GameMgr.instance.happenEvent.Add(nowEvent);
        Destroy(this.gameObject);
    }



    #endregion


    #region 危机
    public void OpenInit_WeiJi()
    {
        DataInit(isKey);
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {//如果有条件并且条件没满足,就重找一个

            _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
            if (loopCount > 50)
            {
                print("死循环");
                return;
            }
        }

        //
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();
        info.text = tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent;
        nowEvent = tempNowDate[_r].name;
        RefreshNowList();

    }

    public void Close_WeiJi()
    {
        GameMgr.instance.happenEvent.Add(nowEvent);
        Destroy(this.gameObject);
    }
    #endregion


    #region 场景
    public void OpenInit_ChangJing()
    {
        
    }
    #endregion

    #endregion



    #region 外部点击/调用事件


    public void Open(bool _isKey)
    {
       
        isKey = _isKey;
        
        
       CharacterManager.instance.pause = true;
        
        
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

    public void CloseAnim()
    {
        GetComponent<Animator>().Play("EventUI_Dis1");
    }

    public void Close()
    {
        CharacterManager.instance.pause = false;
        GameMgr.instance.eventHappen = false;
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
                    Close_YiWai();
                }
                break;
            case EventType.JiaoYi:
                {
                    Close_JiaoYi();
                }
                break;
            case EventType.WeiJi:
                {
                    Close_WeiJi();
                }
                break;
        }
    }

    #endregion
}
