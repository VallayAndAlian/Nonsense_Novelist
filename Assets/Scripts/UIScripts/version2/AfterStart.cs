using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// combat after start
/// </summary>
public class AfterStart : MonoBehaviour
{
    /// <summary>角色简要预制体（手动挂）</summary>
    private GameObject charaShortInstance;

    //生成面板的大小。
    private float scale = 1.8f / 1900;


    /// <summary>角色简要预制体克隆</summary>
    private GameObject charaShortP;
    private Transform charaShort;

    private string buffShortAdr="UI/buffShort";

    /// <summary></summary>
    private bool one;
    private SpriteRenderer sr;

    private Color colorIn = new Color((float)255 / 255, (float)225 / 255, (float)189 / 255, (float)255 / 255);
    private Color colorOut = new Color((float)255 / 255, (float)255 / 255, (float)255 / 255, (float)255 / 255);
    private Color colorNoEnergy = Color.white;
    private Color colorHasEnergy = Color.blue;


    private List<GameObject>[] energy = new List<GameObject>[3];
    private List<GameObject> energy1 = new List<GameObject>();
    private List<GameObject> energy2 = new List<GameObject>();
    private List<GameObject> energy3 = new List<GameObject>();


    //监听角色身上的buff事件
    private EventListener<int> buffCount;
    
    //显示buff的暂存字段。自动赋值
    private Sprite buffSprite;
    private Sprite buffSprite_default;

    //判断UI是否出界
    private RectTransform uiElement;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;


        charaShortInstance = Resources.Load<GameObject>("UI/CharacterShort");

        sr = GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();

        energy[0] = energy1;
        energy[1] = energy2;
        energy[2] = energy3;

        buffCount = new EventListener<int>();
        buffCount.OnVariableChange += WhenBuffCountChange;
        buffSprite_default = Resources.Load<Sprite>("WordImage/Buffs/Default");


    }


    private void OnDestroy()
    {
        buffCount.OnVariableChange -= WhenBuffCountChange;
    }


  
    Vector3 vector3_100 = new Vector3(1, 0, 0);
    private void OnMouseOver()
    {
        if (CharacterManager.instance.pause) return;
        

        //颜色变黄
        sr.color = colorIn;

        if (!one)
        {
            one = true;
            //显示角色简要信息(注意第二个参数对于UI)
            charaShortP = Instantiate(charaShortInstance);
            charaShortP.transform.localScale = Vector3.one* scale;
            charaShortP.transform.parent = this.transform.GetComponentInChildren<Canvas>().gameObject.transform;
            charaShortP.transform.localPosition = new Vector3(0,-900,0);
            charaShort = charaShortP.transform.GetChild(2);
            uiElement = charaShort.gameObject.GetComponent<RectTransform>();
            if (IsUIOffscreen())
            {
                charaShortP.transform.localPosition = vector3_100 * -6000;
            }
            else
            {
                charaShortP.transform.localPosition = Vector3.zero;
            }
            DestoryEnergy();
            FunctionInis();
            WhenBuffCountChange(0);
        }
    }


    private Transform skillList;
    private Transform buffList;
    string energyAdr = "UI/energySingle";
    private float energyOffset = 60;//一行之间的每两个能量值中间的间隔大小
    private float energyOffsetWith = 300;//第一个能量值在x轴上向右的位移



    void WhenBuffCountChange(int _i)
    {

      
        buffList = charaShort.transform.GetChild(5);
        foreach (var _buff in buffList.GetComponentsInChildren<Image>())
        {
            PoolMgr.GetInstance().PushObj(buffShortAdr, _buff.gameObject);
        }
        var buff = abschara.GetComponents<AbstractBuff>();
        for (int x = 0; x < Mathf.Min(buff.Length, 7); x++)
        {
            PoolMgr.GetInstance().GetObj(buffShortAdr, (obj) =>
            {
                
                obj.transform.parent = buffList;
                obj.transform.localScale = Vector3.one;
                buffSprite = Resources.Load<Sprite>("WordImage/Buffs/" + buff[x].GetType().ToString());
               
                if (buffSprite == null)
                    obj.GetComponent<Image>().sprite = buffSprite_default;
                else
                    obj.GetComponent<Image>().sprite = buffSprite;
            });
        }
    }


    //在最开始和刷新后使用。内涵生成物体代码

    //给角色简要赋值
    AbstractCharacter abschara;
