using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

 using UnityEngine.Events;
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

    [HideInInspector]public Vector3 eventWorldPos = Vector3.one;

    //����Ҫ����
    List<test1ExcelItem> needAllDate_nKey = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_nKey = new List<test1ExcelItem>();
    //��Ҫ����
    List<test1ExcelItem> needAllDate_Key = new List<test1ExcelItem>();
    List<test1ExcelItem> needNowDate_Key = new List<test1ExcelItem>();
    //������ʹ�õ�����
    List<test1ExcelItem> tempAllDate = new List<test1ExcelItem>();
    List<test1ExcelItem> tempNowDate = new List<test1ExcelItem>();


    [Header("�ƿ�Ԥ����(�ֶ�)")]
    public GameObject word_adj;
    public GameObject word_verb;
    public GameObject word_item;

  


    #region ����data 
    
    /// <summary>
    /// �ڳ�ʼ����,����isKEY������
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
    /// ���������򿪵�ʱ���ʼ��
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData(EventType _type)
    {
        //�������
        needAllDate_nKey.Clear();
        needAllDate_Key.Clear();
        needNowDate_nKey.Clear();
        needNowDate_Key.Clear();

        //��ȡ�����͵ĵ�ǰ���п��Է�����data
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


    #region ����
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="eventTrigger">��Ҫ����¼���EventTrigger</param>
    /// <param name="eventTriggerType">�¼�����</param>
    /// <param name="callback">�ص�����</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
        Debug.Log("AddPointerEnterEvent");
    }
    #endregion


    //��ǰ���ڴ����nowEvent
    private test1ExcelItem nowEvent;
    private test1ExcelItem[] event_YW=new test1ExcelItem[3];



    IEnumerator TypeDelay(string text, TextMeshProUGUI textUI, float delay)
    {
        int _index = 0;
        textUI.text= "";
        while (_index< text.Length)
        {
            textUI.text += text[_index];
            _index++;
            yield return new WaitForSeconds(delay);
        }
    }


    #region �������͵��¼�

    #region ����
    /// <summary>
    /// ���������ʱ�ĳ�ʼˢ��
    /// </summary>
    public void OpenInit_YiWai()
    {
        //���ݴ����¼�����Ϣ������ɸѡ���е��ı���Ϣ
        DataInit(false);

        //��ȡ���
        checkBotton = this.transform.Find("CheckButton").GetComponent<Button>();
        checkBotton.gameObject.SetActive(false);
        Transform cardParent = this.transform.Find("CardGroup");
        TextMeshProUGUI[] text = cardParent.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < 6; i += 2)
        {
            var et = cardParent.GetChild(i / 2).GetComponent<EventTrigger>();
            AddPointerEvent(et, EventTriggerType.PointerClick, (obj) => { Click_YW(et.gameObject); });

            //��ȡ�����������¼��ı�
            int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
            int loopCount = 0;
            while ((tempNowDate[_r].textTrigger != "") && (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
            {
                _r = UnityEngine.Random.Range(0, tempNowDate.Count); loopCount++;
                if (loopCount > 50)
                {
                    print("��ѭ��");return;
                }
            }

            //�л�UI��������
            text[i].text = tempNowDate[_r].name;
            text[i + 1].text = tempNowDate[_r].textEvent;
            event_YW[i/2] = tempNowDate[_r];

            //ˢ�������¼��б����ظ���ȡ��
            tempNowDate.Remove(tempNowDate[_r]);
            RefreshNowList();
        }
    }


    private GameObject nowChosen_YW = null;
    public void Click_YW(GameObject obj)
    {
        if (nowChosen_YW != obj)
        {
            if (nowChosen_YW != null)
                nowChosen_YW.GetComponent<Image>().color = Color.white;
            nowChosen_YW = obj;
            nowChosen_YW.GetComponent<Image>().color = Color.grey;
            nowEvent = event_YW[obj.transform.GetSiblingIndex()];
        }

        else
        {
            nowChosen_YW.GetComponent<Image>().color = Color.white;
            nowChosen_YW = null;
            nowEvent = null; 
        }



        if (nowChosen_YW == null)
        {
            checkBotton.gameObject.SetActive(false);
        }

        else
        {
            checkBotton.gameObject.SetActive(true);
        }
    }
    public void Close_YiWai()
    {
       
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
        
    }


    #endregion


    #region �ÿ�
    Animator tempAnimator;
    WaitForFixedUpdate tempAnim= new WaitForFixedUpdate();
    private int KeyCharacter = -1;
    [HideInInspector]public int triggerName = -1;
    public void OpenInit_FangKe()
    {
        
        DataInit(isKey); 
        KeyCharacter = -1;
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        int loopCount = 0;
        while ((tempNowDate[_r].textTrigger != "") &&
            (!GameMgr.instance.happenEvent.Contains(tempNowDate[_r].textTrigger)))
        {
            //�����������������û����,������һ��
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
        sprite.GetComponent<Animator>().SetBool("0", true);
        if(triggerName != -1) sprite.GetComponent<Animator>().SetBool(triggerName.ToString(), false);
        tempAnimator = sprite.GetComponent<Animator>();
        StartCoroutine(FangKeAnimation());

        triggerName = -1;
        if (isKey && KeyCharacter != -1)
        {
            triggerName = KeyCharacter;//�������ID 
        }
        else
        {
            triggerName = GameMgr.instance.GetNextCreateChara();//���飬��ID
            triggerName += 1;
        }
        
        sprite.GetComponent<Animator>().SetBool(triggerName.ToString(),true);
        //sprite.SetNativeSize();


        if ((tempNowDate[_r].happen != null) && (tempNowDate[_r].happen != ""))
            KeyCharacter = int.Parse(tempNowDate[_r].happen);
        nowEvent = tempNowDate[_r];
       
        //info.text = tempNowDate[_r].name;
        StartCoroutine(TypeDelay((tempNowDate[_r].name ), info, 0.03f));
       // words.text = tempNowDate[_r].textEvent;
        StartCoroutine(TypeDelay((tempNowDate[_r].textEvent), words, 0.03f));

        RefreshNowList();
    }

    IEnumerator FangKeAnimation()
    {
        int loopMax =0;
        while ((!tempAnimator.GetBool("0"))||(loopMax<5))
        {
            print("loopMax" + loopMax);
            loopMax++;
            yield return tempAnim;
        }
        tempAnimator.SetBool("0", false);
    }
    void Close_FangKe()
    {
        if (isKey && KeyCharacter != -1)
        { 
            GameMgr.instance.CreateTheCharacterPut(KeyCharacter);
        }
        else
        {
            GameMgr.instance.CreateCharacterPut(1);
        }   
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region ϣ��
    Button checkBotton;
    private List<int> showBook=new List<int>();

    public void OpenInit_XiWang()
    {
        DataInit(isKey);

        //�������
        showBook.Clear();
        Transform cardParent = this.transform.Find("CardGroup");
        checkBotton = this.transform.Find("CheckButton").GetComponent<Button>();
        checkBotton.gameObject.SetActive(false);

        for (int tt = 0;tt < 3; tt++)
        {
            //ɾ��
            if (cardParent.GetChild(tt).GetChild(0).childCount != 0)
            {
                for (int i = cardParent.GetChild(tt).GetChild(0).childCount-1; i >= 0; i--)
                {
                    Destroy(cardParent.GetChild(tt).GetChild(0).GetChild(i).gameObject);
                }
            }
        }
       


        for (int i = 0; i < 3; i++)
        {
            //�������
            int _lC = 0;
            int _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
            while (_lC < 50 && showBook.Contains(_rb))
            {
                _rb = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
                _lC++;
                if (_lC > 40) print("��ѭ��");
            }
            //����ǵ�һ���飬��һ������Ǿ籾����
            if (i == 0)
            {
                int _rbbb = UnityEngine.Random.Range(0, 2);
                if (_rbbb == 1) _rb = (int)(GameMgr.instance.nowBook);
            }

            showBook.Add(_rb);


            //��ʾ�鱾ͼ��
            var book = (BookNameEnum)int.Parse(_rb.ToString());
            cardParent.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book.ToString());
            cardParent.GetChild(i).GetComponent<Image>().SetNativeSize();
            if (i == 1) ClickWord_XW(cardParent.GetChild(i).gameObject);

            //�����ȡ����
            List<Type> _typeList = GameMgr.instance.GetBookList(book);
            _typeList.AddRange(GameMgr.instance.GetBookList(BookNameEnum.allBooks));
            List<Type> _typeListRare = new List<Type>();
            for (int j = 0; j < 3; j++)
            {
                //�����ȡ����
                int _rb1 = UnityEngine.Random.Range(0, 100);
                if (_rb1 < GameMgr.instance.cardRate_1)//ϡ�ж�1
                {
                    _typeListRare = AllSkills.Rare_1;
                }
                else if (_rb1 < GameMgr.instance.cardRate_2)//ϡ�ж�2
                {
                    _typeListRare = AllSkills.Rare_2;
                }
                else if (_rb1 < GameMgr.instance.cardRate_3)//ϡ�ж�3
                {
                    _typeListRare = AllSkills.Rare_3;
                }
                else if (_rb1 < GameMgr.instance.cardRate_4)//ϡ�ж�4
                {
                    _typeListRare = AllSkills.Rare_4;
                }

                //���鱾��ͨ����������
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
                    
                    obj.transform.parent = cardParent.GetChild(i).GetChild(0);
                    obj.transform.localScale = Vector3.one;
                    obj.GetComponent<Button>().interactable = false;

             
                    if (obj.TryGetComponent<SeeWordDetail>(out var _s))
                        _s.SetPic(_word);

                });
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
        }

        //�ڵ�ǰ�����������������һ��
        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);

        _r = UnityEngine.Random.Range(0, tempNowDate.Count);
        TextMeshProUGUI info = this.transform.Find("info").GetComponentInChildren<TextMeshProUGUI>();
        //info.text = tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent;
        StartCoroutine(TypeDelay((tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent), info, 0.03f));
        nowEvent = tempNowDate[_r];

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
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region ����
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

        //��ȡ��ӵ�е����У�δ��õĿ���������ͬ
        TextMeshProUGUI titleText= this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        Transform CardGroup = this.transform.Find("CardGroup");


        //��ѡ�������Ϊ������
        choose.transform.localScale = Vector3.zero;

        #region ��ȡ����
        int[] _rWord =new int[3];
        
        int _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        int _loop = 0;
        while (GameMgr.instance.wordList.Contains(AllSkills.list_all[_random]) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("��ѭ��");
        }
        _rWord[0] = _random;

        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0]== _random )|| GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("��ѭ��");
        }
        _rWord[1] =_random;


        _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
        _loop = 0;
        while (((_rWord[0] == _random) || (_rWord[1] == _random) || GameMgr.instance.wordList.Contains(AllSkills.list_all[_random])) && _loop < 50)
        {
            _random = UnityEngine.Random.Range(0, AllSkills.list_all.Count);
            _loop++;
            if (_loop > 40) print("��ѭ��");
        }
        _rWord[2] = _random;
        #endregion


        //���ɿ���
        for (int i = 0; i < 3; i++)
        {
            
            //���ɿ���
            //PoolMgr.GetInstance().GetObj(adr_detail, (obj) =>
            //{
                var  obj = ResMgr.GetInstance().Load<GameObject>(adr_detail);
                var _word=obj.AddComponent(AllSkills.list_all[_rWord[i]])as AbstractWord0;
                obj.GetComponentInChildren<WordInformation>().SetIsDetail(true);
                obj.GetComponent<Canvas>().overrideSorting = false;

                if (_word == null) print("this.gameObject.GetComponent<AbstractWord0>()");
                else
                    obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);


                obj.transform.parent = CardGroup;
                obj.transform.localPosition = new Vector3(0, 0, 3);
                obj.GetComponent<Canvas>().overrideSorting = false;

                obj.transform.localScale = Vector3.one*1.2f;
                obj.GetComponent<Canvas>().overrideSorting = false;

                //���Ӱ�ť���
                var _botton =obj.AddComponent<Button>();
                _botton.transition = Selectable.Transition.None;
                _botton.onClick .AddListener(()=>ClickWord_YJ(_botton));
                // obj.GetComponent<Canvas>().overrideSorting = true;
            //});
            
        }

        //��ȡ�¼���Ϣ

        int _r = UnityEngine.Random.Range(0, tempNowDate.Count);

        //�л���������
        StartCoroutine(TypeDelay((tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent), titleText, 0.03f));
        //titleText.text = tempNowDate[_r].name+"\n"+ tempNowDate[_r].textEvent;
        nowEvent = tempNowDate[_r];

        //ˢ�������¼��б�
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

          
            //cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();
        }
  
        
        
    }


    public void JY_OpenCardRes()
    {
        cardPanal.GetComponent<ShooterWordCheck>().OpenMainPanal();
    }

    void Close_JiaoYi()
    {
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }



    #endregion


    #region Σ��
    [HideInInspector] public bool WJ_static = false;//�ⲿ��open֮ǰ����
    public void OpenInit_WeiJi()
    {
        DataInit(isKey);
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

        //���ð�ť
        if (WJ_static)
        {
            this.transform.Find("button").Find("WelcomeButton").gameObject.SetActive(true);
            this.transform.Find("button").Find("EscapeButton").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("button").Find("WelcomeButton").gameObject.SetActive(true);
            this.transform.Find("button").Find("EscapeButton").gameObject.SetActive(true);
        }

      
        //
        TextMeshProUGUI info = this.transform.Find("EventInfo").GetComponentInChildren<TextMeshProUGUI>();
        //info.text = tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent;
        StartCoroutine(TypeDelay((tempNowDate[_r].name + "\n" + tempNowDate[_r].textEvent), info, 0.03f));
        nowEvent = tempNowDate[_r];
        RefreshNowList();

    }

    public void Click_WeiJi()
    {
        print("Click_WeiJi");

          //�����ȡһ���������ʾ��Ӧ����
        GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().CreateMonster(1);
        CloseAnim();
    }

    public void Close_WeiJi()
    {
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion


    #region ����

    SpriteRenderer sp_cj_image;
    SpriteRenderer sp_cj_word;
    int cj_changeTo = -1;
    public void OpenInit_ChangJing()
    {
        DataInit(isKey);

        cj_changeTo = -1;
        sp_cj_image =transform.Find("scenephoto").Find("scene_jiuba").GetComponent<SpriteRenderer>();
        sp_cj_word=transform.Find("scenephoto").Find("scene_jiuba1").GetComponent<SpriteRenderer>();

        //�鳡��
        cj_changeTo = UnityEngine.Random.Range(0, tempNowDate.Count);
        int _loopC = 0;
        while ((cj_changeTo == GameMgr.instance.levelSenceIndex) && (_loopC < 50))
        {
            cj_changeTo = UnityEngine.Random.Range(0, tempNowDate.Count);  _loopC++;
        }
     
        GameMgr.instance.levelSenceIndex = cj_changeTo;

        //�л���������
        var titleText = this.transform.Find("EventInfo").GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeDelay((tempNowDate[cj_changeTo].name + "\n" + tempNowDate[cj_changeTo].textEvent), titleText, 0.03f));
        nowEvent = tempNowDate[cj_changeTo];
        sp_cj_image.sprite = ResMgr.GetInstance().Load<Sprite>("UI/" + (tempNowDate[cj_changeTo].happen));
        sp_cj_word.sprite = ResMgr.GetInstance().Load<Sprite>("UI/" + (tempNowDate[cj_changeTo].happen) + "_word");

        //ˢ�������¼��б�
        tempNowDate.Remove(tempNowDate[cj_changeTo]);
        RefreshNowList();

    }
    public void Close_ChangJing()
    {
        if (cj_changeTo == -1) return;

        //�������
       
        GameMgr.instance.ChangeLevelTo(cj_changeTo);

        //
        GameMgr.instance.happenEvent.Add(nowEvent.name);
        GameMgr.instance.PopupEvent(eventWorldPos, nowEvent.name, nowEvent.textDraft);
        Destroy(this.gameObject);
    }
    #endregion

    #endregion


    #region �¼�����Ч��invoke-happen 
    //��Ӧtest1���happen��һ�У�д�¼��ĺ�������

    ///�����⡿���ó��趨ҳ��
    void Happen_Setting()
    {
        print("Happen_Setting");
        string adr = "UI/Setting";
        var obj=ResMgr.GetInstance().Load<GameObject>(adr);
        obj.GetComponent<Setting>().InitSetting(settingUiType.Quality);
        obj.transform.parent = GameObject.Find("CharacterCanvas").transform;
        
    }
    ///�����⡿����һ������ۣ������һ������
    void Happen_UnlochkShoot()
    {
        print("Happen_UnlochkShoot");
        GameMgr.instance.AddDice(1);

    }  ///��������ϵ��ʧ��
    void Happen_ShooterBad()
    {
        print("Happen_ShooterBad");


    }  
    
    ///B�ӳ��ִ��������򣬲���á��������������趨
    void Happen_BSettingXNSML()
    {
        print("Happen_BSettingXNSML");

        GameMgr.instance.settingR.Add(typeof(XuNiShengMingLi));
        GameMgr.instance.settingPanel.RefreshList();
    }

    ///A�ӻ�á���ѧ���֡��趨
    void Happen_ASettingHXJL()
    {
        print("Happen_ASettingHXJL");

        GameMgr.instance.settingL.Add( typeof(HuaXueJiLe));
        GameMgr.instance.settingPanel.RefreshList();
    }
    ///�����⡿Nexus-6���ֱۼ����ƿ�
    void Happen_GetNexus6()
    {
        print("Happen_GetNexus6");
        GameMgr.instance.AddCardList(new Nexus_6Arm());
    }

    ///�����⡿���һ�ӣ������������̣�����Σ���Կ����˵Ļ���
    void Happen_WeiJi()
    {
        print("Happen_WeiJi");
        

    }

    ///�����⡿�������鶼��á������д桱����ϸ���ĵ�-�趨��
    void Happen_SettingRXSC()
    {
        print("Happen_SettingRXSC");

        GameMgr.instance.settingL.Add(typeof(RenXingShangCun));
        GameMgr.instance.settingR.Add(typeof(RenXingShangCun));
        GameMgr.instance.settingPanel.RefreshList();

    }

    ///�����⡿����һ��������趨��ȡ�����е�ѡ����С���ɫ����ǩ���Ҳ�����ϡ�ж�
    void Happen_SettingCharacter()
    {
        print("Happen_SettingCharacter");
        string adr = "UI/Setting";
        var obj = ResMgr.GetInstance().Load<GameObject>(adr);
        obj.GetComponent<Setting>().InitSetting(settingUiType.Chara);
    }

    ///�����⡿�������鶼��á������ҡ�����ϸ���ĵ�-�趨��
    void Happen_SettingSJWL()
    {
        print("Happen_SettingSJWL");

        GameMgr.instance.settingL.Add(typeof(ShenJingWenLuan));
        GameMgr.instance.settingR.Add(typeof(ShenJingWenLuan));
        GameMgr.instance.settingPanel.RefreshList();
    }

    ///�����⡿���ű�ֲ��ļ�������ƿ�
    void Happen_GetBeiZhiRuMemory()
    {
        print("Happen_GetBeiZhiRuMemory");
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi());
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi());
        GameMgr.instance.AddCardList(new BeiZhiRuDeJiYi());
    }
    #endregion



    #region �ⲿ���/�����¼�
    public void RefreshEvent()
    {
        //ˢ�µ�ǰ
        switch (type)
        {
            case EventType.FangKe:
                { OpenInit_FangKe(); }
                break;
            case EventType.YiWai:
                { OpenInit_YiWai(); }
                break;
            case EventType.XiWang:
                { OpenInit_XiWang(); }
                break;
        }
    }

    public void Open(bool _isKey)
    {
       
        isKey = _isKey;

        GameMgr.instance.OpenEventUi();
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

        //����ǰ��nowChosenEvent
        if ((type != EventType.FangKe) && (type != EventType.ChangJing))
        {
            if ((nowEvent.happen != null) && (nowEvent.happen != ""))
            {
                print("nowEvent" + nowEvent.happen);
                Invoke(nowEvent.happen,0);
            } 
        }

        //���ݼ���ݸ屾
        GameMgr.instance.draftUi.AddContent(nowEvent.textDraft);
        
        //�ر����
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
            case EventType.ChangJing:
                {
                    Close_ChangJing();
                }
                break;
        }
    }

    #endregion



}
