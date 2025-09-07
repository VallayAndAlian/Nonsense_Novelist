using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class ExperienceSystem : UnitComponent
{
    public int mLevel = 0;
    public float mCurrentExp = 0;
    public int mInitCareerLevel = 1;
    public int mNewCareerLevel = 1;
    protected List<CareerData> mCareers = new List<CareerData>();
    public List<CareerData> Careers=> mCareers;
    public List<CareerData> mNoMaxLevel= new List<CareerData>();
    public Dictionary<AttributeType,float> mAttrDatas= new Dictionary<AttributeType,float>();
    public Dictionary<BattleUnitCareerType,float> mCareerWeights= new Dictionary<BattleUnitCareerType, float>();
    public float ExpToNextLevel => CalculateExpForNextLevel(mLevel);
    public int mPendingLevelUps = 0;
    public class CareerData 
    {
        public BattleUnitCareerAnchor mCareerAnchor;
        public BattleUnitCareerType mCareerType;
        public int mCareerLevel;
        public bool mIsMaxLevel;
    }
    public enum LevelUpOption
    {
       LevelUpCareer,
       ChooseNewCareer,
    }
    public override void Start()
    {
        GainExpWhenkillEnmy();
        AddCareer(Owner.Data.mInitCareer);
        GetCareerWeight(mOwner.Data.mCareerAnchor, mOwner.Data.mInitCareer);
    }
    private static float CalculateExpForNextLevel(int currentLevel)
    {
        return (5 * Mathf.Pow(currentLevel, 2)+5*currentLevel+10)/2;
    }
    public void GainExpWhenkillEnmy()
    {
        EventManager.Subscribe<DamageReport>(EventEnum.UnitKilled, OnGainKillExp);
    }
    public void OnGainKillExpByShoot(DamageReport report)
    {
        if (report.mMeta.mInstigator == null)
        {
            List<BattleUnit> enemies = mOwner.Battle.CampManager.GetAlliesExceptServant(report.mMeta.mTarget);
            if (enemies.Count > 0)
            {
                foreach (BattleUnit battleUnit in enemies)
                {
                    battleUnit.ExperienceSystem.mCurrentExp += report.mMeta.mTarget.Data.mDeathProvideExp / enemies.Count;//各队友平分
                    battleUnit.ExperienceSystem.CheckLevelUp();
                }
            }
            Debug.Log($"mowner {mOwner.Data.mAsset} 发出者 {report.mMeta.mInstigator.Data.mAsset} ");
        }
        if (mPendingLevelUps > 0)
        {
            mOwner.Battle.UnitManager.AddCharacter(mOwner);//
        }
    }
    public void OnGainKillExp(DamageReport report)
    {
        if (report.mMeta.mInstigator == mOwner)
        {
            List<BattleUnit> Allies = mOwner.Battle.CampManager.GetAlliesExceptServant(mOwner);
            mCurrentExp += report.mMeta.mTarget.Data.mDeathProvideExp * 0.7f;
            CheckLevelUp();
            if (Allies.Count > 0)
            {
                foreach (BattleUnit battleUnit in Allies)
                {
                    battleUnit.ExperienceSystem.mCurrentExp += report.mMeta.mTarget.Data.mDeathProvideExp * 0.3f / Allies.Count;//各队友平分
                    battleUnit.ExperienceSystem.CheckLevelUp();
                }
            }
            Debug.Log($"mowner {mOwner.Data.mAsset} 发出者 {report.mMeta.mInstigator.Data.mAsset} ");
        }
        if (mPendingLevelUps > 0)
        {
            mOwner.Battle.UnitManager.AddCharacter(mOwner);//
        }
    }
    public void CheckLevelUp()
    {
        float requiredExp = CalculateExpForNextLevel(mLevel);

        if (mCurrentExp >= requiredExp)
        {
            LevelUpAlterAttr();
            mCurrentExp -=requiredExp;
            mLevel++;
            mPendingLevelUps++;
            // 递归检查是否连续升级
            CheckLevelUp();
        }
    }
    // 处理升级选项（在休息回合调用）
    public void ProcessLevelUp(LevelUpOption option,BattleUnitCareerType type)
    {
        if (mPendingLevelUps <= 0)
        {
            Debug.LogWarning("没有待处理的升级！");
            return;
        }
        switch (option)
        {
            case LevelUpOption.LevelUpCareer:
                LevelUpCareer(type);
                Debug.Log($"{mOwner.Data.mName}选择了职业升级");
                break;
            case LevelUpOption.ChooseNewCareer:
                ChooseNewCareer(type);
                Debug.Log($"{mOwner.Data.mName}选择了新职业");
                break;
            default:
                break;
        }

        //PlayerCharacter.PendingLevelUps--;
        mPendingLevelUps--;
        if(mPendingLevelUps == 0)
        {
            mOwner.Battle.UnitManager.RemoveCharacter(mOwner);
        }
    }
    public void LevelUpCareer(BattleUnitCareerType type)
    {
        foreach (var careerData in mCareers)
        {
            if (careerData.mCareerType == type)
            {
                careerData.mCareerLevel += 1;
                if (careerData.mCareerLevel == 3)
                {
                    careerData.mIsMaxLevel = true;
                    mNoMaxLevel.Remove(careerData);
                }
            }
        }
    }
    public void ChooseNewCareer(BattleUnitCareerType type)
    {
        AddCareer(type);
    }
    // 获取当前角色状态
    public string GetCharacterStatus()
    {
        return $"等级: {mLevel} | 经验: {mCurrentExp}/{ExpToNextLevel} | 待处理升级: {mPendingLevelUps}";
    }
    public void LevelUpAlterAttr()
    {
        foreach(var attr in mAttrDatas)
        {
            mOwner.ModifyBase(attr.Key, attr.Value, false);
        }
    }
    public List<BattleUnitCareerType> GetOptionList()
    {
        List<BattleUnitCareerType> selectedOptions= new List<BattleUnitCareerType>();
        
        if (mCareers.Count <4 && mNoMaxLevel.Count==0)
        {
            var randomdata = new Dictionary<BattleUnitCareerType, float>(mCareerWeights);
            foreach (var career in mCareers)
            {
                randomdata.Remove(career.mCareerType);
            }
            return WeightedLottery<BattleUnitCareerType>.WeightedRandomSelect(randomdata, 3);
        }
        else
        {
            Dictionary<BattleUnitCareerType, float> mRandomCareer = new Dictionary<BattleUnitCareerType, float>();
            if (mCareers.Count == 4&& mNoMaxLevel.Count > 0)
            {
                foreach (var career in mNoMaxLevel)
                {
                    mRandomCareer.Add(career.mCareerType, mCareerWeights[career.mCareerType]);
                    //yongzidian fangchuweishengjide 
                }
                var data = WeightedLottery<BattleUnitCareerType>.WeightedRandomSelect(mRandomCareer, mNoMaxLevel.Count);
                selectedOptions.AddRange(data);
            }
            else
            {
                foreach (var career in mNoMaxLevel)//选出固定位
                {
                    mRandomCareer.Add(career.mCareerType, mCareerWeights[career.mCareerType]);
                }
                var data = WeightedLottery<BattleUnitCareerType>.WeightedRandomSelect(mRandomCareer, 1);
                selectedOptions.AddRange(data);
                //选出剩余两个位置
                var randomdata = new Dictionary<BattleUnitCareerType, float>( mCareerWeights);
                randomdata.Remove(data[0]);//移除固定位
                foreach (var item in mCareers)
                {
                    if (item.mIsMaxLevel)
                    {
                        randomdata.Remove(item.mCareerType);
                    }
                }
                var redata = WeightedLottery<BattleUnitCareerType>.WeightedRandomSelect(randomdata, 2);
                selectedOptions.AddRange(redata);
            }
        }
        return selectedOptions;
    }
    public Dictionary<BattleUnitCareerType,float> GetCareerWeight(BattleUnitCareerAnchor unitCareerAnchor,BattleUnitCareerType unitCareerType)
    {
        var LevelData = UnitLevelUpTable.Find((int)unitCareerAnchor);
        if (LevelData == null)
        {
            Debug.LogError($"unitAnchor_{unitCareerAnchor} not found data");
            return null;
        }
        foreach (var levelData in LevelData.mCareerDatas)
        {
            //if (levelData.mType == BattleUnitCareerType.InitCareer)
            //{
            //    mCareerWeights.Add(mCareers[0].mCareerType, levelData.mWeight);
            //}
            //else
            //{
            //    mCareerWeights.Add(levelData.mType, levelData.mWeight);
            //}
            mCareerWeights.Add(levelData.mType, levelData.mWeight);
        }
        return mCareerWeights;
    }
    public Dictionary<AttributeType, float> GetAttrs(BattleUnitCareerAnchor unitCareerAnchor, BattleUnitCareerType unitCareerType)
    {
        var attrData = UnitLevelUpTable.Find((int)unitCareerAnchor);
        if (attrData == null)
        {
            Debug.LogError($"unitAnchor_{unitCareerAnchor} not found data");
            return null;
        }
        foreach (var attr in attrData.mAttrDatas)
        {
            //if (levelData.mType == BattleUnitCareerType.InitCareer)
            //{
            //    mCareerWeights.Add(mCareers[0].mCareerType, levelData.mWeight);
            //}
            //else
            //{
            //    mCareerWeights.Add(levelData.mType, levelData.mWeight);
            //}
            mAttrDatas.Add(attr.mType, attr.mAddValue);
        }
        return mAttrDatas;
    }
    public void AddCareer(BattleUnitCareerType type)
    {
        if (mCareers.Count < 4)
        {
            CareerData careerData = new CareerData();
            if (mCareers == null)
            {
                careerData.mCareerAnchor = mOwner.Data.mCareerAnchor;
                careerData.mCareerType = type;
                careerData.mCareerLevel = mOwner.Data.mInitCareerLevel;
                careerData.mIsMaxLevel = false;
            }
            else
            {
                careerData.mCareerAnchor = mOwner.Data.mCareerAnchor;
                careerData.mCareerType = type;
                careerData.mCareerLevel = mOwner.Data.mNewCareerLevel;
                careerData.mIsMaxLevel = false;
            }
            mCareers.Add(careerData);
            mNoMaxLevel.Add(careerData);
        }
        return;
    }
}