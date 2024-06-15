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
public class CreateOneCharacter : MonoBehaviour
{
    /// <summary>���ֶ��ң���ɫԤ�����</summary>
    [Header("���ֶ��ң���ɫλ�ø�����")]
    public Transform charaPos;
    private List<Image> LightNowOpen = new List<Image>();

    //[Header("���ֶ��ң��ƹ⸸����")]
    //public Transform lightP;

    [Header("���ֶ��ң���ɫԤ����ء�����ɫID�ҡ�")]
    public GameObject[] charaPrefabs;
    private List<int> array = new List<int>();

    [Header("������ң���ɫԤ����ء�������ID�ҡ�")]
    public GameObject[] monsterPrefabs;

    [Header("������ʾ���ı����")]
    public Text text;

    [Header("���ֶ��ң���Χǽ��")]
    public GameObject wallP;
    private bool needUpdate = true;

    [Header("սǰ��ɫ��С(22)")]
    public float beforeScale=25;

    
    [Header("�����һ����ɫ�������1�������ֵ��Ϊ1")]
    public int IDAmount = 1;
    public static bool firstUseCardlist = true;

    private void Start()
    {
        firstUseCardlist = true;
        CharacterManager.instance.pause = true;
        Camera.main.GetComponent<CameraController>().SetCameraSize(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        if (SceneManager.GetActiveScene().name== "ShootCombat")
        {
            CreateNewCharacter(4);
        }
        else//���԰汾��
        {
            CreateAllCharacter();
        }
    }
    private void Update()
    {
        if (!needUpdate)
            return;

        //�ĸ���ɫȫ���ϳ�
        if (GetComponentInChildren<AbstractCharacter>() == null)
        {
            isAllCharaUp = true;
            needUpdate = false;
        }
    }
    /// <summary>�ж��Ƿ����н�ɫ��λ</summary>
    public static bool isAllCharaUp;
    /// <summary>�жϽ�ɫ�Ƿ�վλ����</summary>
    public static bool isTwoSides;
    ///// <summary>������(�ֶ�)</summary>
    //public GameObject shooter;



    /// <summary>
    /// ��ʼս����click��
    /// </summary>
    public void CombatStart()
    {
        if (CharacterManager.instance.charas.Length > 0)
        {
            for (int i = 0; i < CharacterManager.instance.charas.Length; i++)
            {
                for (int j = i + 1; j < CharacterManager.instance.charas.Length; j++)
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
                text.color = Color.red;
                text.text = "��������Ҫ��һ����ɫ";
            }
            //���н�ɫδ��λ
            else if (!isAllCharaUp)
            {
                text.color = Color.red;
                text.text = "���н�ɫδ��λ";
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
                text.color = Color.red;
                text.text = "��������Ҫ��һ����ɫ";
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
         Camera.main.GetComponent<CameraController>().SetCameraSizeTo(3.57f);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.59f);
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
        wallP.SetActive(true);

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
            GameMgr.instance.PlayCG("ElecSheep_start1", 0.5f);
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




    /// <summary>
    /// �ÿ�ר�ã���ǰ��ü��������Ľ�ɫ
    /// </summary>
    public int GetNextCreateChara()
    {

        charaNext = -1;
        int number = UnityEngine.Random.Range(0, charaPrefabs.Length); print("GetNextCreateChara" + number);
        float loopCount = 0;
        while ((array.Contains(number) || isKeyCharacter(number)) && loopCount < 50)//ȥ��
        {
            number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            loopCount++;
            if (loopCount > 45) print("��ѭ��");
        }
        charaNext = number;
        return number;
    }
    public void GetNextCreateChara(int _id)
    {
        charaNext = _id;
        return;
    }
    int charaNext = -1;

/// <summary>
/// �ⲿ��start���á�����count�����Ľ�ɫ��
/// </summary>
/// <param name="count"></param>
    public void CreateNewCharacter(int count)
    {
        InitPos();
        //���ý�ɫ
        text.color = Color.black;
        text.text = "����ɫ��ק����ս���������໥�Կ�";
        //�ر�ǽ�壬������ק�ж�ʧ��
        wallP.SetActive(false);
        //���ɽ�ɫ
        for (int i = 0; i < count; i++)
        {
             int number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            if ((count == 1) & (charaNext != -1))//����Ѿ������
            {
                number= charaNext;
                charaNext = -1;
            }
            else//�ֳ�
            { 
              
                float loopCount = 0;
                while ((array.Contains(number) || isKeyCharacter(number) )&& loopCount<50)//ȥ��
                {
                    number = UnityEngine.Random.Range(0, charaPrefabs.Length);
                    loopCount++;
                    if (loopCount > 45) print("��ѭ��");
                }
            }
            array.Add(number);
          
            GameObject chara = Instantiate(charaPrefabs[number]);
            chara.transform.SetParent(charaPos.GetChild(i));
            chara.transform.position = new Vector3(charaPos.GetChild(i).position.x, charaPos.GetChild(i).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(i).position.z);


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
        }

        //��ʵʱ������
        needUpdate = true;

        //��վλ�Ͷ�Ӧ�ƹ����ɫ�ָ�
        OpenColor();
    }
    /// <summary>
    /// �߻�debug�����ã���ʼ����ȫ����ɫ��
    /// </summary>
    public void CreateAllCharacter( )
    {
        InitPos();
        //���ý�ɫ
        text.color = Color.black;
        text.text = "����ɫ��ק����ս���������໥�Կ�";
        //�ر�ǽ�壬������ק�ж�ʧ��
        wallP.SetActive(false);
        //���ɽ�ɫ
        for (int i = 0; i < charaPrefabs.Length; i++)
        {
            int number = UnityEngine.Random.Range(0, charaPrefabs.Length);
                       float loopCount = 0;
            while ((array.Contains(number) || isKeyCharacter(number) )&& loopCount<50)//ȥ��
            {
                number = UnityEngine.Random.Range(0, charaPrefabs.Length);
                loopCount++;
                if (loopCount > 45) print("��ѭ��");
            }
            array.Add(number);

            GameObject chara = Instantiate(charaPrefabs[number]);
            chara.transform.SetParent(charaPos.GetChild(i));
            chara.transform.position = new Vector3(charaPos.GetChild(i).position.x, charaPos.GetChild(i).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(i).position.z);
            chara.transform.localScale = Vector3.one * GameMgr.instance.beforeScale;

            SpriteRenderer _sr = chara.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();
            //��ɫ����ʾͼ��ָ�����
            _sr.sortingLayerName = "UICanvas";
            _sr.sortingOrder = 3;
        }

        //��ʵʱ������
        needUpdate = true;

        //��վλ�Ͷ�Ӧ�ƹ����ɫ�ָ�
        OpenColor();
    }

    public bool CreateTheCharacter(int ID)
    {
        InitPos();
        //���ý�ɫ
        text.color = Color.black;
        text.text = "����ɫ��ק����ս���������໥�Կ�";
        //�ر�ǽ�壬������ק�ж�ʧ��
        wallP.SetActive(false);
        //���ɽ�ɫ
        int number = ID - IDAmount;

        if (array.Contains(number))//ȥ��
        {
            return false;
        }
        array.Add(number);

        GameObject chara = Instantiate(charaPrefabs[number]);
        chara.transform.SetParent(charaPos.GetChild(1));
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
        

        //��ʵʱ������
        needUpdate = true;

        //��վλ�Ͷ�Ӧ�ƹ����ɫ�ָ�
        OpenColor();
        return true;
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
        var _p = this.transform.Find("Panel").Find("charaPos");

        for (int j = 0; j < _p.childCount; j++)
        {
            for(int i= _p.GetChild(j).childCount-1; i>=0;i--)
            {
                Destroy(_p.GetChild(j).GetChild(i).gameObject);
            }
        }
       
    }
}
