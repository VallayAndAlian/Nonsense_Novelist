using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CharacterDetail_t : MonoBehaviour
{
    private GameObject nowObj;
    private AbstractCharacter nowCharacter;
    [Header("�ֶ�����4�����")]
    public Transform panel_state;
    public Transform panel_item;
    public Transform panel_skill;
    public Transform panel_bg;
    [Header("�ֶ�����buffGourp�����壨1234��")]
    public Transform buffP;

    [Header("�ֶ�����label�����壨1234��")]
    public Transform labelP;
    public Button label_state;
    [Header("�ֶ����ý�ɫ������Ϣ")]
    public Image bookIcon;
    private string bookAdr = "WordImage/Book/";
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameTrait;
    public Image sprite;
    private string spriteAdr = "WordImage/Character/";

    [Header("���ɴ������������")]
    public GameObject itemPerfab;
    public GameObject buffPerfab;
    public GameObject skillPerfab;
    public GameObject energyPerfab;
    public Color colorHasEnergy;
    public Color colorNoEnergy;
    private float energyOffset = 60;//һ��֮���ÿ��������ֵ�м�ļ����С
    private float energyOffsetWith = 150;//��һ������ֵ��x�������ҵ�λ��
    private Dictionary<string,int> itemDic=new Dictionary<string, int>();
    private void awa()
    {

    }

    
    private void OpenInit()
    {

        label_state.Select();
        CharacterManager.instance.pause = true;
        Time.timeScale = 0f;

        SetPanal(0);

        itemDic.Clear();

        //baseInfo
        Sprite _s1 = Resources.Load<Sprite>(bookAdr + nowCharacter.bookName.ToString());
        if (_s1 == null) _s1 = Resources.Load<Sprite>(bookAdr + "HongLouMeng");
        bookIcon.sprite = _s1;

        nameText.text = nowCharacter.wordName;
        nameTrait.text = nowCharacter.roleName;

        Sprite _s2 = Resources.Load<Sprite>(spriteAdr + nowCharacter.wordName.ToString());
        if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
        sprite.sprite = _s2;

        //panel1
        panel_state.GetComponentInChildren<Slider>().value = nowCharacter.hp/nowCharacter.maxHp;
        panel_state.GetComponentsInChildren<Text>()[0].text = nowCharacter.hp.ToString() + "/" + (nowCharacter.maxHp* nowCharacter.maxHpMul).ToString();

        panel_state.GetComponentsInChildren<Text>()[1].text = (nowCharacter.atk*nowCharacter.atkMul).ToString()+
            " \n<color=#787878><size=25>( " + nowCharacter.atk.ToString() + " * "+(nowCharacter.atkMul*100).ToString()+ "% </size></color> )";
       
        panel_state.GetComponentsInChildren<Text>()[2].text = (nowCharacter.def * nowCharacter.defMul).ToString() +
            " \n<color=#787878><size=25>( " + nowCharacter.def.ToString() + " * " + (nowCharacter.defMul * 100).ToString() + "%</size></color>  )";

        panel_state.GetComponentsInChildren<Text>()[3].text = (nowCharacter.san * nowCharacter.sanMul).ToString() +
            " \n<color=#787878><size=25>( " + nowCharacter.san.ToString() + " * " + (nowCharacter.sanMul * 100).ToString() + "%</size></color> )";

        panel_state.GetComponentsInChildren<Text>()[4].text = (nowCharacter.psy * nowCharacter.psyMul).ToString() +
      "\n<color=#787878><size=25> ( " + nowCharacter.psy.ToString() + " * " + (nowCharacter.psyMul * 100).ToString() + "%</size></color> )";


        foreach (var buff in nowCharacter.GetComponents<AbstractBuff>())
        {
            //���ɶ�Ӧ��
            PoolMgr.GetInstance().GetObj(buffPerfab, (obj) =>
            {
          
                obj.transform.parent = buffP;
                obj.transform.localScale = Vector3.one;
                Sprite buffSprite = Resources.Load<Sprite>("WordImage/Buffs/" + buff.GetType().ToString());
                if (buffSprite == null)
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("WordImage/Buffs/Default");
                else
                    obj.GetComponent<Image>().sprite = buffSprite;
            });
        }


        //panel2

        foreach (var item in nowCharacter.GetComponents<AbstractItems>())
        {
            if (itemDic.ContainsKey(item.wordName))
            {
                itemDic[item.wordName] += 1;
                panel_item.transform.Find(item.wordName).GetComponentInChildren<TextMeshProUGUI>().text = item.wordName+"   x"+ itemDic[item.wordName];
            }
            else
            {
                itemDic.Add(item.wordName,1);

                //���ɶ�Ӧ��
                PoolMgr.GetInstance().GetObj(itemPerfab, (obj) =>
                {
                    obj.AddComponent(item.GetType());
                    obj.name = item.wordName;
                    obj.transform.parent = panel_item;
                    obj.transform.localScale = Vector3.one;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = item.wordName;
                });
            }
           
        }
        //panel3
        //��ȡ��ɫ�ļ����б�

        if (nowCharacter.skills.Count > 3) print(nowCharacter.name + "����������3��");
        for (int x = 0; x < nowCharacter.skills.Count; x++)
        {
            // panel_skill.GetChild(x).GetComponent<Text>().text = abschara.skills[x].wordName;
            PoolMgr.GetInstance().GetObj(skillPerfab, (obj) =>
            {
                obj.transform.GetChild(0).gameObject.AddComponent(nowCharacter.skills[x].GetType());
                obj.transform.parent = panel_skill;
                 obj.transform.localScale = Vector3.one;
                 obj.GetComponentInChildren<TextMeshProUGUI>().text = nowCharacter.skills[x].wordName;

                 for (int i = 0; i < nowCharacter.skills[x].needCD; i++)
                 {
                     PoolMgr.GetInstance().GetObj(energyPerfab, (o) =>
                     {
                         o.transform.parent = obj.transform.GetChild(1);
                         o.transform.localScale = Vector3.one * 0.6f;
                         o.transform.localPosition = new Vector3(i * energyOffset + energyOffsetWith, 0, 0);
                         o.GetComponent<Image>().color = (i < nowCharacter.skills[x].CD) ? colorHasEnergy : colorNoEnergy;
                         o.transform.GetChild(0).gameObject.SetActive((i < nowCharacter.skills[x].CD) ? true : false);
                     });
                 }
             });

           
        }

        
    
        //panel4
        panel_bg.GetComponentInChildren<Text>().text = nowCharacter.description;
    }

    private void SetPanal(int i)
    {
        switch (i)
        {
            case 0:
                {
                    panel_state.gameObject.SetActive(true);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(true);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(false);
                } break;
            case 2:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(true);
                    panel_bg.gameObject.SetActive(false);
                } break;
            case 3:
                {
                    panel_state.gameObject.SetActive(false);
                    panel_item.gameObject.SetActive(false);
                    panel_skill.gameObject.SetActive(false);
                    panel_bg.gameObject.SetActive(true);
                } break;
        }
    }

    #region �����ⲿ����

    public void CloseDetail()
    {
        Camera.main.GetComponent<MouseDown>().CloseDetail();
        Destroy(this.transform.parent.gameObject);
        CharacterManager.instance.pause = false;
        Time.timeScale = 1f;
    }
    public void SetCharacter(GameObject _ac)
    {
        nowObj = _ac;
        nowCharacter = _ac.GetComponent<AbstractCharacter>();
        OpenInit();

    }
    public void ClickLabel_state()
    {
        SetPanal(0);
    }
    public void ClickLabel_item()
    {
        SetPanal(1);
    }
    public void ClickLabel_skill()
    {
        SetPanal(2);
    }
    public void ClickLabel_bg()
    {
        SetPanal(3);
    }
    #endregion
}
