using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region �ṹ��

[System.Serializable]
public enum StageType
{
    boss, weiji, Event
}

[System.Serializable]
public class OneStageData
{
    [Header("�׶�����")]
    public StageType type;
    [Header("��boss���͡�")]
    public GameObject t_boss;//boss ��Ԥ����
    [Header("���¼����͡�")]
    public int t_eventCount;//boss ��Ԥ����
    public bool t_eventKey;//boss ��Ԥ����
    public float t_eventtime;//boss ��Ԥ����
    public float t_eventtime_key;//boss ��Ԥ����

    [Header("��ͨ����")]
    public float time;//����ʱ��
    public int level;

    [Header("sliderͼ��")]
    public Sprite image;//��Ӧͼ��
    public float imageScale;//����ʱ��
    [HideInInspector] public float time_count;//�ۼ�ʱ��   

}

#endregion


[System.Serializable]
public struct Event_Stage
{
    //�¼�����
    public int events;
}


public class GameProcessSlider : MonoBehaviour
{
    [Header("����ͼ��Ԥ���壨�ֶ���")]
    public GameObject perfab_icon;

    [Header("����λ��s����Ϸ������ز������ֶ���")]
    public StagesData time_stage;
    [Header("[����]ÿ�ֵ��¼�����")]
    public Event_Stage[] event_stage;

    private int stageCount = 0;
    private float time_all = 0;
    private float timeNow = 0;

    private bool countTime = false;//��ʱ����
    private Slider sliderProcess;


    [Header("�г���ȡ�鱾������Ϸ")]
    public GameObject bookCanvas;
    public GameObject characterCanvas;

    [Header("�趨��ȡ�б�")]
    public GameObject settingList;

    private GameObject _wordInfo;


    private Vector3 oriScale;
    private AudioPlay audioPlay;
    private AudioSource audioSource;
    //[Header("BGM����")] public float volume=0.2f;
    /// <summary>
    /// �¼����ݹ���
    /// </summary>
    [Header("�¼�λ��")] public GameObject[] eventPoint;
    [Header("�¼����ݣ�ϣ��-�ÿ�-����-Σ��-����-������")] public GameObject[] eventBubblePrefab;

    [Header("[����]ÿ�ּ��ʱ��")] public int eventTime = 30;
    [Header("[����]��Ҫ�¼���ʧʱ��")] public int keyEventTime = 15;
    private int eventCount = 0;
    private List<int> array = new List<int>();
    private List<GameObject> array0 = new List<GameObject>();
    private bool isCreate = false;

    //����
    [Header("�¼�����(�ܺ�100)")]
    [Header("ϣ��")] public int xiWang = 10;
    [Header("�ÿ�")] public int fangKe = 25;
    [Header("����")] public int yiWai = 10;
    [Header("Σ��")] public int weiJi = 30;
    [Header("����")] public int jiaoYi = 25;
    [Header("����")] public int changJing = 10;

