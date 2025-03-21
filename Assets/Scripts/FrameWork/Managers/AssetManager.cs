using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

public class AssetManager
{
    public static T Load<T>(string path, string assetName) where T : Object
    {
        return Resources.Load(Path.Combine(path, assetName)) as T;
    }

    public static void UnLoad(Object asset)
    {
        Resources.UnloadAsset(asset);
    }
   
}
