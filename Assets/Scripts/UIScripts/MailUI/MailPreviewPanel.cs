using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailPreviewPanel : MonoBehaviour
{
    //�ż������ڳ����ĸ��ڵ�λ��
    public Transform MailPosFather;
    //�����ǩ
    public Toggle Author1Toggle;
    public Toggle Author2Toggle;
    public Toggle Author3Toggle;
    //�����ϵ��ż�����
    public MailObj[] mailObjs;
    //��ҳ��ť��һҳ��һҳ
    public TextMeshProUGUI pageNumText;
    public Button AddPageBtn;
    public Button SubPageBtn;

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

    private void Start()
    {
        /* ��ʼ���ֶ� */
        //ҳ���ʼΪ1
        pageNum = 1;
        //����������ɵ��ż�����
        maxMailCount = mailObjs.Length;
        //��ʼ�������ż�����
        ReadMailsData();
        //��ʼ������ʾ����
        RefreshPrepMail();
        //��ʼ����ʾ����
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        print("��ʼ��ʾ��Χ: " + startIndex + " " + lastIndex) ;
        //��ʾ�ż�
        ShowMail();
        //���·�ҳ��ť��ʾ
        butnApprUpdate();
        //��ɸѡ�¼�:����ɸѡ����ʾ����,�����ż�
        Author1Toggle.onValueChanged.AddListener((isOn) =>
        {
            RefreshPrepMail();
            ShowMail();
        });
        Author2Toggle.onValueChanged.AddListener((isOn) =>
        {
            RefreshPrepMail();
            ShowMail();
        });
        Author3Toggle.onValueChanged.AddListener((isOn) =>
        {
            RefreshPrepMail();
            ShowMail();
        });
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
        //��ǰ�����ֶ�ģ������
        mailDataList.Add(new MailInfo(E_MailAuther.Auther1));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther2));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther3));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther2));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther2));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther1));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther3));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther1));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther2));
        mailDataList.Add(new MailInfo(E_MailAuther.Auther1));

        //��ȡstreamingAssets�е����ó�ʼ���ż��б�
        string path = Application.streamingAssetsPath + "/mailData.json";
        string JsonStr = "";
        if (File.Exists(path))
            JsonStr = File.ReadAllText(path);
        mailDataList = JsonMapper.ToObject<List<MailInfo>>(JsonStr);
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
            if (mailDataList[i].isDisPlay == false)
                continue;

            if (mailDataList[i].auther == E_MailAuther.Auther1 && Author1Toggle.isOn) 
                prepMailData.Add(mailDataList[i]);
            
            if (mailDataList[i].auther == E_MailAuther.Auther2 && Author2Toggle.isOn)
                prepMailData.Add(mailDataList[i]);

            if (mailDataList[i].auther == E_MailAuther.Auther3 && Author3Toggle.isOn)
                prepMailData.Add(mailDataList[i]);
        }

        //ÿ��ɸѡ��ǩ�仯,ҳ������
        pageNum = 1;
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //����UIҳ��ΪpageNum
        pageNumText.text = pageNum.ToString();
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
            butnApprUpdate();
        }
    }

    /// <summary>
    /// ���·�ҳ��ť����ʾ���:����startIndex��lastIndex��ʾ��������/�ҷ�ҳ��ť
    /// </summary>
    private void butnApprUpdate()
    {
        if (startIndex <= 1)
            SubPageBtn.gameObject.SetActive(false);
        else
            SubPageBtn.gameObject.SetActive(true);

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
        print("ҳ��" + pageNum);
        print("ҳ��" + startIndex + " - " + lastIndex);
        return true;
    }

    /// <summary>
    /// ����ҳ����ʾ����
    /// </summary>
    public void ShowMail()
    {
        //�������ݼ����Ӧ�ż�
        //����ҳ��������֮ǰ����
        for (int i = 0; i < mailObjs.Length; i++) 
        {
            mailObjs[i].gameObject.SetActive(false);
        }

        print("׼����ʾ:" + startIndex + " - " + lastIndex);
        //����������ʾ
        for (int i = startIndex, j = 0; i <= lastIndex; i++, j++)  
        {
            //���������õ����󲢸��½�����ʾ
            mailObjs[j].SetMailInfo(prepMailData[i]);
            //�������
            mailObjs[j].gameObject.SetActive(true);
        }
    }
}

