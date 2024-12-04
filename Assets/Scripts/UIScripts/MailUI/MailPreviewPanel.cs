using LitJson;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MailTable;

public class MailPreviewPanel : BasePanel<MailPreviewPanel>
{
    //�ż������ڳ����ĸ��ڵ�λ��
    public Transform MailPosFather;
    /// <summary>
    /// �����ǩ����
    /// ����༭ | ����³ҽ�� | ��˿�˵� | ����ľ�༭ 
    /// </summary>
    public Toggle[] AuthorToggles;

    //�����ϵ��ż�����
    public PreMailObj[] mailObjs;
    //��ҳ��ť��һҳ��һҳ
    public TextMeshProUGUI pageNumText;
    public Button AddPageBtn;
    public Button SubPageBtn;
    //���ʼ�����
    public TextMeshProUGUI noneMailText;

    //�洢����Mail
    private List<MailInfo> mailDataList = new List<MailInfo>();
    //����ʾ��MainData
    private List<MailInfo> prepMailData = new List<MailInfo>();
    //ҳ��
    private int pageNum;
    //ÿһҳ�����ɵ�����ż�����
    private int maxMailCount;
    //��ǰӦ����ʾ��������prepMailData�Ŀ�ʼ�ͽ�������
    private int startIndex ,lastIndex;
    
    //��ʼ������ֶ�
    protected override void Init()
    {
        /* ��ʼ���ֶ� */
        //��ʾҳ��
        pageNum = 1;
        //����������ż�����
        maxMailCount = mailObjs.Length;
        //�����ż�����
        ReadMailsData();
        //����ʾ����
        RefreshPrepMail();
        //��ʾ����
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //��ʾ�ż�
        ShowMail();
        //���·�ҳ��ť��ʾ
        BtnApprUpdate();
        //��ɸѡ�¼�:����ɸѡ����ʾ����,��ʾ�ż�
        for (int i = 0; i < AuthorToggles.Length; i++)
        {
            AuthorToggles[i].onValueChanged.AddListener((isOn) => {
                RefreshPrepMail();
                ShowMail();
            });
        }
        //���·�ҳ�¼�
        AddPageBtn.onClick.AddListener(() => {
            TurnPage(true);
        });
        SubPageBtn.onClick.AddListener(() => {
            TurnPage(false);
        });
    }

    /// <summary>
    /// ��ȡ�ż����ݲ��洢(����)
    /// </summary>
    private void ReadMailsData()
    {
        //������
        //TableManager.ReadTables();
        //��ȡ�ż�����
        Dictionary<int, Data> dataList = MailTable.DataList;
    }

    /// <summary>
    /// ����ɸѡ��ǩ�仯ˢ�´���ʾ�����б�
    /// </summary>
    private void RefreshPrepMail()
    {
        //�����������
        prepMailData.Clear();
        //Ȼ����ݱ�ǩ�Ƿ�����ÿ�����ݼ������ʾ�б�
        for (int i = 0; i < mailDataList.Count; i++) 
        {
            //������ô��ż���ǰ����ʾ���������ż�
            //if (mailDataList[i].isDisplay == false)
            //    continue;

            if (mailDataList[i].autherType == E_MailAutherType.BaoShe && AuthorToggles[0].isOn) 
                prepMailData.Add(mailDataList[i]);
            
            if (mailDataList[i].autherType == E_MailAutherType.KeLao && AuthorToggles[1].isOn)
                prepMailData.Add(mailDataList[i]);

            if (mailDataList[i].autherType == E_MailAutherType.BiDe && AuthorToggles[2].isOn)
                prepMailData.Add(mailDataList[i]);
        
            if (mailDataList[i].autherType == E_MailAutherType.WenTeCen && AuthorToggles[3].isOn)
                prepMailData.Add(mailDataList[i]);
        }

        //ÿ��ɸѡ��ǩ�仯,ҳ������
        pageNum = 1;
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //����UIҳ��ΪpageNum
        pageNumText.text = pageNum.ToString();
        //���·�ҳ��ť
        BtnApprUpdate();
    }

