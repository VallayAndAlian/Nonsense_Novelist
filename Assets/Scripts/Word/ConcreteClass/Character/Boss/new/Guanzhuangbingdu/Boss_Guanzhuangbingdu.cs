using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// boss����״����
/// </summary>
public class Boss_Guanzhuangbingdu : AbstractCharacter
{
    private bool hasLowThanHalf = false;

    public float HuanbingBuffCount = 0;


    override public void Awake()
    {
        base.Awake();
        characterID = 0;
        wordName = "��״����";
        bookName = BookNameEnum.FluStudy;
        gender = GenderEnum.noGender;
        camp = CampEnum.stranger;

        hp = maxHp = 600;//600
        atk = 20;//20
        def = 30;//30
        psy = 20;//20
        san = 30;//30
        maxSkillsCount = 3;

        trait = gameObject.AddComponent<Sentimental>();
        roleName = "����������";

        attackInterval = 2.2f;
        attackDistance = 200;

        brief = "";
        description = "";


        situation = GameObject.Find("Circle5.5").GetComponentInChildren<Situation>();
        if (situation == null)
            print("situation5.5==null");
    }


    AbstractSkillMode skillMode;
    private void Start()
    {
        skillMode = gameObject.AddComponent<DamageMode>();

     
        //���ӳ�ʼ�ļ���
        this.AddVerb(gameObject.AddComponent<JiXingHuXi>());
    }


  

  
    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "���ѿ�������һ�����ã�ϸ�����ݣ�ֻ������㣬����΢΢���о�ʱ��毻���ˮ���ж������������磬" + otherChara.wordName + "Ц������������ã����������ġ�";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "���Ҿ�֪�������˲���ʣ�µ�Ҳ�����ҡ�������������һ�仨�꣬��" + otherChara.wordName + "��ȥ";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "�������Ů��Ϣ���������ϻ���ա����㽫һ�����ӣ�һ��ʫ��پ��ڻ����С�";
    }
    public override string DieText()
    {
        return "�����񡭱�����á���������û˵��������˫�ۡ�";
    }

}
