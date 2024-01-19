using UnityEngine;
using UnityEngine.UI;

///<summary>
///����UI
///</summary>
class SPbar : MonoBehaviour
{
    /// <summary>����Ԥ���� </summary>
    public GameObject SPBarPrefab;
    /// <summary>��ɫͷ��������λ�� </summary>
    private Transform SPbarPoint;
    /// <summary>ʣ��������ֵ </summary>
    private Image SPSlider;
    /// <summary>��ɫλ�� </summary>
    private GameObject UIbar;
    /// <summary>��ȡ�ý�ɫ </summary>
    private AbstractCharacter chara;
    /// <summary>��λ�� </summary>
    private Transform[] barPoint;

    public void Start()
    {
        chara = gameObject.GetComponent<AbstractCharacter>();
        barPoint= gameObject.GetComponentsInChildren<Transform>();
        SPbarPoint = barPoint[2];
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "UIcanvas")
            {
                UIbar = Instantiate(SPBarPrefab, canvas.transform);
                SPSlider = UIbar.transform.GetChild(0).GetComponent<Image>();
            }
        }
    }
    public void FixedUpdate()
    {
        //UpdateSPBar(chara.sp, chara.maxSP);
        if (UIbar != null)
        {
            UIbar.transform.position = SPbarPoint.position;
        }
    }
    /// <summary>
    /// ����������ֵ
    /// </summary>
    /// <param name="currentHP">��ǰ����</param>
    /// <param name="maxHP">������</param>currentHP=��ǰѪ��
    private void UpdateSPBar(float currentSP, float maxSP)
    {
        if (HealthBar.isDead&&UIbar!=null) {
           Destroy(UIbar.gameObject);
        }       
        float sliderPercent = (float)currentSP / maxSP;
        if(SPSlider!=null)SPSlider.fillAmount = sliderPercent;
    }
}
