using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Diagnostics;
using UnityEngine.UIElements;

public class BookShelf : MonoBehaviour
{
    [Header("�ֶ������鼮˳��Ҫ��panel˳���Ӧ��")]
    public PanelInstance[] panels;
    [Header("���淵�ذ�ť")]
    public GameObject back;

    [Header("����Ԥ���壨�ֶ���")]
    public GameObject prefab_wordinf;
    public GameObject prefab_chara;
    public GameObject prefab_setting;

    public Color choosenColor = Color.grey;
    private int cardCount = 4;

    private bool b_chara = false;
    private bool b_setting = false;
    private bool b_verb = false;
    private bool b_noun = false;
    private bool b_adj = false;

    public UnityEngine.UI.Image sp_bottom;
    public UnityEngine.UI.Image sp_top;
    private string sp_resName= "WordImage/BookShelf/";
    //private UnityEngine.UI.Toggle toggle;
    void Start()
    {
        if (panels[0] != null)
            panels[0].gameObject.SetActive(false);
        /*for(int i = 0; i < books.Length; i++)
        {
            books[i].GetComponent<UnityEngine.UI.Image>().alphaHitTestMinimumThreshold = 0.5f;
        }*/
    }



    //�����е�ѡ���ǩ
    void OpenAllTag()
    {
        b_chara = true;
        b_setting = true;
        b_verb = true;
        b_noun = true;
        b_adj = true;
        RefreshTag();
        panels[0].transform.Find("tag_juese").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        panels[0].transform.Find("tag_xingrongci").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        panels[0].transform.Find("tag_mingci").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        panels[0].transform.Find("tag_dongci").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        panels[0].transform.Find("tag_sheding").GetComponent<UnityEngine.UI.Image>().color = Color.white;
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
                    {b_chara = false; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                else
                    { b_chara = true; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                RefreshTag();
            }
            break;
            case "tag_xingrongci":
                {
                    if (b_adj)
                    { b_adj = false; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    else
                    { b_adj = true; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    RefreshTag();
                }
                break;
            case "tag_mingci":
                {
                    if (b_noun)
                    { b_noun = false; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    else
                    { b_noun = true; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    RefreshTag();
                }
                break;
            case "tag_dongci":
                {
                    if (b_verb)
                    { b_verb = false; _name.GetComponent<UnityEngine.UI.Image>().color = choosenColor; }
                    else
                    { b_verb = true; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    RefreshTag();
                }
                break;
            case "tag_sheding":
                {
                    if (b_setting)
                    { b_setting = false; _name.GetComponent<UnityEngine.UI.Image>().color =choosenColor; }
                    else
                    { b_setting = true; _name.GetComponent<UnityEngine.UI.Image>().color = Color.white; }
                    RefreshTag();
                }
                break;
        }
    }


    GameObject buttonSelf = null;
    float pageCount = 0;
    int nowPage = 1;

    /// <summary>
    /// ѡ������鱾ҳ��
    /// </summary>
    public void ClickBook()
    {
        back.GetComponent<UnityEngine.UI.Button>().interactable = false;
        buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(buttonSelf!=null)
        {
            OpenAllTag();
        }
        //����ȡ��
        //pageCount = panels[0].gameObject.transform.Find("wordPL").childCount / cardCount;
        float a = panels[0].gameObject.transform.Find("wordPL").childCount;
        float b = cardCount;
        pageCount = Mathf.CeilToInt(a / b);
        nowPage = 1;
    }

    void CreateWordFromTag()
    {
       
        if (buttonSelf.name == BookNameEnum.HongLouMeng.ToString())
        {//�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.HongLouMeng.ToString()+"_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.HongLouMeng.ToString() + "_T");
            panels[0].gameObject.SetActive(true);

            //��ɫ
            if(b_chara)
                CharaShow(AllSkills.hlmList_chara, BookNameEnum.HongLouMeng);
        //��+��+��
            if (b_noun)
                InsWords(AllSkills.hlmList_noun, WordKindEnum.noun, BookNameEnum.HongLouMeng);//231
            if (b_verb)
                InsWords(AllSkills.hlmList_verb, WordKindEnum.verb, BookNameEnum.HongLouMeng);
            if (b_adj)
                InsWords(AllSkills.hlmList_adj, WordKindEnum.adj, BookNameEnum.HongLouMeng);
            //�趨(�Ȳ�д)

        }
        else if (buttonSelf.name == BookNameEnum.ZooManual.ToString())
        {
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.ZooManual.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.ZooManual.ToString() + "_T");
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);

            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.animalList_chara, BookNameEnum.ZooManual);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.animalList_noun, WordKindEnum.noun, BookNameEnum.ZooManual);//231
            if (b_verb)
                InsWords(AllSkills.animalList_verb, WordKindEnum.verb, BookNameEnum.ZooManual);
            if (b_adj)
                InsWords(AllSkills.animalList_adj, WordKindEnum.adj, BookNameEnum.ZooManual);
        }
        else if (buttonSelf.name == BookNameEnum.EgyptMyth.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);
    
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.EgyptMyth.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.EgyptMyth.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.aiJiShenHuaList_chara, BookNameEnum.EgyptMyth);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun, BookNameEnum.EgyptMyth);//231
            if (b_verb)
                InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb, BookNameEnum.EgyptMyth);
            if (b_adj)
                InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj, BookNameEnum.EgyptMyth);
        }
        else if (buttonSelf.name == BookNameEnum.Salome.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);

            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.Salome.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.Salome.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.shaLeMeiList_chara, BookNameEnum.Salome);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun, BookNameEnum.Salome);//231
            if (b_verb)
                InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb, BookNameEnum.Salome);
            if (b_adj)
                InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj, BookNameEnum.Salome);
        }
        else if (buttonSelf.name == BookNameEnum.CrystalEnergy.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);
 
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.CrystalEnergy.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.CrystalEnergy.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.crystalList_chara, BookNameEnum.CrystalEnergy);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.crystalList_noun, WordKindEnum.noun, BookNameEnum.CrystalEnergy);//231
            if (b_verb)
                InsWords(AllSkills.crystalList_verb, WordKindEnum.verb, BookNameEnum.CrystalEnergy);
            if (b_adj)
                InsWords(AllSkills.crystalList_adj, WordKindEnum.adj, BookNameEnum.CrystalEnergy);
        }
        else if (buttonSelf.name == BookNameEnum.PHXTwist.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);

            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.PHXTwist.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.PHXTwist.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.maYiDiGuoList_chara, BookNameEnum.PHXTwist);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun, BookNameEnum.PHXTwist);//231
            if (b_verb)
                InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb, BookNameEnum.PHXTwist);
            if (b_adj)
                InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj, BookNameEnum.PHXTwist);
        }
        else if (buttonSelf.name == BookNameEnum.ElectronicGoal.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);

            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.ElectronicGoal.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.ElectronicGoal.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.humanList_chara, BookNameEnum.ElectronicGoal);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.humanList_noun, WordKindEnum.noun, BookNameEnum.ElectronicGoal);//231
            if (b_verb)
                InsWords(AllSkills.humanList_verb, WordKindEnum.verb, BookNameEnum.ElectronicGoal);
            if (b_adj)
                InsWords(AllSkills.humanList_adj, WordKindEnum.adj, BookNameEnum.ElectronicGoal);
        }
        else if (buttonSelf.name == BookNameEnum.allBooks.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.allBooks.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.allBooks.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.commonList_chara, BookNameEnum.allBooks);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.commonList_noun, WordKindEnum.noun, BookNameEnum.allBooks);//231
            if (b_verb)
                InsWords(AllSkills.commonList_verb, WordKindEnum.verb, BookNameEnum.allBooks);
            if (b_adj)
                InsWords(AllSkills.commonList_adj, WordKindEnum.adj, BookNameEnum.allBooks);
        }
        else if (buttonSelf.name == BookNameEnum.FluStudy.ToString())
        {
            //�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
            panels[0].gameObject.SetActive(true);
            sp_bottom.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.FluStudy.ToString() + "_B");
            sp_top.sprite = ResMgr.GetInstance().Load<Sprite>(sp_resName + BookNameEnum.FluStudy.ToString() + "_T");
            //��ɫ
            if (b_chara)
                CharaShow(AllSkills.liuXingBXList_chara, BookNameEnum.FluStudy);
            //��+��+��
            if (b_noun)
                InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun, BookNameEnum.FluStudy);//231
            if (b_verb)
                InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb, BookNameEnum.FluStudy);
            if (b_adj)
                InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj, BookNameEnum.FluStudy);
            
    }
    }
    /// <summary>
    /// ��ҳ
    /// </summary>
    public void SwitchPanel()
    {
        buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
        if (buttonSelf.name == "rightbtn"&&nowPage<pageCount)//�Ҽ�
        {
            
           for (int i = (nowPage-1)*cardCount; i < nowPage*cardCount; i++)//��һҳ������
           {
                panels[0].gameObject.transform.Find("wordPL").GetChild(i).gameObject.SetActive(false);
                panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
           }
           for (int i = nowPage * cardCount; i < (nowPage+1) * cardCount; i++)//ͬʱ�ڶ�ҳչ��
           {
                if(panels[0].gameObject.transform.Find("wordPL").childCount> i)
                panels[0].gameObject.transform.Find("wordPL").GetChild(i).gameObject.SetActive(true);
                if (panels[0].gameObject.transform.Find("wordPR").childCount > i) 
                panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(true);
           }
            nowPage++;
            print(nowPage);
            print(pageCount);
        }
        else if(buttonSelf.name == "leftbtn" && nowPage >1)//���
        {
            for (int i = (nowPage - 1) * cardCount; i < nowPage * cardCount; i++)//�ڶ�ҳ����
            {
                if (panels[0].gameObject.transform.Find("wordPL").childCount > i)
                    panels[0].gameObject.transform.Find("wordPL").GetChild(i).gameObject.SetActive(false);
                if (panels[0].gameObject.transform.Find("wordPR").childCount > i)
                    panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
            }
            for (int i = (nowPage-2) * cardCount; i < (nowPage - 1) * cardCount; i++)//ͬʱ��һҳ����
            {
                panels[0].gameObject.transform.Find("wordPL").GetChild(i).gameObject.SetActive(true);
                panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(true);
            }
            nowPage--;
        }
    }

    /// <summary>
    /// ����ʾ���������д����ջ�
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
        back.GetComponent<UnityEngine.UI.Button>().interactable = true;
        var toggleSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        DeleteWords();
        //����toggle��ison����false
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
        /// չʾ��nowbook�е����д�
        /// </summary>231
        /// <param name="i">���ԡ�1adj,2noun,3verb</param>
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
    /// �����е������δ�����ʾ����
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
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < cardCount)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                //���غ���ҳ��Ŀ���
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                                //�Ҳ�����panel������������
                                if (panels[0].gameObject.transform.Find("wordPR").childCount >= panels[0].gameObject.transform.Find("wordPL").childCount && panels[0].gameObject.transform.Find("wordPR").childCount % cardCount != 0)
                                {
                                    for (int i = panels[0].gameObject.transform.Find("wordPL").childCount - 1; i < panels[0].gameObject.transform.Find("wordPL").childCount + 3; i++)//����
                                    {
                                        obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.15f;
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
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < cardCount)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                //���غ���ҳ��Ŀ���
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                                //�Ҳ�����panel������������
                                if (panels[0].gameObject.transform.Find("wordPR").childCount >= panels[0].gameObject.transform.Find("wordPL").childCount && panels[0].gameObject.transform.Find("wordPR").childCount % cardCount != 0)
                                {
                                    for (int i = panels[0].gameObject.transform.Find("wordPL").childCount - 1; i < panels[0].gameObject.transform.Find("wordPL").childCount + 3; i++)//����
                                    {
                                        obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.15f;
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
                            if (panels[0].gameObject.transform.Find("wordPL").childCount < cardCount)
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                            }
                            else
                            {
                                obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                                //���غ���ҳ��Ŀ���
                                if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                                {
                                    for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                                    {
                                        panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                                //�Ҳ�����panel������������
                                if (panels[0].gameObject.transform.Find("wordPR").childCount >= panels[0].gameObject.transform.Find("wordPL").childCount && panels[0].gameObject.transform.Find("wordPR").childCount % cardCount != 0)
                                {
                                    for (int i = panels[0].gameObject.transform.Find("wordPL").childCount - 1; i < panels[0].gameObject.transform.Find("wordPL").childCount + 3; i++)//����
                                    {
                                        obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                                    }
                                }
                            }
                            obj.transform.localScale = Vector3.one * 0.15f;
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
                //������Ϣ
                obj.GetComponent<CharacterDetail>().OpenName(chara.Name);
                //
                if (panels[0].gameObject.transform.Find("wordPL").childCount < cardCount)
                {
                    obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                }
                else
                {
                    obj.transform.parent = panels[0].gameObject.transform.Find("wordPR");
                    //���غ���ҳ��Ŀ���
                    if (panels[0].gameObject.transform.Find("wordPR").childCount > cardCount)
                    {
                        for (int i = cardCount; i < panels[0].gameObject.transform.Find("wordPR").childCount; i++)
                        {
                            panels[0].gameObject.transform.Find("wordPR").GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    //�Ҳ�����panel������������
                    if (panels[0].gameObject.transform.Find("wordPR").childCount >= panels[0].gameObject.transform.Find("wordPL").childCount&& panels[0].gameObject.transform.Find("wordPR").childCount%cardCount!=0)
                    {
                        for (int i = panels[0].gameObject.transform.Find("wordPL").childCount-1; i < panels[0].gameObject.transform.Find("wordPL").childCount + 3; i++)//����
                        {
                            obj.transform.parent = panels[0].gameObject.transform.Find("wordPL");
                        }
                    }
                }
                obj.transform.localScale = Vector3.one * 0.5f;
            });
        }
    }
}
