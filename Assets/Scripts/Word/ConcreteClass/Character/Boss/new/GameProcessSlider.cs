using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


#region �ṹ��
[System.Serializable]public struct Time_Stage
{
    public GameObject boss;//boss ��Ԥ����
    public float time;//����ʱ��
    public Sprite image;//��Ӧͼ��
    [HideInInspector]public float time_count;//�ۼ�ʱ��

    
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
    [Header("ÿ�ֵ��¼�����")] 
    public Event_Stage[] event_stage;

    private int stageCount=0;
    private float time_all=0;
    private float timeNow = 0;

    private bool countTime = false;//��ʱ����
    private Slider sliderProcess;


    [Header("�г���ȡ�鱾������Ϸ")]
    public GameObject bookCanvas;
    public GameObject characterCanvas;

    [Header("�趨��ȡ�б�")]
    public GameObject settingList;
    //public GameObject endGame;

    private Vector3 oriScale;

    /// <summary>
    /// �¼����ݹ���
    /// </summary>
    [Header("�¼�λ��")] public GameObject[] eventPoint;
    [Header("�¼����ݣ�����˳���ܸı䣩")] public GameObject[] eventBubblePrefab;

    [Header("ÿ�ּ��ʱ��")] public int eventTime = 30;
    [Header("��Ҫ�¼���ʧʱ��")] public int keyEventTime = 15;
    private int eventCount=0;
    private List<int> array = new List<int>();
    private List<GameObject> array0 = new List<GameObject>();
    public static bool isStart = false;
    private bool isCreate = false;

    //����
    [Header("�¼�����")]
    [Header("ϣ��")] public int xiWang = 10;
    [Header("����")] public int jiaoYi = 25;
    [Header("Σ��")] public int weiJi = 30;
    [Header("�ÿ�")] public int fangKe = 25;
    [Header("����")] public int yiWai = 10;
    private void Start()
    {

        oriScale = this.transform.localScale;



        //���ɽ�����
        sliderProcess = this.GetComponent<Slider>();
        sliderProcess.value = 0;

        for (int _i=0;_i<time_stage.Length;_i++)
        { time_all += time_stage[_i].time;}
        sliderProcess.maxValue = time_all;

        float _timeAmount=0;
        float _width = GetComponent<RectTransform>().sizeDelta.x;
        for (int _i = 0; _i < time_stage.Length; _i++)
        {
          
            _timeAmount += time_stage[_i].time;
            time_stage[_i].time_count = _timeAmount;
       
            GameObject _icon=GameObject.Instantiate<GameObject>(perfab_icon);
            _icon.transform.SetParent(this.transform);
            _icon.GetComponent<RectTransform>().localPosition = Vector3.zero;
            _icon.GetComponent<RectTransform>().localScale = Vector3.one;
            _icon.GetComponent<RectTransform>().localPosition += new Vector3(-_width/2+(_timeAmount / time_all)*_width, 0,0);
            _icon.GetComponent<Image>().sprite = time_stage[_i].image;
        }
        sliderProcess.maxValue = time_all;

        if(settingList!=null)
            settingList.SetActive(false);
    }
    private void FixedUpdate()
    {



        if (CharacterManager.instance.pause) return;
        if (!countTime)
            return;
        if (isStart) CreateEvent();//&&(SceneManager.GetActiveScene().name != "CombatTest")        
  

        timeNow += Time.deltaTime;
        sliderProcess.value = timeNow ;

        //�������
        if (stageCount >= time_stage.Length)
        {
            Debug.LogWarning("time_stage overCount!");
            countTime = false;
        }

        //�������׶�
        if (timeNow>time_stage[stageCount].time_count)
        {
            //demo�Ľ���ҳ��.��ʱд�ģ��ܲ���
            if (time_stage[stageCount].boss == null)
            {
                CharacterManager.instance.EndGame();
            }
            else
            {

                CreateBoss(time_stage[stageCount].boss);
            }

           
            countTime = false;
            stageCount++;
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
      

        //���ɵ���
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //����һ�����Ŀ�꣬ʹ����빥��״̬
        IAttackRange attackRange = new SingleSelector();

        //��һ��Խ����
        AbstractCharacter[] a = attackRange.CaculateRange(100, boss.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all, false);
        boss.GetComponentInChildren<AI.MyState0>().aim .Add (a[Random.Range(0, a.Length)]);


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
    }

    #endregion

    /// <summary>
    /// �����¼�����
    /// </summary>
    private float totalTime = 0;
    public void CreateEvent()
    {
        
        if (eventCount > 3) return;
        totalTime += Time.deltaTime;
        if (totalTime > eventTime)
        {
        
            totalTime = 0;
            array0.Clear();
            int _random = -1;
            //��Ҫ�¼�ÿ���ֳ���һ��
            if (eventCount % 3 == 0)
            {
                //��⵱ǰ�������¼��У���Щ�¼��ǿ���������Ҫ�¼��ģ����Ҵ��������ȡһ��
                _random = Random.Range(0, event_stage[eventCount].events);
            }
            //�����¼�����Ԥ����
            for(int i = 0; i < event_stage[eventCount].events; i++)
            {
                int num0= Random.Range(0, eventPoint.Length);
                //���ʳ�ȡ                
                int numx = Random.Range(1, 101);
                if (numx <= xiWang) { numx = 0; }
                else if (numx > xiWang && numx < xiWang + jiaoYi) numx = 1;
                else if (numx >= xiWang + jiaoYi && numx < xiWang + jiaoYi + weiJi) numx = 2;
                else if (numx >= xiWang + jiaoYi + weiJi && numx < xiWang + jiaoYi + weiJi + fangKe) numx = 3;
                else numx = 4;

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
   
}
