using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardTagDetail
{
    [Tooltip("TagKind")] 
    public int kind;
    
    [Tooltip("名称")]
    public string tagName;
    
    [Tooltip("描述")]
    public string tagDesc;
}

[CreateAssetMenu(fileName = "NewCommonSO", menuName = "BattleSO")]
public class CommonSO : MonoBehaviour
{
    public List<CardTagDetail> tagDetails;

    
    
    public CardTagDetail GetTagDetail(int kind)
    {
        foreach (var it in tagDetails)
        {
            if (it.kind == kind)
            {
                return it;
            }
        }

        return null;
    }
}