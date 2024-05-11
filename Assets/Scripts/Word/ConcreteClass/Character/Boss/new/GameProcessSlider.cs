using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


#region �ṹ��
[System.Serializable]public struct Time_Stage
{
    public StageType type;
    public GameObject t_boss;//boss ��Ԥ����
    public int t_eventCount;//boss ��Ԥ����
    public bool t_eventKey;//boss ��Ԥ����
    public float time;//����ʱ��
    public Sprite image;//��Ӧͼ��
    [HideInInspector]public float time_count;//�ۼ�ʱ��   
    public int level ;
}

[System.Serializable] public enum StageType
{
    boss,weiji,Event
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
    public Time_Stage[] time_stage;
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


    private void Start()
    {

        oriScale = this.transform.localScale;

        //����Ԥ�����õ�ʱ�����ɽ�����
        sliderProcess = this.GetComponent<Slider>();
        sliderProcess.value = 0;

        for (int _i = 0; _i < time_stage.Length; _i++)
        { time_all += time_stage[_i].time; }
        sliderProcess.maxValue = time_all;

        float _timeAmount = 0;
        float _width = GetComponent<RectTransform>().sizeDelta.x;
        for (int _i = 0; _i < time_stage.Length; _i++)
        {

            _timeAmount += time_stage[_i].time;
            time_stage[_i].time_count = _timeAmount;
            if (time_stage[_i].image != null)
            {
                GameObject _icon = GameObject.Instantiate<GameObject>(perfab_icon);
                _icon.transform.SetParent(this.transform);
                _icon.GetComponent<RectTransform>().localPosition = Vector3.zero;
                _icon.GetComponent<RectTransform>().localScale = Vector3.one;
                _icon.GetComponent<RectTransform>().localPosition += new Vector3(-_width / 2 + (_timeAmount / time_all) * _width, 0, 0);
                _icon.GetComponent<Image>().sprite = time_stage[_i].image;
            }
        }
        sliderProcess.maxValue = time_all;

        if (settingList != null)
            settingList.SetActive(false);

        _wordInfo = this.transform.parent.GetComponentInChildren<WordInformation>().gameObject;
        if (_wordInfo != null)
            _wordInfo.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (CharacterManager.instance.pause) return;

        if (!countTime)
            return;

        //CreateEventUpdate();


        timeNow += Time.deltaTime;
        sliderProcess.value = timeNow;

        //�������
        if (stageCount >= time_stage.Length)
        {
            Debug.LogWarning("time_stage overCount!");
            countTime = false;
        }

        //�������׶�
        if (timeNow > time_stage[stageCount].time_count)
        {
            GameMgr.instance.SetStageTo(1);

            if (time_stage[stageCount].type == StageType.boss)
            {
                //����boss�¼�
                if (time_stage[stageCount].t_boss != null)
                {
                    CreateBoss(time_stage[stageCount].t_boss); 
                    countTime = false;
                }
               stageCount++;
            }
            else if (time_stage[stageCount].type == StageType.Event)
            {
                //����Event�¼�
                CreateEvent(time_stage[stageCount].t_eventKey, time_stage[stageCount].t_eventCount);
                countTime = true;
                stageCount++;
            }
            else if (time_stage[stageCount].type == StageType.weiji)
            {
                //�����̶�Σ���¼�
                PoolMgr.GetInstance().GetObj(eventBubblePrefab[3], (a) =>
                {
                    array0.Add(a);
                    a.transform.SetParent(eventPoint[0].transform);
                    a.transform.localPosition = Vector3.zero;
                   
                    a.GetComponent<Bubble>().StartEventBefore(EventType.WeiJi, false,true);
                });
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

    #region boss

    /// <summary>
    /// ����boss
    /// </summary>
    /// <param name="_boss"></param>
    void CreateBoss(GameObject _boss)
    {
        this.transform.localScale = Vector3.zero;

        //����boss
        GameObject boss = Instantiate(_boss);
        boss.transform.SetParent(GameObject.Find("Circle5.5").transform);
        boss.transform.localPosition = Vector3.zero;
        boss.transform.localScale = Vector3.one * GameMgr.instance.afterScale;

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
            CreateEvent((eventCount % 3 == 0)?true:false, event_stage[eventCount].events);
        }

    }


    public void CreateEvent(bool isKey,int count)
 
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

            while (array.Contains(num0))//λ��ȥ��
            {
                num0 = Random.Range(0, eventPoint.Length);
            }
            //ʵ�����¼�����
            if (EventPoint.isEvent[num0])//����ֽ��λ�ã�δ���ԣ�
            {
                PoolMgr.GetInstance().GetObj(eventBubblePrefab[numx], (a) =>
                {
                    array.Add(num0);
                    array0.Add(a);
                    a.transform.SetParent(eventPoint[num0].transform);
                    a.transform.localPosition = Vector3.zero;
                    if (i == _random)
                    {
                        a.GetComponent<Bubble>().isKey = true;
                        a.GetComponent<Bubble>().dTime = keyEventTime;
                    }
                });
            }
        }

        isCreate = true;
        eventCount++;
        array.Clear();
    } 
}
