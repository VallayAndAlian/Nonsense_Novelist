using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


#region 结构体
[System.Serializable]public struct Time_Stage
{
    public GameObject boss;//boss 的预制体
    public float time;//持续时间
    public Sprite image;//对应图标
    [HideInInspector]public float time_count;//累计时间

    
}
#endregion

[System.Serializable]
public struct Event_Stage
{
    //事件气泡
    public int events;
}


public class GameProcessSlider : MonoBehaviour
{


    [Header("进度图标预制体（手动）")]
    public GameObject perfab_icon;

    [Header("【单位：s】游戏进程相关参数（手动）")]
    public Time_Stage[] time_stage;
    [Header("每轮的事件个数")] 
    public Event_Stage[] event_stage;

    private int stageCount=0;
    private float time_all=0;
    private float timeNow = 0;

    private bool countTime = false;//计时开关
    private Slider sliderProcess;


    [Header("中场抽取书本加入游戏")]
    public GameObject bookCanvas;
    public GameObject characterCanvas;

    [Header("设定获取列表")]
    public GameObject settingList;
    //public GameObject endGame;

    private Vector3 oriScale;

    /// <summary>
    /// 事件气泡功能
    /// </summary>
    [Header("事件位置")] public GameObject[] eventPoint;
    [Header("事件气泡（气泡顺序不能改变）")] public GameObject[] eventBubblePrefab;

    [Header("每轮间隔时间")] public int eventTime = 30;
    [Header("重要事件消失时间")] public int keyEventTime = 15;
    private int eventCount=0;
    private List<int> array = new List<int>();
    private List<GameObject> array0 = new List<GameObject>();
    public static bool isStart = false;
    private bool isCreate = false;

    //概率
    [Header("事件概率")]
    [Header("希望")] public int xiWang = 10;
    [Header("交易")] public int jiaoYi = 25;
    [Header("危机")] public int weiJi = 30;
    [Header("访客")] public int fangKe = 25;
    [Header("意外")] public int yiWai = 10;
    private void Start()
    {

        oriScale = this.transform.localScale;



        //生成进度条
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

        //如果超出
        if (stageCount >= time_stage.Length)
        {
            Debug.LogWarning("time_stage overCount!");
            countTime = false;
        }

        //如果进入阶段
        if (timeNow>time_stage[stageCount].time_count)
        {
            //demo的结算页面.临时写的，很草率
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
    /// 生成boss
    /// </summary>
    /// <param name="_boss"></param>
    void CreateBoss(GameObject _boss)
    {
        this.transform.localScale = Vector3.zero;

        //生成boss
        GameObject boss = Instantiate(_boss);
        boss.transform.SetParent(GameObject.Find("Circle5.5").transform);
        boss.transform.localPosition = Vector3.zero;
      

        //生成调整
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //设置一个随机目标，使其进入攻击状态
        IAttackRange attackRange = new SingleSelector();

        //这一句越级了
        AbstractCharacter[] a = attackRange.CaculateRange(100, boss.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all, false);
        boss.GetComponentInChildren<AI.MyState0>().aim .Add (a[Random.Range(0, a.Length)]);


    }
    public void BossDie()
    {
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
        if (settingList != null)
            settingList.SetActive(true);
    }

    #endregion

    /// <summary>
    /// 生成事件气泡
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
            //重要事件每三轮出现一次
            if (eventCount % 3 == 0)
            {
                //检测当前的所有事件中，哪些事件是可以生成重要事件的，并且从中随机抽取一件
                _random = Random.Range(0, event_stage[eventCount].events);
            }
            //生成事件气泡预制体
            for(int i = 0; i < event_stage[eventCount].events; i++)
            {
                int num0= Random.Range(0, eventPoint.Length);
                //概率抽取                
                int numx = Random.Range(1, 101);
                if (numx <= xiWang) { numx = 0; }
                else if (numx > xiWang && numx < xiWang + jiaoYi) numx = 1;
                else if (numx >= xiWang + jiaoYi && numx < xiWang + jiaoYi + weiJi) numx = 2;
                else if (numx >= xiWang + jiaoYi + weiJi && numx < xiWang + jiaoYi + weiJi + fangKe) numx = 3;
                else numx = 4;

                while (array.Contains(num0))//位置去重
                {
                    num0 = Random.Range(0, eventPoint.Length);
                }
                //实例化事件气泡
                if (EventPoint.isEvent[num0])//避免纸球位置（未测试）
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
