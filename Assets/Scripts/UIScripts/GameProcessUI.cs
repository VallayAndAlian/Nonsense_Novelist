using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessUI: BasePanel
{
    SettingList SettingList;
    WordInformation wordInfo;
    public GameProcessSlider gameProcessSlider;
    protected override void Init()
    {
        SettingList = transform.Find("SettingList").GetComponent<SettingList>();
        wordInfo = transform.Find("WordInformation").GetComponent<WordInformation>();
        gameProcessSlider= transform.Find("GameProcess").GetComponent<GameProcessSlider>();
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
