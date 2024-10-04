using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StagesData", menuName = "TT/NewStageData")]
public class StagesData : ScriptableObject
{
    [Header("备注")]

    public string info;

    [Header("队伍")]

    [Tooltip("队伍初始血量")] public float groupMaxHp=100;
    [Tooltip("能量恢复速度")] public float energyRecover = 0.2f;
    [Tooltip("单卡加载时间")] public float oneCardLoad=3;
    [Tooltip("牌库再装填时间")] public float cardListLoad=5;


    [Header("事件概率(总和100)")]

    [Tooltip("希望")] public int xiWang = 10;
    [Tooltip("访客")] public int fangKe = 25;
    [Tooltip("意外")] public int yiWai = 10;
    [Tooltip("危机")] public int weiJi = 30;
    [Tooltip("交易")] public int jiaoYi = 25;
    [Tooltip("场景")] public int changJing = 10;	


    [Header("游戏阶段")]

    public List<OneStageData> stagesData = new List<OneStageData>();
}


