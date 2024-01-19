using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class WordInformation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    [Header("【手动】各词种对应的图")]

    [Tooltip("动词对应的图")]public Sprite spriteVerb;
    [Tooltip("名词对应的图")]public Sprite spriteNoun;
    [Tooltip("形容词对应的图")]public Sprite spriteAdj;




    [Header("【手动】词条信息对应的组件")]

    [Tooltip("卡牌底图")]public Image wordkindBg;

    [Tooltip("卡牌种类文字")]public Text wordkindText;
    private string textVerb = "动词";
    private string textNoun = "名词";
    private string textAdj = "形容词";

    [Tooltip("显示CD的文字")]public Text needCD;
    [Tooltip("词条名称")]public Text title;
    [Tooltip("词条文字")]public TextMeshProUGUI description;
    [Tooltip("是needCD的父物体")] public Image energy;
    [Tooltip("词条图像")] public Image wordImage;

    /// <summary>手动：词条图像读取路径前缀（后加wordname）</summary>
    private string resAdrNoun= "WordImage/Noun/";
    private string resAdrAdj = "WordImage/Adj/";
    private string resAdrVerb = "WordImage/Verb/";
    private Sprite tepSprite;
    private string resName;


    [Header("默认的词语图像")]
    public Sprite defaultWordImage;


    [Header("机制解释面板")]
    public GameObject detailInfoPrefab;
    public Transform detailParent;
    private AbstractWord0 nowWord;
    private bool isDetail = false;

    private bool isCombatScene = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ShootCombat")
            isCombatScene = true;


    }
    public void ChangeInformation(AbstractWord0 word)
    {
        nowWord = word;
        switch (word.wordKind)
        {
            case WordKindEnum.adj:
                {
                    wordkindBg.sprite = spriteAdj;
                    wordkindBg.SetNativeSize();
                    wordkindText.text = textAdj;

                    resName = resAdrNoun + "adj_" + ((AbstractAdjectives)word).adjID;
                    tepSprite = Resources.Load<Sprite>(resName);
                    if (tepSprite == null)
                        wordImage.sprite = defaultWordImage;
                    else
                        wordImage.sprite = Resources.Load<Sprite>(resName);
                }
                break;
            case WordKindEnum.noun:
                {
                    wordkindBg.sprite = spriteNoun;
                    wordkindBg.SetNativeSize();
                    wordkindText.text = textNoun;

                    resName = resAdrNoun + "noun_" + ((AbstractItems)word).itemID;
                    tepSprite = Resources.Load<Sprite>(resName);
                    if (tepSprite == null)
                        wordImage.sprite = defaultWordImage;
                    else
                        wordImage.sprite = Resources.Load<Sprite>(resName);
                }
                break;
            case WordKindEnum.verb:
                {
                    wordkindBg.sprite = spriteVerb;
                    wordkindBg.SetNativeSize();
                    wordkindText.text = textVerb;

                    resName = resAdrNoun + "verb_" + ((AbstractVerbs)word).skillID;
                    tepSprite = Resources.Load<Sprite>(resName);
                    if (tepSprite == null)
                        wordImage.sprite = defaultWordImage;
                    else
                        wordImage.sprite = Resources.Load<Sprite>(resName);
                }
                break;
        }

        title.text = word.wordName;
        description.text=word.description;

        if(word.wordKind==WordKindEnum.verb)
        {
            energy.enabled= true;
            needCD.text =/*word.wordName*/((AbstractVerbs)word).needCD.ToString();
            
        }
        else
        {
            energy.enabled= false;
        }

        if (isDetail)
        {
            ReturnDetailInfo();
            ChangeDetailInfo();
        }
       
    }
    public void SetIsDetail(bool _bool)
    {
        isDetail = _bool;
    }


    string[] info = new string[2];
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCombatScene) return;

        if (CharacterManager.instance.pause) 
            return;
        

        isDetail = true;
        ChangeDetailInfo();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
       // if (CharacterManager.instance.pause) return;
        isDetail = false;
        ReturnDetailInfo();
    }


    void ChangeDetailInfo()
    {
        //从词语中取到其lable的名称
        var _s = nowWord.DetailLable();
        if (_s == null) return;
        for (int i = 0; i < _s.Length; i++)
        {
            //根据取到的名称 获取静态成员参数的值
            System.Type wordType = System.Type.GetType(_s[i]);
            if (wordType != null)
            {
                if (wordType.GetField("s_wordName") == null) print("在" + wordType.ToString() + "中没有定义静态成员s_wordName/s_description");

                info[0] = (string)wordType.GetField("s_wordName").GetValue(null);
                info[1] = (string)wordType.GetField("s_description").GetValue(null);
            }
            else
            {
                print(nowWord.name + "的" + _s + "类型获取失败");
                info[0] = null; info[1] = null;
            }


            if ((info[0] != null) && (info[1] != null))
            {
                PoolMgr.GetInstance().GetObj(detailInfoPrefab, (obj) =>
                 {
                     obj.transform.parent = detailParent;
                     obj.transform.localPosition = new Vector3(0, 0, 0);
                     obj.transform.localScale = Vector3.one;
                     obj.GetComponent<DetailWordInfo>().ChangeInformation(info[0], info[1]);
                 });
            }
            else
            {
                print(_s + "的字段获取失败");
            }
        }
    }

    void ReturnDetailInfo()
    {if (detailParent == null) return;
        foreach (var s in detailParent.GetComponentsInChildren<DetailWordInfo>())
        {
            PoolMgr.GetInstance().PushObj(detailInfoPrefab.name, s.gameObject);
        }
    }

}
