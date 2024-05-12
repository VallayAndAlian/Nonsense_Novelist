using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public static bool firstLetter = false;
    public GameObject firstPanel;
    public GameObject text;
    public Text scoreT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FirstLetter();
    }

    void FirstLetter()
    {
        if (firstLetter)
        {
            text.SetActive(false);
            firstPanel.SetActive(true);
            //scoreT.text = .tostring();
        }
        else
        {
            text.SetActive(true);
            firstPanel.SetActive(false);

        }
    }
}
