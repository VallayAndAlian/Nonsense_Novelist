using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂在父物体(charaPos)上，随机生成4个角色子物体，分别位于四个空物体下
/// start按钮响应函数
/// </summary>
public class CreateOneCharacter : MonoBehaviour
{
    /// <summary>（手动挂）角色预制体池</summary>
    [Header("（手动挂）角色位置父物体")]
    public Transform charaPos;
    private List<Image> LightNowOpen = new List<Image>();

    [Header("（手动挂）灯光父物体")]
    public Transform lightP;

    [Header("（手动挂）角色预制体池")]
    public GameObject[] charaPrefabs;
    private List<int> array = new List<int>();

    [Header("用于提示的文本组件")]
    public Text text;

    [Header("（手动挂）外围墙体")]
    public GameObject wallP;
    private bool needUpdate = true;

    [Header("战前角色大小(22)")]
    public float beforeScale=25;

    private void Start()
    {
        CharacterManager.instance.pause = true;
        Camera.main.GetComponent<CameraController>().SetCameraSize(4);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.01f);
        CreateNewCharacter(4);
    }
    private void Update()
    {
        if (!needUpdate)
            return;

        //四个角色全部上场
        if (GetComponentInChildren<AbstractCharacter>() == null)
        {
            isAllCharaUp = true;
            needUpdate = false;
        }
    }
    /// <summary>判断是否所有角色就位</summary>
    public static bool isAllCharaUp;
    /// <summary>判断角色是否站位两侧</summary>
    public static bool isTwoSides;
    ///// <summary>发射器(手动)</summary>
    //public GameObject shooter;



    /// <summary>
    /// 开始战斗（click）
    /// </summary>
    public void CombatStart()
    {
        if (CharacterManager.instance.charas.Length > 0)
        {
            for (int i = 0; i < CharacterManager.instance.charas.Length; i++)
            {
                for (int j = i + 1; j < CharacterManager.instance.charas.Length; j++)
                {
                    if (CharacterManager.instance.charas[i].camp != CharacterManager.instance.charas[j].camp)
                    {
                        isTwoSides = true;
                    }
                }
            }
        }
        // 两方至少要有一名角色
        if (isAllCharaUp && !isTwoSides)
        {
            text.color = Color.red;
            text.text = "两方至少要有一名角色";
        }
        //仍有角色未就位
        else if (!isAllCharaUp)
        {
            text.color = Color.red;
            text.text = "仍有角色未就位";
        }
        else if (isTwoSides && isAllCharaUp)//成功开始
        {

            BackAnim();
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

        if (lightDisappear != null) StopCoroutine(lightDisappear);
        lightDisappear = StartCoroutine(LightDisappear());

    }
    /// <summary>
    ///缓慢的改变灯光的颜色
    /// </summary>
    IEnumerator LightDisappear()
    {
  
        float _speed = 1f;
        while(LightNowOpen[0].color.a > 0.1f)
        {
            yield return waitD;
            foreach (var it in LightNowOpen)
            {
                it.color -= Color.white* _speed;
            }
        }
      
    }
    /// <summary>
    /// 外部Animation调用，用于改变镜头
    /// </summary>
    public void CameraChange()
    {
         Camera.main.GetComponent<CameraController>().SetCameraSizeTo(3.57f);
        Camera.main.GetComponent<CameraController>().SetCameraYTo(-1.59f);
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
        animTrigger = false; StartGame();
    }


    /// <summary>
    /// 彻底开始游戏
    /// </summary>
    private void StartGame()
    {
         //开启枪体
            wallP.SetActive(true);

            //将UICanvas隐藏
            GameObject.Find("UICanvas").SetActive(false);

            //触发进度条开始开关
            GameObject.Find("GameProcess").GetComponent<GameProcessSlider>().ProcessStart();
            //装载一个shooter
            GameObject.Find("shooter").GetComponent<Shoot>().ReadyWordBullet();

            // 将所有站位颜色隐藏
            foreach (Situation it in Situation.allSituation)
            {
            if (it.GetComponent<CircleCollider2D>() != null)
                it.GetComponent<CircleCollider2D>().radius = 0.4f;
            it.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            foreach (var it in lightP.GetComponentsInChildren<Image>())
            {
                it.color = Color.clear;
            }


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
            }
            //恢复暂停
            CharacterManager.instance.pause = false;
    }



/// <summary>
/// 外部和start调用。生成count数量的角色。
/// </summary>
/// <param name="count"></param>
    public void CreateNewCharacter(int count)
    {        
        //重置角色
        text.color = Color.black;
        text.text = "将角色拖拽放入战场，进行相互对抗";
        //关闭墙体，避免拖拽判定失误
        wallP.SetActive(false);
        //生成角色
        for (int i = 0; i < count; i++)
        {
            int number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            while (array.Contains(number))//去重
            {
                number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            }
            array.Add(number);

            GameObject chara = Instantiate(charaPrefabs[number]);
            chara.transform.SetParent(charaPos.GetChild(i));
            chara.transform.position = new Vector3(charaPos.GetChild(i).position.x, charaPos.GetChild(i).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(i).position.z);
            chara.transform.localScale = Vector3.one * beforeScale;

            SpriteRenderer _sr = chara.GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();
            //角色的显示图层恢复正常
            _sr.sortingLayerName = "UICanvas";
            _sr.sortingOrder = 3;
        }

        //打开实时更新器
        needUpdate = true;

        //把站位和对应灯光的颜色恢复
        OpenColor();
    }


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
                lightP.GetChild(X).GetComponent<Image>().color = Color.white;
                LightNowOpen.Add(lightP.GetChild(X).GetComponent<Image>());
            }
            else
            {
                Situation.allSituation[X].GetComponent<SpriteRenderer>().color = Color.clear;
                if (Situation.allSituation[X].GetComponent<CircleCollider2D>() != null)
                    Situation.allSituation[X].GetComponent<CircleCollider2D>().radius = 0.4f;
                lightP.GetChild(X).GetComponent<Image>().color = Color.clear;
            }

        }
    }
}