    /// <summary>
    /// ��ҳ:���·�ҳ�����ݺ�UIҳ��
    /// </summary>
    /// <param name="isAdd">ҳ���Ƿ�����,false��ʾҳ�����</param>
    private void TurnPage(bool isAdd)
    {
        /*
         ÿҳ��ʾlen������
         ҳ��p  ��ʾ��Χ
         1      0 ~ 2
         2      3 ~ 3 + (3-1) = 5 
         3      6 ~ 8
         ��pҳ�����ݸ���Ϊ:len*(p-1) ~ ��ʼ����start + (len-1)
        */
        //�жϷ�ҳ,����ҳ��
        if (CanTrunPage(isAdd))
        {
            //����UIҳ��Ϊ pageNum
            pageNumText.text = pageNum.ToString();
            //��ʾ����
            ShowMail();
            //���·�ҳ��ť����ʾ
            BtnApprUpdate();
        }
    }

    /// <summary>
    /// ���·�ҳ��ť����ʾ���:����startIndex��lastIndex��ʾ��������/�ҷ�ҳ��ť
    /// </summary>
    private void BtnApprUpdate()
    {
        //���ż���ʾ
        if(prepMailData.Count == 0)
        {
            //���ذ�ť
            SubPageBtn.gameObject.SetActive(false);
            AddPageBtn.gameObject.SetActive(false);
            //��ʾ���ż���ť
            noneMailText.gameObject.SetActive(true);
            return;
        }

        noneMailText.gameObject.SetActive(false);
        //��߰�ť
        if (startIndex <= 1)
            SubPageBtn.gameObject.SetActive(false);
        else
            SubPageBtn.gameObject.SetActive(true);

        //�ұ߰�ť
        if (lastIndex >= prepMailData.Count - 1)
            AddPageBtn.gameObject.SetActive(false);
        else
            AddPageBtn.gameObject.SetActive(true);

    }

    /// <summary>
    /// �ж��Ƿ�ɷ�ҳ,������ʾ��������[startIndex,lastIndex]
    /// </summary>
    /// <returns>�Ƿ���Է�ҳ</returns>
    private bool CanTrunPage(bool isAdd)
    {
        //���跭ҳ,��ҳ��ʼ�ͽ�������
        int nextStart = 0, nextLast = 0 ; 
        if (isAdd)
        {
            //��һҳҳ��ΪpageNum + 1
            //��ҳ��ʼ���� = ÿҳ��ʾ������len * ( page - 1)
            nextStart = maxMailCount * (pageNum + 1 - 1);
            //���ݷ�ҳ��Ŀ�ʼ�����ж��Ƿ���Է�ҳ
            if (nextStart > prepMailData.Count - 1)
                //�����Է�ҳ,�������κζ���
                return false;

            //���Է�ҳ:����ҳ�����ʾ����
            pageNum++;
            //��ʾ��ʼ����
            startIndex = nextStart;
            //��ʾ��������:��ʼ����start + (len-1)
            lastIndex = startIndex + maxMailCount - 1;
            //ʣ�����ݲ�����ֻ��ʾʣ������
            lastIndex = lastIndex > prepMailData.Count - 1 ? prepMailData.Count - 1 : lastIndex;
        }
        else
        {
            //��һҳ��:pageNum - 1
            //��һҳ��ʼ�����ͽ�������
            nextStart = maxMailCount * (pageNum - 1 - 1);
            nextLast = nextStart + maxMailCount - 1;
            //����һҳ��������С��0,��ʾ�������Ϸ�ҳ
            if (nextLast < 0) 
                return false;

            //����ҳ�����ʾ����
            pageNum--;
            startIndex = nextStart;
            lastIndex = nextLast;
            //��һҳ��ҳӦ�ò������Խ��
        }
        return true;
    }

    /// <summary>
    /// ������ʾ������Χ��ʾԤ���ż�
    /// </summary>
    public void ShowMail()
    {
        //�������ݼ����Ӧ�ż�
        //����ҳ��������֮ǰ����
        for (int i = 0; i < mailObjs.Length; i++) 
            mailObjs[i].Hide();

        //����������ʾԤ���ż�
        for (int i = startIndex, j = 0; i <= lastIndex; i++, j++)  
            mailObjs[j].Show(prepMailData[i]);
    }

    /// <summary>
    /// ����id��������ʾ�ż�
    /// </summary>
    /// <param name="id">�ż���id</param>
    public void displayMailById(int id)
    {

    }

    /// <summary>
    /// �����ż��ĸ�������[��δʵ��,����API��ʽ]
    /// </summary>
    /// <param name="id">�ż���id</param>
    /// <param name="attachId">�����ĵ���id</param>
    /// <param name="attachNum">����������</param>
    public void setAttachById(int id,int attachId,int attachNum)
    {
        
    }

    /// <summary>
    /// ����ID���ö�������
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public void setScoreById(int id, int score)
    {
        //ֻ�б���༭���Ͳ��ж�������
    
    }

}

