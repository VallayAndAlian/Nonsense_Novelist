using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// վλ
/// </summary>
public class Situation : MonoBehaviour
{
    /// <summary>����λ���ֶ����룩</summary>
    public float number;//1-10,bossΪ5.5�������������ʱ�õ�0��

    
    /// <summary>����վλ</summary>
    static public Situation[] allSituation;


    public void Awake()
    {
        if (this.number == 4.5f)
            GetComponent<SpriteRenderer>().enabled = false;
        CaculateAllSituation();
    }

    /// <summary>�������</summary>
    static public int Distance(Situation a, Situation b)
    {
        if (a == null|| a.number == 0) 
        {
           return 99900;
        }

      
        if (b== null|| b.number == 0) 
        {
           return 99900;
        }

        return (int)Mathf.Abs(a.number - b.number);
    }

    /// <summary>
    /// ���¼�������վλ
    /// AllCharacter��������������վλ��ÿ��վλ���������ǽ�ɫ
    /// </summary>
    static public Situation[] CaculateAllSituation()
    {
        if (GameObject.Find("AllCharacter") != null)
        {
            
            allSituation = GameObject.Find("AllCharacter").GetComponentsInChildren<Situation>();
           
            return allSituation;
        }
        return null;
    }


   

}
