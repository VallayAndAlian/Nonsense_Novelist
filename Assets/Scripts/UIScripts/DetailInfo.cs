using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailInfo : MonoBehaviour
{
    public Text infoName;
    public Text infoContent;

    public void CloseInfo()
    {
        Destroy(this.transform.gameObject);

    }
    public void SetInfo(string _name, string _content)
    {
        infoName.text = _name;
        infoContent.text = _content;
    }
}
