using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
public class RecordMgr : MonoSingleton<RecordMgr>
{
    public List<TextRecord> recordList =null;

    private string res = "Assets/Record/";
    private void Awake()
    {
        base.Awake();
        //if (recordList == null)
        //{
        //    recordList = ResMgr.GetInstance().Load<FinalText>("TextRecord").textList;
        //}


       
    } 
    
    
    //public void AddRecord(string _name,List<string> _content,int _RAND)
    //{
    //    int id = recordList2.Count;
    //    TextRecord _record = ScriptableObject.CreateInstance<TextRecord>(); 
    //    _record.id = id;
    //    _record.title = _name;
    //    _record.content= _content;
    //    _record.rand = _RAND;
    //    _record.reply = "��";
    //    _record.hasRead = false;
    //    AssetDatabase.CreateAsset(_record, res+id+".asset");
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //    recordList.Add(_record);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //}




    public void SaveByJson(string _name, List<string> _content, int _RAND)
    {
        //���л����̣���Save����ת��Ϊ�ֽ����� 
        //����Save���󲢱��浱ǰ��Ϸ״̬
      
        Save save = new Save();
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets");
        var _files = di.GetFiles("*");
        save.id = _files.Length ;
        save.title = _name;
        save.content = _content;
        save.rand = _RAND;
        save.reply = "��";
        save.hasRead = false;
        //�����ַ���filePath�����ļ�·����Ϣ��������Assets�д�����һ���ļ�������ΪStreamFile,Ȼ��ϵͳ����Ҵ���һ��byJson.json���ڱ�����Ϸ��Ϣ��
        string filePath = Application.dataPath + "/StreamingAssets/" + save.id.ToString() +".json";
        //����JsonMapper��save����ת��ΪJson��ʽ���ַ���(����Ҫ���������ռ�using LitJson)
        string saveJsonStr = JsonMapper.ToJson(save);
        //������ַ���д�뵽�ļ���
        //����һ��StreamWriter,�����ַ���д�뵽�ļ���
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //�ر�StreamWriter
        sw.Close();
    }

    public void ChangeReadJson(string path)
    {
        //���л����̣���Save����ת��Ϊ�ֽ����� 
        //����Save���󲢱��浱ǰ��Ϸ״̬

        Save save = LoadByJson(path);

        save.hasRead = true;
        //�����ַ���filePath�����ļ�·����Ϣ��������Assets�д�����һ���ļ�������ΪStreamFile,Ȼ��ϵͳ����Ҵ���һ��byJson.json���ڱ�����Ϸ��Ϣ��
        string filePath = path;
        //����JsonMapper��save����ת��ΪJson��ʽ���ַ���(����Ҫ���������ռ�using LitJson)
        string saveJsonStr = JsonMapper.ToJson(save);
        //������ַ���д�뵽�ļ���
        //����һ��StreamWriter,�����ַ���д�뵽�ļ���
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //�ر�StreamWriter
        sw.Close();


    }


    public Save LoadByJson(string filepath)
    {

        if (File.Exists(filepath))
        {
            //����һ��StreamReader,������ȡ��
            StreamReader sr = new StreamReader(filepath);
            //����ȡ��������ֵ��jsonStr
            string jsonStr = sr.ReadToEnd();
            //�ر�
            sr.Close();

            //���ַ���jsonStrת��ΪSave����
            Save save = JsonMapper.ToObject<Save>(jsonStr);

            return save;

          
        }
        return null;
    }


}
