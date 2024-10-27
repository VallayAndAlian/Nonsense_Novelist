
using UnityEngine;


/// <summary>
/// combat after start
/// </summary>
public class AfterStart : MonoBehaviour
{
    /// <summary>标识鼠标进入和退出,用于销毁/实例化简要信息面板</summary>
    private bool one;
    private SpriteRenderer sr;

    private Color colorIn = new Color((float)255 / 255, (float)225 / 255, (float)189 / 255, (float)255 / 255);
    private Color colorOut = new Color((float)255 / 255, (float)255 / 255, (float)255 / 255, (float)255 / 255);

    private void Start()
    {
        sr = GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();
    }



    private void OnMouseOver()
    {
        if (CharacterManager.instance.pause) return;
        //颜色变黄
        sr.color = colorIn;
        if (!one)
        {
            one = true;
            //显示角色简要信息(注意第二个参数对于UI)
           
            UIManager.GetInstance().ShowPanel <CharacterShort> ("CharacterShort", E_UI_Layer.Top, (obj) =>
                  {
                      obj.SwitchInfo(GetComponent<AbstractCharacter>());
                  }
            );

        }

    }


    private void OnMouseExit()
    {
        //颜色恢复
        sr.color = colorOut;
        if (one)
        {
            UIManager.GetInstance().HidePanel("CharacterShort");
            one = false;
        }
    }
}
