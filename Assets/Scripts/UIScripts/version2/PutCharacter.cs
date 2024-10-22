using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ���ڸ�����(charaPos)�ϣ��������4����ɫ�����壬�ֱ�λ���ĸ���������
/// start��ť��Ӧ����
/// </summary>
public class PutCharacter : BasePanel
{

    public static string CHARA_PREFAB_ADR = "CharaPrefab/";

    public Transform charaPos;
    private List<Image> LightNowOpen = new List<Image>();

    //[Header("���ֶ��ң��ƹ⸸����")]
    //public Transform lightP;

    [Header("���ֶ��ң���ɫԤ����ء�����ɫID�ҡ�")]
   
    private List<int> array = new List<int>();

    [Header("������ң���ɫԤ����ء�������ID�ҡ�")]
    public GameObject[] monsterPrefabs;

    [Header("������ʾ���ı����")]
    private Text text;

    [Header("���ֶ��ң���Χǽ��")]
    private bool needUpdate = true;

    [Header("սǰ��ɫ��С(22)")]
    public float beforeScale=25;

    
    [Header("�����һ����ɫ�������1�������ֵ��Ϊ1")]
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

        //�ĸ���ɫȫ���ϳ�
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




    /// <summary>�ж��Ƿ����н�ɫ��λ</summary>
    public static bool isAllCharaUp;
    /// <summary>�жϽ�ɫ�Ƿ�վλ����</summary>
    public static bool isTwoSides;
    ///// <summary>������(�ֶ�)</summary>
    //public GameObject shooter;

    /// <summary>
    /// ������ʾ����ʾ��1=˭Ҳû��2=û����3=���
    /// </summary>
    /// <param name="_type"></param>
    void SetTipStyle(int _type)
    {
        switch (_type)
        {
            case 1://˭Ҳû��
                {
                    text.color = Color.red;
                    text.text = "��������Ҫ��һ����ɫ";
                }
                break;
            case 2://�ţ���û����
                {
                    text.color = Color.red;
                    text.text = "���н�ɫδ��λ";
                }
                break;
            case 3://�ʼ����ʾ
                {
                    text.color = Color.white;
                    text.text = "����ɫ��ק����ս���������໥�Կ�";
                }
                break;
           
        }
    }



