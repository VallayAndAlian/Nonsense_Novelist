using System.Collections.Generic;
using UnityEngine;

public enum EffectFxPlayType
{
    None = 0,
    Instant,
    Script,
}

public class EffectFxConfig
{
    [Header("播放类型")]
    public EffectFxPlayType mPlayType = EffectFxPlayType.None;
    
    [Header("插槽")]
    public string mSocketName;

    [Header("特效")]
    public GameObject mFxObj;
}


[CreateAssetMenu(fileName = "NewBattleEffectSO", menuName = "BattleSO/BattleEffectSO")]
public class BattleEffectSO : ScriptableObject
{
    [Tooltip("特效配置")]
    public Dictionary<EffectType, List<EffectFxConfig>> mFxConfigs;
}