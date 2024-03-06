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
    JiaoYi = 4
}


public class EventUI : MonoBehaviour
{
    [Header("这是哪一个界面的UI")]
    public EventType type;



    public test1ExcelData data;
    List<test1ExcelItem> needAllDate = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate = new List<test1ExcelItem>();

    private void Start()
    {
        GameMgr.instance.AddBookList(BookNameEnum.HongLouMeng);
        GameMgr.instance.AddBookList(BookNameEnum.ElectronicGoal);
        //GameMgr.instance.AddBookList(BookNameEnum.EgyptMyth);
        Open();
    }


    #region 处理data
    void DealWithData(EventType _type)
    {
        needAllDate.Clear();
        foreach (var _t in data.items)
        {
            if (_t.type == _type)
            {
                if (!needAllDate.Contains(_t))
                    needAllDate.Add(_t);
            }
        }
        needNowDate.AddRange(needAllDate);
    }



    void RefreshNowList()
    {
        if (needNowDate.Count <= 0)
        {
            needNowDate.AddRange(needAllDate);
        }
    }


    #endregion


    #region 五种类型的事件

    #region 意外
    /// <summary>
    /// 打开意外面板时的初始刷新
    /// </summary>
    public void OpenInit_YiWai()
    {
        Transform cardParent=this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();

       
        for (int i = 0; i < 6; i += 2)
        { 
            //重新抽取
            int _r = UnityEngine.Random.Range(0, needNowDate.Count);

            //切换文字内容
            text[i].text= needNowDate[_r].name;
            text[i+1].text = needNowDate[_r].textEvent;

            //刷新已用事件列表
            needNowDate.Remove(needNowDate[_r]);
            RefreshNowList();
        }

    }

    #endregion


    #region 访客
    public void OpenInit_FangKe()
    {

    }
    #endregion


    #region 希望
    public void OpenInit_XiWang()
    {

    }
    #endregion


    #region 交易

    private AbstractWord0 JY_chooseWord=null;
    Transform choose=null;
    Vector3 chooseScale = new Vector3(0.341f,0.32f,0.341f);
    public void OpenInit_JiaoYi()
    {
        JY_chooseWord = null;
        choose = this.transform.Find("choose");

        string adr_detail = "UI/WordInformation";

        //读取已拥有的书中，未获得的卡，概率相同
        TextMeshProUGUI titleText= this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        Transform CardGroup = this.transform.Find("CardGroup");


        //把选择框设置为看不见
        choose.transform.localScale = Vector3.zero;


        //  选书
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
                {//所有的书都已经获取，则设为null
                    _rBook[i] = BookNameEnum.BookNull;
                    _b = true;
                } 
            }

            _rBook[i] = GameMgr.instance.GetBookList()[x];
            x++;
            if (x >= bookCount) x = 0;

            
        }

        //生成卡牌
        for (int i = 0; i < 3; i++)
        {
            //选卡牌
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
                
            

                //生成卡牌
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

                    //增加按钮组件
                    var _botton=obj.AddComponent<Button>();
                    _botton.transition = Selectable.Transition.None;
                    _botton.onClick .AddListener(()=>ClickWord(_botton));
                  // obj.GetComponent<Canvas>().overrideSorting = true;
                });
            }
        }



        int _r = UnityEngine.Random.Range(0, needNowDate.Count);


        //切换文字内容
        titleText.text = needNowDate[_r].name+"\n"+ needNowDate[_r].textEvent;

        //刷新已用事件列表
        needNowDate.Remove(needNowDate[_r]);
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
        }
  
        

        if (JY_chooseWord == null) print("null");
        else print(JY_chooseWord.wordName);
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
    #endregion


    #region 危机
    public void OpenInit_WeiJi()
    {

    }
    #endregion

    #endregion



    #region 外部点击/调用事件


    public void Open()
    {
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
        }
    }

    

    public void ClickNext()
    {
        switch (type)
        {
            case EventType.XiWang: 
                {

                }break;
            case EventType.FangKe:
                {

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