    bool hasWeiji = false;
    public void WeiJiOpen()
    {
        StartCoroutine(Wait_Weiji());
        countTime = false;
    }
    private void Start()
    {
        audioPlay = GameObject.Find("AudioSource").GetComponent<AudioPlay>();
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        oriScale = this.transform.localScale;

        //����Ԥ�����õ�ʱ�����ɽ�����
        sliderProcess = this.GetComponent<Slider>();
        sliderProcess.value = 0;

        for (int _i = 0; _i < time_stage.stagesData.Count; _i++)
        { time_all += time_stage.stagesData[_i].time; }
        sliderProcess.maxValue = time_all;

        float _timeAmount = 0;
        float _width = GetComponent<RectTransform>().sizeDelta.x;
        for (int _i = 0; _i < time_stage.stagesData.Count; _i++)
        {

            _timeAmount += time_stage.stagesData[_i].time;
            time_stage.stagesData[_i].time_count = _timeAmount;
            if (time_stage.stagesData[_i].image != null)
            {
                GameObject _icon = GameObject.Instantiate<GameObject>(perfab_icon);
                _icon.transform.SetParent(this.transform);
                _icon.GetComponent<RectTransform>().localPosition = Vector3.zero;
                _icon.GetComponent<RectTransform>().localScale = Vector3.one* time_stage.stagesData[_i].imageScale;
                _icon.GetComponent<RectTransform>().localPosition += new Vector3(-_width / 2 + (_timeAmount / time_all) * _width, 0, 0);
                _icon.GetComponent<Image>().sprite = time_stage.stagesData[_i].image;
            }
        }
        var handle = this.transform.Find("HandleSlideArea");
        handle.parent = this.transform.parent;
        handle.parent = this.transform;
        sliderProcess.maxValue = time_all;

        if (settingList != null)
            settingList.SetActive(false);

        _wordInfo = this.transform.parent.GetComponentInChildren<WordInformation>().gameObject;
        if (_wordInfo != null)
            _wordInfo.SetActive(false);
    }
    private void FixedUpdate()
    {
        GameMgr.instance.time1 += Time.deltaTime;

        if (CharacterManager.instance.pause) return;

        //CharacterManager.instance.EndGame();
        if (!countTime)//��countTime=falseʱ����if
        {
            if (CharacterManager.instance.GetStranger() == null)
            {
                if (hasWeiji)
                {
                    countTime = true; hasWeiji = false;
                    int _random = Random.Range(0, 6);
                    switch (_random)
                    {
                        case 3:
                        case 0://�ÿ�
                            { CreateFangke(false, 0); }
                            break;
                        case 4:
                        case 1://�趨
                            {
                                CreateXiWang(false);
                                 }
                            break;
                        case 5:
                        case 2://ϣ��
                            { CreateSetting(false); }
                            break;
                    }
                }


            }
            return;
        }

        
        //CreateEventUpdate();

        GameMgr.instance.time1 += Time.deltaTime;
        timeNow += Time.deltaTime;
        sliderProcess.value = timeNow;

        //�������
        if (stageCount >= time_stage.stagesData.Count)
        {
            Debug.LogWarning("time_stage overCount!");
           
            
            countTime = false;
            return;
        }

        //�������׶�
        if (timeNow > time_stage.stagesData[stageCount].time_count)
        {
         
            GameMgr.instance.SetStageTo(time_stage.stagesData[stageCount].level-1);

            if (time_stage.stagesData[stageCount].type == StageType.boss)
            {
              
                //����boss�¼�
                if (time_stage.stagesData[stageCount].t_boss != null)
                {
                    print("CreateBossCreateBoss"+ countTime);
             
                    CreateBoss(time_stage.stagesData[stageCount].t_boss); 
                    countTime = false; 
                    //StartCoroutine(Wait_Weiji());
                }
               stageCount++;
            }
            else if (time_stage.stagesData[stageCount].type == StageType.Event)
            {
                
                countTime = true;
                //����Event�¼�
                print("����Event�¼�");
                CreateEvent(time_stage.stagesData[stageCount].t_eventKey, time_stage.stagesData[stageCount].t_eventCount
                    , time_stage.stagesData[stageCount].t_eventtime, time_stage.stagesData[stageCount].t_eventtime_key);
                
                stageCount++;
            }
            else if (time_stage.stagesData[stageCount].type == StageType.weiji)
            {
                //�л�BGM-2
                audioPlay.Boss_GuaiWu();
               

                CreateWeijiEvent(false, 99);
                StartCoroutine(Wait_Weiji());
                countTime = false;
                stageCount++;
                
            }

            //demo�Ľ���ҳ��.��ʱд�ģ��ܲ���
            //if (time_stage[stageCount].t_boss == null)
            //{
            //    CharacterManager.instance.EndGame();
            //}
            //else
            //{


            //}


  
        }
    }


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
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.WeiJi, false, monster);
          
        });
    }

    public void CreateFangke(bool isKey, int chara)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[1], (a) =>
        {
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, chara );

        });
    }
    public void CreateXiWang(bool isKey)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[0], (a) =>
        {
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

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
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.JiaoYi, false, 1);

        });
    }
    public void CreateChangJing(bool isKey)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[5], (a) =>
        {
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.ChangJing, false, 1);

        });
    }
    public void CreateYiwai(bool isKey)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(eventBubblePrefab[2], (a) =>
        {
            a.transform.SetParent(eventPoint[0].transform);
            a.transform.localPosition = Vector3.zero;

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

        CharacterManager.instance.EndGame();
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
        if (settingList != null)
            settingList.SetActive(true);

        GameMgr.instance.settingPanel.RefreshList();

        if (_wordInfo != null)
            _wordInfo.gameObject.SetActive(true);
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
    private int ChooseEvent()
    {
        int result = -1;

        //���ʳ�ȡ
        int numx = Random.Range(1, 101);
        if (numx <= xiWang) { result = 0; }
        else if (numx > xiWang && numx < xiWang + fangKe) result = 1;
        else if (numx > xiWang + fangKe && numx < xiWang + fangKe + yiWai) result = 2;
        else if (numx > xiWang + fangKe + yiWai && numx < xiWang + fangKe + yiWai + weiJi) result = 3;
        else if (numx > xiWang + fangKe + yiWai + weiJi && numx < xiWang + fangKe + yiWai + weiJi + jiaoYi) result = 4;
        else result = 5;

        return result;
    }
    public void CreateEventUpdate()
    {

        if (eventCount > 300) return;
        totalTime += Time.deltaTime;
        if (totalTime > eventTime)
        {

            totalTime = 0;
            CreateEvent((eventCount % 3 == 0)?true:false, event_stage[eventCount].events,eventTime,keyEventTime);
        }

    }


    public void CreateEvent(bool isKey,int count,float _time,float _timeKey)
 
    {
        array0.Clear();
        int _random = -1;
        ////��Ҫ�¼�ÿ���ֳ���һ��
        if (isKey)
        {
            //��⵱ǰ�������¼��У���Щ�¼��ǿ���������Ҫ�¼��ģ����Ҵ��������ȡһ��
            _random = Random.Range(0, event_stage[eventCount].events);
        }

        //�����¼�����Ԥ����
        for (int i = 0; i < count; i++)
        {
          
            int num0 = Random.Range(0, eventPoint.Length);
            int numx = ChooseEvent();
         
            if (i == _random)//����Ҫ�¼�
            {
                int loop = 0;
                while ((!GameMgr.instance.HaveCanHappenKeyEvent(numx)) && (loop < 50))
                {
                    loop += 1;
                    numx = ChooseEvent();
                    if (loop > 48) print("��ѭ��");
                }
            }
            

            //ʵ�����¼�����

            int _looppp = 0;
            while (((!EventPoint.isEvent[num0])||(array.Contains(num0)))&&(_looppp<50))//����ֽ��λ��
            {
                _looppp++;
                num0 = Random.Range(0, eventPoint.Length);
            }

            //ʵ�����¼�����               
            PoolMgr.GetInstance().GetObj(eventBubblePrefab[numx], (a) =>
            {
                array.Add(num0);
                array0.Add(a);
                a.transform.SetParent(eventPoint[num0].transform);
                a.transform.localPosition = Vector3.zero;
                a.GetComponent<Bubble>().dTime = _time;
                if (i == _random)
                {
                    a.GetComponent<Bubble>().isKey = true;
                    a.GetComponent<Bubble>().dTime = _timeKey;
                }
            });

       

        }

        isCreate = true;
        eventCount++;
        array.Clear();
    } 
}
