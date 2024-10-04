using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StagesData", menuName = "TT/NewStageData")]
public class StagesData : ScriptableObject
{
    [Header("��ע")]

    public string info;

    [Header("����")]

    [Tooltip("�����ʼѪ��")] public float groupMaxHp=100;
    [Tooltip("�����ָ��ٶ�")] public float energyRecover = 0.2f;
    [Tooltip("��������ʱ��")] public float oneCardLoad=3;
    [Tooltip("�ƿ���װ��ʱ��")] public float cardListLoad=5;


    [Header("�¼�����(�ܺ�100)")]

    [Tooltip("ϣ��")] public int xiWang = 10;
    [Tooltip("�ÿ�")] public int fangKe = 25;
    [Tooltip("����")] public int yiWai = 10;
    [Tooltip("Σ��")] public int weiJi = 30;
    [Tooltip("����")] public int jiaoYi = 25;
    [Tooltip("����")] public int changJing = 10;	


    [Header("��Ϸ�׶�")]

    public List<OneStageData> stagesData = new List<OneStageData>();
}


