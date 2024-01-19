using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 伤害治疗飘字
/// </summary>
public class FloatWord : MonoBehaviour
{
    /// <summary>(手动）</summary>
    public TextMeshProUGUI[] normalTexts;
    /// <summary>(手动）</summary>
    public TextMeshProUGUI[] bossTexts;

    private Color purple = new Color(153 / 255, 0, 255 / 255, 1);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="boss">是否是boss</param>
    /// <param name="damage">是否是伤害</param>
    /// <param name="direct">是否是直接的</param>
    internal void InitPopup(float value, bool boss, FloatWordColor color, bool direct)
    {
        string str = ((int)value).ToString();
        TextMeshProUGUI text = null;
        if (!boss)
        {
            if (value >= 20)
            {
                text = normalTexts[2];
            }
            else if (value >= 10)
            {
                text = normalTexts[1];
            }
            else if (direct || (!direct && value >= 0))
            {
                text = normalTexts[0];
            }
        }
        else//boss
        {
            if (value >= 20)
            {
                text = bossTexts[2];
            }
            else if (value >= 10)
            {
                text = bossTexts[1];
            }
            else if (direct || (!direct && value >= 0))
            {
                text = bossTexts[0];
            }
        }
        if (text == null)
        {

            Destroy(this.gameObject);//不漂字
            return;
        }
        text.text = str;
        text.outlineColor = SetColor(color);

        text.enabled = true;

        float time = 0.8f;
        float height = 1f;//弹射高度
        //(什么移动，终点在哪，花多长时间)
        //.setEaseOutBack()设置曲线（），使其往返.setDestroyOnComplete(true)设置完成后销毁
        LeanTween.moveY(this.gameObject, this.transform.position.y + height, time).setDestroyOnComplete(true);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="boss">是否是boss</param>
    /// <param name="damage">是否是伤害</param>
    /// <param name="direct">是否是直接的</param>
    internal void InitPopup(string textInfo, bool boss, FloatWordColor color, bool direct)
    {

        TextMeshProUGUI text = null;
        if (!boss)
        {

            text = normalTexts[0];

        }
        else//boss
        {


            text = bossTexts[0];

        }
        if (text == null)
        {

            Destroy(this.gameObject);//不漂字
            return;
        }
        text.text = textInfo;
        text.outlineColor = SetColor(color);
        text.enabled = true;

        float time = 1;
        float height = 1f;//弹射高度
        //(什么移动，终点在哪，花多长时间)
        //.setEaseOutBack()设置曲线（），使其往返.setDestroyOnComplete(true)设置完成后销毁
        LeanTween.moveY(this.gameObject, this.transform.position.y + height, time).setDestroyOnComplete(true);
    }


    private Color SetColor(FloatWordColor _style)

    {
        switch (_style)
        {
            case FloatWordColor.physics:
                {
                    return Color.white;

                }
            case FloatWordColor.psychic:
                {
                    return purple;

                }
            case FloatWordColor.heal:
                {
                    return Color.green;

                }
            case FloatWordColor.healMax:
                {
                    return (Color.green + Color.white * 0.4f);

                }
            case FloatWordColor.removeWord:
                {
                    return Color.grey;

                }
            case FloatWordColor.getWord:
                {
                    return Color.yellow;

                }
        }
        return Color.white;
    }
}
    public enum FloatWordColor
    {
        /// <summary>物理+使用技能</summary>
        physics = 0,
        /// <summary>精神</summary>
        psychic = 1,
        /// <summary>治疗</summary>
        heal = 2,
        /// <summary>最大值</summary>
        healMax = 3,
        /// <summary>失去技能</summary>
        removeWord = 4,
        /// <summary>获得技能</summary>
        getWord = 5,

    }

