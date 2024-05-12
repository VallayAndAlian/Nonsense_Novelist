using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [Header("(�ֶ�����)panel1")]
    public Transform panel1;//����
    private string titleName;
    public Text textInput;

    [Header("(�ֶ�����)panel2")]
    public Transform panel2;//�鿴�鱾
    public Text title;
    public GameObject closeBook;
    public GameObject openBook;
    public Text contentL;
    public Text contentR;
    public int indexPage=0;
    private int indexLeft = 0;
    private int indexRight = 0;
    private string[] content;
    private string content_string;
    public int lineWords = 18;
    public int lineCount = 13;
    private Dictionary<int, int> pageContent = new Dictionary<int, int>();

    [Header("(�ֶ�����)panel3")]
    public Transform panel3;//�յ��ż�
    public GameObject letterBig;
    private Animator letetrAnim;
    public GameObject letetrScroe;//�ż��÷�
    public Scrollbar scroll;
    private Vector3 letterScroePos;
    private void Awake()
    {
        //��ʼ������panel1�ر�����
        ChangePanel(1);

        //��ֵ
        letetrAnim = letterBig.GetComponent<Animator>();


        //

        GameMgr.instance.draftUi.AddContent("�����������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                          "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                          "����������˾����Ĺˡ��������ɣ�" +
                      "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
                      "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
                      "�������һ�����е�ʱ����");
        GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
         "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
         "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
         "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
         "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
         "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
         "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
         "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
         "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");
        GameMgr.instance.draftUi.AddContent("�����������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                  "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                  "����������˾����Ĺˡ��������ɣ�" +
              "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
              "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
              "�������һ�����е�ʱ����");
        GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
         "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
         "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
         "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
         "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
         "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
         "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
         "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
         "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");
        GameMgr.instance.draftUi.AddContent("�����������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                  "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                  "����������˾����Ĺˡ��������ɣ�" +
              "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
              "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
              "�������һ�����е�ʱ����");
        GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
         "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
         "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
         "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
         "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
         "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
         "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
         "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
         "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");

        content = GameMgr.instance.draftUi.MergeContent_B();
        content_string = GameMgr.instance.draftUi.MergeContent_A();
        CalculateAllPageIndex();
    }

    /// <summary>
    /// ����panel����_panel=1234
    /// </summary>
    /// <param name="_panel"></param>
    private void ChangePanel(int _panel)
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
      
        switch (_panel)
        {
            case 1:
                {
                    panel1.gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    panel2.gameObject.SetActive(true);
                    //��ǰҳ��Ϊ���ҳ

                    RefreshPanal2();
                }
                break;
            case 3:
                {
                    panel3.gameObject.SetActive(true);
                }
                break;
        
        }
    }
    private void RefreshPanal2()
    {
        //ҳ��=0ʱ����1��2.���ֵ����Ϊ0Ҳ����Ϊ1
        if (indexPage + 1 <= pageContent.Count)   //��-��(1)
        {
            if (!pageContent.ContainsKey(indexPage + 1))
            {
                contentL.text = content_string.Substring(pageContent[indexPage]);
            }
            else
            {
                contentL.text = content_string.Substring(pageContent[indexPage],
                    pageContent[indexPage + 1] - pageContent[indexPage]);
            }
        }
        else
        {
            contentL.text = "";
        }

        if (indexPage +2 <= pageContent.Count) //��-˫(2)
        {
            if (!pageContent.ContainsKey(indexPage + 2))
            {
                contentR.text = content_string.Substring(pageContent[indexPage + 1]);
            }
            else
            {
                contentR.text = content_string.Substring(pageContent[indexPage + 1],
                    pageContent[indexPage + 2] - pageContent[indexPage + 1]);
            }
        }
        else
        {
            contentR.text = "";
        }
    }
    private void CalculateAllPageIndex()
    {

        print(content.Length+ "Length");
        int _INDEX = 0;
        int _wordIndex = 0;
        int o = 0;
        pageContent.Add(0, 0); _INDEX++;
        foreach (var _c in content)
        {
            
            o += Mathf.CeilToInt(_c.Length / (lineWords))+1;
            if (o >= (lineCount))
            {
                int i = o - lineCount;//����������
                int j = ((_c.Length % (lineWords)) == 0 ? lineWords : (_c.Length % (lineWords))) + Mathf.Clamp((i - 1), 0, lineCount + 1) * lineWords;
                int x = _c.Length - j;
                _wordIndex += x;


                pageContent.Add(_INDEX, _wordIndex);

                _INDEX++;
                o = 0;
                o +=i;
                _wordIndex += j; _wordIndex++;
            }
            else
            {
                _wordIndex += _c.Length;
                _wordIndex++;
            }
            
        
        }
        
        return;
    }

    #region �ⲿ����¼�

    public void LastPage()
    {
        if (indexPage <= 0) return;
        indexPage -= 2;
        RefreshPanal2();
    }

    public void NextPage()
    {
        if (indexPage+2 >=pageContent.Count) return;
        indexPage += 2;
        RefreshPanal2();
    }

    public void BackToStudyScene()
    {
        RecordMgr.instance.AddRecord(titleName, GameMgr.instance.draftUi.MergeContent_A(), 2);
    }
    public void ChangeText()
    {
        titleName = textInput.text ;
    }
    public void Change1To2()
    {
        ChangePanel(2);
        P2_ClickOpenBook();
        if((titleName==null)||(titleName.Length<1)) title.text = "δ������Ʒ";
        else title.text = titleName;
    }
    public void P2_ClickCloseBook()
    {
        closeBook.SetActive(false);
        openBook.SetActive(true);
    }
    public void P2_ClickOpenBook()
    {
        closeBook.SetActive(true);
        openBook.SetActive(false);
    }
    public void Change2To3()
    {
        ChangePanel(3);

        letterBig.SetActive(false);
        letetrAnim.enabled = true;
        letetrAnim.SetBool("open", false);
        letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
    }
    public void P3_ClickLetter()
    {
        letterBig.SetActive(true);
        letetrAnim.SetBool("open", true);
    }

    Vector3 vector010 = new Vector3(0, 1, 0);
    public void P3_LetterScroll()
    {
        if (letetrAnim != null && (letetrAnim.enabled))
        {
            letetrAnim.enabled=false; letterScroePos = letetrScroe.GetComponent<RectTransform>().localPosition;
        }
       
        print(" scroll.value" + scroll.value);

        letetrScroe.GetComponent<RectTransform>().localPosition = letterScroePos + vector010 * 1200 * scroll.value;
    }
    //��3�Ķ����е���

    #endregion
}
