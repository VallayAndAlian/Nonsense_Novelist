using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��������ʾ���ؽ��ȣ��һ������ϣ�
/// </summary>
 [HideInInspector]
public class LoadingSlider : MonoBehaviour
{
    private Slider loadingSlider;
    public float realValue;
    public float showValue;
    void Start()
    {
        loadingSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showValue < 0.9f)
        {
            if(showValue < realValue )//�������������ټ���(�㱻ƭ����
            {
                showValue += 0.0065f;
                loadingSlider.value  = showValue;
            }
            
        }
    }
}
