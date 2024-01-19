using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AddBooKAndWords : MonoBehaviour
{
    [Header("���鱾��ѡһ������")]
    public Transform chooseBooksParent;
    Vector3 bookOriPos=Vector3.zero;
    int bookOriNumber = -1;
    private BookNameEnum[] book = new BookNameEnum[3];
    [Header("���鿴�鱾�����Ĵ���������-�ֶ�")]
    public Transform checkWordsParent;
    public Transform wordsArea;
    [Header("����Ԥ���壨�ֶ���")]
    public GameObject prefab_noun;
    public GameObject prefab_verb;
    public GameObject prefab_adj;

    private bool hasEnter = false;
    private void Awake()
    {
        //�رս����е���������(��ʱûд����Ϊ���񲻹�Ҳ����)


        //�ر��鱾�������棬������ѡһ����
        checkWordsParent.gameObject.SetActive(false);
        chooseBooksParent.gameObject.SetActive(true);
        chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.one;
        //
        GetBook();
    }

    /// <summary>
    /// ��ȡû�ù����顣�����������������ʾ�ڶ�����
    /// </summary>
    void GetBook()
    {
        int[] ds= new int[3]{ 0,0,0};
        book[0] = BookNameEnum.Salome;
        book[1] = BookNameEnum.EgyptMyth;
        book[2] = BookNameEnum.ZooManual;
        //��ȡû�õ���
        //����������д
        while (GameMgr.instance.HasBook(book[0]))
        {
            ds[0] = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
            book[0] = (BookNameEnum)int.Parse(ds[0].ToString()); 
            while (GameMgr.instance.HasBook(book[1]) || (book[1]==book[0]))
            {
                ds[1] = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
                book[1] = (BookNameEnum)int.Parse(ds[1].ToString());
                print(book[1] + "ds[1]=" + ds[1]);

                 while (GameMgr.instance.HasBook(book[2]) || (book[2] == book[0])|| (book[2] == book[1]))
                 {
                    ds[2] = UnityEngine.Random.Range(2, System.Enum.GetNames(typeof(BookNameEnum)).Length);
                    book[2] = (BookNameEnum)int.Parse(ds[2].ToString());
                    print(book[2] + "ds[2]=" + ds[2]);
                    }
            } 
        }

        print(book[0] + "ds[0]=====" + ds[0]);
        print(book[2] + "ds[2]=====" + ds[2]);
        print(book[1] + "ds[1]=====" + ds[1]);
        //ˢ��button��ͼ����
        chooseBooksParent.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[0].ToString());
        chooseBooksParent.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[1].ToString());
        chooseBooksParent.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Book/" + book[2].ToString());
    }

    #region ��ť���
    public void Click_A_Book()
    {
        if (hasEnter) return;
        ClickBook(0);
    }
    public void Click_B_Book()
    {
        if (hasEnter) return;
        ClickBook(1);
    }
    public void Click_C_Book()
    {
        if (hasEnter) return;
        ClickBook(2);
    }

    private void ClickBook(int _book)
    {
        hasEnter = true;
        //�ر���ѡһ���棬������ϸ����
        if (_book != 0) chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.zero;
        if (_book != 1) chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.zero;
        if (_book != 2) chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.zero;

        bookOriNumber = _book;
        bookOriPos = chooseBooksParent.GetChild(_book).position;

        chooseBooksParent.GetChild(_book).position = checkWordsParent.Find("book").position;
        checkWordsParent.gameObject.SetActive(true);
        //�����Ǹ����棬�������ɴ���
        switch(book[_book])
        {
            case BookNameEnum.HongLouMeng:
                {
                    InsWords(AllSkills.hlmList_adj,WordKindEnum.adj);
                    InsWords(AllSkills.hlmList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.hlmList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    InsWords(AllSkills.crystalList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.crystalList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.crystalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ZooManual:
                {
                    InsWords(AllSkills.animalList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.animalList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.animalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    InsWords(AllSkills.humanList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.humanList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.humanList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.Salome:
                {
                    InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj);
                    InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun);
                    InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb);
                }
                break;
        }


    }
    #endregion



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
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
                        });
                    }
                }break;
            case WordKindEnum.noun:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_noun, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            obj.GetComponentInChildren<TextMeshProUGUI>().text = _word.wordName;
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
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
                            obj.transform.parent = wordsArea;
                            obj.transform.localScale = Vector3.one;
                        });
                    }
                }
                break;


        }

               
    }


    /// <summary>
    /// ����ʾ���������д����ջ�
    /// </summary>
    void DeleteWords()
    {

        for (int i = wordsArea.childCount-1; i >=0; i--)
        {
            foreach (var a in wordsArea.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(a);
            }
            PoolMgr.GetInstance().PushObj(wordsArea.GetChild(i).gameObject.name, wordsArea.GetChild(i).gameObject);
        }
    
    }
  


    /// <summary>
    /// ����ڲ鿴�����������˰�ť��ȷ�ϡ����ص���Ϸ��������ʧ���������
    /// </summary>
    public void ClickYes()
    {
        GameMgr.instance.AddBookList(book[bookOriNumber]);
    }

    /// <summary>
    /// ����ڲ鿴�����������˰�ť�����ء�������������գ��ص��鱾ѡ�����
    /// </summary>
    public void ClickReturn()
    {
        hasEnter = false;
        DeleteWords();

        chooseBooksParent.GetChild(0).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(1).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(2).GetComponent<Image>().color = Vector4.one;
        chooseBooksParent.GetChild(bookOriNumber).position = bookOriPos;
    }

    public void MouseEnterWord()
    {
      
    }
}
