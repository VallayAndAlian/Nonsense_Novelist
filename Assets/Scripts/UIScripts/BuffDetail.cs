using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDetail : MonoBehaviour
{
    public GameObject infoPerfab;
    /// <summary> �ű������� </summary>
    public string wordname;
    public void ClickBuff()
    {
        
        var a = Instantiate(infoPerfab, this.transform.parent.parent.parent.transform);
     
         var _s = wordname;
        System.Type wordType = System.Type.GetType(_s);
        if (wordType != null)
        {
            if (wordType.GetField("s_wordName") == null) print("��" + wordType.ToString() + "��û�ж��徲̬��Աs_wordName/s_description");

            a.GetComponent<DetailInfo>().SetInfo((string)wordType.GetField("s_wordName").GetValue(null), (string)wordType.GetField("s_description").GetValue(null));

        }
    }
}
