using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("���ֶ��ң��ƹ⸸����")]
    public Transform lightP;

    [Header("���ֶ��ң���ɫԤ�����")]
    public GameObject[] charaPrefabs;
    private List<int> array = new List<int>();

    [Header("������ʾ���ı����")]
    public Text text;

    [Header("���ֶ��ң���Χǽ��")]
    public GameObject wallP;
    private bool needUpdate = true;

    [Header("սǰ��ɫ��С(22)")]
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
                    if (CharacterManager.instance.charas[i].camp != CharacterManager.instance.charas[j].camp)
                    {
                        isTwoSides = true;
                    }
                }
            }
        }
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

        if (lightDisappear != null) StopCoroutine(lightDisappear);
        lightDisappear = StartCoroutine(LightDisappear());

    }
    /// <summary>
    ///�����ĸı�ƹ����ɫ
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
        animTrigger = false; StartGame();
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
            GameObject.Find("shooter").GetComponent<Shoot>().ReadyWordBullet();

            // ������վλ��ɫ����
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
            }
            //�ָ���ͣ
            CharacterManager.instance.pause = false;
    }



/// <summary>
/// �ⲿ��start���á�����count�����Ľ�ɫ��
/// </summary>
/// <param name="count"></param>
    public void CreateNewCharacter(int count)
    {        
        //���ý�ɫ
        text.color = Color.black;
        text.text = "����ɫ��ק����ս���������໥�Կ�";
        //�ر�ǽ�壬������ק�ж�ʧ��
        wallP.SetActive(false);
        //���ɽ�ɫ
        for (int i = 0; i < count; i++)
        {
            int number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            while (array.Contains(number))//ȥ��
            {
                number = UnityEngine.Random.Range(0, charaPrefabs.Length);
            }
            array.Add(number);

            GameObject chara = Instantiate(charaPrefabs[number]);
            chara.transform.SetParent(charaPos.GetChild(i));
            chara.transform.position = new Vector3(charaPos.GetChild(i).position.x, charaPos.GetChild(i).position.y + CharacterMouseDrag.offsetY, charaPos.GetChild(i).position.z);
            chara.transform.localScale = Vector3.one * beforeScale;

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
