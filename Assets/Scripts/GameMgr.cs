using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CustomList : List<string>
{
    public new void Add(string item)
    {
        IsContentValid(item);
        base.Add(item);
    }

    private void IsContentValid(string content)
    {
        for (int tt = 0; tt < GameMgr.instance.leftData.Count; tt++)
        {
            var _t = GameMgr.instance.leftData[tt];
            if (_t.textTrigger == content)
            {
                GameMgr.instance.leftData.Remove(_t);
                if (_t.isKey) GameMgr.instance.canHappenData_Key.Add(_t);
                else GameMgr.instance.canHappenData_nKey.Add(_t);
            }
        }
        //foreach (var _t in GameMgr.instance.leftData)
        //{
            
        //}
    }
}


public class GameMgr : MonoSingleton<GameMgr>
{

    [HideInInspector] public List<AbstractSetting> settingL=new List<AbstractSetting>();
    [HideInInspector] public List<AbstractSetting> settingR = new List<AbstractSetting>();


    //关闭界面相关
    private GameObject exitPanel;
    GameObject exitObj;
    Button exitButton;
    Button cancelButton;
    bool hasOpenExit = false;
    public SettingList settingPanel;
    public GameObject CardRes;


    //当前的剧本
    public BookNameEnum nowBook;

    //玩家已有的书库
    List<BookNameEnum> bookList = new List<BookNameEnum>();

    //玩家已有的，词语全部获取完毕的书库
    List<BookNameEnum> bookAllGetList = new List<BookNameEnum>();

    //战斗总牌库
    public List<Type> wordList = new List<Type>();

    //当前未使用的牌的牌库
    List<Type> wordNowList = new List<Type>();

    //当前待使用的牌的牌库
    List<Type> wordGoingUseList = new List<Type>();

    //当前已使用的牌的牌库
    List<Type> wordHasUseList = new List<Type>();

    //名词的消耗性使用相关
    //public List<int> wordTimes = new List<int>();
    //public List<int> wordHasUseTimes = new List<int>();
    //public List<int> wordCanUseTimes = new List<int>();
    public Dictionary<Type, List<int>> NwordTimes = new Dictionary<Type, List<int>>();
    public Dictionary<Type, List<int>> NwordCanUseTimes = new Dictionary<Type, List<int>>();
    #region 事件
    //当前触发的所有事件
    [HideInInspector] public CustomList happenEvent = new CustomList();


    //全部数据
    public test1ExcelData data;

    //当前可以触发的所有事件
    [HideInInspector] public List<test1ExcelItem> canHappenData_nKey;
    [HideInInspector] public List<test1ExcelItem> canHappenData_Key;
    [HideInInspector] public List<test1ExcelItem> leftData;

    /// <summary>
    /// 读取excel表数据，加入所有没有前置条件的data
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData()
    {
        canHappenData_nKey.Clear();
        canHappenData_Key.Clear();
        leftData.Clear();
    
        foreach (var _t in data.items)
        {
            if ((_t.textTrigger == null)|| (_t.textTrigger ==""))
            {
                if (!_t.isKey)
                {
                    if (!canHappenData_nKey.Contains(_t))
                        canHappenData_nKey.Add(_t);
                }
                else
                {
                    if (!canHappenData_Key.Contains(_t))
                        canHappenData_Key.Add(_t);
                }
            }
            else
            {
                if (!leftData.Contains(_t))
                    leftData.Add(_t);
            }
        }
    }

    public bool HaveCanHappenKeyEvent(int _enumNum)
    {
        var _num = 0;
 
        foreach (var item in canHappenData_Key)
        {
          
            if (item.type == (EventType)(Convert.ToInt32(_enumNum)))
            {

                return true;
            }
        }
  
        return false;
    }

    #endregion

    [Header("界面设置(手动)")]
    public GameObject UiCanvas;
    public GameObject characterCanvas;
    public DraftUi draftUi;
    public GameObject combatCanvas;


    public bool eventHappen = false;

    void OpenCharacterPutting()
    {
        combatCanvas.gameObject.SetActive(true);
        draftUi.gameObject.SetActive(false);
        UiCanvas.gameObject.SetActive(true);
    }
    private void Awake()
    {
        DealWithData();
        CardRes.SetActive(false);
        draftUi.InitContent(); 
    }

