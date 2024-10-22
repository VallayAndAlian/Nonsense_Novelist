using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region ��Ϸ�׶���ؽṹ��

[System.Serializable]
public enum StageType
{
    fight_Pvp = 0,//ս��״̬ - ���ӻ���
    fight_Pve_l = 1,//ս��״̬ - �������
    fight_Pve_r = 2,//ս��״̬ - �������
    fight_Pve_boss = 3,//ս��״̬ - ��boss
    rest = 4,//��Ϣ״̬
    other = 5,//����״̬��
}

[System.Serializable]
public class OneStageData
{
    [Header("�׶�����")]

     public StageType type;

    [Header("��������")]
    [Tooltip("����ʱ��")] public float time;
    [Tooltip("�Ѷȵȼ�")] public int level;

    [Header("slider���")]
    [Tooltip("��Ӧͼ��")] public Sprite image;
    [Tooltip("ͼ������")] public float imageScale=1;
    [HideInInspector]public float time_count=1;

    [Header("�����������ɿգ�")]
    [Tooltip("��Ӧͼ��")] public string cg;

    [Header("��fight_Pve_boss��")]
    [Tooltip("boss ��Ԥ����")] public GameObject t_boss;

    [Header("��rest��")]
    [Tooltip("�����¼�������")] public int eventCount;
    [Tooltip("�Ƿ������Ҫ�¼�")] public bool eventIsKey;
    [Tooltip("�¼������ӳ�ʱ��")] public float eventDelayTime = 5;
}

#endregion


public class GameProcessSlider : MonoBehaviour
{
    [Header("����ͼ��Ԥ���壨�ֶ���")]
    public GameObject perfab_icon;

    private float time_all = 0;
    private float timeNow = 0;

    private bool countTime = false;//��ʱ����
    private Slider sliderProcess;

    private Vector3 oriScale;
    private AudioPlay audioPlay;
    private AudioSource audioSource;

