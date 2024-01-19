using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMgr : MonoSingleton<GameMgr>
{

    //战斗相关
    private List<Type> combatStart = new List<Type>();

    //关闭界面相关
    private GameObject exitPanel;
    GameObject exitObj;
    Button exitButton;
    Button cancelButton;
    bool hasOpenExit = false;
    List<BookNameEnum> listBook = new List<BookNameEnum>();

    private void Start()
    {
        exitPanel = Resources.Load<GameObject>("UI/exitPanel");
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
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
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToGame()
    {hasOpenExit = false;
        Destroy(exitObj.gameObject);
        Time.timeScale = 1f;
        
    }

    public List<Type> GetCombatStartList()
    {
        
        return combatStart;
    }
    public void SetCombatStartList(List<Type> _list)
    {
        listBook.Clear();
        combatStart.Clear();
        combatStart = _list;
    }
    public void AddCombatStartList(List<Type> _list)
    {

        if (combatStart == null)
            combatStart = new List<Type>();
        foreach (var _i in _list)
        {
           
            if (!combatStart.Contains(_i))
                combatStart.Add(_i);
        }
 
        
    }
  
    public void AddBookList(BookNameEnum _book)
    {
        switch (_book)
        {
            case BookNameEnum.HongLouMeng:
                {
                    if (!listBook.Contains(BookNameEnum.HongLouMeng))
                    {   
                        AddCombatStartList(AllSkills.hlmList_all);
                        listBook.Add(BookNameEnum.HongLouMeng);
                    }
                 
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    if (!listBook.Contains(BookNameEnum.CrystalEnergy))
                    {
                        AddCombatStartList(AllSkills.crystalList_all);
                        listBook.Add(BookNameEnum.CrystalEnergy);
                    }
                  
                }
                break;
            case BookNameEnum.Salome:
                {
                    if (!listBook.Contains(BookNameEnum.Salome))
                    {
                        AddCombatStartList(AllSkills.shaLeMeiList_all);
                        listBook.Add(BookNameEnum.Salome);
                    }

                }
                break;
            case BookNameEnum.ZooManual:
                {
                    if (!listBook.Contains(BookNameEnum.ZooManual))
                    {
                        AddCombatStartList(AllSkills.animalList_all);
                        listBook.Add(BookNameEnum.ZooManual);
                    }
                  
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    if (!listBook.Contains(BookNameEnum.PHXTwist))
                    {
                        AddCombatStartList(AllSkills.maYiDiGuoList_all);
                        listBook.Add(BookNameEnum.PHXTwist);
                    }
      
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    if (!listBook.Contains(BookNameEnum.FluStudy))
                    {
                        AddCombatStartList(AllSkills.liuXingBXList_all);
                        listBook.Add(BookNameEnum.FluStudy);
                    }
                  
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    if (!listBook.Contains(BookNameEnum.EgyptMyth))
                    {
                        AddCombatStartList(AllSkills.aiJiShenHuaList_all);
                        listBook.Add(BookNameEnum.EgyptMyth);
                    }                 
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    if (!listBook.Contains(BookNameEnum.ElectronicGoal))
                    {
                        AddCombatStartList(AllSkills.humanList_all);
                        listBook.Add(BookNameEnum.ElectronicGoal);
                    }        
                }
                break;
   
        }
    }
    public bool HasBook(BookNameEnum _book)
    {
        if (listBook.Contains(_book))
            return true;
        return false;
    }
    
}
