using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




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

    #region 属性
    private Transform wallP;

    [Header("【单位：s】游戏进程相关参数（手动）")]
    public StagesData time_stage;
    [HideInInspector] public StageType nowStageType;

    [HideInInspector] public List<Type> settingL = new List<Type>();
    [HideInInspector] public List<Type> settingR = new List<Type>();
    private GameProcessUI gameProcessUI;
    public void AddTo(Type _add, bool _left)
    {
        if (_left)
        {
            settingL.Add(_add);
        }
        else
        {
            settingR.Add(_add);
        }
        gameProcessUI.RefreshSettingList();
    }

    //当前的场景
    [HideInInspector] public int levelSenceIndex = 0;
    //当前的剧本
    [HideInInspector] public BookNameEnum nowBook;

    //玩家已有的书库
    List<BookNameEnum> bookList = new List<BookNameEnum>();

    //玩家已有的，词语全部获取完毕的书库
    List<BookNameEnum> bookAllGetList = new List<BookNameEnum>();

    //战斗总牌库
    [HideInInspector] public List<Type> wordList = new List<Type>();

    //当前未使用的牌的牌库
    List<Type> wordNowList = new List<Type>();

    //当前待使用的牌的牌库
    [HideInInspector] public List<Type> wordGoingUseList = new List<Type>();

    //当前已使用的牌的牌库
    List<Type> wordHasUseList = new List<Type>();

    //名词的消耗性使用相关
    [HideInInspector] public Dictionary<Type, List<int>> NwordTimes = new Dictionary<Type, List<int>>();//目前的所有牌库中，名词和其使用次数。
    [HideInInspector] public Dictionary<Type, List<int>> NwordCanUseTimes = new Dictionary<Type, List<int>>();//目前待使用的牌库中，名词和其使用次数。
    [HideInInspector] public List<Type> outVerbs = new List<Type>();
    //骰子数量
    int diceNumber = 2;

    [Header("（策划）伤害参数")]
    public float attackAmount = 5;
    public void AddDice(int i)
    {
        diceNumber += i;
    }
    public int GetDice()
    {
        return diceNumber;
    }
    public void DeleteDice(int i)
    {
        diceNumber -= i;
        if (diceNumber < 0) diceNumber = 0;
    }



    //触发特殊文本
    [HideInInspector] public List<int> AttackHDList = new List<int>();
    [HideInInspector] public List<int> CureHDList = new List<int>();


    #region 事件
    //当前触发的所有事件
    [HideInInspector] public CustomList happenEvent = new CustomList();

    //当前可以触发的所有事件
    [HideInInspector] public List<eventExcelItem> canHappenData_nKey;
    [HideInInspector] public List<eventExcelItem> canHappenData_Key;
    [HideInInspector] public List<eventExcelItem> leftData;

    /// <summary>
    /// 读取excel表数据，加入所有没有前置条件的data
    /// </summary>
    /// <param name="_type"></param>
    void DealWithData()
    {
        canHappenData_nKey.Clear();
        canHappenData_Key.Clear();
        leftData.Clear();

        foreach (var _t in PoolConfigData.instance.so.data.items)
        {
            if ((_t.textTrigger == null) || (_t.textTrigger == ""))
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

    [Header("数值调整(手动)")]
    public float afterScale = 0.28f;
    public float beforeScale = 18;
    public float afterClickScale = 0.44f;
    public Vector3 charaPosOffset = new Vector3(0, 10, 0);
    public float cardRate_1 = 20;
    public float cardRate_2 = 30;
    public float cardRate_3 = 30;
    public float cardRate_4 = 20;


  
    public LevelController levelController;
    public bool eventHappen = false;

    [Header("开关(测试)")]
    public bool playEventCG = true;
    public bool DebugUi = false;
    private int stageIndex = 0;//游戏阶段
    [HideInInspector] public float time1 = 0;//实际游戏时间(含暂停）
    [HideInInspector] public float time2 = 0;//游戏时间（不含暂停）
    [HideInInspector] public float timeSpeed = 1;

    [Header("新增-游戏回合")]
    public int stageCount = 0;
    public StageType nowStage = StageType.other;


    [Header("新增-阵营血量")]

    //左边阵营的血量
    public float leftGroupMaxHp = 1;
    private float LeftGroupNowHp = 1;
    public float leftGroupNowHp
    {
        set
        {
            if (value < LeftGroupNowHp)
            {
                if (onGroupLostHp != null) onGroupLostHp(true);
            }
            LeftGroupNowHp = value;
            gameProcessUI.RefreshGroupHP();
            if (value <= 0) GameMgr.instance.EndGame();
        }
        get
        {
            return LeftGroupNowHp;
        }
    }
    //右边阵营的血量
    public float rightGroupMaxHp = 1;
    private float RightGroupNowHp = 1;
    public float rightGroupNowHp
    {
        set
        {
            if (value < RightGroupNowHp)
            {
                if (onGroupLostHp != null) onGroupLostHp(false);
            }
            RightGroupNowHp = value;
            gameProcessUI.RefreshGroupHP();
            if (value <= 0) GameMgr.instance.EndGame();
        }
        get
        {
            return RightGroupNowHp;
        }
    }

    public delegate void OnGroupLostHp(bool isleft);
    public event OnGroupLostHp onGroupLostHp;



    public bool pause
    {
        set
        {
            CharacterManager.instance.pause = value;
        }
        get 
        {
            return CharacterManager.instance.pause;
        }
    }

    #endregion



    override public void Awake()
    {
        base.Awake();
        DealWithData();

        wallP = GameObject.Find("wallCol").transform;
        //draftUi.InitContent();

        //界面设置
        OpenCharacterPutting();


        //牌库
        InitCardList();
        Time.timeScale = 1;
        CharacterManager.instance.pause = false;

    }


    private void Update()
    {
        time1 += Time.deltaTime;
        if (!CharacterManager.instance.pause) { time2 += Time.deltaTime; }

        //退出菜单
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.GetInstance().ShowPanel<ExitPanel>("ExitPanel", E_UI_Layer.System, (obj) =>
             {
                 Time.timeScale = 0f;
             });
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!DebugUi) return;
            UIManager.GetInstance().ShowPanel<DebugUi>("DebugUi", E_UI_Layer.System, (obj) =>
            {
            });
        }

    }

    #region 新 牌库
    /// <summary>
    /// 通用卡组
    /// </summary>
    void StartCardList()
    {
        //测试
        AddCardList(new HunHe().GetType());
        AddCardList(new HunHe().GetType());
        AddCardList(new HunHe().GetType()); AddCardList(new HunHe().GetType()); AddCardList(new HunHe().GetType());

        return;
        ////

        AddCardList(new FuTouAxe().GetType());
        AddCardList(new QiGuaiShiXiang().GetType()); 
        AddCardList(new HeartBroken().GetType());
        AddCardList(new FengLi().GetType());
        AddCardList(new QuicklyGrowing().GetType());
        AddCardList(new LuoYingBinFen().GetType());
        AddCardList(new JianRuPanShi().GetType());
  
        RefreshNowList();
    }

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
        print("牌库初始化完成" + wordList.Count + wordGoingUseList.Count);
    }





    /// <summary>
    /// 往卡组里加入卡牌
    /// </summary>
    /// <param name="_word"></param>
    public void AddCardList(Type _word)
    {
        
        wordList.Add(_word);
        wordNowList.Add(_word);
    

        if (_word.BaseType == typeof(AbstractItems))
        {
            //如果输入的词语是名词，则往可用名词列表中加入词语与其次数
            if (NwordTimes.ContainsKey(_word))
            {
                NwordTimes[_word].Add((int)_word.GetField("s_useTimes").GetValue(null));
            }
            else
            {
                NwordTimes.Add(_word, new List<int> { (int)_word.GetField("s_useTimes").GetValue(null) });
                //print("[NwordTimes]Add:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
            }

            if (NwordCanUseTimes.ContainsKey(_word))
            {
                NwordCanUseTimes[_word].Add((int)_word.GetField("s_useTimes").GetValue(null));
                //print("[NwordCanUseTimes]Add:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
            }
            else
            {
                NwordCanUseTimes.Add(_word, new List<int> { (int)_word.GetField("s_useTimes").GetValue(null) });
                // print("[NwordCanUseTimes]AddNew:" + (_word as AbstractItems).wordName + (int)Type.GetType(_word.GetType().Name).GetField("s_useTimes").GetValue(null));
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
            //随机删掉一张使用次数还有的词语
            if (_word is AbstractItems)
            {
                if (NwordTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    //NwordTimes[(_word as AbstractItems).GetType()].Remove(0);
                     NwordTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                    if (NwordTimes[(_word as AbstractItems).GetType()].Count <= 0)
                    {
                        NwordTimes.Remove((_word as AbstractItems).GetType());
                        
                    }
                }
                if (NwordCanUseTimes.ContainsKey((_word as AbstractItems).GetType()))
                {
                    //NwordCanUseTimes[(_word as AbstractItems).GetType()].Remove(0);
                    NwordCanUseTimes[(_word as AbstractItems).GetType()].RemoveAll(item => item == 0);
                    if (NwordCanUseTimes[(_word as AbstractItems).GetType()].Count <= 0)
                    {
                        NwordCanUseTimes.Remove((_word as AbstractItems).GetType());
                       
                    }
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
            bool not = false;
            int i = 0;
            while ( (wordGoingUseList[i] != _word.GetType()) && (i < wordGoingUseList.Count))
            {
                i++;
            }
            if (wordGoingUseList[i] == _word.GetType())
            {
                wordGoingUseList[i]=GetNowListOne();
            }
            
        }
        RefreshNowList();
    }

    public void DeleteCardList(Type _word)
    {
        if (_word == null) print("null");

        if (wordList.Contains(_word))
        {

            wordList.Remove(_word);
            if (_word.BaseType == typeof(AbstractItems))
            {

                if (NwordTimes.ContainsKey(_word))
                {
                    //NwordTimes[_word].Remove(0);
                    NwordTimes[_word].RemoveAll(item => item == 0);
                    if (NwordTimes[_word].Count <= 0)
                    {
                        NwordTimes.Remove(_word);
                        
                    }
                }
                if (NwordCanUseTimes.ContainsKey(_word))
                {
                    //NwordCanUseTimes[_word].Remove(0);
                    NwordCanUseTimes[_word].RemoveAll(item => item == 0);
                    if (NwordCanUseTimes[_word].Count <= 0)
                    {
                        NwordCanUseTimes.Remove(_word);
                      
                    }
                }
            }
        }


        if (wordNowList.Contains(_word))
        {
            wordNowList.Remove(_word);
    
        }
        else if (wordHasUseList.Contains(_word))
        {
            wordHasUseList.Remove(_word);
       
        }
        else if (wordGoingUseList.Contains(_word))
        {
            int i = 0;
            while ((wordGoingUseList[i] != _word.GetType())&&(i<wordGoingUseList.Count-1))
            {
                i++;
                if (i >= wordGoingUseList.Count-1) break;
            }
            if (wordGoingUseList[i] == _word.GetType())
            {
                wordGoingUseList[i] = GetNowListOne();
            }
        }

        RefreshNowList();
    }

    public void UseCard(Type _word)
    {
        if (_word.BaseType == typeof(AbstractItems))
        {

            if (NwordCanUseTimes.ContainsKey(_word))
            {

                int _r = UnityEngine.Random.Range(0, NwordCanUseTimes[_word].Count);

                print("times:" + NwordCanUseTimes[_word][_r]);
                if (NwordCanUseTimes[_word][_r]>0)
                    NwordCanUseTimes[_word][_r] -= 1;
                if (NwordCanUseTimes[_word][_r] <= 0)
                {
                    DeleteCardList(_word);
                    //NwordCanUseTimes[_word].Remove(_r);
                    //if (NwordCanUseTimes[_word].Count <= 0)
                    //{
                    //    NwordCanUseTimes.Remove(_word);
                    //    DeleteCardList(_word);
                    //}
                }
                  
                RefreshNowList();
                
            }
            
        }
        if (_word.BaseType == typeof(AbstractVerbs))
        {
            DeleteCardList(_word);
            outVerbs.Add(_word);
        }
    }


    public void DetectVerb(Type _word)
    {
        if (outVerbs.Contains(_word))
        {
            AddCardList(_word);
            outVerbs.Remove(_word);
        }
    }
    /// <summary>
    /// 返回已经使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetHasUsedList()
    {
        //wordHasUseList.OrderBy(it => it.Name).ToList();
        return wordHasUseList;
    }
    /// <summary>
    /// 返回未使用的词条们
    /// </summary>
    /// <returns></returns>
    public List<Type> GetNowList()
    {
        //wordNowList.OrderBy(it => it.Name).ToList();
        return wordNowList;
    }
    public List<Type> GetGoingToUseList()
    {
        //wordNowList.OrderBy(it => it.Name).ToList();
        return wordGoingUseList;
    }
    /// <summary>
    /// 随机返回一个未使用的词条
    /// </summary>
    /// <returns></returns>
    public Type GetNowListOne()
    {
        
        RefreshNowList();

        if (wordNowList.Count == 0)
        {
            return null;
        }

        int count = UnityEngine.Random.Range(0, wordNowList.Count);
        
      
        // print("卡组里没有卡牌+" + count);
        var _res = wordNowList[count];
        wordNowList.Remove(_res);
      
        //wordHasUseList.Add(_res);

        //if (_res.BaseType == typeof(AbstractItems))
        //{
        //    if (!NwordCanUseTimes.ContainsKey(_res))
        //    {
        //        print("!GameMgr.instance.NwordCanUseTimes.ContainsKey(this)");
        //    }
        //    int _random = UnityEngine.Random.Range(0, NwordCanUseTimes[_res].Count);
        //    NwordCanUseTimes[_res][_random] -= 1;

        //    if (NwordCanUseTimes[_res][_random] <= 0)
        //    {
        //        DeleteCardList(_res);
        //    }

        //}
        return _res;
    }



    public Type GetGoingUseList()
    {

       // RefreshNowList();
        //全部解锁前，只有3个槽位
        for (int i = 0; i < 3; i++)
        {
           
            wordGoingUseList.Add(GetNowListOne());
         

        }
        PutCharacter.firstUseCardlist = false ;
        //wordHasUseList.Add(wordGoingUseList[0]);

        //print(wordGoingUseList[0].)
        //if (wordGoingUseList[0].IsAssignableFrom(typeof(AbstractVerbs)))
        //{
        //    print("动词1");
        //}

        return wordGoingUseList[0];
    }
    public Type GetGoingUseListOne()
    {
        // RefreshNowList();
        wordGoingUseList.Add(GetNowListOne());
        //wordHasUseList.Add(wordGoingUseList[0]);

        return wordGoingUseList[0];
    }
    public List<Type> GetAllList()
    {
        return wordList;
    }

    /// <summary>
    /// 刷新现有卡牌列表
    /// </summary>
    public void RefreshNowList()
    {
        wordHasUseList.Clear();
        foreach (var _c in wordList)
        {
            if ((!wordNowList.Contains(_c)) && (!wordGoingUseList.Contains(_c)))
            {
                wordHasUseList.Add(_c);
            }
        }
        if ((wordNowList.Count == 0) )
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

    #region 书本
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
    public bool IsBookWordAllGet(BookNameEnum _book)
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
            print("_typeList == null"); return null;
        }


        int _R = UnityEngine.Random.Range(0, _typeList.Count);

        while (wordList.Contains(_typeList[_R]))
        {
            _R = UnityEngine.Random.Range(0, _typeList.Count);

        }
        return _typeList[_R];
    }

    #endregion



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


    #region 卡牌稀有度&&游戏阶段

  
    public int GetStage()
    {
        return stageIndex;
    }
    public void SetStageTo(cardRareExcelItem i)
    {
        stageIndex = i.stage;
        bool b = SetRareTo(i);
        if (!b) print("stageIndex超出游戏设定");
    }

    private bool SetRareTo(cardRareExcelItem _stage)
    {
        if (_stage==null)
            return false;

        //这里默认数据顺序和表格一样。如果出错了，加上index检测
        cardRate_1 = _stage.rate1;
        cardRate_2 = _stage.rate2;
        cardRate_3 = _stage.rate3;
        cardRate_4 = _stage.rate4;

        return true;
    }


    #endregion

    
    #region level
    public void ChangeLevelTo(int start)
    {

        levelController.SetLevelTo(start);
    }
    #endregion

    #region 事件气泡
    /// <summary>
    /// 显示事件的飘字
    /// </summary>
    public void PopupEvent(Vector3 pos, string name, string info)
    {
        var obj=ResMgr.GetInstance().Load<GameObject>("UI/popEvent"); 
        //世界坐标转画布坐标
       Vector2 canvasSize = UIManager.GetInstance().canvas.GetComponent<RectTransform>().sizeDelta;
        Vector3 viewPortPos3d = Camera.main.WorldToViewportPoint(pos);
        Vector2 viewPortRelative = new Vector2(viewPortPos3d.x - 0.5f, viewPortPos3d.y - 0.5f);
        Vector2 cubeScreenPos = new Vector2(viewPortRelative.x * canvasSize.x, viewPortRelative.y );
        obj.GetComponent<RectTransform>().position = pos;
        obj.transform.parent = UIManager.GetInstance().canvas.transform;
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
        obj.transform.Find("Image").GetComponent<Animator>().Play("burst");
        while (i< 1f)
        {
            i += 0.02f;
            
            text.gameObject.SetActive(false);
            text.text = text1;
            yield return waitFrame;
        }

        obj.transform.Find("Image").GetComponent<Animator>().Play("card");
        //移动到草稿本
        while (Mathf.Abs((obj.anchoredPosition- pos).x)>2f)
        {
            
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            obj.anchoredPosition -= 0.04f * (obj.anchoredPosition - pos);
            yield return waitFrame;
        }

        //停留在草稿本
        i = 0;
        text.text = "";
        int _index = 0;
        float textDelay = 0.06f;
        float textDAll = 0;
        obj.transform.Find("Image").GetComponent<Animator>().Play("burst");
        while (i < 1f)
        {
            i += 0.02f;
            
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


   


    public void WallSwitch(bool _open)
    {
        if (wallP == null) return;
        if (_open)
        {
            wallP.gameObject.SetActive(true); 
        }
        else
        {
            wallP.gameObject.SetActive(false);
        }
    }
    public int GetNextCreateChara()
    {
        return CharacterManager.instance.AddToPutCharasList(-1).charaID;
    }
    public int GetNextCreateChara(int _sds)
    {
        return CharacterManager.instance.AddToPutCharasList(_sds).charaID;
    }
    public void CreateCharacterPut(int initCharacter)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(1);
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;
        

        for (int _count = 0; _count < initCharacter; _count++)
        {
            CharacterManager.instance.AddToPutCharasList(-1);
        }

        UIManager.GetInstance().ShowPanel<PutCharacter>("", E_UI_Layer.Mid, (obj) =>
          {
              obj.CreateTheCharacter();
          });
        
    }
    public void CreateTheCharacterPut(int characterID)
    {
        //镜头拉远
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(1);
        //生成面板  进入放角色页面
        CharacterManager.instance.pause = true;

        CharacterManager.instance.AddToPutCharasList(characterID);

        UIManager.GetInstance().ShowPanel<PutCharacter>("", E_UI_Layer.Mid, (obj) =>
        {
            obj.CreateTheCharacter();
        });
    }


    #region 调用各种界面
    /// <summary>
    /// 打开角色放置页面
    /// </summary>
    void OpenCharacterPutting()
    {

        UIManager.GetInstance().HidePanel("DraftBook");
        UIManager.GetInstance().HidePanel("GameProcessUI");
        UIManager.GetInstance().HidePanel("CardRes");

        UIManager.GetInstance().ShowPanel<PutCharacter>("PutCharacter", E_UI_Layer.Mid, (obj) =>
        {
        });
    }

    /// <summary>
    /// 打开事件页面。暂时只做其它页面的隐藏处理
    /// </summary>
    public void OpenEventUi()
    {
        UIManager.GetInstance().HidePanel("CardRes");
        UIManager.GetInstance().HidePanel("DraftBook");
        UIManager.GetInstance().HidePanel("PutCharacter");

    }
    /// <summary>
    /// 打开角色放置页面
    /// </summary>
    public void ShowGameUI(bool _display)
    {
        UIManager.GetInstance().HidePanel("DraftBook");
        UIManager.GetInstance().HidePanel("PutCharacter");
        UIManager.GetInstance().HidePanel("CardRes");
        if (_display)
        {
            UIManager.GetInstance().ShowPanel<GameProcessUI>("GameProcessUI", E_UI_Layer.Mid, (obj) =>
            {
            });
            return;
        }
        UIManager.GetInstance().HidePanel("GameProcessUI");
       
    }

  
    #endregion


    #region cg
    WaitForFixedUpdate wait= new WaitForFixedUpdate();
    public void PlayCG(string name, float delayTime)
    {
        if (playEventCG == false) return;
        if (delayTime > 0)
        {
            StartCoroutine(WaitAndCg(name, delayTime));
        }
        else

        { 
            UIManager.GetInstance().ShowPanel<EventCg>("EventCg", E_UI_Layer.Mid, (obj) =>
                {
                    obj.PlayEventCG(name);
                }); }
    }
    IEnumerator WaitAndCg(string name,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //播放开场动画
        UIManager.GetInstance().ShowPanel<EventCg>("EventCg", E_UI_Layer.Mid, (obj) =>
        {
            obj.PlayEventCG(name);
        });
    
    }

    #endregion


    #region
    /// <summary>
    /// 回收场上的所有纸条
    /// </summary>
    public void RecycleWordsInScene()
    {
        //清除场上的纸条
        //纸条回到词库
    }




    //删除场上的所有事件
    public void DeleteAllEventsInScene()
    {
        foreach (var _eventBubble in UIManager.GetInstance().GetPanel<GameProcessUI>("GameProcessUI").gameProcessSlider.eventBubble)
        {
            Destroy(_eventBubble.gameObject);
        }
    }

    public void DeleteAllMonsterInScene()
    {
        foreach (var _monster in CharacterManager.instance.charas)
        {
            if (_monster.Camp == CampEnum.stranger)
                Destroy(_monster.gameObject);
        }
    }


    public void AllCharacterRelife()
    {
        foreach (var _deadChara in CharacterManager.instance.deadChara)
        {
            if (_deadChara.Camp == CampEnum.stranger) return;
            _deadChara.hp = _deadChara.maxHp;
            _deadChara.myState.ChangeActiveState(AI.StateID.idle);
            CharacterManager.instance.deadChara.Remove(_deadChara);
        }
    }



/// <summary>
/// 随机抽取一种类型的事件
/// </summary>
/// <returns></returns>
    public int GetRandomEventType()
    {
        int result = -1;

        //概率抽取
        int max = time_stage.xiWang + time_stage.fangKe + time_stage.yiWai + time_stage.jiaoYi + time_stage.changJing;
        int numx = UnityEngine.Random.Range(1, max);
        if (numx <= time_stage.xiWang) { result = 0; }
        else if (numx > time_stage.xiWang && numx < time_stage.xiWang + time_stage.fangKe) result = 1;
        else if (numx > time_stage.xiWang + time_stage.fangKe && numx < time_stage.xiWang + time_stage.fangKe + time_stage.yiWai) result = 2;
        else if (numx > time_stage.xiWang + time_stage.fangKe + time_stage.yiWai && numx < time_stage.xiWang + time_stage.fangKe + time_stage.yiWai+ time_stage.jiaoYi) result = 3;
        else if (numx > time_stage.xiWang + time_stage.fangKe + time_stage.yiWai+ time_stage.jiaoYi && numx < time_stage.jiaoYi) result = 4;

        return result;
    }
    #endregion


    #region 游戏阶段
    
    public void CreateMonster(int id)
    {
        int _index = -1;
        for (int ttt = 0; (ttt < PoolConfigData.instance.so.monsterDate.items.Length)&&(_index!=-1);ttt++)
        {
            if ((PoolConfigData.instance.so.monsterDate.items[ttt].Mid == id)&&(PoolConfigData.instance.so.monsterDate.items[ttt].name==stageIndex))
            {
                _index = ttt;
            }
        }

        if (_index == -1) return;

        var _data = PoolConfigData.instance.so.monsterDate.items[_index];
        int _id = id - 110;
        var _monster = Instantiate<GameObject>(UIManager.GetInstance().GetPanel<PutCharacter>("PutCharacter").monsterPrefabs[_id]);
        var _mAc = _monster.GetComponent<AbstractCharacter>();
        _mAc.Camp = CampEnum.stranger;
        _mAc.maxHp = _data.hp; 
        _mAc.hp = _mAc.maxHp;
        _mAc.def = _data.def;
        _mAc.atk = _data.atk;
        _mAc.psy = _data.psy;
        _mAc.san = _data.san;
        //放置在合适的位置

        //增加自带技能
        if ((_data.word1 != "") & (_data.word1 != null))
        {
            Type type = Type.GetType(_data.word1);
            if (type != null)
            {
                _monster.AddComponent(type);
            }
        }
    }

    public void EndGame()
    {
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(1);
        UIManager.GetInstance().ShowPanel<EndGame>("EndGame", E_UI_Layer.Top, (obj) => { });
        CharacterManager.instance.pause = true;
    }

    //进入下一个阶段，切换对应UI、事件、角色状态
    public void EnterTheStage(int _stage)
    {
        if (_stage >= time_stage.stagesData.Count)
        {
            Debug.LogError("stageCount wrong"); return;
        }

        stageCount = _stage;

        var OneStageData = time_stage.stagesData[stageCount];
        nowStageType = OneStageData.type;
        switch (OneStageData.type)
        {
            case StageType.fight_Pvp:
                { 
                    EnterStage_Fight(StageType.fight_Pvp); 
                } 
                break;
            case StageType.fight_Pve_r:
                {
                    EnterStage_Fight(StageType.fight_Pve_r);
                } 
                break;
            case StageType.fight_Pve_l: 
                {
                    EnterStage_Fight(StageType.fight_Pve_l);
                } 
                break;
            case StageType.fight_Pve_boss: 
                {
                    EnterStage_Fight(StageType.fight_Pve_boss);
                } 
                break;
            case StageType.rest: 
                {
                    EnterStage_Rest();
                } break;
            case StageType.other: { } break;
        }

        //回收纸条
        RecycleWordsInScene();

        //清除上一回合的怪物
        DeleteAllEventsInScene();

        //清楚上一回合的事件
        DeleteAllMonsterInScene();

        //所有死亡角色回血且复活
        AllCharacterRelife();
    }



    private void EnterStage_Rest()
    {
        //进入休息状态：工具界面;刷新事件；所有角色不战斗；
        UIManager.GetInstance().ShowPanel<RestUI>("RestUI", E_UI_Layer.Top, (obj) => { });

        //所有角色脱离战斗状态
        foreach (var _chara in CharacterManager.instance.charas)
        {
            _chara.myState.ChangeActiveState(AI.StateID.idle);
        }

        //激活事件生成
        UIManager.GetInstance().GetPanel<GameProcessUI>("GameProcessUI").gameProcessSlider
        .CreateEventUpdate(time_stage.stagesData[stageCount].eventDelayTime,
            time_stage.stagesData[stageCount].eventIsKey, time_stage.stagesData[stageCount].eventCount);
    }

    
    private void EnterStage_Fight(StageType _type)
    {

        //进入战斗状态：隐藏工具界面;不刷新事件;所有角色战斗
        UIManager.GetInstance().HidePanel("RestUI");

        foreach (var _chara in CharacterManager.instance.charas)
        {
            _chara.myState.ChangeActiveState(AI.StateID.attack);
        }

        UIManager.GetInstance().GetPanel<GameProcessUI>("GameProcessUI").gameProcessSlider.StopEventUpdate();

        //显示战斗环节的开始面板
       UIManager.GetInstance().ShowPanel<StageUIpvp>("stage_pvp",E_UI_Layer.Mid,(_obj)=> { 
        });
    }

    public void GroupLose(CampEnum _camp)
    {
        //扣血
        switch (_camp)
        {
            case CampEnum.left: { LeftGroupNowHp -= PoolConfigData.instance.so.cardRareDate.items[time_stage.stagesData[stageCount].level].LoseHp; }break;
            case CampEnum.right: { RightGroupNowHp -= PoolConfigData.instance.so.cardRareDate.items[time_stage.stagesData[stageCount].level].LoseHp; } break;
        }

        //进入下一回合
        EnterTheStage(stageCount++);
    }
    #endregion
}
