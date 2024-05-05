using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Diagnostics;
using UnityEngine.UIElements;

public class BookShelf : MonoBehaviour
{
    [Header("�ֶ������鼮˳��Ҫ��panel˳���Ӧ��")]
    public Button[] books;
    public PanelInstance[] panels;

    [Header("�ֶ����ô�����ʾ�ĸ�����")]
    public Transform wordP;

    [Header("����Ԥ���壨�ֶ���")]
    public GameObject prefab_wordinf;
    public GameObject prefab_chara;
    public GameObject prefab_setting;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ѡ������鱾ҳ��
    /// </summary>
    public void ClickBook()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(buttonSelf!=null)
        {
            print(BookNameEnum.HongLouMeng.ToString());
            if (buttonSelf.name == BookNameEnum.HongLouMeng.ToString())
            {//�򿪺�¥��ҳ��+Ĭ����ʾȫ����¥�δ���
                panels[(int)BookNameEnum.HongLouMeng].gameObject.SetActive(true);
                //��+��+��
                ShowNowBookWord(2,BookNameEnum.HongLouMeng);//231
                ShowNowBookWord(3, BookNameEnum.HongLouMeng);
                ShowNowBookWord(1, BookNameEnum.HongLouMeng);
            }
            else if (buttonSelf.name == books[1].name)
            {
                panels[1].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[2].name)
            {
                panels[2].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[3].name)
            {
                panels[3].gameObject.SetActive(true);
            }
            else if (buttonSelf.name == books[4].name)
            {
                panels[4].gameObject.SetActive(true);
            }
        }
        
    }
    /// <summary>
    /// ����ʾ���������д����ջ�
    /// </summary>
    void DeleteWords()
    {

        for (int i = wordP.childCount - 1; i >= 0; i--)
        {
            foreach (var a in wordP.GetChild(i).GetComponents<AbstractWord0>())
            {
                Destroy(a);
            }
            PoolMgr.GetInstance().PushObj(wordP.GetChild(i).gameObject.name, wordP.GetChild(i).gameObject);
        }

    }


    /// <summary>
    /// չʾ��nowbook�е����д�
    /// </summary>231
    /// <param name="i">���ԡ�1adj,2noun,3verb</param>
    void ShowNowBookWord(int i, BookNameEnum _book)
    {
        switch (_book)
        {
            case BookNameEnum.HongLouMeng:
                {
                    if (i == 1)
                        InsWords(AllSkills.hlmList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.hlmList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.hlmList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.CrystalEnergy:
                {
                    if (i == 1)
                        InsWords(AllSkills.crystalList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.crystalList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.crystalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ZooManual:
                {
                    if (i == 1)
                        InsWords(AllSkills.animalList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.animalList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.animalList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.PHXTwist:
                {
                    if (i == 1)
                        InsWords(AllSkills.maYiDiGuoList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.maYiDiGuoList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.maYiDiGuoList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.FluStudy:
                {
                    if (i == 1)
                        InsWords(AllSkills.liuXingBXList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.liuXingBXList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.liuXingBXList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.EgyptMyth:
                {
                    if (i == 1)
                        InsWords(AllSkills.aiJiShenHuaList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.aiJiShenHuaList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.aiJiShenHuaList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.ElectronicGoal:
                {
                    if (i == 1)
                        InsWords(AllSkills.humanList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.humanList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.humanList_verb, WordKindEnum.verb);
                }
                break;
            case BookNameEnum.Salome:
                {
                    if (i == 1)
                        InsWords(AllSkills.shaLeMeiList_adj, WordKindEnum.adj);
                    else if (i == 2)
                        InsWords(AllSkills.shaLeMeiList_noun, WordKindEnum.noun);
                    else
                        InsWords(AllSkills.shaLeMeiList_verb, WordKindEnum.verb);

                }
                break;
        }
    }


    /// <summary>
    /// �����е������δ�����ʾ����
    /// </summary>
    void InsWords(List<Type> wordsList, WordKindEnum wordKind)
    {
        switch (wordKind)
        {
            case WordKindEnum.adj:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            //������Ϣ
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textAdj;
                            jiuwei.resName = obj.GetComponent<WordInformation>().resAdrAdj + "adj_" + ((AbstractAdjectives)_word).adjID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                            { jiuwei.wordImage.sprite = obj.GetComponent<WordInformation>().defaultWordImage; }
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            obj.transform.parent = wordP;
                            obj.transform.localScale = Vector3.one * 1.5f;
                        });
                    }
                }
                break;
            case WordKindEnum.noun:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            //������Ϣ
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textNoun;

                            jiuwei.resName = jiuwei.resAdrNoun + "n_" + ((AbstractItems)_word).itemID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                                jiuwei.wordImage.sprite = jiuwei.defaultWordImage;
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            obj.transform.parent = wordP;
                            obj.transform.localScale = Vector3.one * 1.5f;
                        });
                    }
                }
                break;
            case WordKindEnum.verb:
                {
                    foreach (var word in wordsList)
                    {
                        PoolMgr.GetInstance().GetObj(prefab_wordinf, (obj) =>
                        {
                            var _word = obj.AddComponent(word) as AbstractWord0;
                            //������Ϣ
                            WordInformation jiuwei = obj.GetComponent<WordInformation>();
                            jiuwei.wordkindText.text = jiuwei.textVerb;

                            jiuwei.resName = jiuwei.resAdrVerb + "v_" + ((AbstractVerbs)_word).skillID;
                            jiuwei.tepSprite = Resources.Load<Sprite>(jiuwei.resName);
                            if (jiuwei.tepSprite == null)
                                jiuwei.wordImage.sprite = jiuwei.defaultWordImage;
                            else
                                jiuwei.wordImage.sprite = Resources.Load<Sprite>(jiuwei.resName);
                            //
                            obj.transform.parent = wordP;
                            obj.transform.localScale = Vector3.one * 1.5f;
                        });
                    }
                }
                break;


        }


    }
    void CharaShow(List<Type> charasList,BookNameEnum bookKind)
    {
        foreach (var chara in charasList)
        {
            PoolMgr.GetInstance().GetObj(prefab_chara, (obj) =>
            {
                var _chara = obj.AddComponent(chara) as AbstractCharacter;
                //������Ϣ
                
                //
                obj.transform.parent = wordP;
                obj.transform.localScale = Vector3.one * 1.5f;
            });
        }
    }
}
