using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Networking.UnityWebRequest;
using UnityEngine.Windows;
using TMPro;
using static UnityEngine.UI.CanvasScaler;
public class BattleUnitLevelUpUI : BattleUI
{
    public BattleUnit mOwner;
    public Button mBt1;
    public Button mBt2;
    public Button mBt3;
    public TextMeshProUGUI mText;
    public List<Button> mButtons = new List<Button>();
    public List<BattleUnit> units =new List<BattleUnit>();

    public BattleUnitLevelUpUI()
    {

    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Load<GameObject>("UI/Battle/levelUpPanel");
    }

    public override void Init()
    {
        base.Init();
        mBt1 = UIPanel.transform.Find("Panel/Bt1").GetComponent<Button>();
        mBt2 = UIPanel.transform.Find("Panel/Bt2").GetComponent<Button>();
        mBt3 = UIPanel.transform.Find("Panel/Bt3").GetComponent<Button>();
        mText= UIPanel.transform.Find("Panel/LevelUpText").GetComponent<TextMeshProUGUI>();
        mButtons.Add(mBt1);
        mButtons.Add(mBt2);
        mButtons.Add(mBt3);
        mBt1.onClick.AddListener(OnClickButton1);
        mBt2.onClick.AddListener(OnClickButton2);
        mBt3.onClick.AddListener(OnClickButton3);
        EventManager.Subscribe<List<BattleUnit>>(EventEnum.PendingLevelUps, OnPendingLevelUps);
    }

    public override void Update(float deltaTime)
    {

    }

    public void RefreshSlider(float amount)
    { }
    public void OnClickButton1()
    {
        if (mOwner != null)
        {
            BattleUnitCareerType result = BattleUnitCareerType.None;
            if (Enum.TryParse(mBt1.transform.GetComponentInChildren<TextMeshProUGUI>().text, out result))
            {
                Console.WriteLine("转换成功: " + result);
            }
            else
            {
                Console.WriteLine("转换失败");
                return;
            }
            DealOption(result);
        }
    }
    public void OnClickButton2()
    {
        if (mOwner != null)
        {
            BattleUnitCareerType result = BattleUnitCareerType.None;
            if (Enum.TryParse(mBt2.transform.GetComponentInChildren<TextMeshProUGUI>().text, out result))
            {
                Console.WriteLine("转换成功: " + result);
            }
            else
            {
                Console.WriteLine("转换失败");
                return;
            }
            DealOption(result);
        }
    }
    public void OnClickButton3()
    {
        if (mOwner != null)
        {
            BattleUnitCareerType result = BattleUnitCareerType.None;
            if (Enum.TryParse(mBt3.transform.GetComponentInChildren<TextMeshProUGUI>().text, out result))
            {
                Console.WriteLine("转换成功: " + result);
            }
            else
            {
                Console.WriteLine("转换失败");
                return;
            }
            DealOption(result);
        }
    }
    public void DealOption(BattleUnitCareerType type)
    {
        bool isNewCareer = false;
        foreach (var cardata in mOwner.ExperienceSystem.Careers)
        {
            if (cardata.mCareerType == type)//已有职业
            {
                isNewCareer = false;
                //mOwner.ExperienceSystem.ProcessLevelUp(ExperienceSystem.LevelUpOption.LevelUpCareer);
                break;
            }
            else
            {
                isNewCareer = true;
            }
        }
        if (isNewCareer)
        {
            mOwner.ExperienceSystem.ProcessLevelUp(ExperienceSystem.LevelUpOption.ChooseNewCareer, type);
        }
        else
        {
            mOwner.ExperienceSystem.ProcessLevelUp(ExperienceSystem.LevelUpOption.LevelUpCareer, type);
        }

        if(mOwner.ExperienceSystem.mPendingLevelUps == 0)
        {
            Battle.BattleUI.Hide(this);
            Debug.Log($"{mOwner}待处理的升级数{mOwner.ExperienceSystem.mPendingLevelUps}");
            units.Remove(mOwner);
            if (units.Count > 0)
            {
                PendingLevelUps(units[0]);
            }
            else
            {
                return;
            }
        }
        if (mOwner.ExperienceSystem.mPendingLevelUps > 0)
        {
            Battle.BattleUI.Hide(this);
            PendingLevelUps(mOwner);
        }
        else
        {
            Battle.BattleUI.Hide(this);
        }
    }
    private void OnPendingLevelUps(List<BattleUnit> battleUnits)
    {
        units= battleUnits;
        //foreach (var unit in battleUnits)
        //{
        //    PendingLevelUps(unit);
        //}
        PendingLevelUps(battleUnits[0]);
    }
    public void PendingLevelUps(BattleUnit unit)
    {
        mOwner = unit;
        var OptionList = unit.ExperienceSystem.GetOptionList();
        if (OptionList.Count <= mButtons.Count)
        {
            for (int i = 0; i < OptionList.Count; i++)
            {
                mButtons[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = OptionList[i].ToString();
            }
        }
        mText.text = unit.Data.mName;
        Battle.BattleUI.ShowPanel(this);
    }
}