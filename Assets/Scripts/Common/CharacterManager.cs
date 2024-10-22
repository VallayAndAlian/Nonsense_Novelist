using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// ���ڸ�������.�����ɫ��situation�ĵ���
/// </summary>
public class CharacterManager : MonoSingleton<CharacterManager>
{ 

    public static GameObject father;

    /// <summary>������ս���ϵ�ȫ����ɫ(���+����)</summary>
    private List<AbstractCharacter> Charas=new List<AbstractCharacter>();
    public List<AbstractCharacter> charas
    {
        get
        {
            //��ȡȫ����ɫ
            Charas.Clear();
            Charas.AddRange(GetComponentsInChildren<AbstractCharacter>());
            return Charas;
        }
        set
        {
            Charas = value;
        }
    }
    /// <summary>�������б�������н�ɫ</summary>
    public List<string> CanPutCharas = new List<string>();
    ///<summary>������ս���ϵ�ȫ����ɫ+�������б�������н�ɫ</summary>
    private List<string> hasUsedCharas
    {
        get
        {
            //��ȡȫ����ɫ
            HasUsedCharas.Clear();
            foreach (var _chara in charas)
            {
                HasUsedCharas.Add(_chara.name);
            }
            foreach (var _chara in CanPutCharas)
            {
                HasUsedCharas.Add(_chara);
            }
            return HasUsedCharas;
        }
        set
        {
            HasUsedCharas = value;
        }
    }
    private List<string> HasUsedCharas=new List<string>();
     
 
    /// <summary>���ϵ����й���</summary>
    private List<AbstractCharacter> Strangers=new List<AbstractCharacter>();




    bool hasEnd = false;
    
    public List<AbstractCharacter> deadChara = new List<AbstractCharacter>();
    /// <summary>����ɫ</summary>
    public  static List<AbstractCharacter> charas_left = new List<AbstractCharacter>();
    /// <summary>�Ҳ��ɫ</summary>
    public static List<AbstractCharacter> charas_right = new List<AbstractCharacter>();

    /// <summary>
    /// int=situation��number��������Ѱ��situation���Ե�
    /// </summary>
    public static Dictionary<float, Situation> situationDic = new Dictionary<float, Situation>();
    public void RefreshStanger()
    {
        Strangers.Clear();

        foreach (var _m in charas)
        {
            if (_m.Camp == CampEnum.stranger)
            {
                Strangers.Add(_m);
            }
        }
     
    }

    public AbstractCharacter[] GetStranger()
    {
      
        if (Strangers .Count!= 0)
            return Strangers.ToArray();
        else
            return null;
    }

    [HideInInspector] public static SpriteRenderer[] spSituations = new SpriteRenderer[12];

    #region pauseSetting

   
    private bool PAUSE;
    /// <summary>
    /// �ֶ��������á�ͬʱ�����������ý��á��ӵ��ٶ�Ҳ�ᱻͬʱ���á����еĽ�ɫ�������빥��״̬
    /// true��ͣfalse��ʼ
    /// </summary>
    public bool pause
    {
        get { return PAUSE; }
        set
        {
            PAUSE = value;
            SetShooterTo(PAUSE);
            WordBallPause(PAUSE);
            AllCharaAnimPause(PAUSE);
        }
    }
    private Transform shooter;
    private void SetShooterTo(bool _b)
    {//true,��ͣ����ʱ�ر�
        if (SceneManager.GetActiveScene().name == "ShootCombat")
        {
            if(shooter==null) shooter = GameObject.Find("shooter").transform;
            shooter.GetComponent<Shoot>().enabled = (!_b);
        } 
        else shooter.GetComponent<TestShoot>().enabled = (!_b);
        shooter.GetComponent<RollControler>().enabled = (!_b);
    }

