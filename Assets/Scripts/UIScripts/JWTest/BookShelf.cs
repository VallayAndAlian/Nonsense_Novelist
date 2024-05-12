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

    //private UnityEngine.UI.Toggle toggle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 选书进入书本页面
    /// </summary>
    public void ClickBook()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(buttonSelf!=null)
        {
            if (buttonSelf.name == BookNameEnum.HongLouMeng.ToString())
            {//打开红楼梦页面+默认显示全部红楼梦词条
                panels[(int)BookNameEnum.HongLouMeng].gameObject.SetActive(true);
                //角色
                CharaShow(AllSkills.hlmList_chara, BookNameEnum.HongLouMeng);
                //名+动+形
                InsWords(AllSkills.hlmList_noun, WordKindEnum.noun,BookNameEnum.HongLouMeng);//231
                InsWords(AllSkills.hlmList_verb, WordKindEnum.verb, BookNameEnum.HongLouMeng);
                InsWords(AllSkills.hlmList_adj, WordKindEnum.adj, BookNameEnum.HongLouMeng);
                //设定(先不写)

            }
            else if (buttonSelf.name == books[1].name)
            {
                panels[1].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[2].name)
            {
                panels[2].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[3].name)
            {
                panels[3].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[4].name)
            {
                panels[4].gameObject.SetActive(true);
            }
        }
        
    }
    /// <summary>
    /// 将显示出来的所有词条收回
    /// </summary>
    void DeleteWords()
    {
        var toggleSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Transform a= toggleSelf.gameObject.transform.parent.Find("wordPL");
        Transform b = toggleSelf.gameObject.transform.parent.Find("wordPR");

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
                    panels[(int)BookNameEnum.HongLouMeng].gameObject.SetActive(false);
                }
                break;
            case "JJJ":
                {
                    panels[(int)BookNameEnum.HongLouMeng].gameObject.SetActive(false);
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
                            //卡牌信息
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textAdj;
                            jiuwei.resName = obj.GetComponent<WordInformation>().resAdrAdj + "adj_" + ((AbstractAdjectives)_word).adjID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                            { jiuwei.wordImage.sprite = obj.GetComponent<WordInformation>().defaultWordImage; }
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            if (panels[(int)_book].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPR");
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
                            //卡牌信息
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textNoun;

                            jiuwei.resName = jiuwei.resAdrNoun + "n_" + ((AbstractItems)_word).itemID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                                jiuwei.wordImage.sprite = jiuwei.defaultWordImage;
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            if (panels[(int)_book].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPR");
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
                            //卡牌信息
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textVerb;

                            jiuwei.resName = jiuwei.resAdrVerb + "v_" + ((AbstractVerbs)_word).skillID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                                jiuwei.wordImage.sprite = jiuwei.defaultWordImage;
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            if (panels[(int)_book].gameObject.transform.Find("wordPL").childCount < 9)
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[(int)_book].gameObject.transform.Find("wordPR");
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
                if (panels[(int)bookKind].gameObject.transform.Find("wordPL").childCount < 9)
                {
                    obj.transform.parent = panels[(int)bookKind].gameObject.transform.Find("wordPL");
                }
                else
                {
                    obj.transform.parent = panels[(int)bookKind].gameObject.transform.Find("wordPR");
                }
                obj.transform.localScale = Vector3.one * 0.42f;
            });
        }
    }
}
