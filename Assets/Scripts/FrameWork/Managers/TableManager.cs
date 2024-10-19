
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TableManager
{
    public static List<TableBase> Tables = new List<TableBase>();

    public void Add<T>(T table) where T : TableBase
    {
        Tables.Add(table);
    }

    public void AddTables()
    {
        Add(new AbilityTable());
        Add(new AbilityTemplateTable());
    }

    public void Init()
    {
        AddTables();
        LoadTables();
    }

    public void LoadTables()
    {
        foreach (var table in Tables)
        {
            var meta = table.Load();

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
        }
    }
}

