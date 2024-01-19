using AI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(CharaAnim))]
//[RequireComponent(typeof(AI.MyState0))]
//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]

/// <summary>
/// 抽象角色类（启用，会在Awake自动关上，需要外部脚本再启用）
/// </summary>
abstract public class AbstractCharacter : AbstractWord0
{
    public MyState0 myState;

    /// <summary>序号</summary>
    [HideInInspector] public int characterID;
    /// <summary>AudioSource</summary>
    [HideInInspector] public AudioSource source;
    /// <summary>平A音效(手动拖拽）</summary>
    public AudioClip aAttackAudio;
    /// <summary>走路音效（手动拖拽）</summary>
    public AudioClip walkAudio;

    /// <summary>特效</summary>
    [HideInInspector] public TeXiao teXiao;
    /// <summary>子弹(手动挂）</summary>
    public GameObject bullet;

    /// <summary>阵营</summary>
    public CampEnum camp;

    /// <summary>身份名</summary>
    [HideInInspector] public string roleName;



    #region 血量

    private Slider hpSlider;

    /// <summary>当前血量</summary>
    private float HP = 0;
    virtual public float hp
    {
        get { return HP; }
        set
        {
    
            HP = value;
            HPSetting();
        }
    }


    /// <summary> 总血量 </summary>
    private float MaxHp = 0;
    virtual public float maxHp
    {
        get { return MaxHp; }
        set
        {
            MaxHp = value;
            HPSetting();
        }
    }

    private float MaxHpMul = 1;
    virtual public float maxHpMul
    {
        get { return MaxHpMul; }
        set
        {
            MaxHpMul = value;
            HPSetting();
        }
    }


    /// <summary>
    /// 检测上下限；修改血条数值
    /// </summary>
    void HPSetting()
    {
        if (HP < 0)
            HP = 0;
        if (HP > MaxHp * MaxHpMul)
            HP = MaxHp * MaxHpMul;
        if (hpSlider != null)
            hpSlider.value = HP / (MaxHp * MaxHpMul);
    }



    /// <summary>
    /// boss用。返回hpsplder
    /// </summary>
    /// <returns></returns>
    public Slider GetHpSlider()
    {
        return hpSlider;
    }

    /// <summary>
    /// boss用。skill物体们
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetSkillText()
    {
        return new GameObject[3] { skillText[0].transform.parent.gameObject, skillText[1].transform.parent.gameObject, skillText[2].transform.parent.gameObject };
    }
    #endregion

    #region 恢复
  
    private float CURE = 0;
    virtual public float cure
    {
        get { return CURE; }
        set
        {
         
            CURE = value;
            if (cure < 0) cure = 0;
        }
    }
    float cureTime;
    bool cureOpen;
    void CureHp()
    {
       
        if(CURE!=0)
        {
          
            CreateFloatWord(CURE, FloatWordColor.heal, false);
            hp += CURE;
        }
      
    }



    #endregion

    #region 基础四维数值


    /// <summary>攻击力</summary>
    protected float ATK = 0;
    virtual public float atk
    {
        get { return ATK; }
        set
        {
            ATK = value;
            if (ATK <= 0)
                ATK = 0;
            CaculateValue();
        }
    }

    /// <summary>攻击力的独立乘区 </summary>
    protected float ATKmul = 1;
    virtual public float atkMul
    {
        get { return ATKmul; }
        set
        {
            ATKmul = value;
            if (ATKmul < 0) ATKmul = 0;
            CaculateValue();
        }
    }



    /// <summary>防御力</summary>
    protected float DEF = 0;
    virtual public float def
    {
        get { return DEF; }
        set
        {
            DEF = value;
            if (DEF <0 /*-19*/)
                DEF = 0;
            CaculateValue();
        }
    }

    /// <summary>防御力的独立乘区 </summary>
    protected float DEFmul = 1;
    virtual public float defMul
    {
        get { return DEFmul; }
        set
        {
            DEFmul = value; if (DEFmul < 0) DEFmul = 0;
            CaculateValue();
        }
    }



