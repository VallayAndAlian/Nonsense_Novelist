using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMgr : MonoSingleton<GameMgr>
{

    //�رս������
    private GameObject exitPanel;
    GameObject exitObj;
    Button exitButton;
    Button cancelButton;
    bool hasOpenExit = false;


    List<BookNameEnum> bookList = new List<BookNameEnum>();

    //ս�����ƿ�
    List<Type> wordList = new List<Type>();

    //��ǰδʹ�õ��Ƶ��ƿ�
    List<Type> wordNowList = new List<Type>();

    //��ǰδʹ�õ��Ƶ��ƿ�
    List<Type> wordHasUseList = new List<Type>();


    private void Start()
    {
        //�˳��˵�
        exitPanel = Resources.Load<GameObject>("UI/exitPanel");
        DontDestroyOnLoad(this.gameObject);

        //�ƿ�
        InitCardList();
    }
    private void Update()
    {
        //�˳��˵�
        if (hasOpenExit) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitObj = Instantiate(exitPanel);
            Time.timeScale = 0f;
            hasOpenExit = true;
            exitButton = exitObj.transform.GetComponentsInChildren<Button>()[0];
            cancelButton = exitObj.transform.GetComponentsInChildren<Button>()[1];
            exitButton.onClick.AddListener(ExitButton);
            cancelButton.onClick.AddListener(BackToGame);
        }
    }



    #region exit�˵����
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToGame()
    {hasOpenExit = false;
        Destroy(exitObj.gameObject);
        Time.timeScale = 1f;
        
    }
    #endregion

    #region �� �ƿ�

    public void SetCombatStartList(List<Type> _list)
    {
        bookList.Clear();
        wordList.Clear();
        wordList = _list;
    }
    public void AddCombatStartList(List<Type> _list)
    {

        foreach (var _i in _list)
        {

            if (!wordList.Contains(_i))
            {
                _i.GetType();
                int _r = 1;
                //var count = _i.GetField("rarity").GetValue(null);
                //if (count != null) _r = (int)count;

                //for (int i = 0; i < _r; i++)
                //{
                    wordList.Add(_i);
                //}
            }
                
        }
    }
  
    public void AddBookList(BookNameEnum _book)
    {
        switch (_book)
        {
            case BookNameEnum.HongLouMeng:
                {
                    if (!bookList.Contains(BookNameEnum.HongLouMeng))
                    {   
                        AddCombatStartList(AllSkills.hlmList_all);
                        bookList.Add(BookNameEnum.HongLouMeng);
                    }
                 
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    if (!bookList.Contains(BookNameEnum.CrystalEnergy))
                    {
                        AddCombatStartList(AllSkills.crystalList_all);
                        bookList.Add(BookNameEnum.CrystalEnergy);
                    }
                  
                }
                break;
            case BookNameEnum.Salome:
                {
                    if (!bookList.Contains(BookNameEnum.Salome))
                    {
                        AddCombatStartList(AllSkills.shaLeMeiList_all);
                        bookList.Add(BookNameEnum.Salome);
                    }

                }
                break;
            case BookNameEnum.ZooManual:
                {
                    if (!bookList.Contains(BookNameEnum.ZooManual))
                    {
                        AddCombatStartList(AllSkills.animalList_all);
                        bookList.Add(BookNameEnum.ZooManual);
                    }
                  
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    if (!bookList.Contains(BookNameEnum.PHXTwist))
                    {
                        AddCombatStartList(AllSkills.maYiDiGuoList_all);
                        bookList.Add(BookNameEnum.PHXTwist);
                    }
      
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    if (!bookList.Contains(BookNameEnum.FluStudy))
                    {
                        AddCombatStartList(AllSkills.liuXingBXList_all);
                        bookList.Add(BookNameEnum.FluStudy);
                    }
                  
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    if (!bookList.Contains(BookNameEnum.EgyptMyth))
                    {
                        AddCombatStartList(AllSkills.aiJiShenHuaList_all);
                        bookList.Add(BookNameEnum.EgyptMyth);
                    }                 
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    if (!bookList.Contains(BookNameEnum.ElectronicGoal))
                    {
                        AddCombatStartList(AllSkills.humanList_all);
                        bookList.Add(BookNameEnum.ElectronicGoal);
                    }        
                }
                break;
            case BookNameEnum.allBooks:
                {
                    if (!bookList.Contains(BookNameEnum.allBooks))
                    {
                        AddCombatStartList(AllSkills.commonList_all);
                        bookList.Add(BookNameEnum.allBooks);
                    }

                }
                break;

        }
    }
    public bool HasBook(BookNameEnum _book)
    {
        if (bookList.Contains(_book))
            return true;
        return false;
    }
    #endregion


    #region �� �ƿ�


    /// <summary>
    /// �ƿ��ʼ��
    /// </summary>
    void InitCardList()
    {
        wordList.Clear();
        wordNowList.Clear();
        wordHasUseList.Clear();


        //������
        wordList.AddRange(new Type[] { typeof(SheQunFengRong), typeof(ChanLuan), typeof(ShiWuFengRong) });

        //����ͨ�ô���
        if(wordList==null)
            AddBookList(BookNameEnum.allBooks);
        RefreshNowList();
    }



    public List<Type> GetHasUsedList()
    {
        return wordHasUseList;
    }
    public List<Type> GetNowList()
    {

        if (wordNowList == null) //����ͨ�ô���
            AddBookList(BookNameEnum.allBooks);
        return wordNowList;
    }
    public Type GetNowListOne()
    {
        int count = UnityEngine.Random.Range(0, wordNowList.Count);
        var _res = wordNowList[count];
        wordNowList.Remove(_res);
        wordHasUseList.Add(_res);
        RefreshNowList();
        return _res;
    }

    public List<Type> GetAllList()
    {
        return wordList;
    }



    /// <summary>
    /// ˢ�����п����б�
    /// </summary>
    void RefreshNowList()
    {
        if (wordNowList.Count == 0)
        {
            wordNowList.AddRange(wordList);
            wordHasUseList.Clear();
        }
    }
    #endregion
}
