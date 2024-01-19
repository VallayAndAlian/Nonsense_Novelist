using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class BookDesk : MonoBehaviour
{
    [Header("ɸѡ��ťԤ���壨�ֶ���")]
    public Toggle toggle_verb;
    public Toggle toggle_noun;
    public Toggle toggle_adj;

    [Header("����Ԥ���壨�ֶ���")]
    public GameObject prefab_noun;
    public GameObject prefab_verb;
    public GameObject prefab_adj;

    [Header("�����ʹ�����")]
    public Transform bookParent;
    public Transform wordParent;
    private BookNameEnum nowBook=BookNameEnum.HongLouMeng;
    private int nowBookIndex = 0;
    private string bookIconAdr = "WordImage/Book/";
    //�����·����鱾�б��˳��
    private BookNameEnum[] bookList = new BookNameEnum[8] { BookNameEnum.HongLouMeng,BookNameEnum.ZooManual,BookNameEnum.EgyptMyth,BookNameEnum.Salome,
    BookNameEnum.CrystalEnergy,BookNameEnum.ElectronicGoal,BookNameEnum.FluStudy,BookNameEnum.PHXTwist};

    private int booklistAmount=0;
    private void Start()
    {
        toggle_verb.isOn = true;
        toggle_noun.isOn = true;
        toggle_adj.isOn = true;
        RefreshWord();
        SwitchBookList();
        }
    private void RefreshWord()
    {
        DeleteWords();
        if (toggle_verb.isOn)
        {
            ShowNowBookWord(3);

        }
            if (toggle_noun.isOn)
        {
            ShowNowBookWord(2);
        }
        if (toggle_adj.isOn)
        {
            ShowNowBookWord(1);
        }
    }
    /// <summary>
    /// ����ʾ���������д����ջ�
    /// </summary>
    void DeleteWords()
    {

        for (int i = wordParent.childCount - 1; i >= 0; i--)
        {
            foreach (var a in wordParent.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(a);
            }
            PoolMgr.GetInstance().PushObj(wordParent.GetChild(i).gameObject.name, wordParent.GetChild(i).gameObject);
        }

    }


    /// <summary>
    /// չʾ��nowbook�е����д�
    /// </summary>
    /// <param name="i">���ԡ�1adj,2noun,3verb</param>
    void ShowNowBookWord(int i)
    {
        switch (nowBook)
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
        }
    }


    /// <summary>
        /// �����е����д�����ʾ����
        /// </summary>
    void InsWords(List<Type> wordsList, WordKindEnum wordKind)
        {
            switch (wordKind)
            {
                case WordKindEnum.adj:
                    {
                        foreach (var word in wordsList)
                        {
                            PoolMgr.GetInstance().GetObj(prefab_adj, (obj) =>
                            {
                                var _word = obj.AddComponent(word) as AbstractWord0;
                                obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                                obj.transform.parent = wordParent;
                                obj.transform.localScale = Vector3.one*1.5f;
                            });
                        }
                    }
                    break;
                case WordKindEnum.noun:
                    {
                        foreach (var word in wordsList)
                        {
                            PoolMgr.GetInstance().GetObj(prefab_noun, (obj) =>
                            {
                                var _word = obj.AddComponent(word) as AbstractWord0;
                                obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                                obj.transform.parent = wordParent;
                                obj.transform.localScale = Vector3.one * 1.5f;
                            });
                        }
                    }
                    break;
                case WordKindEnum.verb:
                    {
                        foreach (var word in wordsList)
                        {
                            PoolMgr.GetInstance().GetObj(prefab_verb, (obj) =>
                            {
                                var _word = obj.AddComponent(word) as AbstractWord0;
                                obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                                obj.transform.parent = wordParent;
                                obj.transform.localScale = Vector3.one * 1.5f;
                            });
                        }
                    }
                    break;


            }


        }

    /// <summary>
    /// ˢ���鱾�б�(����ˢ��ѡ��״̬)
    /// </summary>
    void SwitchBookList()
    {
        int _temp = 0;
        for (int i = 0; i <4; i++)
        {
            _temp = booklistAmount + i;
            if (booklistAmount + i > 7) _temp -= 8;
            Sprite _s= Resources.Load<Sprite>(bookIconAdr + bookList[_temp].ToString());
            if(_s==null) _s=Resources.Load<Sprite>(bookIconAdr + "HongLouMeng");
            bookParent.GetChild(i).GetComponent<Image>().sprite = _s;
            if (_temp == nowBookIndex)
                bookParent.GetChild(i).GetComponent<Image>().color = Color.grey;
            else
                bookParent.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }

    /// <summary>
    ///����ֻˢ��ѡ��״̬)
    /// </summary>
    void ChoosenBookColor()
    {
        int _temp = 0;
        for (int i = 0; i < 4; i++)
        {
            _temp = booklistAmount + i;
            if (booklistAmount + i > 7) _temp -= 8;
            if(_temp== nowBookIndex)
                bookParent.GetChild(i).GetComponent<Image>().color = Color.grey;
            else
                bookParent.GetChild(i).GetComponent<Image>().color = Color.white;
        }
      
    }
        #region �ⲿ����
    /// <summary>
    /// ���������Ե�toggle�仯ʱ��ˢ�´����б�
    /// </summary>
        public void ToggleChange()
        {
            RefreshWord();
        }


    /// <summary>
    /// ��left Button����Ӧ�¼�
    /// </summary>
    public void ClickLeft()
    {
        if (booklistAmount == 0) booklistAmount = 7;
        else booklistAmount -= 1;
        SwitchBookList();
    }



    /// <summary>
    /// ��left Button����Ӧ�¼�
    /// </summary>
    public void ClickRight()
    {
        if (booklistAmount == 7) booklistAmount = 0;
        else booklistAmount += 1;
        SwitchBookList();
    }

    
    public void ClickBook1()
    {
        int _temp = booklistAmount + 0;
        if (booklistAmount + 0 > 7) _temp -= 8;
        nowBook = bookList[_temp];
        nowBookIndex = _temp;
        RefreshWord();
        ChoosenBookColor();
    }
    public void ClickBook2()
    {
        int _temp = booklistAmount + 1;
        if (booklistAmount + 1 > 7) _temp -= 8;
        nowBook = bookList[_temp];
        nowBookIndex = _temp;
        RefreshWord();
        ChoosenBookColor();
    }
    public void ClickBook3()
    {
        int _temp = booklistAmount + 2;
        if (booklistAmount + 2 > 7) _temp -= 8;
        nowBook = bookList[_temp];
        nowBookIndex = _temp;
        RefreshWord();
        ChoosenBookColor();
    }
    public void ClickBook4()
    {
        int _temp = booklistAmount + 3;
        if (booklistAmount + 3 > 7) _temp -= 8;
        nowBook = bookList[_temp];
        nowBookIndex = _temp;
        RefreshWord();
        ChoosenBookColor();
    }

    #endregion
}
