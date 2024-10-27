
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class TableManager
{
    public static List<TableBase> Tables = new List<TableBase>();

    public static void Add<T>(T table) where T : TableBase
    {
        Tables.Add(table);
    }

    public static void AddTables()
    {
        Add(new AbilityTable());
        Add(new AbilityTemplateTable());
    }

    public static void ReadTables()
    {
        Debug.Log("----------------------Begin read tables---------------------");
        
        AddTables();
        LoadTables();
        
        Debug.Log("----------------------Finish read tables---------------------");
    }

    public static void LoadTables()
    {
        foreach (var table in Tables)
        {
            table.PreLoad();
            
            var meta = table.Load();
            
            if (meta.mErrorType == TableErrorType.None)
            {
                table.PostLoad();
            }
            
            // print table load info
            switch (meta.mErrorType)
            {
                case TableErrorType.None:
                    Debug.Log($"Load table {table.AssetName} success!");
                    break;
                
                case TableErrorType.NotExist:
                    Debug.LogError($"Not Find {table.AssetName}!");
                    break;
                
                case TableErrorType.ParseLine:
                    Debug.Log($"Failed to parse table {table.AssetName} in line {meta.row} col {meta.col}!");
                    break;
                
                case TableErrorType.Duplicate:
                    Debug.Log($"table {table.AssetName} has duplicate key in line {meta.row}!");
                    break;
            }
            
            table.PostLoad();
        }
    }
}

