using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region 游戏阶段相关结构体

[System.Serializable]
public enum StageType
{
    fight_Pvp = 0,//战斗状态 - 两队互打
    fight_Pve_l = 1,//战斗状态 - 怪物打左
    fight_Pve_r = 2,//战斗状态 - 怪物打右
    fight_Pve_boss = 3,//战斗状态 - 打boss
    rest = 4,//休息状态
    other = 5,//新手状态等
}

[System.Serializable]
public class OneStageData
{
    [Header("阶段类型")]

     public StageType type;

    [Header("基本属性")]
    [Tooltip("持续时间")] public float time;
    [Tooltip("难度等级")] public int level;

    [Header("slider相关")]
    [Tooltip("对应图标")] public Sprite image;
    [Tooltip("图标缩放")] public float imageScale=1;
    [HideInInspector]public float time_count=1;

    [Header("开场动画（可空）")]
    [Tooltip("对应图标")] public string cg;

    [Header("【fight_Pve_boss】")]
    [Tooltip("boss 的预制体")] public GameObject t_boss;

    [Header("【rest】")]
    [Tooltip("生成事件总数量")] public int eventCount;
    [Tooltip("是否包含重要事件")] public bool eventIsKey;
    [Tooltip("事件出现延迟时间")] public float eventDelayTime = 5;
}

#endregion


public class GameProcessSlider : MonoBehaviour
{
    [Header("进度图标预制体（手动）")]
    public GameObject perfab_icon;

    private float time_all = 0;
    private float timeNow = 0;

    private bool countTime = false;//计时开关
    private Slider sliderProcess;

    private Vector3 oriScale;
    private AudioPlay audioPlay;
    private AudioSource audioSource;

    [Header("事件位置")] public GameObject eventPoint;
    [Header("事件气泡（希望-访客-意外-危机-交易-场景）")] public GameObject[] eventBubblePrefab;
    private Bubble[] EventBubble;
    public Bubble[] eventBubble
    {
        get
        {
            //获取全部角色
            EventBubble = eventPoint.GetComponentsInChildren<Bubble>();
            return EventBubble;
        }
    }

    private int eventCount = 0;
    private List<int> array = new List<int>();
    private List<GameObject> array0 = new List<GameObject>();
    private bool isCreate = false;

    

    bool hasWeiji = false;

    GameProcessUI gameprocessUI;


    public void WeiJiOpen()
    {
        StartCoroutine(Wait_Weiji());
        countTime = false;
    }


    private void CreateProcessSlider()
    {
        
        sliderProcess = this.GetComponent<Slider>();
        sliderProcess.value = 0;

        var _stage = GameMgr.instance.time_stage;

        for (int _i = 0; _i < _stage.stagesData.Count; _i++)
        { time_all += _stage.stagesData[_i].time; }
        sliderProcess.maxValue = time_all;

        float _timeAmount = 0;
        float _width = GetComponent<RectTransform>().sizeDelta.x;
        for (int _i = 0; _i < _stage.stagesData.Count; _i++)
        {

            _timeAmount += _stage.stagesData[_i].time;
            _stage.stagesData[_i].time_count = _timeAmount;
            if (_stage.stagesData[_i].image != null)
            {
                GameObject _icon = GameObject.Instantiate<GameObject>(perfab_icon);
                _icon.transform.SetParent(this.transform);
                _icon.GetComponent<RectTransform>().localPosition = Vector3.zero;
                _icon.GetComponent<RectTransform>().localScale = Vector3.one * _stage.stagesData[_i].imageScale;
                _icon.GetComponent<RectTransform>().localPosition += new Vector3(-_width / 2 + (_timeAmount / time_all) * _width, 0, 0);
                _icon.GetComponent<Image>().sprite = _stage.stagesData[_i].image;
            }
        }
        var handle = this.transform.Find("HandleSlideArea");
        handle.parent = this.transform.parent;
        handle.parent = this.transform;
        sliderProcess.maxValue = time_all;

        GameMgr.instance.EnterTheStage(0);

    }


    private void Start()
    {
        gameprocessUI = UIManager.GetInstance().GetPanel<GameProcessUI>("GameProcessUI");
           audioPlay = GameObject.Find("AudioSource").GetComponent<AudioPlay>();
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        oriScale = this.transform.localScale;

        //按照预先设置的时间生成进度条
        CreateProcessSlider();

        //关闭面板显示
        gameprocessUI.SwitchSettingList(false);

        gameprocessUI.SwitchWordInformatio(false);
    }

    private void FixedUpdate()
    {
        GameMgr.instance.time1 += Time.deltaTime;

        if (CharacterManager.instance.pause) return;
        sliderProcess.value = timeNow;

        GameMgr.instance.time2 += Time.deltaTime;

       
        
    }
    //private void FixedUpdate()
    //{
    //    GameMgr.instance.time1 += Time.deltaTime;

