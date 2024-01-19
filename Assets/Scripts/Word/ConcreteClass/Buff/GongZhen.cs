using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff������
/// </summary>
public class GongZhen : AbstractBuff
{

    static public string s_description = "����ÿ��4�������񡱣���ά+1��<sprite name=\"hp\">+30";
    static public string s_wordName = "����";
    int count=0;
    /// <summary>�����ɫ����</summary>
    static int num;
    /// <summary>�ϴ������¼</summary>
    private int record;
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        book = BookNameEnum.CrystalEnergy;
        description = "����ÿ��4�������񡱣���ά+1��<sprite name=\"hp\">+30";
   

        //if (this.GetComponents<GongZhen>().Length == 1)
        //{
        //    num++;
        //}
        //GongZhen[] all = CharacterManager.father.GetComponentsInChildren<GongZhen>();
        //foreach (GongZhen gz in all)
        //{
        //    gz.NumChanged();
        //}
        GZCountChange();
    }
    GameObject allCha;
    GongZhen[] allGZ;
    private void GZCountChange()
    {
        //ÿ���������ӡ�ɾ��ʱ�����й���ִ��һ���������+ֵ�仯
        allCha = CharacterManager.instance.gameObject;
        allGZ = allCha.GetComponentsInChildren<GongZhen>();
        num = Mathf.FloorToInt(allGZ.Length / 4);
       // print("���ڳ�����" + allGZ.Length + "������Ҳ����" + num + "��Ч��");
        foreach (var _gz in allGZ)
        {
            _gz.NumChanged(num);
        }
    }
    public void NumChanged(int _num)
    {
        num = _num;
        if (Mathf.Abs(num - record) >= 1)
        {
           
            chara.atk += (num - record);
            chara.def += (num - record);
            chara.psy += (num - record);
            chara.san += (num - record);
            chara.maxHp += (num - record) * 4;
            chara.CreateFloatWord((num-record)*4, FloatWordColor.healMax, false);
            record = num;
        }
       
    }


    private void OnDestroy()
    {
        base.OnDestroy();
        if (GetComponent<AbstractCharacter>() == null) return;
        GZCountChange();
        //chara.atk -= record;
        //chara.def -= record;
        //chara.psy -= record;
        //chara.san -= record;
        chara.maxHp -= 30 * num;
        chara.def -= 1 * num;
        chara.san -= 1 * num;
        chara.psy -= 1 * num;
        chara.atk -= 1 * num;

        //if(this.GetComponents<GongZhen>().Length==1)//��ʾ��ʣ�Լ�
        //{
        //    num--;
        //}
        //GongZhen[] all = CharacterManager.father.GetComponentsInChildren<GongZhen>();
        //foreach (GongZhen gz in all)
        //{
        //    if(gz!=this)
        //    gz.NumChanged();
        //}
    }
}
