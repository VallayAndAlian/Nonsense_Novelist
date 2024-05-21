using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Diagnostics;
using UnityEngine.UIElements;


public class BookShelf : MonoBehaviour
{
    [Header("手动设置书籍顺序（要与panel顺序对应）")]
    public Button[] books;
    public PanelInstance[] panels;

    [Header("词条预制体（手动）")]
    public GameObject prefab_wordinf;
    public GameObject prefab_chara;
    public GameObject prefab_setting;
    public TextMeshProUGUI text;
    public Color choosenColor = Color.grey;
    private int cardCount = 9;

    private bool b_chara = false;
    private bool b_setting = false;
    private bool b_verb = false;
    private bool b_noun = false;
    private bool b_adj = false;

    //private UnityEngine.UI.Toggle toggle;
    void Start()
    {

    }



    //打开所有的选择标签
    void OpenAllTag()
    {
        b_chara = true;
        b_setting = true;
        b_verb = true;
        b_noun = true;
        b_adj = true;
        RefreshTag();
        panels[0].transform.Find("tag_juese").GetComponent<UnityEngine.UI.Image>().color = choosenColor;
        panels[0].transform.Find("tag_xingrongci").GetComponent<UnityEngine.UI.Image>().color = choosenColor;
        panels[0].transform.Find("tag_mingci").GetComponent<UnityEngine.UI.Image>().color = choosenColor;
        panels[0].transform.Find("tag_dongci").GetComponent<UnityEngine.UI.Image>().color = choosenColor;
        panels[0].transform.Find("tag_sheding").GetComponent<UnityEngine.UI.Image>().color = choosenColor;
    }

    void RefreshTag()
    {
        DeleteWords();
        CreateWordFromTag();
    }

