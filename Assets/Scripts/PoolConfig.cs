using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PoolConfig", menuName = "TT/PoolConfig")]
public class PoolConfig : ScriptableObject
{ // ID起始序号
    public static int EFFECT_BEGIN = 10000;
    public static int OTHER_BEGIN = 60000;

    [Header("数据")]
   public eventExcelData data;
    public MonsterExcelData monsterDate;/* AssetDatabase.LoadAssetAtPath<MonsterExcelData>(@"Assets/Resources/ExcelAsset/MonsterExcelData.asset");*/
    public cardRareExcelData cardRareDate;
    public CharaInfoExcelData charaInfo;

    //[Header("预制体")]
    public List<PoolConfigEntry> bullet;

    public List<PoolConfigEntry> effect;

    private List<PoolConfigEntry> other;


    public PoolConfigEntry this[int idx]
    {
        get
        {
            if (idx >= OTHER_BEGIN && idx < OTHER_BEGIN + other.Count)
            {
                return other[idx - OTHER_BEGIN];
            }
            else if (idx >= EFFECT_BEGIN && idx < EFFECT_BEGIN + effect.Count)
            {
                return effect[idx - EFFECT_BEGIN];
            }
            else if (idx >= 0 && idx < bullet.Count)
            {
                return bullet[idx];
            }
            else
            {
                return null;
            }
        }
    }

    public PoolConfigEntry getByID(int idx)
    {
        
            if (idx >= OTHER_BEGIN && idx < OTHER_BEGIN + other.Count)
            {
                return other[idx - OTHER_BEGIN];
            }
            else if (idx >= EFFECT_BEGIN && idx < EFFECT_BEGIN + effect.Count)
            {
                return effect[idx - EFFECT_BEGIN];
            }
            else if (idx >= 0 && idx < bullet.Count)
            {
                return bullet[idx];
            }
            else
            {
                return null;
            }
        
    }


    public string GetIndexById(int idx, out int index)
    {
        if (idx >= OTHER_BEGIN && idx < OTHER_BEGIN + other.Count)
        {
            index = idx - OTHER_BEGIN;
            return nameof(other);
        }
        else if (idx >= EFFECT_BEGIN && idx < EFFECT_BEGIN + effect.Count)
        {
            index = idx - EFFECT_BEGIN;
            return nameof(effect);
        }
        else if (idx >= 0 && idx < bullet.Count)
        {
            index = idx;
            return nameof(bullet);
        }
        else
        {
            index = -1;
            return null;
        }
    }

    public bool isValid(int idx)
    {
        return (idx >= 0 && idx < bullet.Count) || (idx >= EFFECT_BEGIN && idx < EFFECT_BEGIN + effect.Count) || (idx >= OTHER_BEGIN && idx < OTHER_BEGIN + other.Count);
    }

    public void ForEach(System.Action<PoolConfigEntry, int> fun)
    {
        for (var i = 0; i < bullet.Count; i++) fun(bullet[i], i);
        for (var i = 0; i < effect.Count; i++) fun(effect[i], i);
        for (var i = 0; i < other.Count; i++) fun(other[i], i);
    }

}

[Serializable]
public class PoolConfigEntry
{
    public int InitialCount;
    public string note;

    public GameObject prefab;

    public bool IsKeepInGotoNextLv;

    //        public int maxCount;
    public Stack<GameObject> stack;
}
