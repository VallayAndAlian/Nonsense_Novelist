using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂在父物体上.负责角色和situation的单例
/// </summary>
public class CharacterManager : MonoSingleton<CharacterManager>
{
    public GameObject endGame;

    public static GameObject father;
    /// <summary>当下全部角色</summary>
    private AbstractCharacter[] Charas;
    public AbstractCharacter[] charas
    {
        get
        {
            //获取全部角色
            Charas = GetComponentsInChildren<AbstractCharacter>();
            return Charas;
        }
        set
        {
            Charas = value;
        }
    }
    /// <summary>左侧角色</summary>
    public static List<AbstractCharacter> charas_left = new List<AbstractCharacter>();
    /// <summary>右侧角色</summary>
    public static List<AbstractCharacter> charas_right = new List<AbstractCharacter>();

    /// <summary>
    /// int=situation的number。方便快捷寻找situation的自典
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
    /// 手动设置设置。同时，发射器弃用禁用、子弹速度也会被同时设置。所有的角色都会脱离攻击状态
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
    {//true,暂停，此时关闭
        shooter.GetComponent<Shoot>().enabled = (!_b);
        shooter.GetComponent<RollControler>().enabled = (!_b);
    }

    Vector2[] wordVtemp = new Vector2[100];
    float[] wordAVtemp = new float[100];
    private void WordBallPause(bool _b)
    {

        if (_b)//暂停的时候
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
        else//开始的时候
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
        if (situationDic.Count == 0) print("初始化Situation字典失败");
    }


    /// <summary>
    /// 获取所有的situation，存入字典
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
    /// 返回与输入的Situation相邻的situation的数值
    /// </summary>
    /// <param name="a">需要计算相邻situation的点</param>
    /// <returns>Situation[0]和Situation[1]（和Situation[2]）</returns>
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
    /// 返回与输入的Situation相邻的situation的数值
    /// </summary>
    /// <param name="a">需要计算相邻situation的点</param>
    /// <returns>Situation[0]和Situation[1]（和Situation[2]）</returns>
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
