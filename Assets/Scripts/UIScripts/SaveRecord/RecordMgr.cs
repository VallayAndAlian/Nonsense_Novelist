using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RecordMgr : MonoSingleton<RecordMgr>
{
    public List<TextRecord> recordList =null;
    private string res = "Assets/Record/";
    private void Awake()
    {
        base.Awake();
        if (recordList == null)
        {
            recordList = ResMgr.GetInstance().Load<FinalText>("TextRecord").textList;
        }


       
    } 
    
    
    public void AddRecord(string _name,List<string> _content,int _RAND)
    {
        int id = recordList.Count;
        TextRecord _record = ScriptableObject.CreateInstance<TextRecord>(); 
        _record.id = id;
        _record.title = _name;
        _record.content= _content;
        _record.rand = _RAND;
        _record.reply = "нч";
        _record.hasRead = false;
        AssetDatabase.CreateAsset(_record, res+id+".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        recordList.Add(_record);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

   
}