void FunctionInis()
    {

        //给角色简要赋值
        abschara = GetComponent<AbstractCharacter>();

        //ATK1
        charaShort.transform.GetChild(0).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.atk* abschara.atkMul);
        //def4
        charaShort.transform.GetChild(3).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.def * abschara.defMul);
        //san3
        charaShort.transform.GetChild(2).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.san * abschara.sanMul);
        //psy2
        charaShort.transform.GetChild(1).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.psy * abschara.psyMul);


        //获取角色的技能列表
        skillList = charaShort.transform.GetChild(4);
        if (abschara.skills.Count > 3) print(abschara.name + "技能数超过3个");

        skillList.GetComponentsInChildren<Text>()[0].text = "";
        skillList.GetComponentsInChildren<Text>()[1].text = "";
        skillList.GetComponentsInChildren<Text>()[2].text = "";
        skillList.GetChild(0).GetComponent<RawImage>().color = Color.clear;
        skillList.GetChild(2).GetComponent<RawImage>().color = Color.clear;
        skillList.GetChild(1).GetComponent<RawImage>().color = Color.clear;


        //
        energy[0].Clear(); 
         energy[1].Clear();
        energy[2].Clear();
        for (int x = 0; x < abschara.skills.Count; x++)
        {
            
            skillList.GetComponentsInChildren<Text>()[x].text = abschara.skills[x].wordName;
            skillList.GetChild(x).GetComponent<RawImage>().color = Color.white;

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
        }
       

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


    //在update中使用。刷新技能cd和其它数值。
    void FunctionUpdate()
    {
        abschara = GetComponent<AbstractCharacter>();
        if (abschara == null) print("absChara==null");
        if (charaShort == null) print("charaShort==null");
        //ATK1
        charaShort.transform.GetChild(0).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.atk * abschara.atkMul);
        //def4
        charaShort.transform.GetChild(3).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.def * abschara.defMul);
        //san3
        charaShort.transform.GetChild(2).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.san * abschara.sanMul);
        //psy2
        charaShort.transform.GetChild(1).GetComponentInChildren<Text>().text = IntToString.SwitchATK(abschara.psy * abschara.psyMul);



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

    private void Update()
    {
        if (!one)
            return;

        FunctionUpdate();
        buffCount.Value = GetComponents<AbstractBuff>().Length;

    }
   
    Coroutine destoryEnergyIEnumerator;
    private void DestoryEnergy()
    {
        if (!one)
            return;

        skillList = charaShort.transform.GetChild(4);
        foreach (var eo in skillList.GetComponentsInChildren<Image>())
        {
            PoolMgr.GetInstance().PushObj(energyAdr, eo.gameObject);
        }
        //FunctionInis();

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
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(uiCorners[i]);
            if (!cameraRect.Contains(viewportPoint))
            {
                return true; // UI元素至少有一个点在相机的可见区域外
            }
        }

        return false; // 所有点都在相机的可见区域内
    }
    /// <summary>
    /// 在AbstractCharacter中的AddVerbs调用
    /// </summary>
    public void GetNewVerbs()
    {
        if (!one)
            return;
        DestoryEnergy();
        FunctionInis();
        
    }
   



    private void OnMouseExit()
    {
        //颜色恢复

        sr.color = colorOut;

    

        if (one)
        {
            //DestoryEnergy();
            //角色简要不显示
            Destroy(charaShortP.gameObject);

            one = false;
        }
    }


}
