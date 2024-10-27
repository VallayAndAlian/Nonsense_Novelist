using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterShort : BasePanel
{
    Transform CharaShort;
    Transform charaShort
    {
        get
        {
            if (CharaShort == null) CharaShort = this.transform.Find("CharacterShort");
            return CharaShort;
        }
    }
    RectTransform uiElement;
    AbstractCharacter abschara;
    Text textAtk;
    Text textDef;
    Text textPsy;
    Text textSan;
    Text[] textSkill=new Text[3];
    RawImage[] rImageSkill= new RawImage[3];
    //生成面板的大小
    private float scale = 0.16f;
    private List<GameObject>[] energy = new List<GameObject>[3];
    private List<GameObject> energy1 = new List<GameObject>();
    private List<GameObject> energy2 = new List<GameObject>();
    private List<GameObject> energy3 = new List<GameObject>();

    //监听角色身上的buff事件
    private EventListener<int> buffCount;

    //显示buff的暂存字段。自动赋值
    private Sprite buffSprite;
    private Sprite buffSprite_default;

    private Transform skillList;
    private Transform buffList;

    private float energyOffset = 60;//一行之间的每两个能量值中间的间隔大小
    private float energyOffsetWith = 300;//第一个能量值在x轴上向右的位移
    private Dictionary<string, int> showBuffDic = new Dictionary<string, int>();


    Vector3 vector3_100 = new Vector3(1, 0, 0);
    string energyAdr = "UI/energySingle";
   

    string buffShortAdr = "UI/buffShort";
    bool hasInit = false;
    protected override void Init()
    {

     
    }
    override public void Hide(UnityAction callBack = null)
    {
        base.Hide();
        buffCount.OnVariableChange -= WhenBuffCountChange;
    }
    override  protected void Update()
    {
        base.Update();
        if (!hasInit) return;
        FunctionUpdate();
        buffCount.Value = GetComponents<AbstractBuff>().Length;
    }


    public void SwitchInfo(AbstractCharacter _chara)
    {
        hasInit = false;
        energy[0] = energy1;
        energy[1] = energy2;
        energy[2] = energy3; 
        buffCount = new EventListener<int>();
        buffCount.OnVariableChange += WhenBuffCountChange;
        buffSprite_default = Resources.Load<Sprite>("WordImage/Buffs/Default");


        _chara.event_AddVerb += GetNewVerbs;
        //先调整范围

        this.transform.localScale = Vector3.one * scale;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_chara.transform.position);
        // 将屏幕位置转换为世界空间中的位置，以应用到 B 面板
        
        // 设置 B 面板的位置
        this.transform.position = screenPosition;

        uiElement = charaShort.gameObject.GetComponent<RectTransform>();
        if (IsUIOffscreen())
        {
            this.transform.position -= vector3_100 * -1;
        }    

        abschara = _chara;
        DestoryEnergy();
        FunctionInis();
        WhenBuffCountChange(0);
    }

    private bool IsUIOffscreen()
    {
        // 获取UI元素的边界信息
        Vector3[] uiCorners = new Vector3[4];
        uiElement.GetWorldCorners(uiCorners);

        // 获取相机的视口边界信息
        Rect cameraRect = new Rect(0f, 0f, 1f, 1f); // 整个屏幕

        // 判断UI元素的边界是否在相机的可见区域内
        for (int i = 0; i < 4; i++)
        {
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(uiCorners[i]);
            if (!cameraRect.Contains(viewportPoint))
            {
                return true; // UI元素至少有一个点在相机的可见区域外
            }
        }

        return false; // 所有点都在相机的可见区域内
    }

    private void DestoryEnergy()
    {

        var skillList = charaShort.transform.GetChild(4);
        foreach (var eo in skillList.GetComponentsInChildren<Image>())
        {
            PoolMgr.GetInstance().PushObj(energyAdr, eo.gameObject);
        }
       

    }

    void WhenBuffCountChange(int _i)
    {

        //获取bufflist的母物体并且收回之前生成的全部buff。
        buffList = charaShort.transform.GetChild(5);
        foreach (var _buff in buffList.GetComponentsInChildren<Image>())
        {
            PoolMgr.GetInstance().PushObj(buffShortAdr, _buff.gameObject);
        }
        showBuffDic.Clear();

        //获取角色身上的所有buff并生成
        var buff = abschara.GetComponents<AbstractBuff>();
        for (int x = 0; x < buff.Length; x++)
        {
            if (showBuffDic.Count > 7)
            {
                //缩略栏只显示7种buff
            }
            else
            {
                //
                if (showBuffDic.ContainsKey(buff[x].buffName))
                {
                    //相同的buff叠加，数字显现。
                    showBuffDic[buff[x].buffName] += 1;
                }
                else
                {
                    showBuffDic.Add(buff[x].buffName, 1);
                    PoolMgr.GetInstance().GetObj(buffShortAdr, (obj) =>
                    {
                        obj.transform.parent = buffList;
                        obj.transform.localScale = Vector3.one;
                        buffSprite = Resources.Load<Sprite>("WordImage/Buffs/" + buff[x].GetType().ToString());
                        obj.name = buff[x].buffName.ToString();

                        if (buffSprite == null)
                            obj.GetComponent<Image>().sprite = buffSprite_default;
                        else
                            obj.GetComponent<Image>().sprite = buffSprite;


                    });

                }
                //界面显示改变
                buffList.Find(buff[x].buffName.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = showBuffDic[buff[x].buffName].ToString();
            }
        }
    }


    //给角色简要赋值
    void FunctionInis()
    {
        //ATK1
        textAtk = charaShort.transform.GetChild(0).GetComponentInChildren<Text>();
        textAtk.text = (abschara.atk * abschara.atkMul).ToString();
        //def3
        textDef=charaShort.transform.GetChild(2).GetComponentInChildren<Text>();
        textDef.text = (abschara.def * abschara.defMul).ToString();
        //san4
        textSan = charaShort.transform.GetChild(3).GetComponentInChildren<Text>();
        textSan.text = (abschara.san * abschara.sanMul).ToString();
        //psy2
        textPsy = charaShort.transform.GetChild(1).GetComponentInChildren<Text>(); 
        textPsy.text = (abschara.psy * abschara.psyMul).ToString();

        //获取角色的技能列表
        skillList = charaShort.transform.GetChild(4);
        if (abschara.skills.Count > 3)
            print(abschara.name + "技能数超过3个");

        textSkill[0] = skillList.GetComponentsInChildren<Text>()[0];
        textSkill[0].text = "";
        textSkill[1]=skillList.GetComponentsInChildren<Text>()[1];
        textSkill[1].text = "";
        textSkill[2] = skillList.GetComponentsInChildren<Text>()[2];
        textSkill[2].text = "";

        rImageSkill[0] = skillList.GetChild(0).GetComponent<RawImage>();
        rImageSkill[0].color = Color.clear;
        rImageSkill[2] = skillList.GetChild(2).GetComponent<RawImage>();
        rImageSkill[2].color = Color.clear;
        rImageSkill[1] = skillList.GetChild(1).GetComponent<RawImage>();
        rImageSkill[1].color = Color.clear;

        //
        energy[0].Clear();
        energy[1].Clear();
        energy[2].Clear();
        for (int x = 0; x < abschara.skills.Count; x++)
        {

            textSkill[x].text = abschara.skills[x].wordName;
            rImageSkill[x].color = Color.white;

            for (int i = 0; i < abschara.skills[x].needCD; i++)
            {
                PoolMgr.GetInstance().GetObj(energyAdr, (o) =>
                {
                    energy[x].Add(o);
                    o.transform.parent = skillList.GetChild(x).GetChild(0);
                    o.transform.localScale = Vector3.one;
                    o.transform.localPosition = new Vector3(i * energyOffset + energyOffsetWith, 0, 0);
                    //o.GetComponent<Image>().color = (i < abschara.skills[x].CD) ? colorHasEnergy : colorNoEnergy;
                    o.GetComponent<Image>().transform.GetChild(0).gameObject.SetActive((i < abschara.skills[x].CD) ? true : false);

                });

            }

            hasInit = true;
        }

        //by--铭心:仅在初始化显示角色的信息
        //判断此为角色或怪物 通过`abschara`??怎么判断,缩略框显示的内容在哪里???


        ////获取角色的状态列表
        //buffList = charaShort.transform.GetChild(5);
        //var buff = abschara.GetComponents<AbstractBuff>();
        //for (int x = 0; x < Mathf.Min(buff.Length, 7); x++)
        //{
        //    //生成对应的
        //    PoolMgr.GetInstance().GetObj(buffShortAdr, (obj) =>
        //    {
        //        obj.transform.parent = buffList;
        //        obj.transform.localScale = Vector3.one;
        //        buffSprite = Resources.Load<Sprite>("WordImage/Buffs/" + buff[x].name);
        //        if (buffSprite == null)
        //            obj.GetComponent<Image>().sprite = buffSprite_default;
        //        else
        //            obj.GetComponent<Image>().sprite = buffSprite;
        //    });
        //}
    }

    void FunctionUpdate()
    {
        //ATK1
        textAtk.text = (abschara.atk * abschara.atkMul).ToString();
        //def4
        textDef.text = (abschara.def * abschara.defMul).ToString();
        //san3
        textSan.text = (abschara.san * abschara.sanMul).ToString();
        //psy2
        textPsy.text = (abschara.psy * abschara.psyMul).ToString();



        for (int x = 0; x < abschara.skills.Count; x++)
        {

            for (int i = 0; i < abschara.skills[x].needCD; i++)
            {
                //o.GetComponent<Image>().color = (i < abschara.skills[x].CD) ? colorHasEnergy : colorNoEnergy;
                //energy[x][i].GetComponent<Image>().color = (i < abschara.skills[x].CD) ? colorHasEnergy : colorNoEnergy;
                skillList.GetChild(x).GetComponentsInChildren<Image>()[i].transform.GetChild(0).gameObject.
                    SetActive((i < abschara.skills[x].CD) ? true : false); //= (i < abschara.skills[x].CD) ? colorHasEnergy : colorNoEnergy;
            }
        }

    }
    /// <summary>
    /// 在AbstractCharacter中的AddVerbs调用
    /// </summary>
    public void GetNewVerbs(AbstractVerbs highestverb)
    {
        DestoryEnergy();
        FunctionInis();

    }
}
