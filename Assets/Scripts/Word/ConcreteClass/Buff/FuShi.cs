using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:��ʴ
/// </summary>
public class FuShi: AbstractBuff
{
    static public string s_description = "����1 <sprite name=\"def\">���ɵ���";
    static public string s_wordName = "��ʴ";

    override protected void Awake()
    {
        //����-1
       // ���״̬����Ҫ��ͼ����ʾ�����������صĸ���״̬
        //���Ա������������������״̬�ļ��㣬��Ϊ�����Ե��ӵĲ���̫����
        buffName = "��ʴ";
        description = "����1<sprite name=\"def\">���ɵ���";
        
        book = BookNameEnum.allBooks;

        isAll = true;
        isBad = true;

        base.Awake(); 

        chara.def -= 1;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        chara.def += 1;
    }

}
