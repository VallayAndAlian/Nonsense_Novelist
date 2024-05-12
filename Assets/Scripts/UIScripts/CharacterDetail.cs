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
        if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
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
        switch(ac)
        {
            case "LinDaiYu":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "������");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
                    sprite.sprite = _s2;

                    nameText.text = "������";
                    roleName.text = "ʫ��";
                    roleInfo.text = "����+25%��������-20%";

                    atk.text ="3";
                    def.text = "3";
                    psy.text ="100";
                    san.text = "3";
                }
                break;
            case "WangXiFeng":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "������");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "������");
                    sprite.sprite = _s2;

                    nameText.text = "������";
                    roleName.text = "��ҳ�";
                    roleInfo.text = "��ʼ��ά+5��ÿ���һ��Σ����boss����ά��+5";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "SiYangYuan":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "����Ա");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "����Ա");
                    sprite.sprite = _s2;

                    nameText.text = "����Ա";
                    roleName.text = "����Ա";
                    roleInfo.text = "��ͨ���������ƣ�����5s����";

                    atk.text = "0";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "6";
                }
                break;
            case "Anubis":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "��Ŭ��˹");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "��Ŭ��˹");
                    sprite.sprite = _s2;

                    nameText.text = "��Ŭ��˹";
                    roleName.text = "����";
                    roleInfo.text = "�ָ�+5%";

                    atk.text = "3";
                    def.text = "3";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "BeiLuoJi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "���弧������");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "���弧������");
                    sprite.sprite = _s2;

                    nameText.text = "���弧������";
                    roleName.text = "�Ϻ�";
                    roleInfo.text = "ͬʱ���������ѷ�";

                    atk.text = "0";
                    def.text = "4";
                    psy.text = "3";
                    san.text = "7";
                }
                break;
            case "DiKaDe":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "�ҿ���");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "�ҿ���");
                    sprite.sprite = _s2;

                    nameText.text = "�ҿ���";
                    roleName.text = "����ɱ��";
                    roleInfo.text = "����������ɶԷ��������ֵ3%���˺�";

                    atk.text = "5";
                    def.text = "3";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "LongDuanGongSi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "¢�Ϲ�˾");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "¢�Ϲ�˾");
                    sprite.sprite = _s2;

                    nameText.text = "¢�Ϲ�˾";
                    roleName.text = "˰��";
                    roleInfo.text = "��ͨ��������˺���30%��ת��Ϊ����";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "MuNaiYi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "ľ����");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "ľ����");
                    sprite.sprite = _s2;

                    nameText.text = "ľ����";
                    roleName.text = "����";
                    roleInfo.text = "ÿ�θ����+3����+6��������á����";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "Rat":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "����");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "����");
                    sprite.sprite = _s2;

                    nameText.text = "����";
                    roleName.text = "С͵";
                    roleInfo.text = "��������͵ȡ�Է�������";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "ShiLian":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "ʧ��");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "ʧ��");
                    sprite.sprite = _s2;

                    nameText.text = "ʧ��";
                    roleName.text = "��������";
                    roleInfo.text = "�����м����öԷ���ɥ";

                    atk.text = "3";
                    def.text = "3";
                    psy.text = "4";
                    san.text = "4";
                }
                break;
            /*case "BenJieShiDui":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "��ӣ�����ʿ��");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "��ӣ�����ʿ��");
                    sprite.sprite = _s2;

                    nameText.text = "��ӣ�����ʿ��";
                    roleName.text = "����";
                    roleInfo.text = "ÿ�θ����+3����+6��������á����";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;*/
        }
    }

    #region �ⲿ����¼�

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