    /// <summary>
    /// ��ʼս����click��
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
            // ��������Ҫ��һ����ɫ
            if (isAllCharaUp && !isTwoSides)
            {
                SetTipStyle(1);
            }
            //���н�ɫδ��λ
            else if (!isAllCharaUp)
            {
                SetTipStyle(2);
            }
            else if (isTwoSides && isAllCharaUp)//�ɹ���ʼ
            {

                BackAnim();
            }
        }
        else//���԰汾��
        {
            // ��������Ҫ��һ����ɫ
            if (!isTwoSides)
            {
                SetTipStyle(1);
            }
            else//�ɹ���ʼ
            {
                BackAnim();
            }
        }
           

    }


    bool animTrigger = false;
    Coroutine lightDisappear = null;
    WaitForFixedUpdate waitD = new WaitForFixedUpdate();

    /// <summary>
    /// ��ʼִ���ⲿ�Ĺرն�����������ͷ�ƶ�
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
    ///�����ĸı�ƹ����ɫ
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
    /// �ⲿAnimation���ã����ڸı侵ͷ
    /// </summary>
    public void CameraChange()
    {
        Camera.main.GetComponent<CameraController>().ZoomChangeTo(2);
    }
   WaitForFixedUpdate wait = new WaitForFixedUpdate();
    private Animator animator;

    /// <summary>
    /// �ⲿAnimattion�ϵ��ã���ʾanimation��������Ϸ��ʽ��ʼ
    /// </summary>
     public void AnimFinish()
    {
        if (!animTrigger) return;
        animator.SetBool("back", false);
        animTrigger = false;

        StartGame();
    }


    /// <summary>
    /// ���׿�ʼ��Ϸ
    /// </summary>
    private void StartGame()
    {
        //����ǹ��
        GameMgr.instance.WallSwitch(true);

            //��UICanvas����
        GameObject.Find("UICanvas").SetActive(false);

            //������������ʼ����
        GameObject.Find("GameProcess").GetComponent<GameProcessSlider>().ProcessStart();

        //װ��һ��shooter
        if (SceneManager.GetActiveScene().name == "ShootCombat") {
            GameObject.Find("shooter").GetComponent<Shoot>().ReadyWordBullet();
        
            GameObject a = GameObject.Find("CombatCanvas");
            //����ͼƬ����������Դ
            //print(GameMgr.instance.wordGoingUseList[0]);
            //print(GameMgr.instance.wordGoingUseList[1]);
            //print(GameMgr.instance.wordGoingUseList[2]);
/*            a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
            a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1]+"");
            a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[2]+"");
            */
        }
        else GameObject.Find("shooter").GetComponent<TestShoot>().ReadyWordBullet();
        // ������վλ��ɫ����
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


        // ���н�ɫ������ק
        foreach (AbstractCharacter it in CharacterManager.instance.charas)
            {
                //��ɫ����ʾͼ��ָ�����
                it.charaAnim.GetComponent<SpriteRenderer>().sortingLayerName = "Character";
            it.charaAnim.GetComponent<SpriteRenderer>().sortingOrder = 2;
                it.charaAnim.GetComponent<AI.MyState0>().enabled = true;
                it.GetComponent<AbstractCharacter>().enabled = true;
                it.gameObject.AddComponent(typeof(AfterStart));
                Destroy(it.GetComponent<CharacterMouseDrag>());
           

            //��ײ�������
            var _colE = it.GetComponent<PolygonCollider2D>();
            var _colB = it.GetComponent<BoxCollider2D>();
            if (_colE != null)
            {
                if (_colB != null) { _colB.enabled = true; _colE.enabled = true; }
            }



        }

  
        //�ָ���ͣ
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



    #region ���ɽ�ɫ���

   
    /// <summary>
    /// ���ա����ý�ɫ�б�������ɫ
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="_chara"></param>
    /// <returns></returns>
    public void CreateTheCharacter()
    {
        InitPos();
        SetTipStyle(3);

        GameMgr.instance.WallSwitch(false);

        //���ɽ�ɫ
        for (int _inx = 0; ((_inx < CharacterManager.instance.CanPutCharas.Count) && (_inx < charaPos.childCount));)
        {
            var _chara = CharacterManager.instance.CanPutCharas[_inx];
            //array.Add(number);

            GameObject chara = ResMgr.GetInstance().Load<GameObject>(CHARA_PREFAB_ADR + _chara);
            if (chara == null) print(_chara);
            chara.transform.SetParent(charaPos.GetChild(_inx));
            chara.transform.position = new Vector3(charaPos.GetChild(1).position.x, charaPos.GetChild(1).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(1).position.z);
            SpriteRenderer _sr = chara.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();

            //��ɫ����ʾͼ��ָ�����
            _sr.sortingLayerName = "UICanvas";
            _sr.sortingOrder = 3;

            //��ײ��
            var _colE = chara.GetComponent<PolygonCollider2D>();
            var _colB = chara.GetComponent<BoxCollider2D>();
            if (_colE != null)
            {
                if (_colB != null) { _colB.enabled = false; _colE.enabled = true; }
            }


            _inx++;
        }
        //��ʵʱ������
        needUpdate = true;
        //��վλ�Ͷ�Ӧ�ƹ����ɫ�ָ�
        OpenColor();
    }





    /// <summary>
    /// Σ��ר�ã���ǰ��ü��������Ĺ���
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
        for (int j = 0; j < count; j++)//��ȡ��ͬ������monster��ÿ��monster�������ѡ�����಻ͬ
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
           
            //�ҿ�λ
            int _pos = FindMonsterRandomNullPos();
            if (_pos >=0)
            { 
                GameObject chara = Instantiate(monsterPrefabs[number]);
                chara.transform.SetParent(CharacterManager.instance.transform.GetChild(_pos)) ;
                chara.transform.position = chara.transform .parent.position+ GameMgr.instance.charaPosOffset;
                chara.transform.localScale = Vector3.one * GameMgr.instance.afterScale;

                //���ɵ���
                chara.GetComponent<AbstractCharacter>().Camp = CampEnum.stranger;
                chara.GetComponent<AbstractCharacter>().situation = CharacterManager.instance.transform.GetChild(_pos).GetComponent<Situation>();
                chara.gameObject.AddComponent(typeof(AfterStart));
                Destroy(chara.GetComponent<CharacterMouseDrag>());

                //����һ�����Ŀ�꣬ʹ����빥��״̬
                IAttackRange attackRange = new SingleSelector();

                //��һ��Խ����
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
            {print("���ɹ���ʧ��");
                return null;
                
            }
            
        }
        return _return;
    }


    #endregion


    /// <summary>
    ///��վλ�Ͷ�Ӧ�ƹ����ɫ�ָ�
    /// </summary>
    private void OpenColor()
    {
        LightNowOpen.Clear();
        for (int X = 0; X < Situation.allSituation.Length; X++)
        {
            //����Ĵ���Ҫ��ƹ��վλ��������˳����ȫһ�£��ҵƹ�ҲҪ����5.5���Ǹ�
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
    /// �ҿ�λ���������һ����λindex
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
                if (_loop > 48) print("��ѭ��");
            }
            if(_temp[_result] == 8) return -1;
            else return _temp[_result];
        } 
        else return -1;
    }


    /// <summary>
    /// �ҹ����λ���������һ����λindex
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
                if (_loop > 48) print("��ѭ��");
            }
        return _temp[_result];
        }
        else return -1;
    }

    /// <summary>
    /// ��ս�ɫ�ݷý���ĺ�Ӱ
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
