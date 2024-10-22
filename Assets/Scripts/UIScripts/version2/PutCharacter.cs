using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 挂在父物体(charaPos)上，随机生成4个角色子物体，分别位于四个空物体下
/// start按钮响应函数
/// </summary>
public class PutCharacter : BasePanel
{

    public static string CHARA_PREFAB_ADR = "CharaPrefab/";

    public Transform charaPos;
    private List<Image> LightNowOpen = new List<Image>();

    //[Header("（手动挂）灯光父物体")]
    //public Transform lightP;

    [Header("（手动挂）角色预制体池【按角色ID挂】")]
   
    private List<int> array = new List<int>();

    [Header("（怪物挂）角色预制体池【按怪物ID挂】")]
    public GameObject[] monsterPrefabs;

    [Header("用于提示的文本组件")]
    private Text text;

    [Header("（手动挂）外围墙体")]
    private bool needUpdate = true;

    [Header("战前角色大小(22)")]
    public float beforeScale=25;

    
    [Header("比如第一个角色的序号是1，这个数值就为1")]
    public int IDAmount = 1;
    public static bool firstUseCardlist = true;


    protected override void Init()
    {
        charaPos = this.transform.Find("charaPos");
        text = this.transform.Find("Text").GetComponent<Text>();

        firstUseCardlist = true;
        CharacterManager.instance.pause = true;
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(1);


        ////
        for (int _count = 0; _count < 4; _count++)
        {
            CharacterManager.instance.AddToPutCharasList(-1);
        }
        CreateTheCharacter();
    }


   
    protected override void Update()
    {
        base.Update();

        if (!needUpdate)
            return;

        //四个角色全部上场
        if (GetComponentInChildren<AbstractCharacter>() == null)
        {
            isAllCharaUp = true;
            needUpdate = false;
        }
    }


    override protected void OnClick(string _btnName)
    {
        switch (_btnName)
        {
            case "start":
                { CombatStart(); }
                break;
        }
    }




    /// <summary>判断是否所有角色就位</summary>
    public static bool isAllCharaUp;
    /// <summary>判断角色是否站位两侧</summary>
    public static bool isTwoSides;
    ///// <summary>发射器(手动)</summary>
    //public GameObject shooter;

    /// <summary>
    /// 控制提示的显示。1=谁也没放2=没放完3=最初
    /// </summary>
    /// <param name="_type"></param>
    void SetTipStyle(int _type)
    {
        switch (_type)
        {
            case 1://谁也没放
                {
                    text.color = Color.red;
                    text.text = "两方至少要有一名角色";
                }
                break;
            case 2://放，但没放完
                {
                    text.color = Color.red;
                    text.text = "仍有角色未就位";
                }
                break;
            case 3://最开始的提示
                {
                    text.color = Color.white;
                    text.text = "将角色拖拽放入战场，进行相互对抗";
                }
                break;
           
        }
    }