    /// <summary>精神力</summary>
    protected float PSY = 0;
    virtual public float psy
    {
        get { return PSY; }
        set
        {
            PSY = value;
            if (PSY <= 0)
                PSY = 0;
            CaculateValue();
        }
    }

    /// <summary>精神力的独立乘区 </summary>
    protected float PSYmul = 1;
    virtual public float psyMul
    {
        get { return PSYmul; }
        set
        {
            PSYmul = value; if (PSYmul <= 0)
                PSYmul = 0;
            CaculateValue();
        }
    }




    /// <summary>意志力</summary>
    protected float SAN = 0;
    virtual public float san
    {
        get { return SAN; }
        set
        {
            SAN = value;
            if (SAN </* -19*/0)
                SAN = 0;
            CaculateValue();
        }
    }

    /// <summary>意志力的独立乘区 </summary>
    protected float SANmul = 1;
    virtual public float sanMul
    {
        get { return SANmul; }
        set
        {
            SANmul = value; if (SANmul < 0)
                SANmul = 0;
            CaculateValue();
        }
    }


    /// <summary>四维之和，节省性能 </summary>
    public float allValue { get; private set; }
    public void CaculateValue() { allValue = atk * atkMul + def * defMul + psy * psyMul + san * sanMul; }




    #endregion



    #region 弃用
    /// <summary>人物暴击默认台词（弃用）</summary>
    //[HideInInspector] public string criticalSpeak;
    /// <summary>人物死亡默认台词（弃用）</summary>
    //[HideInInspector] public string deadSpeak;
    /// <summary>性别(弃用)</summary>
    [HideInInspector] public GenderEnum gender;
    /// <summary>主属性(弃用)</summary>
    public Dictionary<string, string> mainProperty = new Dictionary<string, string>();
    /// <summary>性格（弃用）</summary>
    [HideInInspector] public AbstractTrait trait;
    /// <summary>暴击几率(弃用)</summary>
    [HideInInspector] public float criticalChance = 0;
    /// <summary>暴击倍数(弃用）</summary>
    [HideInInspector] public float multipleCriticalStrike = 2;
    #endregion


    #region 特殊buff用

    /// <summary>所有buff《buffID，是否有buff》</summary>
    public Dictionary<int, int> buffs;
    /// <summary>剩余眩晕时间</summary>
    public float dizzyTime;
    /// <summary>是否有复活状态(仍不可叠加，但数量为0则不可复活)</summary>
    private int relifes;
    public int reLifes
    {
        get { return relifes; }
        set
        {
            relifes = value;
            if (relifes < 0) relifes = 0;
        }
    }


    /// <summary>
    /// 判断该角色是否有该buff
    /// </summary>
    public bool HasBuff(int buffID)
    {
        if (!buffs.ContainsKey(buffID))
            return false;
        else if (buffs[buffID] <= 0)
            return false;
        else
            return true;
    }


    /// <summary>
    /// 加个buff
    /// </summary>
    /// <param name="buffID"></param>
    public void AddBuff(int buffID)
    {
        if (!buffs.ContainsKey(buffID))
            buffs.Add(buffID, 1);
        else
            buffs[buffID]++;
    }


    /// <summary>
    /// 去个buff
    /// </summary>
    /// <param name="buffID"></param>
    public void RemoveBuff(int buffID)
    {
        buffs[buffID]--;
    }


    #endregion


    #region 给角色[增加]技能或随从等等


    /// <summary>拥有技能（所挂组件,自带技能/身份 在初始赋值）</summary>
    public List<AbstractVerbs> skills;

    /// <summary>拥有的随从（最多3个）</summary>
    public List<GameObject> servants;


