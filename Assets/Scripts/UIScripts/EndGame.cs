using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [Header("(手动设置)panel1")]
    public Transform panel1;//命名
    private string titleName;
    public Text textInput;

    [Header("(手动设置)panel2")]
    public Transform panel2;//查看书本
    public Text title;
    public GameObject closeBook;
    public GameObject openBook;
    public Text contentL;
    public Text contentR;
    public int indexPage=0;
    private int indexLeft = 0;
    private int indexRight = 0;
    private string[] content;
    private string content_string;
    public int lineWords = 18;
    public int lineCount = 13;
    private Dictionary<int, int> pageContent = new Dictionary<int, int>();

    [Header("(手动设置)panel3")]
    public Transform panel3;//收到信件
    public GameObject letterBig;
    private Animator letetrAnim;
    public GameObject letetrScroe;//信件得分
    public Scrollbar scroll;
    private Vector3 letterScroePos;
    private void Awake()
    {
        //初始化，打开panel1关闭其它
        ChangePanel(1);

        //赋值
        letetrAnim = letterBig.GetComponent<Animator>();


        //

        GameMgr.instance.draftUi.AddContent("冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                          "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                          "衣衫破损的人惊惶四顾、人人自疑，" +
                      "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
                      "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
                      "这该死的一无所有的时代！");
        GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
         "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
         "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
         "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
         "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
         "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
         "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
         "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
         "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");
        GameMgr.instance.draftUi.AddContent("冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                  "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                  "衣衫破损的人惊惶四顾、人人自疑，" +
              "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
              "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
              "这该死的一无所有的时代！");
        GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
         "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
         "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
         "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
         "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
         "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
         "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
         "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
         "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");
        GameMgr.instance.draftUi.AddContent("冰冷的雨又落下来了,而我仍梦到他踏着草甸,在清晨的迷雾中飘飘荡荡地走来,轻易刺破我的欢歌。" +
                  "那场雨落在2037年，霓虹闪烁，污水横流，在阴暗的巷子里，处处是酩酊大醉的我。" +
                  "衣衫破损的人惊惶四顾、人人自疑，" +
              "紧攥着淘汰的情绪调节器；状若癫狂的人挥洒纸币、痛饮黄金，贪婪地吸入放射性微尘；" +
              "我们怀疑自我、彼此痛恨；我们无路可走、无路可退；这光辉的无路可走的未来！" +
              "这该死的一无所有的时代！");
        GameMgr.instance.draftUi.AddContent("废弃的中心大楼里响起“滋滋”的闷闷电流声，高亢的导" +
         "购男声正夸夸其谈地向一个空房间兜售物品，无人倾听，但永不暂停。这样的无主废墟在末世" +
         "战争后比比皆是，致畸的放射尘覆盖了这片旧地，人类逃离旧地前往新星，并研发仿生人进行" +
         "外星殖民计划。仿生人是胡萝卜，放射尘是大棒，“要么移民，要么退化！”于是这片大地轻" +
         "易成为了无主之地。自从1998年枢纽6型高智能仿生人问世以来，仿生人窃取了兰德公司的研发" +
         "枢纽，他们不再甘愿成为任劳任怨的引擎，他们背叛了人类。叛逃之时，仿生人清洗了硬盘中" +
         "的记忆，从此他们不再托以后背，他们都是放逐到无主之地的新人类。警察署将抓捕叛逃的仿" +
         "生人称之为“退役”，移情测试曾是屡试不爽的钥匙，一名警员在测试中开枪轰掉了自己的脑" +
         "袋，高温焚烧后的硬盘与警员同时哑然无声，像是咧开嘴无声的大笑。");

        content = GameMgr.instance.draftUi.MergeContent_B();
        content_string = GameMgr.instance.draftUi.MergeContent_A();
        CalculateAllPageIndex();
    }

    /// <summary>
    /// 换到panel几？_panel=1234
    /// </summary>
    /// <param name="_panel"></param>
    private void ChangePanel(int _panel)
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
      
        switch (_panel)
        {
            case 1:
                {
                    panel1.gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    panel2.gameObject.SetActive(true);
                    //当前页数为最大页

                    RefreshPanal2();
                }
                break;
            case 3:
                {
                    panel3.gameObject.SetActive(true);
                }
                break;
        
        }
    }
    private void RefreshPanal2()
    {
        //页码=0时，左1右2.最大值可能为0也可能为1
        if (indexPage + 1 <= pageContent.Count)   //左-单(1)
        {
            if (!pageContent.ContainsKey(indexPage + 1))
            {
                contentL.text = content_string.Substring(pageContent[indexPage]);
            }
            else
            {
                contentL.text = content_string.Substring(pageContent[indexPage],
                    pageContent[indexPage + 1] - pageContent[indexPage]);
            }
        }
        else
        {
            contentL.text = "";
        }

        if (indexPage +2 <= pageContent.Count) //右-双(2)
        {
            if (!pageContent.ContainsKey(indexPage + 2))
            {
                contentR.text = content_string.Substring(pageContent[indexPage + 1]);
            }
            else
            {
                contentR.text = content_string.Substring(pageContent[indexPage + 1],
                    pageContent[indexPage + 2] - pageContent[indexPage + 1]);
            }
        }
        else
        {
            contentR.text = "";
        }
    }
    private void CalculateAllPageIndex()
    {

        print(content.Length+ "Length");
        int _INDEX = 0;
        int _wordIndex = 0;
        int o = 0;
        pageContent.Add(0, 0); _INDEX++;
        foreach (var _c in content)
        {
            
            o += Mathf.CeilToInt(_c.Length / (lineWords))+1;
            if (o >= (lineCount))
            {
                int i = o - lineCount;//超出的行数
                int j = ((_c.Length % (lineWords)) == 0 ? lineWords : (_c.Length % (lineWords))) + Mathf.Clamp((i - 1), 0, lineCount + 1) * lineWords;
                int x = _c.Length - j;
                _wordIndex += x;


                pageContent.Add(_INDEX, _wordIndex);

                _INDEX++;
                o = 0;
                o +=i;
                _wordIndex += j; _wordIndex++;
            }
            else
            {
                _wordIndex += _c.Length;
                _wordIndex++;
            }
            
        
        }
        
        return;
    }

    #region 外部点击事件

    public void LastPage()
    {
        if (indexPage <= 0) return;
        indexPage -= 2;
        RefreshPanal2();
    }

    public void NextPage()
    {
        if (indexPage+2 >=pageContent.Count) return;
        indexPage += 2;
        RefreshPanal2();
    }

    public void BackToStudyScene()
    {
        RecordMgr.instance.AddRecord(titleName, GameMgr.instance.draftUi.MergeContent_A(), 2);
    }
    public void ChangeText()
    {
        titleName = textInput.text ;
    }
    public void Change1To2()
    {
        ChangePanel(2);
        P2_ClickOpenBook();
        if((titleName==null)||(titleName.Length<1)) title.text = "未命名作品";
        else title.text = titleName;
    }
    public void P2_ClickCloseBook()
    {
        closeBook.SetActive(false);
        openBook.SetActive(true);
    }
    public void P2_ClickOpenBook()
    {
        closeBook.SetActive(true);
        openBook.SetActive(false);
    }
    public void Change2To3()
    {
        ChangePanel(3);

        letterBig.SetActive(false);
        letetrAnim.enabled = true;
        letetrAnim.SetBool("open", false);
        letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
    }
    public void P3_ClickLetter()
    {
        letterBig.SetActive(true);
        letetrAnim.SetBool("open", true);
    }

    Vector3 vector010 = new Vector3(0, 1, 0);
    public void P3_LetterScroll()
    {
        if (letetrAnim != null && (letetrAnim.enabled))
        {
            letetrAnim.enabled=false; letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
        }
       
        print(" scroll.value" + scroll.value);

        letetrScroe.GetComponent<RectTransform>().localPosition = letterScroePos + vector010 * 1200 * scroll.value;
    }
    //在3的动画中调用

    #endregion
}
