#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class BuildExcelEditor : Editor
{

}

public class BuildExcelWindow : EditorWindow
{
    //[MenuItem("MyTools/Excel/Build Script")]
    //public static void CreateExcelCode()
    //{
    //    ExcelDataReader.ReadAllExcelToCode();
    //}

    //[MenuItem("MyTools/Excel/Build Asset")]
    //public static void CreateExcelAssset()
    //{
    //    ExcelDataReader.CreateAllExcelAsset();
    //}

    [MenuItem("MyTools/Excel Window")]
    public static void ShowExcelWindow()
    {
        //��ʾ�������ڷ�ʽһ
        //BuildExcelWindow buildExcelWindow = GetWindow<BuildExcelWindow>();
        //buildExcelWindow.Show();
        //��ʾ�������ڷ�ʽ��
        EditorWindow.GetWindow(typeof(BuildExcelWindow));
    }

    private string showNotify;
    private Vector2 scrollPosition = Vector2.zero;

    private List<string> fileNameList = new List<string>();
    private List<string> filePathList = new List<string>();

    private void Awake()
    {
        titleContent.text = "Excel���ݶ�ȡ";
    }

    private void OnEnable()
    {
        showNotify = "";
        GetExcelFile();
    }

    private void OnDisable()
    {
        showNotify = "";

        fileNameList.Clear();
        filePathList.Clear();
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition,
            GUILayout.Width(position.width), GUILayout.Height(position.height));
        //�Զ�����C#�ű�
        GUILayout.Space(10);
        GUILayout.Label("Excel To Script");
        for (int i = 0; i < fileNameList.Count; i++)
        {
            if (GUILayout.Button(fileNameList[i], GUILayout.Width(200), GUILayout.Height(30)))
            {
                SelectExcelToCodeByIndex(i);
            }
        }
        if (GUILayout.Button("All Excel", GUILayout.Width(200), GUILayout.Height(30)))
        {
            SelectExcelToCodeByIndex(-1);
        }
        //�Զ�����Asset�ļ�
        GUILayout.Space(20);
        GUILayout.Label("Script To Asset");
        for (int i = 0; i < fileNameList.Count; i++)
        {
            if (GUILayout.Button(fileNameList[i], GUILayout.Width(200), GUILayout.Height(30)))
            {
                SelectCodeToAssetByIndex(i);
            }
        }
        if (GUILayout.Button("All Excel", GUILayout.Width(200), GUILayout.Height(30)))
        {
            SelectCodeToAssetByIndex(-1);
        }
        //
        GUILayout.Space(20);
        GUILayout.Label(showNotify);
        //
        GUILayout.EndScrollView();
        //this.Repaint();
    }

    //��ȡָ��·���µ�Excel�ļ���
    private void GetExcelFile()
    {
        fileNameList.Clear();
        filePathList.Clear();

        if (!Directory.Exists(ExcelDataReader.excelFilePath))
        {
            showNotify = "��Ч·����" + ExcelDataReader.excelFilePath;
            return;
        }
        string[] excelFileFullPaths = Directory.GetFiles(ExcelDataReader.excelFilePath, "*.xlsx");

        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            showNotify = ExcelDataReader.excelFilePath + "·����û���ҵ�Excel�ļ�";
            return;
        }

        filePathList.AddRange(excelFileFullPaths);
        for (int i = 0; i < filePathList.Count; i++)
        {
            string fileName = filePathList[i].Split('/').LastOrDefault();
            fileName = filePathList[i].Split('\\').LastOrDefault();
            fileNameList.Add(fileName);
        }
        showNotify = "�ҵ�Excel�ļ���" + fileNameList.Count + "��";
    }

    //�Զ�����C#�ű�
    private void SelectExcelToCodeByIndex(int index)
    {
        if (index >= 0 && index < filePathList.Count)
        {
            string fullPath = filePathList[index];
            ExcelDataReader.ReadOneExcelToCode(fullPath);
        }
        else
        {
            ExcelDataReader.ReadAllExcelToCode();
        }
    }

    //�Զ�����Asset�ļ�
    private void SelectCodeToAssetByIndex(int index)
    {
        if (index >= 0 && index < filePathList.Count)
        {
            string fullPath = filePathList[index];
            ExcelDataReader.CreateOneExcelAsset(fullPath);
        }
        else
        {
            ExcelDataReader.CreateAllExcelAsset();
        }
    }
}

#endif