    //    if (CharacterManager.instance.pause) return;

    //    //CharacterManager.instance.EndGame();
    //    if (!countTime)//当countTime=false时进入if
    //    {
    //        if (CharacterManager.instance.GetStranger() == null)
    //        {
    //            if (hasWeiji)
    //            {
    //                countTime = true; hasWeiji = false;
    //                int _random = Random.Range(0, 6);
    //                switch (_random)
    //                {
    //                    case 3:
    //                    case 0://访客
    //                        { CreateFangke(false, 0); }
    //                        break;
    //                    case 4:
    //                    case 1://设定
    //                        {
    //                            CreateXiWang(false);
    //                        }
    //                        break;
    //                    case 5:
    //                    case 2://希望
    //                        { CreateSetting(false); }
    //                        break;
    //                }
    //            }


    //        }
    //        return;
    //    }


    //    //CreateEventUpdate();

    //    GameMgr.instance.time1 += Time.deltaTime;
    //    timeNow += Time.deltaTime;
    //    sliderProcess.value = timeNow;

    //    //如果超出
    //    if (stageCount >= time_stage.stagesData.Count)
    //    {
    //        Debug.LogWarning("time_stage overCount!");


    //        countTime = false;
    //        return;
    //    }

    //    //如果进入阶段
    //    if (timeNow > time_stage.stagesData[stageCount].time_count)
    //    {

    //        GameMgr.instance.SetStageTo(time_stage.stagesData[stageCount].level);

    //        if (time_stage.stagesData[stageCount].type == StageType.fight_Pve_boss)
    //        {

    //            //创建boss事件
    //            if (time_stage.stagesData[stageCount].t_boss != null)
    //            {
    //                print("CreateBossCreateBoss" + countTime);

    //                CreateBoss(time_stage.stagesData[stageCount].t_boss);
    //                countTime = false;
    //                //StartCoroutine(Wait_Weiji());
    //            }
    //            stageCount++;
    //        }
    //        else if (time_stage.stagesData[stageCount].type == StageType.rest)
    //        {

    //            countTime = true;
    //            //创建Event事件
    //            print("创建Event事件");
    //            CreateEvent(time_stage.stagesData[stageCount].level.eventKey, time_stage.stagesData[stageCount].level.eventCount
    //                , 0, 0);

    //            stageCount++;
    //        }
    //        else if (time_stage.stagesData[stageCount].type == StageType.fight_Pve_l)
    //        {
    //            //切换BGM-2
    //            audioPlay.Boss_GuaiWu();


    //            CreateWeijiEvent(false, 99);
    //            StartCoroutine(Wait_Weiji());
    //            countTime = false;
    //            stageCount++;

    //        }

    //        //demo的结算页面.临时写的，很草率
    //        //if (time_stage[stageCount].t_boss == null)
    //        //{
    //        //    CharacterManager.instance.EndGame();
    //        //}
    //        //else
    //        //{


    //        //}



    //    }
    //}


    IEnumerator Wait_Weiji()
    {
        yield return new WaitForSeconds(5);
        hasWeiji = true;
    }