    /// <summary>
    /// 开始战斗（click）
    /// </summary>
    public void CombatStart()
    {
        if (CharacterManager.instance.charas.Count > 0)
        {
            for (int i = 0; i < CharacterManager.instance.charas.Count; i++)
            {
                for (int j = i + 1; j < CharacterManager.instance.charas.Count; j++)
                {
                    if (CharacterManager.instance.charas[i].Camp != CharacterManager.instance.charas[j].Camp)
                    {
                        isTwoSides = true;
                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "ShootCombat")
        {
            // 两方至少要有一名角色
            if (isAllCharaUp && !isTwoSides)
            {
                SetTipStyle(1);
            }
            //仍有角色未就位
            else if (!isAllCharaUp)
            {
                SetTipStyle(2);
            }
            else if (isTwoSides && isAllCharaUp)//成功开始
            {

                BackAnim();
            }
        }
        else//测试版本用
        {
            // 两方至少要有一名角色
            if (!isTwoSides)
            {
                SetTipStyle(1);
            }
            else//成功开始
            {
                BackAnim();
            }
        }
           

    }


    bool animTrigger = false;
    Coroutine lightDisappear = null;
    WaitForFixedUpdate waitD = new WaitForFixedUpdate();

    /// <summary>
    /// 开始执行外部的关闭动画。包括镜头移动
    /// </summary>
    private void BackAnim()
    {

        CharacterManager.instance.SetSituationColorClear(3);
        animator = GetComponent<Animator>();
        animator.SetBool("back", true);

        animTrigger = true;

        //if (lightDisappear != null) StopCoroutine(lightDisappear);
        //lightDisappear = StartCoroutine(LightDisappear());

    }
    /// <summary>
    ///缓慢的改变灯光的颜色
    /// </summary>
    //IEnumerator LightDisappear()
    //{
  
    //    float _speed = 1f;
    //    while(LightNowOpen[0].color.a > 0.1f)
    //    {
    //        yield return waitD;
    //        foreach (var it in LightNowOpen)
    //        {
    //            it.color -= Color.white* _speed;
    //        }
    //    }
      
    //}
    /// <summary>
    /// 外部Animation调用，用于改变镜头
    /// </summary>
    public void CameraChange()
    {
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(2);
    }
   WaitForFixedUpdate wait = new WaitForFixedUpdate();
    private Animator animator;

    /// <summary>
    /// 外部Animattion上调用；表示animation结束，游戏正式开始
    /// </summary>
     public void AnimFinish()
    {
        if (!animTrigger) return;
        animator.SetBool("back", false);
        animTrigger = false;

        StartGame();
    }


    /// <summary>
    /// 彻底开始游戏
    /// </summary>
    private void StartGame()
    {
        //开启枪体
        GameMgr.instance.WallSwitch(true);

            //将UICanvas隐藏
        GameObject.Find("UICanvas").SetActive(false);

            //触发进度条开始开关
        GameObject.Find("GameProcess").GetComponent<GameProcessSlider>().ProcessStart();

        //装载一个shooter
        if (SceneManager.GetActiveScene().name == "ShootCombat") {
            GameObject.Find("shooter").GetComponent<Shoot>().ReadyWordBullet();
        
            GameObject a = GameObject.Find("CombatCanvas");
            //加载图片等美术给资源
            //print(GameMgr.instance.wordGoingUseList[0]);
            //print(GameMgr.instance.wordGoingUseList[1]);
            //print(GameMgr.instance.wordGoingUseList[2]);
/*            a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
            a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1]+"");
            a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[2]+"");
            */
        }
        else GameObject.Find("shooter").GetComponent<TestShoot>().ReadyWordBullet();
        // 将所有站位颜色隐藏
        foreach (Situation it in Situation.allSituation)
            {
            if (it.GetComponent<CircleCollider2D>() != null)
                it.GetComponent<CircleCollider2D>().radius = 0.4f;
            it.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            //foreach (var it in lightP.GetComponentsInChildren<Image>())
            //{
            //    it.color = Color.clear;
            //}


        // 所有角色不可拖拽
        foreach (AbstractCharacter it in CharacterManager.instance.charas)
            {
                //角色的显示图层恢复正常
                it.charaAnim.GetComponent<SpriteRenderer>().sortingLayerName = "Character";
            it.charaAnim.GetComponent<SpriteRenderer>().sortingOrder = 2;
                it.charaAnim.GetComponent<AI.MyState0>().enabled = true;
                it.GetComponent<AbstractCharacter>().enabled = true;
                it.gameObject.AddComponent(typeof(AfterStart));
                Destroy(it.GetComponent<CharacterMouseDrag>());
           

            //碰撞体的设置
            var _colE = it.GetComponent<PolygonCollider2D>();
            var _colB = it.GetComponent<BoxCollider2D>();
            if (_colE != null)
            {
                if (_colB != null) { _colB.enabled = true; _colE.enabled = true; }
            }



        }

  
        //恢复暂停
        CharacterManager.instance.pause = false;


        if (temp) return;
            GameMgr.instance.PlayCG("ElecSheep_start1", 0f);
        temp = true;


    }
    bool temp = false;
  

    private bool isKeyCharacter(int number)
    {
        if (number == (8 - IDAmount) || number == (9 - IDAmount))
        {
            return true;
        }
        return false;
    }



    #region 生成角色相关

   
    /// <summary>
    /// 按照“放置角色列表”创建角色
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="_chara"></param>
    /// <returns></returns>
    public void CreateTheCharacter()
    {
        InitPos();
        SetTipStyle(3);

        GameMgr.instance.WallSwitch(false);

        //生成角色
        for (int _inx = 0; ((_inx < CharacterManager.instance.CanPutCharas.Count) && (_inx < charaPos.childCount));)
        {
            var _chara = CharacterManager.instance.CanPutCharas[_inx];
            //array.Add(number);

            GameObject chara = ResMgr.GetInstance().Load<GameObject>(CHARA_PREFAB_ADR + _chara);
            if (chara == null) print(_chara);
            chara.transform.SetParent(charaPos.GetChild(_inx));
            chara.transform.position = new Vector3(charaPos.GetChild(1).position.x, charaPos.GetChild(1).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(1).position.z);
            SpriteRenderer _sr = chara.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();

            //角色的显示图层恢复正常
            _sr.sortingLayerName = "UICanvas";
            _sr.sortingOrder = 3;

            //碰撞体
            var _colE = chara.GetComponent<PolygonCollider2D>();
            var _colB = chara.GetComponent<BoxCollider2D>();
            if (_colE != null)
            {
                if (_colB != null) { _colB.enabled = false; _colE.enabled = true; }
            }


            _inx++;
        }
        //打开实时更新器
        needUpdate = true;
        //把站位和对应灯光的颜色恢复
        OpenColor();
    }





    /// <summary>
    /// 危机专用，提前获得即将创建的怪物
    /// </summary>
    public int GetNextCreateMonster()
    {
        monsterNext = -1;
        int number = UnityEngine.Random.Range(0, monsterPrefabs.Length);

        monsterNext = number;
        return number;
    }

    public void GetNextCreateMonster(int _id)
    {
 
        monsterNext = _id;
        return;
    }
    int monsterNext = -1;

    public List<GameObject> CreateMonster(int count)
    {
        List<GameObject> _return = new List<GameObject>();
        for (int j = 0; j < count; j++)//抽取不同数量的monster，每个monster都随机抽选，种类不同
        {
            int number = 0;
            if (monsterNext != -1)
            {
                number = monsterNext;
                monsterNext = -1;
            }
            else
            {
                number = UnityEngine.Random.Range(0, monsterPrefabs.Length);
            }

            print(monsterPrefabs[number].name + number.ToString());
           
            //找空位
            int _pos = FindMonsterRandomNullPos();
            if (_pos >=0)
            { 
                GameObject chara = Instantiate(monsterPrefabs[number]);
                chara.transform.SetParent(CharacterManager.instance.transform.GetChild(_pos)) ;
                chara.transform.position = chara.transform .parent.position+ GameMgr.instance.charaPosOffset;
                chara.transform.localScale = Vector3.one * GameMgr.instance.afterScale;

                //生成调整
                chara.GetComponent<AbstractCharacter>().Camp = CampEnum.stranger;
                chara.GetComponent<AbstractCharacter>().situation = CharacterManager.instance.transform.GetChild(_pos).GetComponent<Situation>();
                chara.gameObject.AddComponent(typeof(AfterStart));
                Destroy(chara.GetComponent<CharacterMouseDrag>());

                //设置一个随机目标，使其进入攻击状态
                IAttackRange attackRange = new SingleSelector();

                //这一句越级了
                AbstractCharacter[] a = attackRange.CaculateRange(100, chara.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all, false); 
                chara.GetComponentInChildren<AI.MyState0>().aim.Add(a[UnityEngine.Random.Range(0, a.Length)]);
                chara.GetComponentInChildren<AI.MyState0>().enabled = true;
                chara.GetComponent<AbstractCharacter>().enabled = true;

                if ((_pos == 2) || (_pos == 3) || (_pos == 6) || (_pos == 7))
                {
                    chara.GetComponent<AbstractCharacter>().turn();
                }

                CharacterManager.instance.RefreshStanger();
                _return.Add(chara);
            }
            else
            {print("生成怪物失败");
                return null;
                
            }
            
        }
        return _return;
    }


    #endregion


    /// <summary>
    ///把站位和对应灯光的颜色恢复
    /// </summary>
    private void OpenColor()
    {
        LightNowOpen.Clear();
        for (int X = 0; X < Situation.allSituation.Length; X++)
        {
            //下面的代码要求灯光和站位的子物体顺序完全一致；且灯光也要保留5.5的那个
            if (Situation.allSituation[X].GetComponentInChildren<AbstractCharacter>() == null)
            {
                Situation.allSituation[X].GetComponent<SpriteRenderer>().color = Color.white;
                if (Situation.allSituation[X].GetComponent<CircleCollider2D>()!=null)
                    Situation.allSituation[X].GetComponent<CircleCollider2D>().radius = 1.4f;
                //lightP.GetChild(X).GetComponent<Image>().color = Color.white;
                //LightNowOpen.Add(lightP.GetChild(X).GetComponent<Image>());
            }
            else
            {
                Situation.allSituation[X].GetComponent<SpriteRenderer>().color = Color.clear;
                if (Situation.allSituation[X].GetComponent<CircleCollider2D>() != null)
                    Situation.allSituation[X].GetComponent<CircleCollider2D>().radius = 0.4f;
                //lightP.GetChild(X).GetComponent<Image>().color = Color.clear;
            }

        }
    }


    /// <summary>
    /// 找空位，随机返回一个空位index
    /// </summary>
    public int FindOneRandomNullPos()
    {
        int _count = 0;
        int[] _temp=new int[9];
        for (int X = 0; X < Situation.allSituation.Length; X++)
        {
            if (Situation.allSituation[X].GetComponentInChildren<AbstractCharacter>() == null)
            {
                _temp[_count] = X;
                _count++;
            }
        }

        if (_count != 0)
        {
            int _result = UnityEngine.Random.Range(0, _count);
            int _loop = 0;
            while ((_temp[_result] == 8)&&(_loop<50))
            {
                _result = UnityEngine.Random.Range(0, _count); 
                _loop++;
                if (_loop > 48) print("死循环");
            }
            if(_temp[_result] == 8) return -1;
            else return _temp[_result];
        } 
        else return -1;
    }


    /// <summary>
    /// 找怪物空位，随机返回一个空位index
    /// </summary>
    public int FindMonsterRandomNullPos()
    {
        int _count = 0;
        int[] _temp = new int[3];
        if (CharacterManager.instance.transform.Find("Circle4.1").childCount == 0)
        { _temp[_count] = 9;_count++; }
        if (CharacterManager.instance.transform.Find("Circle4.2").childCount == 0)
        { _temp[_count] = 10; _count++; }
        if (CharacterManager.instance.transform.Find("Circle4.3").childCount == 0)
        { _temp[_count] = 11; _count++; }
        if (_count != 0)
        {
            int _result = UnityEngine.Random.Range(0, _count);
            int _loop = 0;
            while ((_temp[_result] == 8) && (_loop < 50))
            {
                _result = UnityEngine.Random.Range(0, _count);
                _loop++;
                if (_loop > 48) print("死循环");
            }
        return _temp[_result];
        }
        else return -1;
    }

    /// <summary>
    /// 清空角色拜访界面的黑影
    /// </summary>
    void InitPos()
    {
    

        for (int j = 0; j < charaPos.childCount; j++)
        {
            for(int i= charaPos.GetChild(j).childCount-1; i>=0;i--)
            {
                Destroy(charaPos.GetChild(j).GetChild(i).gameObject);
            }
        }
       
    }
}
