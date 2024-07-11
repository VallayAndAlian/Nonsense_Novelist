#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Reflection;
using System;

//Excel�м�����
public class ExcelMediumData
{
    //Excel����
    public string excelName;
    //Dictionary<�ֶ�����, �ֶ�����>����¼��������ֶμ�������
    public Dictionary<string, string> propertyNameTypeDic;
    //List<һ������>��List<Dictionary<�ֶ�����, һ�е�ÿ����Ԫ���ֶ�ֵ>>
    //��¼��������ֶ�ֵ�����м�¼
    public List<Dictionary<string, string>> allItemValueRowList;
}

public static class ExcelDataReader
{
    //Excel��2�ж�Ӧ�ֶ�����
    const int excelNameRow = 2;
    //Excel��4�ж�Ӧ�ֶ�����
    const int excelTypeRow = 4;
    //Excel��5�м��Ժ��Ӧ�ֶ�ֵ
    const int excelDataRow = 5;

    //Excel��ȡ·��
    public static string excelFilePath = Application.dataPath + "/Excel";
    //public static string excelFilePath = Application.dataPath.Replace("Assets/Excel", "Excel");

    //�Զ�����C#���ļ�·��
    static string excelCodePath = Application.dataPath + "/Script/Excel/AutoCreateCSCode";
    //�Զ�����Asset�ļ�·��
    static string excelAssetPath = "Assets/Resources/ExcelAsset";

    #region --- Read Excel ---

