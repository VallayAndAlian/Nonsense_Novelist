using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �����ƿ�UI��
/// </summary>
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

    public bool jiaoYi = false;


    [Header("�ƿ���������(�ֶ�)")]
    public Button btn_cancel;
    public Button btn_Check;
    public Button btn_exit;
    #region 1

    public void OpenMainPanal()
    {
        mainPanal.SetActive(true); 
        if (!jiaoYi)
        {
   
            btn_Check.gameObject.SetActive(false);
            btn_cancel.gameObject.SetActive(false);
            btn_exit.gameObject.SetActive(true);
        }
        else
        {
            btn_Check.gameObject.SetActive(true);
            btn_cancel.gameObject.SetActive(true);
            btn_exit.gameObject.SetActive(false);
            GetComponent<Animator>().Play("CardRes_Up");
        }
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

                if (jiaoYi)
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => ClickThis(obj));

                }
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

                if (jiaoYi)
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => ClickThis(obj));

                }
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

    [HideInInspector]public AbstractWord0 chooseWord=null;
    void ClickThis(GameObject _botton)
    {
        chooseWord = _botton.GetComponent<AbstractWord0>();
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
    public void ClickCancelButton()
    {
        GetComponent<Animator>().Play("CardRes_Down");
    }
    public void ClickCheckButton()
    {
        GetComponent<Animator>().Play("CardRes_Down");
        GameMgr.instance.DeleteCardList(chooseWord);
        GameMgr.instance.AddCardList(this.transform.parent.GetComponent<EventUI>().JY_chooseWord);
        this.transform.parent.GetComponent<EventUI>().CloseAnim();
    }

    #endregion

    #region �����¼�
    public void Anim_Down()
    {
        CloseMainPanal();
    }
    #endregion
}