    #region 生成事件
    //希望-访客-意外-危机-交易-场景
    public void CreateWeijiEvent(bool isKey,int monster)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[3], (a) =>
        {
            array0.Add(a);
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.WeiJi, false, monster);
          
        });
    }

    public void CreateFangke(bool isKey, int chara)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[1], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one*1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, chara );

        });
    }
    public void CreateXiWang(bool isKey)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[0], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.XiWang, false, 1);

        });
    }
    public void CreateSetting(bool _isleft)
    {

        string adr = "UI/Setting";
        var obj = ResMgr.GetInstance().Load<GameObject>(adr);
        obj.GetComponent<Setting>().InitSetting(settingUiType.Quality, _isleft);
        obj.transform.parent = GameObject.Find("CharacterCanvas").transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
    }
    public void CreateJiaoYi(bool isKey)
    { 
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[4], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.JiaoYi, false, 1);

        });
    }
    public void CreateChangJing(bool isKey)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[5], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.ChangJing, false, 1);

        });
    }
    public void CreateYiwai(bool isKey)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[2], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.YiWai, false, 1);

        });
    }
    #endregion


    #region boss

    /// <summary>
    /// 生成boss
    /// </summary>
    /// <param name="_boss"></param>
    void CreateBoss(GameObject _boss)
    {
        print("CreateBoss");

        this.transform.localScale = Vector3.zero;

        //生成boss
        GameObject boss = Instantiate(_boss);
        boss.transform.SetParent(GameObject.Find("Circle4.5").transform);
        boss.transform.localPosition = Vector3.zero;
        boss.transform.localScale = Vector3.one * GameMgr.instance.afterScale;
        //bossBGM
        audioPlay.Boss_HuaiYiZhuYi();
        //生成调整
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //设置一个随机目标，使其进入攻击状态
        IAttackRange attackRange = new SingleSelector();

        //这一句越级了
        AbstractCharacter[] a = attackRange.CaculateRange(100, boss.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all, false);
        boss.GetComponentInChildren<AI.MyState0>().aim.Add(a[Random.Range(0, a.Length)]);
    }
    public void BossDie()
    {
        //BGM
      //audioPlay.RandomPlay();

        GameMgr.instance.EndGame();
        this.transform.localScale = oriScale;
        //CreateBookCanvas();
    }

    #endregion

    #region 抽取书本
    /// <summary>
    ///进入抽取书本页面
    /// </summary>
    //void CreateBookCanvas()
    //{
    //    //镜头拉远
    //    Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
    //    Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
    //    //游戏暂停
    //    CharacterManager.instance.pause = true;
    //    //生成面板
    //    if (bookCanvas != null)
    //        bookCanvas.SetActive(true);
    //    if (characterCanvas != null)
    //        characterCanvas.SetActive(false);
    //}
    /// <summary>
    /// 在BookCanvas的确认按钮Onclick上调用
    /// </summary>



    /// <summary>
    /// 在createOneCharacter中执行
    /// </summary>
    public void ProcessStart()
    {
        countTime = true;
        CharacterManager.instance.pause = false;

       gameprocessUI.SwitchSettingList(true);
        gameprocessUI.SwitchWordInformatio(true);
    }

    #endregion

    /// <summary>
    /// 生成事件气泡
    /// </summary>
    private float totalTime = 0;

    /// <summary>
    /// 随机抽取并返回事件ID（希望0-访客1-意外2-危机3-交易4-场景5                
    /// </summary>
    /// <returns></returns>

    private Coroutine coroutineEvent = null;
    public void CreateEventUpdate(float _delayTime, bool isKey, int count)
    {

        if (coroutineEvent != null) { StopCoroutine(coroutineEvent);  }
            coroutineEvent =StartCoroutine(CreateEvent(_delayTime,isKey,count));
    }


    public void StopEventUpdate()
    {
        if (coroutineEvent == null)
            return;
       
        StopCoroutine(coroutineEvent);
        coroutineEvent = null;
    }


    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();
    /// <summary>
    /// 在场上随机生成事件气泡
    /// </summary>
    /// <param name="isKey">是否是关键事件</param>
    /// <param name="count">生成个数</param>
    IEnumerator CreateEvent(float _delayTime,bool isKey,int count)
    {
        float _timeCountr = 0;
        while (_timeCountr<=_delayTime)
        {
           
            yield return new WaitForSeconds(Time.deltaTime);
            if (!GameMgr.instance.pause) _timeCountr += Time.deltaTime;
           
        }
        

           array0.Clear();
            int _random = -1;
        //if (isKey)
        //{
        //    //检测当前的所有事件中，哪些事件是可以生成重要事件的，并且从中随机抽取一件
        //    _random = Random.Range(0, event_stage[eventCount].events);
        //}

        //生成事件气泡预制体
        for (int i = 0; i < count; i++)
        {
            int num0 = Random.Range(0, eventPoint.transform.childCount);
            int numx = GameMgr.instance.GetRandomEventType();
         
            if (i == _random)//是重要事件
            {
                int loop = 0;
                while ((!GameMgr.instance.HaveCanHappenKeyEvent(numx)) && (loop < 50))
                {
                    loop += 1;
                    numx = GameMgr.instance.GetRandomEventType();
                    if (loop > 48) print("死循环");
                }
            }
            

            //实例化事件气泡

            int _looppp = 0;
            while (((!EventPoint.isEvent[num0])||(array.Contains(num0)))&&(_looppp<50))//避免纸球位置
            {
                _looppp++;
                num0 = Random.Range(0, eventPoint.transform.childCount);
            }

            //实例化事件气泡               
            PoolMgr.GetInstance().GetObj(eventBubblePrefab[numx], (a) =>
            {
                array.Add(num0);
                array0.Add(a);
                a.transform.SetParent(eventPoint.transform.GetChild(num0).transform);
                a.transform.localPosition = Vector3.zero;
                a.GetComponent<Bubble>().dTime = Mathf.Infinity;
                if (i == _random)
                {
                    a.GetComponent<Bubble>().isKey = true;
                    a.GetComponent<Bubble>().dTime = Mathf.Infinity;
                }
            });
        }

        isCreate = true;
        eventCount++;
        array.Clear();
        coroutineEvent = null;
    }






    #region 进入不同阶段
    //判定是否结束阶段
    public void ChangeStageTrigger()
    {
        //根据不同的类型，有不同的判定条件


        //本阶段结束。进入下一个阶段
        GameMgr.instance.RecycleWordsInScene();
        GameMgr.instance.EnterTheStage(GameMgr.instance.stageCount+1);

    }




   


    #endregion
}