    Vector2[] wordVtemp = new Vector2[100];
    float[] wordAVtemp = new float[100];
    private void WordBallPause(bool _b)
    {

        if (_b)//��ͣ��ʱ��
        {
            var obj = GameObject.Find("AfterShootTF").GetComponentsInChildren<Rigidbody2D>();
            for (int i = 0; i < obj.Length; i++)
            {
                wordVtemp[i] = obj[i].velocity;
                wordAVtemp[i] = obj[i].angularVelocity;
                obj[i].velocity = Vector2.zero;
                obj[i].angularVelocity = 0;
            }
        }
        else//��ʼ��ʱ��
        {
            var obj = GameObject.Find("AfterShootTF").GetComponentsInChildren<Rigidbody2D>();
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].velocity = wordVtemp[i];
                obj[i].angularVelocity = wordAVtemp[i];
            }
        }

    }



    private void AllCharaAnimPause(bool _b)
    {
        foreach(var _c in this.GetComponentsInChildren<AbstractCharacter>())
        {
            if (_b)
            {
                _c.AttackSpeedPause(true);
            }
            else
            {
                _c.AttackSpeedPause(false);
            }
        }
    }
    #endregion


    public override void Awake()
    {
        base.Awake();
        father = this.gameObject;
        shooter = GameObject.Find("shooter").transform;



    }


    private void Start()
    {
        GetAllSituation();
        if (situationDic.Count == 0) print("��ʼ��Situation�ֵ�ʧ��");
    }


    /// <summary>
    /// ��ȡ���е�situation�������ֵ�
    /// </summary>
    static private void GetAllSituation()
    {
        Situation[] _sits;
        _sits = GameObject.Find("AllCharacter").GetComponentsInChildren<Situation>();

        for (int i = 0; i < _sits.Length; i++)
        {
            spSituations[i] = _sits[i].GetComponent<SpriteRenderer>();
            if (!situationDic.ContainsKey(_sits[i].number))
                situationDic.Add(_sits[i].number, _sits[i]);

        }


    }

    public CharaInfoExcelItem AddToPutCharasList(int _charaID)
    {
        //�ڱ��в��Ҷ�ӦID�ġ�δ�ϳ�Ҳδ�ڴ������б���Ľ�ɫ
        if (PoolConfigData.instance.so==null) print("1");
        if (PoolConfigData.instance.so.charaInfo == null)  print("2");
        if (PoolConfigData.instance.so.charaInfo.items== null) print("3");
        var _max = PoolConfigData.instance.so.charaInfo.items.Length;
        var _random = Random.Range(0, _max);
        var _loopTime = 0;
        string randomPrefabName = PoolConfigData.instance.so.charaInfo.items[_random].prefabName;
        while ((hasUsedCharas.Contains(randomPrefabName) || 
            (PoolConfigData.instance.so.charaInfo.items[_random].charaID > PoolConfigData.CHARA1_CHARAID_MAX))
            &&(_loopTime<100))
            //ȷ���鵽�Ľ�ɫ
        {
             _random = Random.Range(0, _max);
             randomPrefabName = PoolConfigData.instance.so.charaInfo.items[_random].prefabName;
            _loopTime++;
        }
        if (hasUsedCharas.Contains(randomPrefabName)) return null;


        CanPutCharas.Add(PoolConfigData.instance.so.charaInfo.items[_random].prefabName);
        hasUsedCharas.Add(PoolConfigData.instance.so.charaInfo.items[_random].prefabName);

        return PoolConfigData.instance.so.charaInfo.items[_random];
    }


    Coroutine coroutineColor = null;
    public void SetSituationColorClear(int _speed)
    {
        if (coroutineColor != null) StopCoroutine(coroutineColor);
        coroutineColor = StartCoroutine(ColorClear(_speed));
    }
    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    IEnumerator ColorClear(int _speed)
    {
        int count = 0;
        while(spSituations[0].color.a>0.05f&&count<120)
        {
       
            yield return wait;
            foreach (var _sp in spSituations)
            {
                _sp.color -= Color.white * 1 / 100 * _speed;
            }
            count++;
        }
    }

   


    /// <summary>
    /// �����������Situation���ڵ�situation����ֵ
    /// </summary>
    /// <param name="a">��Ҫ��������situation�ĵ�</param>
    /// <returns>Situation[0]��Situation[1]����Situation[2]��</returns>
    public AbstractCharacter[] GetNearBy_C(Situation a)
    {
        AbstractCharacter[] _resSits = new AbstractCharacter[3];

        switch (a.number)
        {
            case 1: { _resSits[0] = situationDic[2].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[3].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 2: { _resSits[0] = situationDic[1].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[4].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 3: { _resSits[0] = situationDic[1].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[4].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = situationDic[4.5f].GetComponentInChildren<AbstractCharacter>(); } break;
            case 4: { _resSits[0] = situationDic[2].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[3].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 5: { _resSits[0] = situationDic[6].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[7].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = situationDic[4.5f].GetComponentInChildren<AbstractCharacter>(); } break;
            case 6: { _resSits[0] = situationDic[5].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[8].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 7: { _resSits[0] = situationDic[5].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[8].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
            case 8: { _resSits[0] = situationDic[6].GetComponentInChildren<AbstractCharacter>(); _resSits[1] = situationDic[7].GetComponentInChildren<AbstractCharacter>(); _resSits[2] = null; } break;
        }
        return _resSits;
    }



    float bulletSpeed=3.3f;
    public float DelayAccount(float _startP,float _endP)
    {
        float count =( (Mathf.Abs(_startP - _endP)) / bulletSpeed);

        return count;
    }

    public List<AbstractCharacter> GetEnemy(CampEnum _your)
    {
        if (_your == CampEnum.left) return charas_right;
        else if (_your == CampEnum.right) return charas_left;
        else return charas_left;
    }

    public List<AbstractCharacter> GetFriend(CampEnum _your)
    {
        if (_your == CampEnum.left) return charas_left;
        else if (_your == CampEnum.right) return charas_right;
        else return charas_left;//����
    }



}