    ///<summary>这个角色身上可以挂载的最大技能数 </summary>
    public int maxSkillsCount = 3;
    /// <summary>
    /// 增加verb技能时调用
    /// </summary>
    public void AddVerb(AbstractVerbs _av)
    {
        if (maxSkillsCount == 0)
        {
            print(this.gameObject.name + "maxSkillsCount==0");
            return;
        }

        if ((skills.Count != 0) && (skills.Count >= maxSkillsCount))
        {
            //技能数超出，移除最前面的（此处先写成，如果是xx技能，则不排除。后续可以考虑别的方法）
            if (skills[0].wordName == "认知固化")
            {
                if (skills[1].wordName == "猜疑链")
                {
                    myState.character.CreateFloatWord("<s>"+skills[2].wordName+ "</s>", FloatWordColor.removeWord, false);
                    Destroy(skills[2]); skills.RemoveAt(2);
                }
                else
                {
                    myState.character.CreateFloatWord("<s>" + skills[1].wordName + "</s>", FloatWordColor.removeWord, false);
                    Destroy(skills[1]); skills.RemoveAt(1);
                }
                    
            }
            else
            {
                myState.character.CreateFloatWord("<s>" + skills[0].wordName + "</s>", FloatWordColor.removeWord, false);
                Destroy(skills[0]); skills.RemoveAt(0);
            }
        }
        skills.Add(_av);

        for (int i = 0; i < 3; i++)
        {
            skillText[i].transform.parent.gameObject.SetActive(i < skills.Count ? true : false);
        }
        //刷新short列表

        GetComponentInChildren<AfterStart>().GetNewVerbs();

        print(this.wordName + "增加" + _av.wordName + "身上技能：" + skills.Count);
    }



    ///<summary>这个角色身上可以挂载的最大随从数 </summary>
    private int maxServantsCount = 3;
    /// <summary>
    /// 增加随从时调用
    /// </summary>
    public void AddServant(string _a)
    {
        if (wordName == "怀疑主义") return;



        GameObject _servant = Instantiate<GameObject>(Resources.Load<GameObject>("Servants/" + _a));
        var _sa = _servant.GetComponent<ServantAbstract>();
        //将随从的主人设置成这个
        _sa.masterNow = this;
        _sa.enabled = true;

        _servant.GetComponentInChildren<AI.MyState0>().enabled = true;
        //_servant.gameObject.AddComponent(typeof(AfterStart));

        //给随从增加一个目标，使其立刻进入攻击状态
        _servant.GetComponentInChildren<AI.MyState0>().aim = null;
        _sa.situation = this.situation;

        if ((servants.Count != 0) && (servants.Count >= maxServantsCount))
        {
            print(this.gameObject.name + "随从数超出，移除" + servants[0].name);
            //技能数超出，移除最前面的（此处可能有问题）
            servants.RemoveAt(0);
        }
        servants.Add(_servant);

        print(this.wordName + "增加随从：" + _a + "(" + servants.Count);

        //生成随从
        //if (Resources.Load<GameObject>("Servants/" + _av.GetType().Name) == null) print("Resources.Load<GameObject>(Servants /  +_av.GetType().Name)==null");

        ServantRefresh();
    }

