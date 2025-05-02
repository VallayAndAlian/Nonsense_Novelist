using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

public class AssetManager
{
    protected static BattleEffectSO mEffectSO = null;
    protected static CommonSO mCommonSO = null;
    
    public static T Load<T>(string path, string assetName) where T : Object
    {
        return Resources.Load(Path.Combine(path, assetName)) as T;
    }
    
    public static T Load<T>(string assetPath) where T : Object
    {
        return Resources.Load(assetPath) as T;
    }
    
    public static T Create<T>(string assetPath) where T : Object
    {
        return Object.Instantiate(Load<T>(assetPath));
    }

    public static void UnLoad(Object asset)
    {
        Resources.UnloadAsset(asset);
    }

    public static BattleEffectSO GetEffectAsset()
    {
        if (mEffectSO == null)
        {
            mEffectSO = Load<BattleEffectSO>("SO/Effect", "EffectSO");
        }
            
        return mEffectSO;
    }
    
    public static CommonSO GetCommonAsset()
    {
        if (mCommonSO == null)
        {
            mCommonSO = Load<CommonSO>("SO", "CommonSO");
        }
            
        return mCommonSO;
    }
}
