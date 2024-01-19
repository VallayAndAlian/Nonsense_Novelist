using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class NewGame : MonoBehaviour
{
    [Header("�ֶ������ĸ�����")]
    public GameObject panal1;
    public GameObject panal2;
    public GameObject panal3;
    int nowPanalIndex = 0;

    [Header("�ֶ�����ҳ���ǩ�ĸ�����(123)")]
    public Transform panalLableP;
    private Image lable1;
    private Image lable2;
    private Image lable3;

    [Header("�ֶ����ÿ���ҳ����ת�ĸ�����(����)")]
    public Transform panalChangeP;
    private Button panalLeft;
    private Button panalRight;

    [Header("�ֶ������鱾ѡ��ҳ��ĸ�����")]
    public Transform bookP;

    [Header("�ֶ����õ�ǰѡ���鱾�ĸ�����")]
    public Transform chosenBookP;
    private Image chosenBook1;
    private Image chosenBook2;

    [Header("�ֶ����ô���ɸѡ�ĸ�����(������)")]
    public Transform toggleP;
    private Toggle toggle_verb;
    private Toggle toggle_noun;
    private Toggle toggle_adj;

    [Header("�ֶ����ô�����ʾ�ĸ�����")]
    public Transform wordP;

    [Header("����Ԥ���壨�ֶ���")]
    public GameObject prefab_noun;
    public GameObject prefab_verb;
    public GameObject prefab_adj;

    private string bookIconAdr = "WordImage/Book/";
    //�����·����鱾�б��˳��
    private BookNameEnum[] bookList = new BookNameEnum[8] { BookNameEnum.HongLouMeng,BookNameEnum.ZooManual,BookNameEnum.EgyptMyth,BookNameEnum.Salome,
    BookNameEnum.CrystalEnergy,BookNameEnum.ElectronicGoal,BookNameEnum.FluStudy,BookNameEnum.PHXTwist};
    private int booklistAmount = 0;
    private List<BookNameEnum> nowBook=new List<BookNameEnum>();

    void Awake()
    {
        panalLeft = panalChangeP.GetChild(0).GetComponent<Button>();
        panalRight = panalChangeP.GetChild(1).GetComponent<Button>();
        lable1 = panalLableP.GetChild(0).GetComponent<Image>();
        lable2 = panalLableP.GetChild(1).GetComponent<Image>();
        lable3 = panalLableP.GetChild(2).GetComponent<Image>();
        toggle_verb = toggleP.GetChild(0).GetComponent<Toggle>();
        toggle_noun = toggleP.GetChild(1).GetComponent<Toggle>();
        toggle_adj = toggleP.GetChild(2).GetComponent<Toggle>();
        chosenBook1=chosenBookP.GetChild(0).GetComponent<Image>();
        chosenBook2 = chosenBookP.GetChild(1).GetComponent<Image>();
        //��������
        ChangePanal();
        nowBook.Clear();
    }


    #region �仯ҳ�������õ�
    private void ChangePanal ()
    {
        ChangePanalLable();
        ChangePanalButton();

        if (nowPanalIndex == 0)
        {
            panal1.SetActive(true);
            panal2.SetActive(false);
            panal3.SetActive(false);
        }
        else if (nowPanalIndex == 1)
        {
            panal1.SetActive(false);
            panal2.SetActive(true);
            panal3.SetActive(false);

            SwitchBookList();
        }
        else
        {
            panal1.SetActive(false);
            panal2.SetActive(false);
            panal3.SetActive(true);

            RefreshWord();
            ChangeChosenBook();
        }
    }


    private void ChangePanalButton()
    {

        if (nowPanalIndex == 0)
        { 
            panalLeft.GetComponent<Image>().color = Color.grey;
            panalRight.GetComponent<Image>().color = Color.white;
        }
        else if (nowPanalIndex == 1)
        {
            panalLeft.GetComponent<Image>().color = Color.white;
            panalRight.GetComponent<Image>().color = Color.white;
        }
        else
        {
            panalLeft.GetComponent<Image>().color = Color.white;
            panalRight.GetComponent<Image>().color = Color.clear;
        }

    }
    private void ChangePanalButton_panal1()
    {
        if (nowBook.Count != 2)
            panalRight.GetComponent<Image>().color = Color.grey;
       else
            panalRight.GetComponent<Image>().color = Color.white;

    }

    private void ChangePanalLable()
    {
        if (nowPanalIndex == 0)
        {
            lable1.color = Color.white;
            lable2.color = Color.grey;
            lable3.color = Color.grey;

        }
        else if (nowPanalIndex == 1)
        {
            lable1.color = Color.white;
            lable2.color = Color.white;
            lable3.color = Color.grey;
        }
        else
        {
            lable1.color = Color.white;
            lable2.color = Color.white;
            lable3.color = Color.white;
        }
    }
    #endregion


#region ѡ���鱾�����õ�
    /// <summary>
    /// ˢ���鱾�б�.����ˢ���鱾ѡ��״̬
    /// </summary>
    void SwitchBookList()
    {
        int _temp = 0;
        for (int i = 0; i < 4; i++)
        {
            _temp = booklistAmount + i;
            if (booklistAmount + i > 7) _temp -= 8;
            Sprite _s = Resources.Load<Sprite>(bookIconAdr + bookList[_temp].ToString());
            if (_s == null) _s = Resources.Load<Sprite>(bookIconAdr + "HongLouMeng");
            bookP.GetChild(i).GetComponent<Image>().sprite = _s;
        }
        SwitchBookColor();

    }

    /// <summary>
    /// ˢ���鱾ѡ��״̬
    /// </summary>
    void SwitchBookColor()
    {
        int _temp = 0;
        for (int i = 0; i < 4; i++)
        {
            _temp = booklistAmount + i;
            if (booklistAmount + i > 7) _temp -= 8;
            if (nowBook.Contains(bookList[_temp]))
                bookP.GetChild(i).GetComponent<Image>().color = Color.grey;
            else
                bookP.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        ChangePanalButton_panal1();
    }

    #endregion

    #region ��ʾ���������õ�
    private void RefreshWord()
    {
        DeleteWords();
        if (toggle_verb.isOn)
        {
            ShowNowBookWord(3,nowBook[0]);
            ShowNowBookWord(3, nowBook[1]);

        }
        if (toggle_noun.isOn)
        {
            ShowNowBookWord(2, nowBook[0]);
            ShowNowBookWord(2, nowBook[1]);
        }
        if (toggle_adj.isOn)
        {
            ShowNowBookWord(1, nowBook[0]);
            ShowNowBookWord(1, nowBook[1]);
        }
    }

    private void ChangeChosenBook()
    {
        Sprite _s1 = Resources.Load<Sprite>(bookIconAdr+nowBook[0].ToString());
        if (_s1 == null) _s1 = Resources.Load<Sprite>(bookIconAdr + "HongLouMeng");
        chosenBook1.sprite = _s1;
        Sprite _s2 = Resources.Load<Sprite>(bookIconAdr + nowBook[1].ToString());
        if (_s2 == null)
            _s2 = Resources.Load<Sprite>(bookIconAdr + "HongLouMeng");
        chosenBook2.sprite = _s2;
    }

    /// <summary>
    /// ����ʾ���������д����ջ�
    /// </summary>
    void DeleteWords()
    {

        for (int i = wordP.childCount - 1; i >= 0; i--)
        {
            foreach (var a in wordP.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(a);
            }
            PoolMgr.GetInstance().PushObj(wordP.GetChild(i).gameObject.name, wordP.GetChild(i).gameObject);
        }

    }


    /// <summary>
    /// չʾ��nowbook�е����д�
    /// </summary>
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
                            obj.transform.parent = wordP;
                            obj.transform.localScale = Vector3.one * 1.5f;
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
                            obj.transform.parent = wordP;
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
                            obj.transform.parent = wordP;
                            obj.transform.localScale = Vector3.one * 1.5f;
                        });
                    }
                }
                break;


        }


    }


    #endregion

    #region �ⲿ����¼�
    public void ClickPanalLeft()
    {
        if (nowPanalIndex == 0) return;
        nowPanalIndex -= 1;
        ChangePanal();
       
    }


    public void ClickPanalRight()
    {
        if (nowPanalIndex == 2) return;
        if ((nowPanalIndex == 1) && (nowBook.Count != 2)) return;
        nowPanalIndex += 1;
        ChangePanal();
        
    }



    /// <summary>
    /// ��book-left Button����Ӧ�¼�
    /// </summary>
    public void ClickLeft()
    {
        if (booklistAmount == 0) booklistAmount = 7;
        else booklistAmount -= 1;
        SwitchBookList();
    }



    /// <summary>
    /// ��book-left Button����Ӧ�¼�
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
        if (nowBook.Contains(bookList[_temp]))
        {
            nowBook.Remove(bookList[_temp]);
        }
        else
        {
            if (nowBook.Count >= 2) nowBook.RemoveAt(0);
            nowBook.Add(bookList[_temp]);
        }
            
        SwitchBookColor();
    }
    public void ClickBook2()
    {
        int _temp = booklistAmount + 1;
        if (booklistAmount + 1 > 7) _temp -= 8;
        if (nowBook.Contains(bookList[_temp]))
        {
            nowBook.Remove(bookList[_temp]);
        }
        else
        {
            if (nowBook.Count >= 2) nowBook.RemoveAt(0);
            nowBook.Add(bookList[_temp]);
        }

        SwitchBookColor();
    }
    public void ClickBook3()
    {
        int _temp = booklistAmount + 2;
        if (booklistAmount + 2 > 7) _temp -= 8;
        if (nowBook.Contains(bookList[_temp]))
        {
            nowBook.Remove(bookList[_temp]);
        }
        else
        {
            if (nowBook.Count >= 2) nowBook.RemoveAt(0);
            nowBook.Add(bookList[_temp]);
        }

        SwitchBookColor();
    }
    public void ClickBook4()
    {
        int _temp = booklistAmount + 3;
        if (booklistAmount + 3 > 7) _temp -= 8;
        if (nowBook.Contains(bookList[_temp]))
        {
            nowBook.Remove(bookList[_temp]);
        }
        else
        {
            if (nowBook.Count >= 2) nowBook.RemoveAt(0);
            nowBook.Add(bookList[_temp]);
        }

        SwitchBookColor();
    }

    public void ToggleChange()
    {
        RefreshWord();
    }

    public void CombatStart()
    {
        GameMgr.instance.SetCombatStartList(null);
        GameMgr.instance.AddBookList(nowBook[0]);
        GameMgr.instance.AddBookList(nowBook[1]);
        GameMgr.instance.AddCombatStartList(AllSkills.commonList_all);
    }

    #endregion
}