    /// <summary>
    /// 删除随从时调用
    /// </summary>
    public void DeleteServant(GameObject _a)
    {
        if (servants.Contains(_a))
            servants.Remove(_a);
        ServantRefresh();
    }
    void ServantRefresh()
    {
        for (int i = 0; i < servants.Count; i++)
        {
            servants[i].transform.parent = this.transform.Find("Servants").GetChild(i);
            servants[i].transform.localPosition = Vector3.zero;
            servants[i].transform.localEulerAngles = Vector3.zero;
            servants[i].transform.localScale = Vector3.one;
            if (servants[i].transform.parent.parent.parent.parent.GetComponent<Situation>().number >= 5)
            {
                servants[i].transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    #endregion


    #region 平A

    //ui
    private Canvas energyCanvas;
    private Slider energySlider;
    private Text energyText;
    public delegate void energyFull();
    public event energyFull OnEnergyFull;
    public Text[] skillText = new Text[3];


    /// <summary>攻击间隔(检定攻击的次序，以及每两次攻击间隔时长)</summary>
    public float attackInterval = 2.2f;

    /// <summary>站位</summary>
    public Situation situation;
    /// <summary>攻击射程</summary>
    public int attackDistance = 99;
    /// <summary>角色动画</summary>
    public CharaAnim charaAnim;


    /// <summary>
    /// 将寻找目标的方式变为true or false
    /// </summary>
    /// <param name="_bool"></param>
    public void SetAimRandom(bool _bool)
    {
        myState.isAimRandom = _bool;
    }


    /// <summary>能量充能</summary>
    public float energy;



    /// <summary>平A模式</summary>
    [HideInInspector] public AbstractSkillMode attackA;

    /// <summary>能用的技能个数 </summary>
    private int _canUseSkills;
    public int canUseSkills
    {
        get
        {
            return _canUseSkills;
        }
        set
        {
            _canUseSkills = value;
            if (energyText != null)
                energyText.text = _canUseSkills.ToString();
        }
    }


    //外部可以增加每秒检测的委托入口
    [HideInInspector] public delegate void Event_AttackA();
    [HideInInspector] public Event_AttackA event_AttackA;

    /// <summary>
    /// 平A
    /// </summary>
    /// <returns>是否平A成功（影响AttackState平A冷却重置）</returns>
    virtual public bool AttackA()
    {

        if (myState.aim != null)
        {

            myState.character.CreateBullet(myState.aim.gameObject);
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            //攻击
            myState.character.charaAnim.Play(AnimEnum.attack);
      
            myState.aim.CreateFloatWord(
                attackA.UseMode(myState.character, myState.character.atk * (1 - myState.aim.def / (myState.aim.def + 20)), myState.aim)
                , FloatWordColor.physics, false);

            //执行外部委托
            if (event_AttackA != null)
                event_AttackA();

            //
            return true;
        }
        return false;
    }


    /// <summary>发出子弹 </summary>
    public virtual void CreateBullet(GameObject aimChara)
    {
        DanDao danDao = GameObjectPool.instance.CreateObject(bullet.gameObject.name, bullet.gameObject, this.transform.position, aimChara.transform.rotation)
            .GetComponent<DanDao>();
        danDao.aim = aimChara;
        danDao.bulletSpeed = 0.5f;
        danDao.SetOff(this.transform.position);
    }

    Vector3[] pos =new Vector3[] { new Vector3(-400, 400, 0) , new Vector3(0, 0, 0) , new Vector3(400, -400, 0), new Vector3(0, 600, 0) };
    int floatCount = 0;

    /// <summary>漂浮文字 </summary>
    public void CreateFloatWord(float value, FloatWordColor color, bool direct)
    {
        PoolMgr.GetInstance().GetObj("SecondStageLoad/floatWord", (obj) =>
         {
    
             obj.transform.localScale = Vector3.one * 20 / 8000f;
             obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
             obj.transform.parent = energyCanvas.transform;
             obj.transform.localPosition = this.transform.position + pos[(floatCount++)%4];
          
             obj.GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);

             if(color==FloatWordColor.heal) teXiao.PlayTeXiao("hpAdd");
             else if(color==FloatWordColor.healMax) teXiao.PlayTeXiao("hpmaxAdd");
             else if (color == FloatWordColor.getWord) teXiao.PlayTeXiao("getWord");
         });
        //Instantiate<GameObject>(Resources.Load("SecondStageLoad/floatWord") as GameObject, this.transform.position + pos, Quaternion.Euler(Vector3.zero), energyCanvas.transform)
        //    .GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);
    }


    /// <summary>漂浮文字 </summary>
    public void CreateFloatWord(string text, FloatWordColor color, bool direct)
    {
        PoolMgr.GetInstance().GetObj("SecondStageLoad/floatWord", (obj) =>
        {
 obj.transform.localScale = Vector3.one * 20 / 8000f;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
           obj.transform.parent = energyCanvas.transform;
            obj.transform.localPosition = this.transform.position + pos[(floatCount++) % 4];
           
            obj.GetComponent<FloatWord>().InitPopup(text, this.camp == CampEnum.stranger, color, direct);

            if (color == FloatWordColor.heal) teXiao.PlayTeXiao("hpAdd");
            else if (color == FloatWordColor.healMax) teXiao.PlayTeXiao("hpmaxAdd");
            else if (color == FloatWordColor.getWord) teXiao.PlayTeXiao("getWord");
        });
        //Instantiate<GameObject>(Resources.Load("SecondStageLoad/floatWord") as GameObject, this.transform.position + pos, Quaternion.Euler(Vector3.zero), energyCanvas.transform)
        //    .GetComponent<FloatWord>().InitPopup(value, this.camp == CampEnum.stranger, color, direct);
    }
    #endregion


    virtual public void Awake()
    {
        energyCanvas = this.GetComponentInChildren<Canvas>();
        energyCanvas.worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        energySlider = energyCanvas.transform.Find("CD").GetComponent<Slider>();
        hpSlider = energyCanvas.transform.Find("HP").GetComponent<Slider>();
        energyText = this.GetComponentInChildren<Text>();
        energyCanvas.gameObject.SetActive(false);

        skillText[0] = energyCanvas.transform.Find("skill1").GetComponentInChildren<Text>();
        skillText[1] = energyCanvas.transform.Find("skill2").GetComponentInChildren<Text>();
        skillText[2] = energyCanvas.transform.Find("skill3").GetComponentInChildren<Text>();
        skillText[0].transform.parent.gameObject.SetActive(false);
        skillText[1].transform.parent.gameObject.SetActive(false);
        skillText[2].transform.parent.gameObject.SetActive(false);


        myState = GetComponentInChildren<MyState0>();
        myState.character = this;
        myState.enabled = false;

        attackA = gameObject.AddComponent<DamageMode>();//平A是伤害类型
        attackA.attackRange = new SingleSelector();

        teXiao = GetComponentInChildren<TeXiao>();
        source = this.GetComponent<AudioSource>();
        buffs = new Dictionary<int, int>();
        charaAnim = GetComponentInChildren<CharaAnim>();

        if (GameObject.Find("AllCharacter") != null)
        {
            AbstractCharacter[] a = GameObject.Find("AllCharacter").GetComponentsInChildren<AbstractCharacter>();
            AbstractCharacter b = CollectionHelper.Find(a, p => p.wordName != wordName);
            AbstractBook.beforeFightText += ShowText(b);
        }

        this.enabled = false;

        cureTime = 0;
        cureOpen = true;

    }

    private void OnEnable()
    {
        if (energyCanvas == null) return;
        energyCanvas.gameObject.SetActive(true);
    }


    private void Update()
    {

        if (CharacterManager.instance.pause) return;

        //角色的能量条积攒
        energy += Time.deltaTime;
        if (energy > 5)//每5秒恢复一点能量
        {
            energy = 0;
            _canUseSkills = 0;
            if (OnEnergyFull != null)
                OnEnergyFull();
        }


        //更新CD条
        for (int x = 0; x < skills.Count; x++)
        {
            if (skills[x].needCD == 0) skillText[x].text = "猜";
            else
                skillText[x].text = ((int)(skills[x].needCD - skills[x].CD)).ToString();
            // print("needCd" + skills[x].needCD + "CD" + skills[x].CD);

        }

        //旧版本CD条
        //energySlider.value = energy;


        if (cureOpen)
            cureTime += Time.deltaTime;
        if (cureTime >= 10)
        {
            cureTime = 0;
            CureHp();
        }
 
    }


    /// <summary>
    /// 翻转
    /// </summary>
    public void turn()
    {
  
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //角色的canvas子物体不转向
        energyCanvas.transform.localScale = new Vector3(-energyCanvas.transform.localScale.x, energyCanvas.transform.localScale.y, energyCanvas.transform.localScale.z);
    }

    #region 文本
    /// <summary>
    /// 人物出场文本(加到AbstractBook.beforeFightText)
    /// </summary>
    abstract public string ShowText(AbstractCharacter otherChara);


    /// <summary>
    /// 暴击文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string CriticalText(AbstractCharacter otherChara);


    /// <summary>
    /// 低血量文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string LowHPText();


    /// <summary>
    /// 死亡文本(加到AbstractBook.afterFightText)
    /// </summary>
    abstract public string DieText();
    #endregion
}

