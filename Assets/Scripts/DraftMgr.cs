using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftMgr : MonoSingleton<DraftMgr>
{
    //文本内容
    public List<string> content = new List<string>();

    


    #region 文本生成

    public void InitContent()
    {
        content.Clear();
    }

    public void AddContent(string _new)
    {

        content.Add(_new);
    }

    public string MergeContent_A()
    {

        string Merge = "";
        foreach (var _content in content)
        {
            Merge += _content;
            Merge += "\n";
        }

        return Merge;
    }
    public string[] MergeContent_B()
    {
        return content.ToArray();
    }
    public void ChangeIndexContent(int oldIndex, int newIndex)
    {
        if (oldIndex == newIndex) return;

        string _new = content[oldIndex];
        List<string> newList = new List<string>();

        if (oldIndex < newIndex)
        {
            for (int x = oldIndex + 1; x < content.Count; x++)
            {

                newList.Add(content[x]);
                if (x == newIndex)
                    newList.Add(_new);
            }
            content.RemoveRange(oldIndex, content.Count - oldIndex);
            content.AddRange(newList);
        }
        else
        {
            for (int x = newIndex; x < content.Count; x++)
            {
                if (x != oldIndex)
                    newList.Add(content[x]);
            }
            content.RemoveRange(newIndex, content.Count - newIndex);
            content.Add(_new);
            content.AddRange(newList);
        }



    }
    #endregion
}
