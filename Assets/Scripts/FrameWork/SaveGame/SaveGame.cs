﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
[System.Serializable]
public abstract class Save
{
    public abstract string mFileName { get; }
    public  string mMd5 { get; set; }   
    public string GetFolderPath()
    {
        string folderPath = Path.Combine(Application.dataPath + "SaveUserI");
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("尚未保存信息");
            return null;
        }
        else
        {
            return folderPath;
        }
    }
    //获取文件路径
    public string GetFilePath()
    {
        string folderPath = Path.Combine(Application.dataPath+"SaveUserI");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); // 创建文件夹
            return Path.Combine(folderPath, mFileName+".bin");
        }
        else
        {
            //Debug.Log("文件夹已存在。");
            return Path.Combine(folderPath, mFileName + ".bin");
        }
    }
    public bool LoadData()//rewrite
    {
        string path = GetFilePath();
        string folderPath = GetFolderPath();
        List<string> data = new List<string>();
        data = TraverseDirectory(folderPath);
        foreach (string file in data)
        {
            path = file;//获取有md5的文件路径
        }
        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        //1.read file data
        string[] words = path.Split('.');
        foreach (var word in words)
        {
            System.Console.WriteLine($"<{word}>");
        }
        mMd5 = words[1];
        bool isok = VerifyMD5Hash(stream,mMd5);
        stream.Close();
        //2.Deseriolize(read)
        Read(binary,path);
        if (Read(binary, path)&&isok)
        {
            Debug.Log("加载成功！");
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool SaveData()
    {
        string path = GetFilePath();
        BinaryFormatter binary = new BinaryFormatter();
        //1.seriolize(write)
        bool isw=Write(binary,path);
        //2.save file data
        if (isw)//序列化成功用md5加密获取字符串
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            mMd5 = GetMd5FromStream(stream);
            stream.Close();
            int index = path.IndexOf('.');
            string oFileName =path;
            string nFileName = path.Substring(0, index) + "." + mMd5+".bin";
            File.Move(oFileName, nFileName);
           // Debug.Log(nFileName);
            //Debug.Log(Path.GetFileName(nFileName));
            Debug.Log("保存成功！");
            return true;
        }
        else
        {
            return false;
        }
    }
    public abstract bool Write(BinaryFormatter binary,string path);
    public abstract bool Read(BinaryFormatter binary, string path);
    /// <summary>
    /// 从指定文件夹获取相应文件
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public List<string> TraverseDirectory(string folderPath)
    {
        List<string> xlsxFiles = new List<string>();
        string searchPattern = mFileName+"*"; // 使用通配符来匹配文件名的一部分，*表示任意字符
        string[] files = Directory.GetFiles(folderPath, searchPattern);
        foreach (string file in files)
        {
            // 将文件路径添加到列表中
            xlsxFiles.Add(file);
            //Debug.Log(file);
        }

        // 遍历当前目录下的所有子目录
        foreach (string subdirectory in Directory.GetDirectories(folderPath))
        {
            // 递归调用遍历子目录，并将结果合并到列表中
            xlsxFiles.AddRange(TraverseDirectory(subdirectory));
        }

        return xlsxFiles;
    }
    /// <summary>
    /// 将一个文件转换为md5字符串，并保存
    /// </summary>
    /// <param name="fileName">File name.</param>
    void PaseFile(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);//文件路径
        string fileMd5Path = System.IO.Path.Combine(Application.streamingAssetsPath, "md5_" + fileName);//md5 存储路径
        if (System.IO.File.Exists(filePath))
        {
            using (System.IO.FileStream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open))
            {
                stream.Position = 0;//从文件首部开始
                string md5 = GetMd5FromStream(stream);//获取文件对应的md5数据
                System.IO.FileStream fs = new System.IO.FileStream(fileMd5Path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                byte[] buff = System.Text.Encoding.UTF8.GetBytes(md5);
                fs.Write(buff, 0, buff.Length);//保存生成的md5信息
                fs.Close();
                Debug.Log("<color=#ff0000> change over</color>");
            }
        }
    }
    /// <summary>
    /// 
    /// 通过数据流获取对应的md5文件
    /// </summary>
    /// <returns>The md5.</returns>
    /// <param name="stream">Stream.</param>
    string GetMd5FromStream(System.IO.FileStream stream)
    {
        byte[] buff;
        using (System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
        {
            buff = md5.ComputeHash(stream);

        }
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (var item in buff)
        {
            builder.Append(item.ToString("x2").ToLower());//把二进制的字节，转换为16进制的数字形式的字符串
        }
        string res = builder.ToString();
       // Debug.Log(res);
        return res;
    }
    /// <summary>
    /// 验证MD5
    /// </summary>
    /// <param name="stream">数据流</param>
    /// <param name="hash">保存的MD5码</param>
    /// <returns></returns>
    public bool VerifyMD5Hash(System.IO.FileStream stream, string md5)
    {
        string hashOfInput = GetMd5FromStream(stream);
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        return 0 == comparer.Compare(hashOfInput, md5);
    }
    /// <summary>
    /// 通过字节数组获取md5字符串
    /// </summary>
    /// <returns>The md5 from bytes.</returns>
    /// <param name="data">Data.</param>
    string GetMd5FromBytes(byte[] data)
    {
        byte[] buff;
        using (System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
        {
            buff = md5.ComputeHash(data);
        }
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (var item in buff)
        {
            builder.Append(item.ToString("x2").ToLower());//把二进制的字节，转换为16进制的数字形式的字符串
        }
        string res = builder.ToString();
        Debug.Log(res);
        return res;

    }
}
public class CreateMD5
{
    //单例模式
    private static CreateMD5 createmd5;
    public static CreateMD5 Createmd5
    {
        get
        {
            if (createmd5 == null)
                createmd5 = new CreateMD5();
            return createmd5;
        }
    }

    public string inputString;
    public string hashString;
    public MD5 md5Hash;
    //调用其他方案之前要先实例化MD5对象
    public void MD5Init()
    {
        md5Hash = MD5.Create();
    }
    //生成加密字符串
    public string GetMD5Hash(string input)
    {
        //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder builder = new StringBuilder();
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int cont = 0; cont < data.Length; cont++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
            builder.Append(data[cont].ToString("x2"));
        }
        return builder.ToString();
    }
    //对加密字符串进行验证
    public bool VerifyMD5Hash(string input, string hash)
    {
        string hashOfInput = GetMD5Hash(input);

        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return 0 == comparer.Compare(hashOfInput, hash);
    }
}