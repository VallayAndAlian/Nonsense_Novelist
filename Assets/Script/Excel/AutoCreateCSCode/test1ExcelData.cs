/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class test1ExcelItem : ExcelItemBase
{
	public EventType type;
	public string name;
	public string textEvent;
	public string textDraft;
	public string textTrigger;
	public bool isKey;
	public string happen;
}

[CreateAssetMenu(fileName = "test1ExcelData", menuName = "Excel To ScriptableObject/Create test1ExcelData", order = 1)]
public class test1ExcelData : ExcelDataBase<test1ExcelItem>
{
}

#if UNITY_EDITOR
public class test1AssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		test1ExcelItem[] items = new test1ExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new test1ExcelItem();
			items[i].id = allItemValueRowList[i]["id"];
			items[i].type = (EventType)(Convert.ToInt32(allItemValueRowList[i]["type"]));
			items[i].name = allItemValueRowList[i]["name"];
			items[i].textEvent = allItemValueRowList[i]["textEvent"];
			items[i].textDraft = allItemValueRowList[i]["textDraft"];
			items[i].textTrigger = allItemValueRowList[i]["textTrigger"];
			items[i].isKey = Convert.ToBoolean(allItemValueRowList[i]["isKey"]);
			items[i].happen = allItemValueRowList[i]["happen"];
		}
		test1ExcelData excelDataAsset = ScriptableObject.CreateInstance<test1ExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(test1ExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