    private void Start()
    {
        //界面设置
        OpenCharacterPutting();

        //退出菜单
        exitPanel = Resources.Load<GameObject>("UI/exitPanel");
        //DontDestroyOnLoad(this.gameObject);

        //牌库
        InitCardList();
    }
    private void Update()
    {
        //退出菜单
        if (hasOpenExit) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitObj = Instantiate(exitPanel);
            Time.timeScale = 0f;
            hasOpenExit = true;
            exitButton = exitObj.transform.GetComponentsInChildren<Button>()[0];
            cancelButton = exitObj.transform.GetComponentsInChildren<Button>()[1];
            exitButton.onClick.AddListener(ExitButton);
            cancelButton.onClick.AddListener(BackToGame);
        }
    }



    #region 事件气泡
    /// <summary>
    /// 显示事件的飘字
    /// </summary>
    public void PopupEvent(Vector3 pos, string name, string info)
    {
        var obj=ResMgr.GetInstance().Load<GameObject>("UI/popEvent"); 
        //世界坐标转画布坐标
        Vector2 canvasSize = characterCanvas.GetComponent<RectTransform>().sizeDelta;
        Vector3 viewPortPos3d = Camera.main.WorldToViewportPoint(pos);
        Vector2 viewPortRelative = new Vector2(viewPortPos3d.x - 0.5f, viewPortPos3d.y - 0.5f);
        Vector2 cubeScreenPos = new Vector2(viewPortRelative.x * canvasSize.x, viewPortRelative.y );
        obj.GetComponent<RectTransform>().position = pos;
        obj.transform.parent = characterCanvas.transform;
        obj.transform.localScale = Vector3.one;
         StartCoroutine(MoveToPosCanvas( obj.GetComponent<RectTransform>(), name,info));
      
      
    }

    WaitForSeconds waitFrame = new WaitForSeconds(0.02f);
    IEnumerator MoveToPosCanvas(RectTransform obj,string text1,string text2)
    {  
        var pos = new Vector2(-796, -425);
        Image image = obj.gameObject.GetComponentInChildren<Image>();
        TextMeshProUGUI text = obj.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        //停留在最开始的点 
        float i = 0;
        while (i< 0.6f)
        {
            i += 0.02f;
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
            text.text = text1;
            yield return waitFrame;
        }


        //移动到草稿本
        while (Mathf.Abs((obj.anchoredPosition- pos).x)>2f)
        {
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            obj.anchoredPosition -= 0.08f * (obj.anchoredPosition - pos);
            yield return waitFrame;
        }

        //停留在草稿本
        i = 0;
        text.text = "";
        int _index = 0;
        float textDelay = 0.06f;
        float textDAll = 0;
  
        while (i < 0.6f)
        {
            i += 0.02f;
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
            if (i > textDAll)
            {
                textDAll += textDelay;
                if (_index < text2.Length)
                {
                    text.text += text2[_index];
                    _index += 1;
                }
            }
          
            yield return waitFrame;
        }
        Destroy(obj.gameObject);
    }


    #endregion


    #region exit菜单相关
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToGame()
    {hasOpenExit = false;
        Destroy(exitObj.gameObject);
        Time.timeScale = 1f;
        
    }
    #endregion



    #region 新 牌库


    /// <summary>
    /// 牌库初始化(包含初始牌库组)
    /// </summary>
    /// 

    void InitCardList()
    {
        wordList.Clear();
        wordNowList.Clear();
        wordGoingUseList.Clear();
        wordHasUseList.Clear();

        NwordTimes.Clear();
        //wordHasUseTimes.Clear();
        NwordCanUseTimes.Clear();

        happenEvent.Clear();

        //测试用
        // wordList.AddRange(new Type[] { typeof(BuryFlower), typeof(QiChongShaDance) });

        nowBook = BookNameEnum.ElectronicGoal;

        //加入通用词组
        StartCardList();
    }


    /// <summary>
    /// 通用卡组
    /// </summary>
    void StartCardList()
    {
        //wordList.AddRange(new Type[] { typeof(BuryFlower), typeof(Shuai), typeof(Shuai), typeof(FuTouAxe), typeof(Shuai) , typeof(HeartBroken),typeof(ZiShuiJIng),
        //typeof(XianZhiHead)});
        AddCardList(new BuryFlower());
        AddCardList(new Shuai());AddCardList(new Shuai());
        AddCardList(new FuTouAxe());
        AddCardList(new Shuai());
        AddCardList(new ZiShuiJIng());
        AddCardList(new HeartBroken());
        AddCardList(new XianZhiHead());
        RefreshNowList();
    }


    /// <summary>
    /// 往卡组里加入卡牌
    /// </summary>
    /// <param name="_word"></param>
    public void AddCardList(AbstractWord0 _word)
    {
        wordList.Add(_word.GetType());
        wordNowList.Add(_word.GetType());

        if (_word is AbstractItems)
        {
 
            if (NwordTimes.ContainsKey((_word as AbstractItems).GetType()))
            {
                NwordTimes[(_word as AbstractItems).GetType()].Add((_word as AbstractItems).useTimes);
            }
            else
            {
                NwordTimes.Add((_word as AbstractItems).GetType(), new List<int> { (_word as AbstractItems).useTimes });
            }

            if (NwordCanUseTimes.ContainsKey((_word as AbstractItems).GetType()))
            {
                NwordCanUseTimes[(_word as AbstractItems).GetType()].Add((_word as AbstractItems).useTimes);
            }
            else
            {
                NwordCanUseTimes.Add((_word as AbstractItems).GetType(), new List<int> { (_word as AbstractItems).useTimes });
            }

        }
    }


    /// <summary>
    /// 从卡组里删除卡牌
    /// </summary>
    /// <param name="_word"></param>
    public void DeleteCardList(AbstractWord0 _word)
    {
        if (_word == null) print("null");
        if (wordList.Contains(_word.GetType()))
        {
            wordList.Remove(_word.GetType()); 
            if (_word is AbstractItems)
            {
                if (NwordTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    NwordTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                }
                if (NwordCanUseTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    NwordCanUseTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                }
            }
        }

        if (wordNowList.Contains(_word.GetType()))
        {
            wordNowList.Remove(_word.GetType());
            return;
        }
        else if (wordHasUseList.Contains(_word.GetType()))
        {
            wordHasUseList.Remove(_word.GetType());
            return;
        }
        else if (wordGoingUseList.Contains(_word.GetType()))
        {
            wordGoingUseList.Remove(_word.GetType());
        }
    }

    public void DeleteCardList(Type _word)
    {
        if (_word == null) print("null");
        print("0"+ _word);
        if (wordList.Contains(_word))
        {
            print("1");
            wordList.Remove(_word);
            if (_word.BaseType == typeof(AbstractItems))
            {
                print("2");
                if (NwordTimes.ContainsKey(_word))
                {
                    NwordTimes[_word].RemoveAll(item => item == 0);
                }
                if (NwordCanUseTimes.ContainsKey(_word))
                {
                    NwordCanUseTimes[_word].RemoveAll(item => item == 0);
                }
            }
        }


        if (wordNowList.Contains(_word))
        {
            wordNowList.Remove(_word);
            return;
        }
        else if (wordHasUseList.Contains(_word))
        {
            wordHasUseList.Remove(_word);
            return;
        }
        else if (wordGoingUseList.Contains(_word))
        {
            wordGoingUseList.Remove(_word);
        }


    }



    /// <summary>
    /// 返回已经使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetHasUsedList()
    {
        return wordHasUseList;
    }
    /// <summary>
    /// 返回未使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetNowList()
    {
        return wordNowList;
    }

    /// <summary>
    /// 随机返回一个未使用的词条
    /// </summary>
    /// <returns></returns>
    public Type GetNowListOne()
    {
        int count = UnityEngine.Random.Range(0, wordNowList.Count);
        var _res = wordNowList[count];
        wordNowList.Remove(_res);
        wordHasUseList.Add(_res);
        wordGoingUseList.Add(_res);
        RefreshNowList();

        if (_res.BaseType == typeof(AbstractItems))
        {
            if (!NwordCanUseTimes.ContainsKey(_res))
            {
                print("!GameMgr.instance.NwordCanUseTimes.ContainsKey(this)");
            }
            int _random = UnityEngine.Random.Range(0, NwordCanUseTimes[_res].Count);
            NwordCanUseTimes[_res][_random] -= 1;

            if (NwordCanUseTimes[_res][_random] <= 0)
            {
                DeleteCardList(_res);
            }

        }
        return _res;
    }


    
    public Type GetGoingUseList()
    {
        //全部解锁前，只有3个槽位
        for(int i = 0; i < 3; i++)
        {
            GetNowListOne();//待使用词库有5个词
        }        
        return wordGoingUseList[5];
    }
    public List<Type> GetAllList()
    {
        return wordList;
    }


    //获取已有的书本列表
    public List<BookNameEnum> GetBookList()
    {
        return bookList;
    }

   

    /// <summary>
    /// 检测是否一本书的所有词组都已经获取。返回是或否
    /// </summary>
    /// <param name="_book"></param>
    /// <returns></returns>
   public  bool IsBookWordAllGet(BookNameEnum _book)
    {
        if (bookAllGetList.Contains(_book)) return true;

        List<Type> _typeList = GetBookList(_book);
        if (_typeList == null)
        {
            print("_typeList == null"); return true;
        }


        foreach (var _w in _typeList)
        {
            if (!wordList.Contains(_w))
            {
                return false;
            }
        }
        bookAllGetList.Add(_book);
        return true;
    }


    public List<Type> GetBookList(BookNameEnum _book)
    {
        List<Type> _typeList = null;
        switch (_book)
        {
            case BookNameEnum.HongLouMeng:
                {
                    _typeList = AllSkills.hlmList_all;
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    _typeList = AllSkills.crystalList_all;
                }
                break;
            case BookNameEnum.Salome:
                {
                    _typeList = AllSkills.shaLeMeiList_all;
                }
                break;
            case BookNameEnum.ZooManual:
                {
                    _typeList = AllSkills.animalList_all;
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    _typeList = AllSkills.maYiDiGuoList_all;
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    _typeList = AllSkills.liuXingBXList_all;
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    _typeList = AllSkills.aiJiShenHuaList_all;
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    _typeList = AllSkills.humanList_all;
                }
                break;
            case BookNameEnum.allBooks:
                {
                    _typeList = AllSkills.commonList_all;
                }
                break;

        }
        return _typeList;
    }


    /// <summary>
    /// 随机获取书本中还未获取的词语
    /// </summary>
    /// <param name="_book"></param>
    public Type GetBookListNeedWordOne(BookNameEnum _book)
    {
        //已经没有没获得的词语了，返回null
        if (IsBookWordAllGet(_book)) return null;

        List<Type> _typeList = GetBookList(_book);
        if (_typeList == null)
        {
            print("_typeList == null");return null;
        }
        

        int _R = UnityEngine.Random.Range(0, _typeList.Count);

        while (wordList.Contains(_typeList[_R]))
        {
            _R = UnityEngine.Random.Range(0, _typeList.Count);

        }
        return _typeList[_R];
    }


    /// <summary>
    /// 刷新现有卡牌列表
    /// </summary>
    void RefreshNowList()
    {
        if (wordNowList.Count == 0)
        {
            wordNowList.AddRange(wordList);
            wordHasUseList.Clear();

            NwordCanUseTimes.Clear();
            foreach (var _card in NwordTimes)
            {
                if (NwordCanUseTimes.ContainsKey(_card.Key))
                {
                    NwordCanUseTimes[_card.Key].Clear();
                    NwordCanUseTimes[_card.Key].AddRange(_card.Value);

                }
                else
                {
                    NwordCanUseTimes.Add(_card.Key, _card.Value);
                }
            }
        }
    }



    public void SetCombatStartList(List<Type> _list)
    {
        bookList.Clear();
        wordList.Clear();
        wordList = _list;
    }
    public void AddCombatStartList(List<Type> _list)
    {

        foreach (var _i in _list)
        {

            if (!wordList.Contains(_i))
            {
                _i.GetType();
                int _r = 1;
                //var count = _i.GetField("rarity").GetValue(null);
                //if (count != null) _r = (int)count;

                //for (int i = 0; i < _r; i++)
                //{
                wordList.Add(_i);
                //}
            }

        }
    }

    //public void AddBookList(BookNameEnum _book)
    //{
    //    switch (_book)
    //    {
    //        case BookNameEnum.HongLouMeng:
    //            {
    //                if (!bookList.Contains(BookNameEnum.HongLouMeng))
    //                {
    //                    //AddCombatStartList(AllSkills.hlmList_all);
    //                    bookList.Add(BookNameEnum.HongLouMeng);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.CrystalEnergy:
    //            {
    //                if (!bookList.Contains(BookNameEnum.CrystalEnergy))
    //                {
    //                    //AddCombatStartList(AllSkills.crystalList_all);
    //                    bookList.Add(BookNameEnum.CrystalEnergy);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.Salome:
    //            {
    //                if (!bookList.Contains(BookNameEnum.Salome))
    //                {
    //                    //AddCombatStartList(AllSkills.shaLeMeiList_all);
    //                    bookList.Add(BookNameEnum.Salome);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.ZooManual:
    //            {
    //                if (!bookList.Contains(BookNameEnum.ZooManual))
    //                {
    //                    //AddCombatStartList(AllSkills.animalList_all);
    //                    bookList.Add(BookNameEnum.ZooManual);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.PHXTwist:
    //            {
    //                if (!bookList.Contains(BookNameEnum.PHXTwist))
    //                {
    //                    // AddCombatStartList(AllSkills.maYiDiGuoList_all);
    //                    bookList.Add(BookNameEnum.PHXTwist);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.FluStudy:
    //            {
    //                if (!bookList.Contains(BookNameEnum.FluStudy))
    //                {
    //                    //AddCombatStartList(AllSkills.liuXingBXList_all);
    //                    bookList.Add(BookNameEnum.FluStudy);
    //                }

    //            }
    //            break;
    //        case BookNameEnum.EgyptMyth:
    //            {
    //                if (!bookList.Contains(BookNameEnum.EgyptMyth))
    //                {
    //                    //AddCombatStartList(AllSkills.aiJiShenHuaList_all);
    //                    bookList.Add(BookNameEnum.EgyptMyth);
    //                }
    //            }
    //            break;
    //        case BookNameEnum.ElectronicGoal:
    //            {
    //                if (!bookList.Contains(BookNameEnum.ElectronicGoal))
    //                {
    //                    //AddCombatStartList(AllSkills.humanList_all);
    //                    bookList.Add(BookNameEnum.ElectronicGoal);
    //                }
    //            }
    //            break;
    //        case BookNameEnum.allBooks:
    //            {
    //                if (!bookList.Contains(BookNameEnum.allBooks))
    //                {
    //                    AddCombatStartList(AllSkills.commonList_all);
    //                    bookList.Add(BookNameEnum.allBooks);
    //                }

    //            }
    //            break;

    //    }
    //}
    //public bool HasBook(BookNameEnum _book)
    //{
    //    if (bookList.Contains(_book))
    //        return true;
    //    return false;
    //}
    #endregion


    #region 调用各种界面


    public int GetNextCreateChara()
    {
        return UiCanvas.GetComponentInChildren<CreateOneCharacter>().GetNextCreateChara();
    }
    public void CreateCharacterPut(int initCharacter)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f); 
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;
        if (UiCanvas != null)
            UiCanvas.SetActive(true);
        UiCanvas.GetComponentInChildren<CreateOneCharacter>().CreateNewCharacter(initCharacter);
    }
    public void CreateTheCharacterPut(int characterID)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;
        if (UiCanvas != null)
            UiCanvas.SetActive(true);
        UiCanvas.GetComponentInChildren<CreateOneCharacter>().CreateTheCharacter(characterID);
    }

    public void OpenEventUi()
    {
   
        draftUi.gameObject.SetActive(false);
        UiCanvas.gameObject.SetActive(false);
    }
    #endregion
}
