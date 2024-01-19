using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DraftUi : MonoBehaviour
{

    // 不换行的的空格符
    public static readonly string NO_BREAKING_SPACE = "\u00A0";//"\u3000";

    public TMP_InputField inputField;
    public TextMeshProUGUI text;
    bool a = false;
    private void Start()
    {
        inputField.text = "";
      
        text.gameObject.SetActive(true);
        a = false;
    }
    private void Update()
    {
        if (inputField.isFocused&&(!a))
        {
            Debug.Log("inputField.isFocused");
            Func1();
        }
        if (!inputField.isFocused && (a))
        {
            Debug.Log("inputField.isFocused");
            Func2();
        }
    }
    void Func1()
    {
        inputField.text = text.text;
        text.gameObject.SetActive(false);
        a = true;
    }
    void Func2()
    {
      
        text.text = HandleTextContentFormat(inputField.text);
        inputField.text = "";
        text.gameObject.SetActive(true);
        a = false;
    }
    public static string HandleTextContentFormat(string content)
    {
        if (content.Contains(" "))
        {
            content = content.Replace(" ", NO_BREAKING_SPACE);
        }

        return content;
    }
    public void CheckContent()
    {
        inputField.text=HandleTextContentFormat(inputField.text);
    }
}



