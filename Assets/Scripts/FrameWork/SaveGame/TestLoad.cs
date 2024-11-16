using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class TestLoad :MonoBehaviour
{
    public TMP_InputField mTMP;
    public string mName;
    public Button mSaveButton; 
    public Button mLoadButton;
    public void Save()
    {
        if (mTMP.text != "")
        {
            Test test = new Test();
            test.mName = mName;
            test.SaveData();
        }
        else
        {
            Debug.Log("信息为空，无法保存");
        }
    }
    public void Load()
    {
        Test test = new Test();
        test.LoadData();
        mTMP.text=test.mName;
    }
    public void TextChange(string username)
    {
        if (mTMP != null)
        {
            mName=username;
        }
       // Debug.Log(username);
    }
}
public class Test : Save
{
    public override string mFileName => "user";
    public string mName;
    public override bool Write(BinaryFormatter binary, string path)
    {
        string folderPath = GetFolderPath();
        List<string> data = new List<string>();
        data = TraverseDirectory(folderPath);
        if (data!=null)
        {
            foreach (string file in data)
            {
                File.Delete(file);
            }
            FileStream stream = new FileStream(path, FileMode.Create);
            binary.Serialize(stream, mName);
            stream.Close();
            //Debug.Log("写入成功");
            return true;
        }
        else
        {
            
            FileStream stream = new FileStream(path, FileMode.Create);
            binary.Serialize(stream, mName);
            stream.Close();
            //Debug.Log("写入成功");
            return true;
        }
    }
    public override bool Read(BinaryFormatter binary, string path)
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            mName = (string)binary.Deserialize(stream);
            stream.Close();
            return true;
        }
        else
        {
            Debug.LogError("Player data file not found!");
            return false;
        }
    }
}
