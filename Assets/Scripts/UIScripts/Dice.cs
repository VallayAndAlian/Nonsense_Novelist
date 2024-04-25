using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dice : MonoBehaviour
{
    [Header("���ӵĸ���������ǹ��нű�����屾��")]
    public TextMeshProUGUI textNumber;

    private void Awake()
    {
        textNumber = this.GetComponentInChildren<TextMeshProUGUI>();
        textNumber.text = GameMgr.instance.GetDice().ToString();
    }
    //�ڵ��ʱ��ˢ�����ҳ�档��������Ԥ�����ϣ������ڸ�����Ԥ������

    public void ClickDice()
    {
        if (GameMgr.instance.GetDice() <= 0)
        {
            return;
        }

        GameMgr.instance.DeleteDice(1);
        textNumber.text = GameMgr.instance.GetDice().ToString();
        //ˢ�µ�ǰҳ��
        if (this.transform.parent.TryGetComponent<EventUI>(out var e))
        {
            e.RefreshEvent();
        }
        if (this.transform.parent.TryGetComponent<Setting>(out var a))
        {
            a.RefreshEvent();
        }
        
    }
}
