#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

public class ExcelCodeCreater
{

    #region --- Create Code ---

    //�������룬��������C#��
    public static string CreateCodeStrByExcelData(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;
        //Excel����
        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;
        //Dictionary<�ֶ�����, �ֶ�����>
        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;
        //List<һ������>��List<Dictionary<�ֶ�����, һ�е�ÿ����Ԫ���ֶ�ֵ>>
        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;
        //����������
        string itemClassName = excelName + "ExcelItem";
        //������������
        string dataClassName = excelName + "ExcelData";

        //������
        StringBuilder classSource = new StringBuilder();
        classSource.Append("/*Auto Create, Don't Edit !!!*/\n");
        classSource.Append("\n");
        //�������
        classSource.Append("using UnityEngine;\n");
        classSource.Append("using System.Collections.Generic;\n");
        classSource.Append("using System;\n");
        classSource.Append("using System.IO;\n");
        classSource.Append("\n");
        //�����������࣬��¼ÿ������
        classSource.Append(CreateExcelRowItemClass(itemClassName, propertyNameTypeDic));
        classSource.Append("\n");
        //�������������࣬��¼����Excel������������
        classSource.Append(CreateExcelDataClass(dataClassName, itemClassName));
        classSource.Append("\n");
        //����Asset�����࣬�����Զ�����Excel��Ӧ��Asset�ļ�����ֵ
        classSource.Append(CreateExcelAssetClass(excelMediumData));
        classSource.Append("\n");
        return classSource.ToString();
    }

    //----------

    //������������
    private static StringBuilder CreateExcelRowItemClass(string itemClassName, Dictionary<string, string> propertyNameTypeDic)
    {
        //����Excel��������
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[Serializable]\n");
        classSource.Append("public class " + itemClassName + " : ExcelItemBase\n");
        classSource.Append("{\n");
        //���������ֶ�
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append(CreateCodeProperty(item.Key, item.Value));
        }
        classSource.Append("}\n");
        return classSource;
    }

    //�������������ֶ�
    private static string CreateCodeProperty(string name, string type)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        if (name == "id")
            return null;

        //�ж��ֶ�����
        if (type == "int" || type == "Int" || type == "INT")
            type = "int";
        else if (type == "float" || type == "Float" || type == "FLOAT")
            type = "float";
        else if (type == "bool" || type == "Bool" || type == "BOOL")
            type = "bool";
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
            type = type.Split('|').LastOrDefault();
        else
            type = "string";
        //����
        string propertyStr = "\tpublic " + type + " " + name + ";\n";
        return propertyStr;
    }

    //----------

    //����������
    private static StringBuilder CreateExcelDataClass(string dataClassName, string itemClassName)
    {
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[CreateAssetMenu(fileName = \"" + dataClassName + "\", menuName = \"Excel To ScriptableObject/Create " + dataClassName + "\", order = 1)]\n");
        classSource.Append("public class " + dataClassName + " : ExcelDataBase<" + itemClassName + ">\n");
        classSource.Append("{\n");
        //�����ֶΣ�������������
        //classSource.Append("\tpublic " + itemClassName + "[] items;\n");
        classSource.Append("}\n");
        return classSource;
    }

    //----------

    //����Asset������
    private static StringBuilder CreateExcelAssetClass(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;

        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;

        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;

        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;

        string itemClassName = excelName + "ExcelItem";
        string dataClassName = excelName + "ExcelData";

        StringBuilder classSource = new StringBuilder();
        classSource.Append("#if UNITY_EDITOR\n");
        //����
        classSource.Append("public class " + excelName + "AssetAssignment\n");
        classSource.Append("{\n");
        //������
        classSource.Append("\tpublic static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)\n");
        //�����壬������Ҫ�ɼ���try/catch
        classSource.Append("\t{\n");
        classSource.Append("\t\tif (allItemValueRowList == null || allItemValueRowList.Count == 0)\n");
        classSource.Append("\t\t\treturn false;\n");
        classSource.Append("\t\tint rowCount = allItemValueRowList.Count;\n");
        classSource.Append("\t\t" + itemClassName + "[] items = new " + itemClassName + "[rowCount];\n");
        classSource.Append("\t\tfor (int i = 0; i < items.Length; i++)\n");
        classSource.Append("\t\t{\n");
        classSource.Append("\t\t\titems[i] = new " + itemClassName + "();\n");
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append("\t\t\titems[i]." + item.Key + " = ");

            classSource.Append(AssignmentCodeProperty("allItemValueRowList[i][\"" + item.Key + "\"]", propertyNameTypeDic[item.Key]));
            classSource.Append(";\n");
        }
        classSource.Append("\t\t}\n");
        classSource.Append("\t\t" + dataClassName + " excelDataAsset = ScriptableObject.CreateInstance<" + dataClassName + ">();\n");
        classSource.Append("\t\texcelDataAsset.items = items;\n");
        classSource.Append("\t\tif (!Directory.Exists(excelAssetPath))\n");
        classSource.Append("\t\t\tDirectory.CreateDirectory(excelAssetPath);\n");
        classSource.Append("\t\tstring pullPath = excelAssetPath + \"/\" + typeof(" + dataClassName + ").Name + \".asset\";\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.DeleteAsset(pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.Refresh();\n");
        classSource.Append("\t\treturn true;\n");
        classSource.Append("\t}\n");
        //
        classSource.Append("}\n");
        classSource.Append("#endif\n");
        return classSource;
    }

    //����Asset�������ֶ�
    private static string AssignmentCodeProperty(string stringValue, string type)
    {
        //�ж�����
        if (type == "int" || type == "Int" || type == "INT")
        {
            return "Convert.ToInt32(" + stringValue + ")";
        }
        else if (type == "float" || type == "Float" || type == "FLOAT")
        {
            return "Convert.ToSingle(" + stringValue + ")";
        }
        else if (type == "bool" || type == "Bool" || type == "BOOL")
        {
            return "Convert.ToBoolean(" + stringValue + ")";
        }
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
        {
            return "(" + type.Split('|').LastOrDefault() + ")(Convert.ToInt32(" + stringValue + "))";
        }
        else
            return stringValue;
    }

    #endregion

}
#endif