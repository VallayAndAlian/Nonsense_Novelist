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
    //�������Ĵ�С
    private float scale = 0.16f;
    private List<GameObject>[] energy = new List<GameObject>[3];
    private List<GameObject> energy1 = new List<GameObject>();
    private List<GameObject> energy2 = new List<GameObject>();
    private List<GameObject> energy3 = new List<GameObject>();

    //������ɫ���ϵ�buff�¼�
    private EventListener<int> buffCount;

    //��ʾbuff���ݴ��ֶΡ��Զ���ֵ
    private Sprite buffSprite;
    private Sprite buffSprite_default;

    private Transform skillList;
    private Transform buffList;

    private float energyOffset = 60;//һ��֮���ÿ��������ֵ�м�ļ����С
    private float energyOffsetWith = 300;//��һ������ֵ��x�������ҵ�λ��
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
        //�ȵ�����Χ

        this.transform.localScale = Vector3.one * scale;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_chara.transform.position);
        // ����Ļλ��ת��Ϊ����ռ��е�λ�ã���Ӧ�õ� B ���
        
        // ���� B ����λ��
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
        // ��ȡUIԪ�صı߽���Ϣ
        Vector3[] uiCorners = new Vector3[4];
        uiElement.GetWorldCorners(uiCorners);

        // ��ȡ������ӿڱ߽���Ϣ
        Rect cameraRect = new Rect(0f, 0f, 1f, 1f); // ������Ļ

        // �ж�UIԪ�صı߽��Ƿ�������Ŀɼ�������
        for (int i = 0; i < 4; i++)
        {
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(uiCorners[i]);
            if (!cameraRect.Contains(viewportPoint))
            {
                return true; // UIԪ��������һ����������Ŀɼ�������
            }
        }

        return false; // ���е㶼������Ŀɼ�������
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

        //��ȡbufflist��ĸ���岢���ջ�֮ǰ���ɵ�ȫ��buff��
        buffList = charaShort.transform.GetChild(5);
        foreach (var _buff in buffList.GetComponentsInChildren<Image>())
        {
            PoolMgr.GetInstance().PushObj(buffShortAdr, _buff.gameObject);
        }
        showBuffDic.Clear();

        //��ȡ��ɫ���ϵ�����buff������
        var buff = abschara.GetComponents<AbstractBuff>();
        for (int x = 0; x < buff.Length; x++)
        {
            if (showBuffDic.Count > 7)
            {
                //������ֻ��ʾ7��buff
            }
            else
            {
                //
                if (showBuffDic.ContainsKey(buff[x].buffName))
                {
                    //��ͬ��buff���ӣ��������֡�
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
                //������ʾ�ı�
                buffList.Find(buff[x].buffName.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = showBuffDic[buff[x].buffName].ToString();
            }
        }
    }


    //����ɫ��Ҫ��ֵ
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

        //��ȡ��ɫ�ļ����б�
        skillList = charaShort.transform.GetChild(4);
        if (abschara.skills.Count > 3)
            print(abschara.name + "����������3��");

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

        //by--����:���ڳ�ʼ����ʾ��ɫ����Ϣ
        //�жϴ�Ϊ��ɫ����� ͨ��`abschara`??��ô�ж�,���Կ���ʾ������������???


        ////��ȡ��ɫ��״̬�б�
        //buffList = charaShort.transform.GetChild(5);
        //var buff = abschara.GetComponents<AbstractBuff>();
        //for (int x = 0; x < Mathf.Min(buff.Length, 7); x++)
        //{
        //    //���ɶ�Ӧ��
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
    /// ��AbstractCharacter�е�AddVerbs����
    /// </summary>
    public void GetNewVerbs(AbstractVerbs highestverb)
    {
        DestoryEnergy();
        FunctionInis();

    }
}
