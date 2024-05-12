using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterDetail : MonoBehaviour
{
    [Header("设置组件（手动）")]
    public Image sprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI roleName;
    public TextMeshProUGUI roleInfo;

    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI psy;
    public TextMeshProUGUI san;


    private AbstractCharacter nowCharacter;
    private string spriteAdr = "WordImage/Character/";

    public void Open(AbstractCharacter _ac)
    {
        nowCharacter = _ac;
        InitDetail();
    }


    //public void Open(string _ac)
    //{
    //    AbstractCharacter _tempac=new AbstractCharacter();
    //    switch (_ac)
    //    {
    //        case "LinDaiYu":
    //        {
    //            _tempac.wordName = "林黛玉";
    //            _tempac.roleName = "^";
    //            //……设置一系列参数
    //        }break;
    //    }
        
    //    nowCharacter = _tempac;
    //    InitDetail();
    //}


    void InitDetail()
    {
        Sprite _s2 = Resources.Load<Sprite>(spriteAdr + nowCharacter.wordName.ToString());
        if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "林黛玉");
        sprite.sprite = _s2;

        nameText.text = nowCharacter.wordName.ToString();
        roleName.text = nowCharacter.roleName;
        roleInfo.text = nowCharacter.roleInfo;

        atk.text = nowCharacter.atk.ToString(); 
        def.text = nowCharacter.def.ToString();
        psy.text = nowCharacter.psy.ToString();
        san.text = nowCharacter.san.ToString();

    }


    #region 外部点击事件

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
