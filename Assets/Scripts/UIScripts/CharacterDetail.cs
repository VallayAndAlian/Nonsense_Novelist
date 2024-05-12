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
    public void OpenName(string ac)
    {
        switch(ac)
        {
            case "LinDaiYu":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "林黛玉");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "林黛玉");
                    sprite.sprite = _s2;

                    nameText.text = "林黛玉";
                    roleName.text = "诗人";
                    roleInfo.text = "精神+25%，防御力-20%";

                    atk.text ="3";
                    def.text = "3";
                    psy.text ="100";
                    san.text = "3";
                }
                break;
            case "WangXiFeng":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "王熙凤");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "王熙凤");
                    sprite.sprite = _s2;

                    nameText.text = "王熙凤";
                    roleName.text = "大家长";
                    roleInfo.text = "初始四维+5，每打败一次危机和boss，四维再+5";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "SiYangYuan":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "饲养员");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "饲养员");
                    sprite.sprite = _s2;

                    nameText.text = "饲养员";
                    roleName.text = "饲养员";
                    roleInfo.text = "普通攻击的治疗，附带5s亢奋";

                    atk.text = "0";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "6";
                }
                break;
            case "Anubis":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "阿努比斯");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "阿努比斯");
                    sprite.sprite = _s2;

                    nameText.text = "阿努比斯";
                    roleName.text = "死神";
                    roleInfo.text = "恢复+5%";

                    atk.text = "3";
                    def.text = "3";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "BeiLuoJi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "贝洛姬·姬妮");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "贝洛姬·姬妮");
                    sprite.sprite = _s2;

                    nameText.text = "贝洛姬·姬妮";
                    roleName.text = "蚁后";
                    roleInfo.text = "同时治疗三个友方";

                    atk.text = "0";
                    def.text = "4";
                    psy.text = "3";
                    san.text = "7";
                }
                break;
            case "DiKaDe":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "狄卡德");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "狄卡德");
                    sprite.sprite = _s2;

                    nameText.text = "狄卡德";
                    roleName.text = "银翼杀手";
                    roleInfo.text = "攻击额外造成对方最大生命值3%的伤害";

                    atk.text = "5";
                    def.text = "3";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "LongDuanGongSi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "垄断公司");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "垄断公司");
                    sprite.sprite = _s2;

                    nameText.text = "垄断公司";
                    roleName.text = "税收";
                    roleInfo.text = "普通攻击造成伤害的30%，转化为生命";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "MuNaiYi":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "木乃伊");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "木乃伊");
                    sprite.sprite = _s2;

                    nameText.text = "木乃伊";
                    roleName.text = "再生";
                    roleInfo.text = "每次复活攻击+3防御+6，出场获得“复活”";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "Rat":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "老鼠");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "老鼠");
                    sprite.sprite = _s2;

                    nameText.text = "老鼠";
                    roleName.text = "小偷";
                    roleInfo.text = "攻击可以偷取对方的名词";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;
            case "ShiLian":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "失恋");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "失恋");
                    sprite.sprite = _s2;

                    nameText.text = "失恋";
                    roleName.text = "负面情绪";
                    roleInfo.text = "攻击有几率让对方沮丧";

                    atk.text = "3";
                    def.text = "3";
                    psy.text = "4";
                    san.text = "4";
                }
                break;
            /*case "BenJieShiDui":
                {
                    Sprite _s2 = Resources.Load<Sprite>(spriteAdr + "随从：本杰士堆");
                    if (_s2 == null) _s2 = Resources.Load<Sprite>(spriteAdr + "随从：本杰士堆");
                    sprite.sprite = _s2;

                    nameText.text = "随从：本杰士堆";
                    roleName.text = "再生";
                    roleInfo.text = "每次复活攻击+3防御+6，出场获得“复活”";

                    atk.text = "3";
                    def.text = "5";
                    psy.text = "3";
                    san.text = "3";
                }
                break;*/
        }
    }

    #region 外部点击事件

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
