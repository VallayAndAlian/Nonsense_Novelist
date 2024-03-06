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
    JiaoYi = 4
}


public class EventUI : MonoBehaviour
{
    [Header("������һ�������UI")]
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


    #region ����data
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


    #region �������͵��¼�

    #region ����
    /// <summary>
    /// ���������ʱ�ĳ�ʼˢ��
    /// </summary>
    public void OpenInit_YiWai()
    {
        Transform cardParent=this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();

       
        for (int i = 0; i < 6; i += 2)
        { 
            //���³�ȡ
            int _r = UnityEngine.Random.Range(0, needNowDate.Count);

            //�л���������
            text[i].text= needNowDate[_r].name;
            text[i+1].text = needNowDate[_r].textEvent;

            //ˢ�������¼��б�
            needNowDate.Remove(needNowDate[_r]);
            RefreshNowList();
        }

    }

    #endregion


    #region �ÿ�
    public void OpenInit_FangKe()
    {

    }
    #endregion


    #region ϣ��
    public void OpenInit_XiWang()
    {

    }
    #endregion


    #region ����

    private AbstractWord0 JY_chooseWord=null;
    Transform choose=null;
    Vector3 chooseScale = new Vector3(0.341f,0.32f,0.341f);
    public void OpenInit_JiaoYi()
    {
        JY_chooseWord = null;
        choose = this.transform.Find("choose");

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



        int _r = UnityEngine.Random.Range(0, needNowDate.Count);


        //�л���������
        titleText.text = needNowDate[_r].name+"\n"+ needNowDate[_r].textEvent;

        //ˢ�������¼��б�
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


    #region Σ��
    public void OpenInit_WeiJi()
    {

    }
    #endregion

    #endregion



    #region �ⲿ���/�����¼�


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
