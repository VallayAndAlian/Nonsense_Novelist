using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterDetail : MonoBehaviour
{
    [Header("����������ֶ���")]
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

    void InitDetail()
    {
        Sprite _s2 = Resources.Load<Sprite>(spriteAdr + nowCharacter.wordName.ToString());
        if (_s2 == null) 
            _s2 = Resources.Load<Sprite>(spriteAdr + "������");
        sprite.sprite = _s2;

        nameText.text = nowCharacter.wordName.ToString();
        roleName.text = nowCharacter.roleName;
        roleInfo.text = nowCharacter.roleInfo;

        atk.text = nowCharacter.atk.ToString(); 
        def.text = nowCharacter.def.ToString();
        psy.text = nowCharacter.psy.ToString();
        san.text = nowCharacter.san.ToString();

    }
    public void OpenName(string ac)
    {
        CharaInfoExcelItem data = null;
        for (int i = 0; (i < AllData.instance.charaInfo.items.Length) && (data == null); i++)
        {
            var _data = AllData.instance.charaInfo.items[i];
            if (_data.typeName == ac)
            {
                data = _data;
            }
        }
        if (data == null)
            return;

        Sprite _s2 = Resources.Load<Sprite>(spriteAdr + data.name);
        if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
        sprite.sprite = _s2;

        nameText.text = data.name;
        roleName.text = data.roleName;
        roleInfo.text = data.roleInfo;

        atk.text = data.atk.ToString();
        def.text = data.def.ToString();
        psy.text = data.psy.ToString();
        san.text = data.san.ToString();
    }

    #region �ⲿ����¼�

    public void ClickClose()
    {
        Destroy(this.gameObject);
        BookShelfInf.isfirst = true;
    }
    #endregion
}
