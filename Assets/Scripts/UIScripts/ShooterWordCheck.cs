using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShooterWordCheck : MonoBehaviour
{

    [Header("�ƿ������(�ֶ�)")]
    public GameObject mainPanal;

    [Header("�ƿ���������(�ֶ�)")]
    public Text textCount;
    public Transform wordsArea;



    [Header("�ƿ�Ԥ����(�ֶ�)")]
    public GameObject word_adj;
    public GameObject word_verb;
    public GameObject word_item;
    // Start is called before the first frame update
    void Start()
    {
        CloseMainPanal();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 1

    void SetParameter()
    {
        
    }
    void OpenMainPanal()
    {
        mainPanal.SetActive(true);

        //�������ڵ��ƿ�����
        textCount.text = GameMgr.instance.GetNowList().Count.ToString()+"/" + GameMgr.instance.GetAllList().Count.ToString();

        foreach (var _word in GameMgr.instance.GetNowList())
        {
            
            PoolMgr.GetInstance().GetObj(word_adj, (obj) =>
            {
                var word = obj.AddComponent(_word) as AbstractWord0;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = wordsArea;
                obj.transform.localScale = Vector3.one;
            });
        }
        foreach (var _word in GameMgr.instance.GetHasUsedList())
        {

            PoolMgr.GetInstance().GetObj(word_item, (obj) =>
            {
                var word = obj.AddComponent(_word) as AbstractWord0;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = word.wordName;
                obj.transform.parent = wordsArea;
                obj.transform.localScale = Vector3.one;
            });
        }
    }

    void CloseMainPanal()
    {
        mainPanal.SetActive(false);

        for(int i= wordsArea.childCount-1; i>=0;i--)
        {
            PoolMgr.GetInstance().PushObj(wordsArea.GetChild(i).gameObject.name, wordsArea.GetChild(i).gameObject);
        }
    }
    #endregion


    #region �ⲿ����¼�

    /// <summary>
    /// ����ⲿ���-������
    /// </summary>
    public void ClickShooterButton()
    {
        OpenMainPanal();
        CharacterManager.instance.pause = true;
    }

    public void ClickExitButton()
    {
        CloseMainPanal();
        CharacterManager.instance.pause = false;
    }

    #endregion
}
