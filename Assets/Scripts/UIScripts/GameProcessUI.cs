using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessUI: BasePanel
{
    private SettingList SettingList;
    public SettingList settingList 
    { get 
        {
            if(SettingList==null) SettingList= transform.Find("SettingList").GetComponent<SettingList>();
            return SettingList;
        }
    }


    private WordInformation WordInfo;
    public WordInformation wordInfo
    {
        get
        {
            if (WordInfo == null) WordInfo = transform.Find("WordInformation").GetComponent<WordInformation>();
            return WordInfo;
        }
    }


    private GameProcessSlider GameProcessSlider;
    public GameProcessSlider gameProcessSlider
    {
        get
        {
            if (GameProcessSlider == null) GameProcessSlider = transform.Find("GameProcess").GetComponent<GameProcessSlider>();
            return GameProcessSlider;
        }
    }


    private Transform ShootChild;
    public Transform shootChild
    {
        get
        {
            if (ShootChild == null) 
                ShootChild = transform.Find("ShootTime");
            return ShootChild;
        }
    }
    protected override void Init()
    {
    }


    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "ShootButton":
                {
                    UIManager.GetInstance().ShowPanel<CardRes>("CardRes",E_UI_Layer.Top,(obj)=>
                    {
                        
                    });
                }break;
        }
    }
    #region settingList
    public void RefreshSettingList()
    {
        SettingList.RefreshList();
    }
    public void RefreshGroupHP()
    {
        SettingList.RefreshGroupHP();
    }
    #endregion
    public void SwitchSettingList(bool _display)
    {
        if (SettingList == null) return;
        if (_display)
        {
            SettingList.RefreshList();
            SettingList.gameObject.SetActive(true);
        }
        else
        {
            SettingList.gameObject.SetActive(false);
        }
    }

    public void SwitchWordInformatio(bool _display)
    {
        if (wordInfo == null) return;
        if (_display)
        {
           
            wordInfo.gameObject.SetActive(true);
        }
        else
        {
            wordInfo.gameObject.SetActive(false);
        }
    }
}
