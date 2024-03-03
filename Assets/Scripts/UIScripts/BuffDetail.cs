using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDetail : MonoBehaviour
{
    public GameObject infoPerfab;
    /// <summary> 脚本的名称 </summary>
    public string wordname;
    public void ClickBuff()
    {
        
        var a = Instantiate(infoPerfab, this.transform.parent.parent.parent.transform);
     
         var _s = wordname;
        System.Type wordType = System.Type.GetType(_s);
        if (wordType != null)
        {
            if (wordType.GetField("s_wordName") == null) print("在" + wordType.ToString() + "中没有定义静态成员s_wordName/s_description");

            a.GetComponent<DetailInfo>().SetInfo((string)wordType.GetField("s_wordName").GetValue(null), (string)wordType.GetField("s_description").GetValue(null));

        }
    }
}
