using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// �����鱾�����ƶ�
/// ����ClickBookPanel��
/// </summary>
public class BookMove : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform panelTF;

    public float speed = 1.5f;
    public void RightMove()
    {
        if (panel.anchoredPosition.x < 250)
        {
            panel.Translate(Vector3.right * speed);
        }
    }
    public void LeftMove()
    {
        if(panel.anchoredPosition.x >-250)
            panel.Translate(Vector3.left * speed);
    }
}