    [Header("�¼�λ��")] public GameObject eventPoint;
    [Header("�¼����ݣ�ϣ��-�ÿ�-����-Σ��-����-������")] public GameObject[] eventBubblePrefab;
    private Bubble[] EventBubble;
    public Bubble[] eventBubble
    {
        get
        {
            //��ȡȫ����ɫ
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

        //����Ԥ�����õ�ʱ�����ɽ�����
        CreateProcessSlider();

        //�ر������ʾ
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
    //    if (!countTime)//��countTime=falseʱ����if
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
    //                    case 0://�ÿ�
    //                        { CreateFangke(false, 0); }
    //                        break;
    //                    case 4:
    //                    case 1://�趨
    //                        {
    //                            CreateXiWang(false);
    //                        }
    //                        break;
    //                    case 5:
    //                    case 2://ϣ��
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

    //    //�������
    //    if (stageCount >= time_stage.stagesData.Count)
    //    {
    //        Debug.LogWarning("time_stage overCount!");


    //        countTime = false;
    //        return;
    //    }

    //    //�������׶�
    //    if (timeNow > time_stage.stagesData[stageCount].time_count)
    //    {

    //        GameMgr.instance.SetStageTo(time_stage.stagesData[stageCount].level);

    //        if (time_stage.stagesData[stageCount].type == StageType.fight_Pve_boss)
    //        {

    //            //����boss�¼�
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
    //            //����Event�¼�
    //            print("����Event�¼�");
    //            CreateEvent(time_stage.stagesData[stageCount].level.eventKey, time_stage.stagesData[stageCount].level.eventCount
    //                , 0, 0);

    //            stageCount++;
    //        }
    //        else if (time_stage.stagesData[stageCount].type == StageType.fight_Pve_l)
    //        {
    //            //�л�BGM-2
    //            audioPlay.Boss_GuaiWu();


    //            CreateWeijiEvent(false, 99);
    //            StartCoroutine(Wait_Weiji());
    //            countTime = false;
    //            stageCount++;

    //        }

    //        //demo�Ľ���ҳ��.��ʱд�ģ��ܲ���
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

    #region �����¼�
    //ϣ��-�ÿ�-����-Σ��-����-����
    public void CreateWeijiEvent(bool isKey,int monster)
    {
        //�����̶�Σ���¼�
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
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[1], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one*1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, chara );

        });
    }
    public void CreateXiWang(bool isKey)
    {
        //�����̶�Σ���¼�
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
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[4], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.JiaoYi, false, 1);

        });
    }
    public void CreateChangJing(bool isKey)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[5], (a) =>
        {
            a.transform.SetParent(eventPoint.transform.GetChild(0));
            a.transform.localPosition = Vector3.one * 1010;

            a.GetComponent<Bubble>().StartEventBefore(EventType.ChangJing, false, 1);

        });
    }
    public void CreateYiwai(bool isKey)
    {
        //�����̶�Σ���¼�
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
    /// ����boss
    /// </summary>
    /// <param name="_boss"></param>
    void CreateBoss(GameObject _boss)
    {
        print("CreateBoss");

        this.transform.localScale = Vector3.zero;

        //����boss
        GameObject boss = Instantiate(_boss);
        boss.transform.SetParent(GameObject.Find("Circle4.5").transform);
        boss.transform.localPosition = Vector3.zero;
        boss.transform.localScale = Vector3.one * GameMgr.instance.afterScale;
        //bossBGM
        audioPlay.Boss_HuaiYiZhuYi();
        //���ɵ���
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //����һ�����Ŀ�꣬ʹ����빥��״̬
        IAttackRange attackRange = new SingleSelector();

        //��һ��Խ����
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

    #region ��ȡ�鱾
    /// <summary>
    ///�����ȡ�鱾ҳ��
    /// </summary>
    //void CreateBookCanvas()
    //{
    //    //��ͷ��Զ
    //    Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
    //    Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
    //    //��Ϸ��ͣ
    //    CharacterManager.instance.pause = true;
    //    //�������
    //    if (bookCanvas != null)
    //        bookCanvas.SetActive(true);
    //    if (characterCanvas != null)
    //        characterCanvas.SetActive(false);
    //}
    /// <summary>
    /// ��BookCanvas��ȷ�ϰ�ťOnclick�ϵ���
    /// </summary>



    /// <summary>
    /// ��createOneCharacter��ִ��
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
    /// �����¼�����
    /// </summary>
    private float totalTime = 0;

    /// <summary>
    /// �����ȡ�������¼�ID��ϣ��0-�ÿ�1-����2-Σ��3-����4-����5                
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
    /// �ڳ�����������¼�����
    /// </summary>
    /// <param name="isKey">�Ƿ��ǹؼ��¼�</param>
    /// <param name="count">���ɸ���</param>
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
        //    //��⵱ǰ�������¼��У���Щ�¼��ǿ���������Ҫ�¼��ģ����Ҵ��������ȡһ��
        //    _random = Random.Range(0, event_stage[eventCount].events);
        //}

        //�����¼�����Ԥ����
        for (int i = 0; i < count; i++)
        {
            int num0 = Random.Range(0, eventPoint.transform.childCount);
            int numx = GameMgr.instance.GetRandomEventType();
         
            if (i == _random)//����Ҫ�¼�
            {
                int loop = 0;
                while ((!GameMgr.instance.HaveCanHappenKeyEvent(numx)) && (loop < 50))
                {
                    loop += 1;
                    numx = GameMgr.instance.GetRandomEventType();
                    if (loop > 48) print("��ѭ��");
                }
            }
            

            //ʵ�����¼�����

            int _looppp = 0;
            while (((!EventPoint.isEvent[num0])||(array.Contains(num0)))&&(_looppp<50))//����ֽ��λ��
            {
                _looppp++;
                num0 = Random.Range(0, eventPoint.transform.childCount);
            }

            //ʵ�����¼�����               
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






    #region ���벻ͬ�׶�
    //�ж��Ƿ�����׶�
    public void ChangeStageTrigger()
    {
        //���ݲ�ͬ�����ͣ��в�ͬ���ж�����


        //���׶ν�����������һ���׶�
        GameMgr.instance.RecycleWordsInScene();
        GameMgr.instance.EnterTheStage(GameMgr.instance.stageCount+1);

    }




   


    #endregion
}