    public void ClickToggle(GameObject _name)
    {
        switch (_name.name)
        {
            case "tag_juese":
            {
                if (b_chara)
                    {b_chara = false; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                else
                    { b_chara = true; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                RefreshTag();
            }
            break;
            case "tag_xingrongci":
                {
                    if (b_adj)
                    { b_adj = false; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    else
                    { b_adj = true; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    RefreshTag();
                }
                break;
            case "tag_mingci":
                {
                    if (b_noun)
                    { b_noun = false; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    else
                    { b_noun = true; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    RefreshTag();
                }
                break;
            case "tag_dongci":
                {
                    if (b_verb)
                    { b_verb = false; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    else
                    { b_verb = true; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    RefreshTag();
                }
                break;
            case "tag_sheding":
                {
                    if (b_setting)
                    { b_setting = false; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    else
                    { b_setting = true; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    RefreshTag();
                }
                break;
        }
    }


    GameObject buttonSelf = null;
    /// <summary>
    /// 选书进入书本页面
    /// </summary>
    public void ClickBook()
    {
        buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(buttonSelf!=null)
        {
            OpenAllTag();
        }
        
    }

    void CreateWordFromTag()
    {
       
        if (buttonSelf.name == BookNameEnum.HongLouMeng.ToString())
        {//打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "红楼梦";
            //角色
            if(b_chara)
                CharaShow(AllSkills.hlmList_chara, BookNameEnum.HongLouMeng);
        //名+动+形
            if (b_noun)
                InsWords(AllSkills.hlmList_noun, WordKindEnum.noun, BookNameEnum.HongLouMeng);//231
            if (b_verb)
                InsWords(AllSkills.hlmList_verb, WordKindEnum.verb, BookNameEnum.HongLouMeng);
            if (b_adj)
                InsWords(AllSkills.hlmList_adj, WordKindEnum.adj, BookNameEnum.HongLouMeng);
            //设定(先不写)

        }
        else if (buttonSelf.name == BookNameEnum.ZooManual.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "动物园丰容手册";
            //角色
            if (b_chara)
                CharaShow(AllSkills.animalList_chara, BookNameEnum.ZooManual);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.animalList_noun, WordKindEnum.noun, BookNameEnum.ZooManual);//231
            if (b_verb)
                InsWords(AllSkills.animalList_verb, WordKindEnum.verb, BookNameEnum.ZooManual);
            if (b_adj)
                InsWords(AllSkills.animalList_adj, WordKindEnum.adj, BookNameEnum.ZooManual);
        }
        else if (buttonSelf.name == BookNameEnum.EgyptMyth.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "埃及神话";
            //角色
            if (b_chara)
                CharaShow(AllSkills.aiJiShenHuaList_chara, BookNameEnum.EgyptMyth);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun, BookNameEnum.EgyptMyth);//231
            if (b_verb)
                InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb, BookNameEnum.EgyptMyth);
            if (b_adj)
                InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj, BookNameEnum.EgyptMyth);
        }
        else if (buttonSelf.name == BookNameEnum.Salome.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "莎乐美";
            //角色
            if (b_chara)
                CharaShow(AllSkills.shaLeMeiList_chara, BookNameEnum.Salome);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun, BookNameEnum.Salome);//231
            if (b_verb)
                InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb, BookNameEnum.Salome);
            if (b_adj)
                InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj, BookNameEnum.Salome);
        }
        else if (buttonSelf.name == BookNameEnum.CrystalEnergy.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "水晶能量疗愈";
            //角色
            if (b_chara)
                CharaShow(AllSkills.crystalList_chara, BookNameEnum.CrystalEnergy);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.crystalList_noun, WordKindEnum.noun, BookNameEnum.CrystalEnergy);//231
            if (b_verb)
                InsWords(AllSkills.crystalList_verb, WordKindEnum.verb, BookNameEnum.CrystalEnergy);
            if (b_adj)
                InsWords(AllSkills.crystalList_adj, WordKindEnum.adj, BookNameEnum.CrystalEnergy);
        }
        else if (buttonSelf.name == BookNameEnum.PHXTwist.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "蚂蚁帝国";
            //角色
            if (b_chara)
                CharaShow(AllSkills.maYiDiGuoList_chara, BookNameEnum.PHXTwist);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun, BookNameEnum.PHXTwist);//231
            if (b_verb)
                InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb, BookNameEnum.PHXTwist);
            if (b_adj)
                InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj, BookNameEnum.PHXTwist);
        }
        else if (buttonSelf.name == BookNameEnum.ElectronicGoal.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "仿生人会梦到电子羊吗";
            //角色
            if (b_chara)
                CharaShow(AllSkills.humanList_chara, BookNameEnum.ElectronicGoal);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.humanList_noun, WordKindEnum.noun, BookNameEnum.ElectronicGoal);//231
            if (b_verb)
                InsWords(AllSkills.humanList_verb, WordKindEnum.verb, BookNameEnum.ElectronicGoal);
            if (b_adj)
                InsWords(AllSkills.humanList_adj, WordKindEnum.adj, BookNameEnum.ElectronicGoal);
        }
        else if (buttonSelf.name == BookNameEnum.allBooks.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "字典";
            //角色
            if (b_chara)
                CharaShow(AllSkills.commonList_chara, BookNameEnum.allBooks);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.commonList_noun, WordKindEnum.noun, BookNameEnum.allBooks);//231
            if (b_verb)
                InsWords(AllSkills.commonList_verb, WordKindEnum.verb, BookNameEnum.allBooks);
            if (b_adj)
                InsWords(AllSkills.commonList_adj, WordKindEnum.adj, BookNameEnum.allBooks);
        }
        else if (buttonSelf.name == BookNameEnum.FluStudy.ToString())
        {
            //打开红楼梦页面+默认显示全部红楼梦词条
            panels[0].gameObject.SetActive(true);
            text.text = "流行病毒";
            //角色
            if (b_chara)
                CharaShow(AllSkills.liuXingBXList_chara, BookNameEnum.FluStudy);
            //名+动+形
            if (b_noun)
                InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun, BookNameEnum.FluStudy);//231
            if (b_verb)
                InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb, BookNameEnum.FluStudy);
            if (b_adj)
                InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj, BookNameEnum.FluStudy);
            
    }
    }
    int clickCount = 0;
    /// <summary>
    /// 翻页
    /// </summary>
    public void SwitchPanel()
    {
        buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        clickCount = panels[0].gameObject.transform.childCount / 2 * cardCount;
        print(panels[0].gameObject.transform.Find("wordPR").childCount);
        if (buttonSelf.name == "rightbtn")//右键
        {
                for (int i = 0; i < cardCount; i++)//第一页的隐藏
                {
                    panels[0].gameObject.transform.Find("wordPL").GetChild(i).gameObject.SetActive(false);
                }
        }
        else//左键
        {
            if(clickCount< panels[0].gameObject.transform.childCount / 2 * cardCount) { }
        }
    }

    /// <summary>
    /// 将显示出来的所有词条收回
    /// </summary>
    void DeleteWords()
    {
        var toggleSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Transform a= toggleSelf.gameObject.transform.parent.Find("wordPL");
        if (a == null) return;
        Transform b = toggleSelf.gameObject.transform.parent.Find("wordPR");
        if (b == null) return;

        if (a.childCount == 0) return;
        for (int i = a.childCount - 1; i >= 0; i--)
        {
            foreach (var mm in a.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(mm);
            }
            PoolMgr.GetInstance().PushObj(a.GetChild(i).gameObject.name, a.GetChild(i).gameObject);
        }
        for (int i = b.childCount - 1; i >= 0; i--)
        {
            foreach (var mm in b.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(mm);
            }
            PoolMgr.GetInstance().PushObj(b.GetChild(i).gameObject.name, b.GetChild(i).gameObject);
        }
    }
    


    public void CloseHLMPanel()
    {
        var toggleSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        DeleteWords();
        //所有toggle的ison调成false
        UnityEngine.UI.Toggle[] tog = toggleSelf.gameObject.transform.parent.GetComponentsInChildren<UnityEngine.UI.Toggle>();
        foreach (var mm in tog)
        {
            mm.isOn = false;
        }
        switch (toggleSelf.gameObject.transform.parent.name)
        {
            case "HLM":
                {
                    panels[0].gameObject.SetActive(false);
                }
                break;
            case "JJJ":
                {
                    panels[0].gameObject.SetActive(false);
                }
                break;
        }

    }

    /*    /// <summary>
        /// 展示出nowbook中的所有词
        /// </summary>231
        /// <param name="i">词性。1adj,2noun,3verb</param>
        void ShowNowBookWord(int i, BookNameEnum _book)
        {
            switch (_book)
            {
                case BookNameEnum.HongLouMeng:
                    {
                        if (i == 1)
                            InsWords(AllSkills.hlmList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.hlmList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.hlmList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.CrystalEnergy:
                    {
                        if (i == 1)
                            InsWords(AllSkills.crystalList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.crystalList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.crystalList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.ZooManual:
                    {
                        if (i == 1)
                            InsWords(AllSkills.animalList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.animalList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.animalList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.PHXTwist:
                    {
                        if (i == 1)
                            InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.FluStudy:
                    {
                        if (i == 1)
                            InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.EgyptMyth:
                    {
                        if (i == 1)
                            InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.ElectronicGoal:
                    {
                        if (i == 1)
                            InsWords(AllSkills.humanList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.humanList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.humanList_verb, WordKindEnum.verb);
                    }
                    break;
                case BookNameEnum.Salome:
                    {
                        if (i == 1)
                            InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj);
                        else if (i == 2)
                            InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun);
                        else
                            InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb);

                    }
                    break;
            }*/
    //}
    bool isFirst = true;
    public void ToggleClick()
    {
        if(isFirst) { DeleteWords();isFirst = false; }
        

        var toggleSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Transform a = toggleSelf.gameObject.transform.parent.Find("wordPL");
        Transform b = toggleSelf.gameObject.transform.parent.Find("wordPR");
        if (toggleSelf.gameObject.transform.parent.name == "HLM")
        {
            UnityEngine.UI.Toggle jw = toggleSelf.gameObject.GetComponent<UnityEngine.UI.Toggle>();
            switch (toggleSelf.name)
            {
                case "tag_juese":
                    {
                        if (jw.isOn)
                        {
                            CharaShow(AllSkills.hlmList_chara, BookNameEnum.HongLouMeng);
                        }
                        else
                        {
                            CharacterDetail[] bb= a.GetComponentsInChildren<CharacterDetail>();
                            for (int i = bb.Length - 1; i >= 0; i--)
                            {
                                PoolMgr.GetInstance().PushObj(bb[i].gameObject.name, bb[i].gameObject);
                            }
                        }
                    }break;
                case "tag_mingci":
                    {
                        if (jw.isOn)
                        {
                            InsWords(AllSkills.hlmList_noun, WordKindEnum.noun, BookNameEnum.HongLouMeng);
                        }
                        else
                        {
                            AbstractItems[] bb = a.GetComponentsInChildren<AbstractItems>();
                            for (int i = bb.Length - 1; i >= 0; i--)
                            {
                                PoolMgr.GetInstance().PushObj(bb[i].gameObject.name, bb[i].gameObject);
                            }
                        }
                    }
                    break;
                case "tag_dongci":
                    {
                        if (jw.isOn)
                        {
                            InsWords(AllSkills.hlmList_verb, WordKindEnum.verb, BookNameEnum.HongLouMeng);
                            
                        }
                        else
                        {
                            AbstractVerbs[] bb = a.GetComponentsInChildren<AbstractVerbs>();
                            for (int i = bb.Length - 1; i >= 0; i--)
                            {
                                PoolMgr.GetInstance().PushObj(bb[i].gameObject.name, bb[i].gameObject);
                            }
                        }
                    }
                    break;
                case "tag_xingrongci":
                    {
                        if (jw.isOn)
                        {
                            InsWords(AllSkills.hlmList_adj, WordKindEnum.adj, BookNameEnum.HongLouMeng);
                        }
                        else
                        {
                            AbstractAdjectives[] bb = a.GetComponentsInChildren<AbstractAdjectives>();
                            for (int i = bb.Length - 1; i >= 0; i--)
                            {
                                PoolMgr.GetInstance().PushObj(bb[i].gameObject.name, bb[i].gameObject);
                            }
                        }
                    }
                    break;
                
            }
        }
    }

    /// <summary>
    /// 将书中的名动形词条显示出来
    /// </summary>
    void InsWords(List<Type> wordsList, WordKindEnum wordKind, BookNameEnum _book)
    {
        switch (wordKind)
        {
            case WordKindEnum.adj:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                              obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.1f;
                        });
                    }
                }
                break;
            case WordKindEnum.noun:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                             obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount ; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.1f;
                        });
                    }
                }
                break;
            case WordKindEnum.verb:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                           
                            obj.GetComponentInChildren<WordInformation>().ChangeInformation(_word);
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.1f;
                        });
                    }
                }
                break;


        }


    }
    void CharaShow(List<Type> charasList,BookNameEnum bookKind)
    {
        foreach (var chara in charasList)
        {
            PoolMgr.GetInstance().GetObj(prefab_chara, (obj) =>
            {
                //卡牌信息
                obj.GetComponent<CharacterDetail>().OpenName(chara.Name);
                //
                if (panels[0].gameObject.transform.Find("wordPL").childCount < cardCount)
                {
                    obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                }
                else
                {
                    obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                    if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                    {
                        for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++) {
                            panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    
                }
                obj.transform.localScale = Vector3.one * 0.42f;
            });
        }
    }
}
