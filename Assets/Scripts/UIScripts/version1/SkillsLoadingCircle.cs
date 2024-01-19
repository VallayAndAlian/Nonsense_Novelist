using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

///<summary>
///���ܴ�����ԲȦ������ʾ
///</summary>
class SkillsLoadingCircle : MonoBehaviour
{
    /// <summary>����ԲȦԤ���� </summary>
    public GameObject verbLoadingPrefab;    
    /// <summary>��ɫͷ������ԲȦ�ĸ�����λ�� </summary>
    private Transform[] verbLoadingPoints=new Transform[4];
    /// <summary>ʣ�����ʱ����ֵ </summary>
    private List<Image> verbCD = new List<Image>();
    /// <summary>��ɫλ�� </summary>
    private List<GameObject> skillUIbar = new List<GameObject>();
    /// <summary>��ȡ�ý�ɫ </summary>
    private AbstractCharacter charaComponent;
    [Tooltip("�ֶ����ã�����Ļ�Ĭ��HP")]
    /// <summary>��λ�� </summary>
    public Transform barPoint;
    //
    private int count;

    public void Start()
    {
        charaComponent = gameObject.GetComponent<AbstractCharacter>();

        if(barPoint==null)
            barPoint = gameObject.transform.Find("HP");

        for(int i = 0; i < barPoint.childCount; i++)
        {
            verbLoadingPoints[i] = barPoint.GetChild(i);
        }
        count = charaComponent.skills.Count;
    }
    public void FixedUpdate()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "UIcanvas")
            {
                if(charaComponent.skills.Count>count)
                {
                    //charaComponent.realSkills = charaComponent.GetComponents<AbstractVerbs>();
                    count = charaComponent.skills.Count;
                    skillUIbar.Add(Instantiate(verbLoadingPrefab, canvas.transform));
                    verbCD.Add(skillUIbar[count-1].transform.GetChild(0).GetComponent<Image>()) ;
                    skillUIbar[count-1].transform.position= verbLoadingPoints[count - 1].position;
                }
            }
        }   


        if (skillUIbar.Count != 0&&charaComponent!=null)
        {            
            for (int i = 0; i < count; i++)
            {
                //��ȡ���ý�ɫ���ϵ�skills���еĶ��ʼ���
                float percent = charaComponent.skills[i].CD / charaComponent.skills[i].needCD;
                verbCD[i].fillAmount = percent;
                //ʵʱ����λ��
                skillUIbar[i].transform.position = verbLoadingPoints[i].position;
            }
        }
        if (charaComponent.hp <= 0)
        {
            for(int i = 0; i < skillUIbar.Count; i++)
            {
                Destroy(skillUIbar[i]);
            }
        }
    }
}
