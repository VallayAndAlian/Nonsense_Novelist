using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// Ů��ѧͽת���ģ����������,ÿ�δ򶷽�������ã�
/// </summary>
class BookNvWuXueTu : AbstractBook
{

    /// <summary>
    /// ���ô˷�������
    /// </summary>
    /// <param name="character">�½���1.2.(0Ϊ��������)</param>
    /// <param name="part">��1.2.Ļ(0Ϊ�½ڱ���)</param>
    /// <param name="phase">1����2ս��3����</param>
    /// <returns></returns>
    public override string GetText(int character,int part,int phase)
    {
         if (character == 0)
                return StartText();
         if (character==1)
            {
                if (part == 0)
                    return "��һ�� ����֮��";
                if(part == 1)
                {
                    switch(phase)
                    {
                        case 1:
                            return First_1_Start();
                        case 2:
                            return First_1_Fight();
                        case 3:
                            return First_1_End();
                    default:
                        return null;
                    }
                }
                if(part == 2)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
                else
                     return null;
            }
        if (character == 2)
        {
            if (part == 0)
                return "�ڶ��� ������";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Second_1_Start();
                    case 2:
                        return Second_1_Fight();
                    case 3:
                        return Second_1_End();
                    default:
                        return null;
                }
            }
            if (part == 2)
            {
                switch (phase)
                {
                    case 1:
                        return Second_2_Start();
                    case 2:
                        return Second_2_Fight();
                    case 3:
                        return Second_2_End();
                    default:
                        return null;
                }
            }
            if (part == 3)
            {
                switch (phase)
                {
                    case 1:
                        return Second_3_Start();
                    case 2:
                        return Second_3_Fight();
                    case 3:
                        return Second_3_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 3)
        {
            if (part == 0)
                return "������ ��Ĭ��ɭ��";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Third_1_Start();
                    case 2:
                        return Third_1_Fight();
                    case 3:
                        return Third_1_End();
                    default:
                        return null;
                }
            }
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return Second_1_Start();
                    case 2:
                        return Second_1_Fight();
                    case 3:
                        return Second_1_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 4)
        {
            if (part == 0)
                return "������ �ξ�̽��";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 5)
        {
            if (part == 0)
                return "������ ���ټ���";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 6)
        {
            if (part == 0)
                return "������ ��������";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 7)
        {
            if (part == 0)
                return "������ ������˹�Ĺ���";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 8)
        {
            if (part == 0)
                return "�ڰ��� ���ֽ��";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        if (character == 9)
        {
            if (part == 0)
                return "�ھ��� ��ڵؽ�";
            if (part == 1)
            {
                switch (phase)
                {
                    case 1:
                        return First_2_Start();
                    case 2:
                        return First_2_Fight();
                    case 3:
                        return First_2_End();
                    default:
                        return null;
                }
            }
            else
                return null;
        }
        else
            return null;
    }

    /// <summary>
    /// ��������
    /// </summary>
    private string StartText()
    {
        return File.ReadAllText("Assets/StreamingAssets/Ů��ѧͽ/0.txt");
    }
    /// <summary>
    /// ��һ�µ�һĻ����
    /// </summary>
    private string First_1_Start()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/Ů��ѧͽ/1_1_1.txt");
        FindLeadingChara();
        string result = a[0] + leadingChara.wordName + a[1];

        if (leadingChara.gender == GenderEnum.girl)
            result += "��";
        else if (leadingChara.gender == GenderEnum.boy)
            result += "��";
        else
            result += "��";

        result += a[2];
        return result;
    }

    /// <summary>
    /// ��һ�µ�һĻս��
    /// </summary>
    private string First_1_Fight()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/Ů��ѧͽ/1_1_2.txt");
        AbstractVerbs[] verbs=fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = a[0];
        foreach(AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText())) 
                continue;
            else
            result+=verb.UseText();
        }
        result += a[1];
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText ="";
        return result;
    }
    /// <summary>
    /// ��һ�µ�һĻ����
    /// </summary>
    private string First_1_End()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/Ů��ѧͽ/1_1_3.txt");
        FindLeadingChara();
        string result = a[0] + leadingChara.wordName + a[1];

        if (leadingChara.gender == GenderEnum.girl)
            result += "����һЦ";
        else
            result += "������Ц";
        result += a[2];
        result += leadingChara.wordName;
        result += a[3];
        result += leadingChara.wordName;
        result += a[4];
        return result;
    }

    /// <summary>
    /// ��һ�µڶ�Ļ����
    /// </summary>
    private string First_2_Start()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/Ů��ѧͽ/1_2_1.txt");
        FindLeadingChara();
        //�����ѷ�
        AbstractCharacter[] z = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara = null;
        if (a.Length >= 2) secondChara = z[1];

        if (secondChara == null) leadingChara = secondChara;//��ֹ��
        string result = "ͬ" +secondChara.wordName + a[0]+leadingChara.wordName+a[1]+leadingChara.name+a[2];
        if (secondChara.trait.traitName != "����")
        {
            result += "����";
            result += a[3];
            result+=secondChara.wordName;
            result += "�����ǳ�";
            result += a[4];
        }
        else
        {
            result += "��Ц";
            result += a[3];
            result+=secondChara.wordName;
            result += "С������";
            result += a[4];
        }

        return result;
    }
    /// <summary>
    /// ��һ�µڶ�Ļս��
    /// </summary>
    private string First_2_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result="";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// ��һ�µڶ�Ļ����
    /// </summary>
    private string First_2_End()
    {
        string[] a = File.ReadAllLines("Assets/StreamingAssets/Ů��ѧͽ/1_2_3.txt");
        FindLeadingChara();
        string result = a[0]+"\n"+a[1]+leadingChara.wordName+a[2]+leadingChara.wordName+a[3]+leadingChara.wordName+a[4];;
        return result;
    }

    /// <summary>
    /// �ڶ��µ�һĻ����
    /// </summary>
    private string Second_1_Start()
    {
        FindLeadingChara();
        //�����ѷ�
        AbstractCharacter[] a = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara=null;
        if(a.Length >=2)secondChara = a[1];

        string result;
        if (secondChara == null || secondChara == leadingChara) //��ֹ��
            result = leadingChara.wordName;
        else
            result = leadingChara.wordName + "��" + secondChara.wordName;

        result += "վ�ڴ������ǰһһɨ�ӡ����������ǵ䡷����Ƣ���ǲ����е�̫���ˣ������Ȳ��������˰ɡ����������ǵ䡷б�������һ���ʱ��ʱ������������\n";
        result += "���š��������ŵ���Կ�ס������Ҳ�Ǽ�¼�Ź��ڵ��������֪ʶ���ҿɲ���ȥ�ٻ���Щ��ͷ�Ļ�������������ǧ�������Ӹ���ȼ�ˡ���" + leadingChara.wordName + "�����Ȿ��Ϊ���ص��飬ÿ��ɨ�ҵ������ﶼ���鷳�ˡ�\n";
        result += "�����Ρ���ռ����ħҩ����Щ��Ҳû��������ѵ���ǡ����������ɨ���ţ�Ȼ������������һ���н���װ֡�ľ����鼮����ƽʱ������˿ϲ���á�������ħ�䡷����Ӽ�������Ϸˣ�����һ���ϲ����Щ���ںÿ���С���ģ�Ҫ����ѧһѧ�ͺ��ˡ���" + leadingChara.wordName + "����ˡ�������ħ�䡷�������ĸ���ȼ����������Χ���ḧ�鼹����������ƽ�����鱾���棬���ǻ���ħ����ļ�����ʽ��\n";
        result += "����" + leadingChara.wordName + "������ô�������㻽�ѵ��ң�����������������Щ����������˿�أ����������������𾴵������������������ԭ�¡�" + leadingChara.name + "ǫ����˵�����صĵ�����˿����˵ȥɽ���ﴦ��ˮ��Ģ���ˣ�����֪��ʲôʱ����ܹ�������\n";
        result += "���£��ǵ��ǹ��Ƶ�æһ���ˡ�������˵������" + leadingChara.wordName + " ����������ʲô�أ�͵͵��ħ�����Ҫ���ϳ͵�Ŷ������������Ц�ÿ������֡�\n";
        result += "���𾴵������������������е���Ҫ�������ǵ��鷳����������Ҫ��ƽ���ǵķ����ء�" + leadingChara.wordName + "���ߵ�˵����\n";
        result += "�������������л�û���հ�������������ʮ��ɨ�ˡ���ɱ�����ң���Ҫѧ�ҵ�ħ�����Ը����ǡ���������������Ц������" + leadingChara.wordName + "������üͷ���򱻿����˶�����Щ���������𾴵��������������ʳ�������Щ���µ�ħ�����⣬�ܷ������һЩ�򵥵�ħ���أ���\n";
        result += "���š��㻹��˵������������Ť��Ť�����鱾�зɳ�����һ��Сֽ�����ɶ�ĵ�����˿��Ȼ��һ����Ϊ���۵ļ򵥷���������ǩ���������������ôŪ��Ū�������������������ˣ���\n";

        result += "#";

        result += "��Ҫ��������Ҵ���ӵ���������Ū�������һ������ܺþá��������������������ˡ��������С�Ӽ�Ӧ�ù������ˣ��Ͻ��õ�����ԽԶԽ�ã���" + leadingChara.wordName + "�ӵ��ϼ�������СֽƬ���ǵ�����˿�ӱʼǱ���˺������һ�����õ�С�������������޾Ϳ���ѧ�ᡣ����л�𾴵������������������ʩ�񣬲�׼������ȥ���������ҽ����Ż���ܣ���\n";
        result += "��������������ô���ܣ��������������˵����������ҳ�ȥ��һ�棬�����ڲ�Ҫ��ȥ����������������Ȼ���鷳������" + leadingChara.wordName + "���յ����š��Ǿ��ȷ������ɣ�������һ�����С�������⻹���ҵ�һ��ѧ��������أ����뵽����" + leadingChara.wordName + "�ֿ�ʼ����������\n";
        result += "����ٯ������������ŵ����������" + leadingChara.wordName + "����ֽƬ���ţ��������Ž�ħ�����������������塰ŵ��������������������������˵������оͱĳ�������С���򣬻��������š���������һ�ӣ������ɳ���һ���ҩ�޴����ˡ�\n";
        result += "��Ӵ����ѧϰħ����������������츳�������������������������������Ȥ��˵������Ŭ����ʮ�꣬Ҳ��ѧ��һ�����ҵ�ħ���ء���\n";
        result += "�����ҵ㶫��ըһ�£�����Ⱥ����ɡ����������ĵ�˵��������ը�飡��\n";

        return result;
    }

    /// <summary>
    /// �ڶ��µ�һĻս��
    /// </summary>
    private string Second_1_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// �ڶ��µ�һĻ���
    /// </summary>
    private string Second_1_End()
    {
        string result = "�Ӽ������ڽӴ��������һ˲�䣬��ֻ��������������ڲ�����֬�������������ͣ��Ӷ�������ͣ�������Ƥ�ұ�˺�����˼��޶�ը�ѿ�����\n";
        result += "����������Щ���������๼���������˷ܵ�˵�������ҳ�ȥ���߰ɣ�ȥɭ�����档��\n";

        return result;
    }

    /// <summary>
    /// �ڶ��µڶ�Ļ��ʼ
    /// </summary>
    private string Second_2_Start()
    {
        string result = "���š���Ȼ����Щ����Ҳ��������𣿡����������ּ�������ְԱ�������������С���"+leadingChara.wordName+ "�����øղŵķ����������ǣ������ɶ񣬱������ˡ�������ְԱ��׼������ȥ��ˮ����"+leadingChara.wordName+ "���Ǿͱ�����Ƕ����ˣ�������ְԱ��Χ��������\n";
        return result;
    }
    /// <summary>
    /// �ڶ��µڶ�Ļս��
    /// </summary>
    private string Second_2_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// �ڶ��µڶ�Ļ���
    /// </summary>
    private string Second_2_End()
    {
        
        string result="";

        return result;
    }
    /// <summary>
    /// �ڶ��µ���Ļ��ʼ
    /// </summary>
    private string Second_3_Start()
    {
        return "���ɶ񣬶������ϣ�����������д�������������ӿ������Ǯ����׼�������ö����𣡡�";
    }

    /// <summary>
    /// �ڶ��µ���Ļս��
    /// </summary>
    private string Second_3_Fight()
    {
        AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
        string result = "";
        foreach (AbstractVerbs verb in verbs)
        {
            if (string.IsNullOrEmpty(verb.UseText()))
                continue;
            else
                result += verb.UseText();
        }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }
    /// <summary>
    /// �ڶ��µ���Ļ���
    /// </summary>
    private string Second_3_End()
    {
        string result = "һȺ��װ���ĵ�����ְԱ�Ǽ���ͷ��С�������ꡰ���ܣ���СѧͽҲ��ħ������\n";
        result += "��������������Щ���еľ�ֻ���⸱�����𣿡������������˼����Ц��������׷��ȥ�����Ǹɵ��ɡ������⡭�����û��Ҫ�˰ɡ����������Ц�ţ���û��Ҫ�������ִ�ɣ���ѵһ�����Ǿͺ��˰ɡ������Ǿ���׷��ȥ������ƨ��������һ�£��죡����������Ц�ţ�����Ҫ��������Щ������������ƨ���ܣ��Ҿ�ָ�������С�����������Ү�������������������۷Ź⣬�㱧�š�������ħ�䡷�����С�ݡ����ܣ���\n";

        return result;
    }
    /// <summary>
    /// �����µ�һĻ����
    /// </summary>
    private string Third_1_Start()
    {
        FindLeadingChara();
        //�����ѷ�
        AbstractCharacter[] a = fatherObject.transform.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        AbstractCharacter secondChara = null;
        if (a.Length >= 2) secondChara = a[1];

        string result = "�ڲ��̵��ּ�С���"+leadingChara.wordName+ "׷�����Ӽ����������Ǳ����ߵ�����ְԱ�ǡ������������㿴��ǰ���Ǹ��һ���������ƨ�ɶ��Ż��ˣ������������ĵ�˵����ιιι�����ƨ�ɶ��ս�����������������Ǹ��һ����һ�£���"+leadingChara.wordName+ "����׼��һ�����⻬�����Ƶ�����ְԱ��ȥ��һ��С����򣬽�������ˣ�����Ĩ���͹��ͷ��ը����Ģ��״�����𾴵������������������𣿡��������ʵ������ҿɲ����������ħ�������ӵ����������������������аɡ��������Ӱɣ����ǻ�ȥ����������ͻȻ��ɫ���أ�����˭����";
        if (secondChara != null && secondChara == leadingChara)
            result += leadingChara.wordName + "��" + secondChara.wordName;
        else
            result+=leadingChara.wordName;

        result += "һ���ɻ�ؿ������ܣ����⸽�����в��ŵļһ��𣿡���ѧϰ����֮���ļһ���ٸ���������������ħ��ͻȻ��" + leadingChara.wordName + "���з���������������ҳ���ٵط����ţ�����ð�ŵ�������ɫ�Ĺ�â��ǿ����������ּ������Ҷɨ�����׷���ײ��������֦������ϸ�ܵ�����������ǿ���������ڿ���һ�ô�׳������Զ�����ƺ���ʧֹͣ�ˡ��ߡ����š�����ô�׳�����ɺ��߳���һ����̬���Σ�������������Ĺ��ˣ�����ɢ���Ź��ܵ���Ϣ��������ĳ�Ĭ�ߣ������ӵؽ���������һֻ�����ª����ı������ӡ�����������������ؿ�����������Ĭ�ߣ���"
            + leadingChara.wordName + "��Ȼ���⣬Ҳ������˶Է��������ࣺ�����������������鷳�Ĳ�ֹ���С���\n"
            + leadingChara.wordName + "˵�����ͼ��ȫ���ͷų�һ�Ż��򣬵������Լ��������䷭ӿ��������ȴһ�ݳ�ָ��㻯Ϊ���С���"
            + leadingChara.wordName + "�������ҿ��ܡ��������������˵�������ڵ����޷�������������㹻ǿ���ħ��������ǰ�ǲ�����Ч�ġ������á��������񱧽��ˡ�������ħ�䡷׼��ת���ܻ�С�ݣ�ȴͻȻ�������Ѿ����˻ص�С�ݵ�·�ϡ��ߺߣ�����Ĭ�ߵ��������˳��������������������û����·�ˣ�ֻ�ܺ���ƴ�ˣ���";
        if (secondChara != null && secondChara == leadingChara)
            result += leadingChara.wordName + "��" + secondChara.wordName;
        else
            result += leadingChara.wordName;

        result += "�мܺ�׼��ӭ�С���" + leadingChara.wordName + "��ҪС�ģ���û�������ṩ��������������˵����\n";

        return result;
    }
    /// <summary>
    /// �����µ�һĻս��
    /// </summary>
    private string Third_1_Fight()
    {
            AbstractVerbs[] verbs = fatherObject.GetComponentsInChildren<AbstractVerbs>();
            string result = "";
            foreach (AbstractVerbs verb in verbs)
            {
                if (string.IsNullOrEmpty(verb.UseText()))
                    continue;
                else
                    result += verb.UseText();
            }
        result = beforeFightText + result + afterFightText;
        beforeFightText = afterFightText = "";
        return result;
    }

    /// <summary>
    /// �����µ�һĻ���
    /// </summary>
    private string Third_1_End()
    {
        FindLeadingChara();
        //�����ѷ�
        AbstractCharacter secondChara = fatherObject.transform.Find("SelfCharacter").GetComponentInChildren<AbstractCharacter>();
        if (secondChara == null) secondChara = leadingChara;
        string result = "����ɢ���ڵ��ϵı�����֧������ĳ�Ĭ�ߣ�" + leadingChara.wordName + "ƣ�����п���һ���������������ڿ��еı��������������������𣿡�"
            + secondChara.wordName + "˵�����Ҿ��ÿ��²�����ô�򵥣����ǳ����ڿ��ߡ������á���"
            + leadingChara.wordName + "����������ħ�䡷����Ů��С���ߡ���������ı��������أ����ϵı���������Ʈ�������γ���һ����ɢ�����Σ�����������Ϣ�ػ�����������������δ��Զ�����ܵ������������Σ���Ҷ��֦Ҳ���궯����������������û��������������˵���������ߣ�����������ǿ�󣡡��������ͷ��������Ĭ��֧��������������ۼ�����������Χ�Ŀ����ƺ�Ҫ������ק��ȥ�����Ǿ�����ʼȫ��������ͻȻһ�󰲾�����Χ�����궯����Ƭ����ı���������ĺ����㣬�����ӱ��������ȫ�����ܵ�"
            + leadingChara.wordName + "�����ߡ�" + leadingChara.wordName + "֪����Ĭ�����ˣ��ܵĸ����ˣ���������ƮƮ��ȴ�޷�˦����������Խ��Խ��شӱ���׷�ϡ�Խ��Խ�࣬Խ��Խ��ı������Ѿ��в����Ѿ���Խ�����ǡ�ͻȻһ�£���ͬһֻ���εĴ���߬סȭͷ�������������ؼ�ѹס����������壬����߬���޷���������"
            + leadingChara.wordName + "����ͻȻ���������صط�����У�ȴû�����¡�����ץס�ˣ�����������æ��˵������"
            +leadingChara.wordName + "��Ȼ��ش�ȴֻ�ܸо��Լ�����Ŀ��������ݳ��������γ�һ�������Ļ�����������Խ��Խǿ��"
            + leadingChara.wordName + "�о����Լ����߹ǿ�Ҫ���ѣ����ϵĹؽڸ�֨���죬��Ҫ����ɾ޴��ѹ��ѹ���ˡ����������Ұ��������ڰ��������ʧȥ����ʶ��\n";

        return result;
    }
}
