using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCardSO", menuName = "BattleSO/CardData")]
    public class CardSO : ScriptableObject
    {
        [Header("基础信息信息")][Space(5)]

        [Tooltip("卡牌正式名称")]
        public string unitName;

        [Tooltip("卡牌描述")][TextArea]
        public string prefab;

        [Tooltip("卡牌图像")]
        public Texture2D infoShort;



        [Space(20)][Header("关联词条")][Space(5)]

        [Tooltip("关联卡牌ID")]
        public List<int> relativeCard;

        [Tooltip("关联技能")]
        public List<CardSO> relativeSkills;
    
    }
