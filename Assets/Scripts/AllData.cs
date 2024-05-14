using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllData 
{
    public static test1ExcelData data = AssetDatabase.LoadAssetAtPath<test1ExcelData>(@"Assets/Resources/ExcelAsset/test1ExcelData.asset");
    public static MonsterExcelData monsterDate = AssetDatabase.LoadAssetAtPath<MonsterExcelData>(@"Assets/Resources/ExcelAsset/MonsterExcelData.asset");
    public static cardRareExcelData cardRareDate = AssetDatabase.LoadAssetAtPath<cardRareExcelData>(@"Assets/Resources/ExcelAsset/cardRareExcelData.asset");
    public static CharaInfoExcelData charaInfo = AssetDatabase.LoadAssetAtPath<CharaInfoExcelData>(@"Assets/Resources/ExcelAsset/CharaInfoExcelData.asset");
}