    //����Excel��Ӧ��C#��
    public static void ReadAllExcelToCode()
    {
        //��ȡ����Excel�ļ�
        //ָ��Ŀ¼����ָ��������ģʽ��ѡ��ƥ����ļ����������ƣ�����·���������飻���δ�ҵ��κ��ļ�����Ϊ�����顣
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");

        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //��������Excel������C#��
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            ReadOneExcelToCode(excelFileFullPaths[i]);
        }
    }

    //����Excel��Ӧ��C#��
    public static void ReadOneExcelToCode(string excelFileFullPath)
    {
        //����Excel��ȡ�м�����
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFileFullPath);
        if (excelMediumData != null)
        {
            //������������C#�ű�
            string classCodeStr = ExcelCodeCreater.CreateCodeStrByExcelData(excelMediumData);
            if (!string.IsNullOrEmpty(classCodeStr))
            {
                //д�ļ�������CSharp.cs
                if (WriteCodeStrToSave(excelCodePath, excelMediumData.excelName + "ExcelData", classCodeStr))
                {
                    Debug.Log("<color=green>Auto Create Excel Scripts Success : </color>" + excelMediumData.excelName);
                    return;
                }
            }
        }
        //����ʧ��
        Debug.LogError("Auto Create Excel Scripts Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
    }

    #endregion

    #region --- Create Asset ---

    //����Excel��Ӧ��Asset�����ļ�
    public static void CreateAllExcelAsset()
    {
        //��ȡ����Excel�ļ�
        //ָ��Ŀ¼����ָ��������ģʽ��ѡ��ƥ����ļ����������ƣ�����·���������飻���δ�ҵ��κ��ļ�����Ϊ�����顣
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");
        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //��������Excel������Asset
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            CreateOneExcelAsset(excelFileFullPaths[i]);
        }
    }

    //����Excel��Ӧ��Asset�����ļ�
    public static void CreateOneExcelAsset(string excelFileFullPath)
    {
        //����Excel��ȡ�м�����
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFileFullPath);
        if (excelMediumData != null)
        {
            //��ȡ��ǰ����
            Assembly assembly = Assembly.GetExecutingAssembly();
           // �������ʵ��������Ϊ object ���ͣ���Ҫǿ������ת����assembly.CreateInstance("�����ȫ�޶����������������ռ䣩");
            object class0bj = assembly.CreateInstance(excelMediumData.excelName + "Assignment",true);

            //����������г�����������͡���ǰ��Assembly-CSharp-Editor�У�Ŀ��������Assembly-CSharp�У���ͬ�����޷���ȡ����
            Type type = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                //����Ŀ������
                Type tempType = asm.GetType(excelMediumData.excelName + "AssetAssignment");
                if (tempType != null)
                {
                    type = tempType;
                    break;
                }
            }
            if (type != null)
            {
                //�����ȡ����
                MethodInfo methodInfo = type.GetMethod("CreateAsset");
                if (methodInfo != null)
                {
                    methodInfo.Invoke(null, new object[] { excelMediumData.allItemValueRowList, excelAssetPath });
                    //����Asset�ļ��ɹ�
                    Debug.Log("<color=green>Auto Create Excel Asset Success : </color>" + excelMediumData.excelName);
                    return;
                }
            }
        }
        //����Asset�ļ�ʧ��
        Debug.LogError("Auto Create Excel Asset Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
    }

    #endregion

    #region --- private ---

    //����Excel�������м�����
    private static ExcelMediumData CreateClassCodeByExcelPath(string excelFileFullPath)
    {
        if (string.IsNullOrEmpty(excelFileFullPath))
            return null;

        excelFileFullPath = excelFileFullPath.Replace("\\", "/");

        FileStream stream = File.Open(excelFileFullPath, FileMode.Open, FileAccess.Read);
        if (stream == null)
            return null;
        //����Excel
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //��ЧExcel
        if (excelReader == null || !excelReader.IsValid)
        {
            Debug.Log("Invalid excel �� " + excelFileFullPath);
            return null;
        }

        //<��������,��������>
        KeyValuePair<string, string>[] propertyNameTypes = null;
        //List<KeyValuePair<��������, ��Ԫ������ֵ>[]>����������ֵ�����м�¼
        List<Dictionary<string, string>> allItemValueRowList = new List<Dictionary<string, string>>();

        //ÿ����������
        int propertyCount = 0;
        //��ǰ�����У���1��ʼ
        int curRowIndex = 1;
        //��ʼ��ȡ�����б���
        while (excelReader.Read())
        {
            if (excelReader.FieldCount == 0)
                continue;
            //��ȡһ�е�����
            string[] datas = new string[excelReader.FieldCount];
            for (int j = 0; j < excelReader.FieldCount; ++j)
            {
                //��ֵһ�е�ÿһ����Ԫ������
                datas[j] = excelReader.GetString(j);
            }
            //����/�е�һ����Ԫ��Ϊ�գ���Ϊ��Ч����
            if (datas.Length == 0 || string.IsNullOrEmpty(datas[0]))
            {
                curRowIndex++;
                continue;
            }
            //������
            if (curRowIndex >= excelDataRow)
            {
                //������Ч
                if (propertyCount <= 0)
                    return null;

                Dictionary<string, string> itemDic = new Dictionary<string, string>(propertyCount);
                //����һ�����ÿ����Ԫ������
                for (int j = 0; j < propertyCount; j++)
                {
                    //�жϳ���
                    if (j < datas.Length)
                        itemDic[propertyNameTypes[j].Key] = datas[j];
                    else
                        itemDic[propertyNameTypes[j].Key] = null;
                }
                allItemValueRowList.Add(itemDic);
            }
            //����������
            else if (curRowIndex == excelNameRow)
            {
                //����������ȷ��ÿ�е���������
                propertyCount = datas.Length;
                if (propertyCount <= 0)
                    return null;
                //��¼��������
                propertyNameTypes = new KeyValuePair<string, string>[propertyCount];
                for (int i = 0; i < propertyCount; i++)
                {
                    propertyNameTypes[i] = new KeyValuePair<string, string>(datas[i], null);
                }
            }
            //����������
            else if (curRowIndex == excelTypeRow)
            {
                //����������������ָ��������������Ч
                if (propertyCount <= 0 || datas.Length < propertyCount)
                    return null;
                //��¼�������Ƽ�����
                for (int i = 0; i < propertyCount; i++)
                {
                    propertyNameTypes[i] = new KeyValuePair<string, string>(propertyNameTypes[i].Key, datas[i]);
                }
            }
            curRowIndex++;
        }

        if (propertyNameTypes.Length == 0 || allItemValueRowList.Count == 0)
            return null;

        ExcelMediumData excelMediumData = new ExcelMediumData();
        //����
        excelMediumData.excelName = excelReader.Name;
        //Dictionary<��������,��������>
        excelMediumData.propertyNameTypeDic = new Dictionary<string, string>();
        //ת���洢��ʽ
        for (int i = 0; i < propertyCount; i++)
        {
            //�������ظ���������Ч
            if (excelMediumData.propertyNameTypeDic.ContainsKey(propertyNameTypes[i].Key))
                return null;
            excelMediumData.propertyNameTypeDic.Add(propertyNameTypes[i].Key, propertyNameTypes[i].Value);
        }
        excelMediumData.allItemValueRowList = allItemValueRowList;
        return excelMediumData;
    }

    //д�ļ�
    private static bool WriteCodeStrToSave(string writeFilePath, string codeFileName, string classCodeStr)
    {
        if (string.IsNullOrEmpty(codeFileName) || string.IsNullOrEmpty(classCodeStr))
            return false;
        //��鵼��·��
        if (!Directory.Exists(writeFilePath))
            Directory.CreateDirectory(writeFilePath);
        //д�ļ�������CS���ļ�
        StreamWriter sw = new StreamWriter(writeFilePath + "/" + codeFileName + ".cs");
        sw.WriteLine(classCodeStr);
        sw.Close();
        //
        UnityEditor.AssetDatabase.Refresh();
        return true;
    }

    #endregion

}
#endif