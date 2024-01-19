using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���ڸ�������.�����ɫ��situation�ĵ���
/// </summary>
public class CharacterManager : MonoSingleton<CharacterManager>
{
    public GameObject endGame;

    public static GameObject father;
    /// <summary>����ȫ����ɫ</summary>
    private AbstractCharacter[] Charas;
    public AbstractCharacter[] charas
    {
        get
        {
            //��ȡȫ����ɫ
            Charas = GetComponentsInChildren<AbstractCharacter>();
            return Charas;
        }
        set
        {
            Charas = value;
        }
    }
    /// <summary>����ɫ</summary>
    public static List<AbstractCharacter> charas_left = new List<AbstractCharacter>();
    /// <summary>�Ҳ��ɫ</summary>
    public static List<AbstractCharacter> charas_right = new List<AbstractCharacter>();

    /// <summary>
    /// int=situation��number��������Ѱ��situation���Ե�
    /// </summary>
    public static Dictionary<float, Situation> situationDic = new Dictionary<float, Situation>();



    [HideInInspector] public static SpriteRenderer[] spSituations = new SpriteRenderer[9];

    #region pauseSetting

    public void EndGame()
    {
        Camera.main.GetComponent<CameraController>().SetCameraSizeTo(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        Instantiate(endGame);
        pause = true;
    }
    private bool PAUSE;
    /// <summary>
    /// �ֶ��������á�ͬʱ�����������ý��á��ӵ��ٶ�Ҳ�ᱻͬʱ���á����еĽ�ɫ�������빥��״̬
    /// </summary>
    public bool pause
    {
        get { return PAUSE; }
        set
        {
            PAUSE = value;
            SetShooterTo(PAUSE);
            WordBallPause(PAUSE);
        }
    }
    private Transform shooter;
    private void SetShooterTo(bool _b)
    {//true,��ͣ����ʱ�ر�
        shooter.GetComponent<Shoot>().enabled = (!_b);
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
    #endregion


    public override void Awake()
    {
        base.Awake();
        Charas = GetComponentsInChildren<AbstractCharacter>();
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
     public Situation[] GetNearBy_S(Situation a)
    {
        Situation[] _resSits = new Situation[3];

        switch (a.number)
        {
            case 1: { _resSits[0] = situationDic[2]; _resSits[1] = situationDic[3]; _resSits[2] = null; } break;
            case 2: { _resSits[0] = situationDic[1]; _resSits[1] = situationDic[4]; _resSits[2] = null; } break;
            case 3: { _resSits[0] = situationDic[1]; _resSits[1] = situationDic[4]; _resSits[2] = situationDic[4.5f]; } break;
            case 4: { _resSits[0] = situationDic[2]; _resSits[1] = situationDic[3]; _resSits[2] = null; } break;
            case 5: { _resSits[0] = situationDic[6]; _resSits[1] = situationDic[7]; _resSits[2] = situationDic[4.5f]; } break;
            case 6: { _resSits[0] = situationDic[5]; _resSits[1] = situationDic[8]; _resSits[2] = null; } break;
            case 7: { _resSits[0] = situationDic[5]; _resSits[1] = situationDic[8]; _resSits[2] = null; } break;
            case 8: { _resSits[0] = situationDic[6]; _resSits[1] = situationDic[7];  _resSits[2] = null;} break;
        }
        return _resSits;
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
}
