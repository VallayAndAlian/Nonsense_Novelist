using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailPreviewPanel : MonoBehaviour
{
    //信件对象在场景的父节点位置
    public Transform MailPosFather;
    //分类标签
    public Toggle Author1Toggle;
    public Toggle Author2Toggle;
    public Toggle Author3Toggle;
    //界面上的信件对象
    public MailObj[] mailObjs;
    //翻页按钮上一页下一页
    public TextMeshProUGUI pageNumText;
    public Button AddPageBtn;
    public Button SubPageBtn;

    //存储所有Mail
    private List<MailInfo> mailDataList = new List<MailInfo>();
    //待显示的MainData
    private List<MailInfo> prepMailData = new List<MailInfo>();
    //页码
    private int pageNum;
    //每一页能容纳的最大信件数量
    private int maxMailCount;
    //当前应该显示的数据在prepMailData的开始和结束索引
    private int startIndex ,lastIndex;

    private void Start()
    {
        /* 初始化字段 */
        //页码初始为1
        pageNum = 1;
        //信箱可以容纳的信件数量
        maxMailCount = mailObjs.Length;
        //初始化所有信件数据
        ReadMailsData();
        //初始化待显示数据
        RefreshPrepMail();
        //初始化显示索引
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        print("初始显示范围: " + startIndex + " " + lastIndex) ;
        //显示信件
        ShowMail();
        //更新翻页按钮显示
        butnApprUpdate();
        //绑定筛选事件:重新筛选待显示数据,重新信件
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
        //上下翻页事件
        AddPageBtn.onClick.AddListener(() => {
            TurnPage(true);
        });
        SubPageBtn.onClick.AddListener(() => {
            TurnPage(false);
        });
        
    }

    /// <summary>
    /// 读取信件数据并存储(缓存)
    /// </summary>
    private void ReadMailsData()
    {
        //当前测试手动模拟数据
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

        //读取streamingAssets中的配置初始化信件列表
        string path = Application.streamingAssetsPath + "/mailData.json";
        string JsonStr = "";
        if (File.Exists(path))
            JsonStr = File.ReadAllText(path);
        mailDataList = JsonMapper.ToObject<List<MailInfo>>(JsonStr);
    }

    /// <summary>
    /// 根据筛选标签变化刷新待显示数据列表
    /// </summary>
    private void RefreshPrepMail()
    {
        //先清除旧数据
        prepMailData.Clear();
        //然后根据标签是否开启将每个数据加入待显示列表
        for (int i = 0; i < mailDataList.Count; i++) 
        {
            //如果配置此信件当前不显示则跳过此信件
            if (mailDataList[i].isDisPlay == false)
                continue;

            if (mailDataList[i].auther == E_MailAuther.Auther1 && Author1Toggle.isOn) 
                prepMailData.Add(mailDataList[i]);
            
            if (mailDataList[i].auther == E_MailAuther.Auther2 && Author2Toggle.isOn)
                prepMailData.Add(mailDataList[i]);

            if (mailDataList[i].auther == E_MailAuther.Auther3 && Author3Toggle.isOn)
                prepMailData.Add(mailDataList[i]);
        }

        //每次筛选标签变化,页码重置
        pageNum = 1;
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //更新UI页码为pageNum
        pageNumText.text = pageNum.ToString();
    }

    /// <summary>
    /// 翻页:更新翻页后数据和UI页码
    /// </summary>
    /// <param name="isAdd">页码是否增加,false表示页码后退</param>
    private void TurnPage(bool isAdd)
    {
        /*
         每页显示len个数据
         页码p  显示范围
         1      0 ~ 2
         2      3 ~ 3 + (3-1) = 5 
         3      6 ~ 8
         第p页的数据个数为:len*(p-1) ~ 开始索引start + (len-1)
        */
        //判断翻页,更新页码
        if (CanTrunPage(isAdd))
        {
            //更新UI页码为 pageNum
            pageNumText.text = pageNum.ToString();
            //显示数据
            ShowMail();
            //更新翻页按钮的显示
            butnApprUpdate();
        }
    }

    /// <summary>
    /// 更新翻页按钮的显示情况:根据startIndex和lastIndex显示和隐藏左/右翻页按钮
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
    /// 判断是否可翻页,更新显示数据索引[startIndex,lastIndex]
    /// </summary>
    /// <returns>是否可以翻页</returns>
    private bool CanTrunPage(bool isAdd)
    {
        //假设翻页,下页开始和结束索引
        int nextStart = 0, nextLast = 0 ; 
        if (isAdd)
        {
            //下一页页码为pageNum + 1
            //下页开始索引 = 每页显示最大个数len * ( page - 1)
            nextStart = maxMailCount * (pageNum + 1 - 1);
            //根据翻页后的开始索引判断是否可以翻页
            if (nextStart > prepMailData.Count - 1)
                //不可以翻页,不更新任何东西
                return false;

            //可以翻页:更新页码和显示索引
            pageNum++;
            //显示开始索引
            startIndex = nextStart;
            //显示结束索引:开始索引start + (len-1)
            lastIndex = startIndex + maxMailCount - 1;
            //剩余数据不足则只显示剩余数据
            lastIndex = lastIndex > prepMailData.Count - 1 ? prepMailData.Count - 1 : lastIndex;
        }
        else
        {
            //上一页码:pageNum - 1
            //上一页开始索引和结束索引
            nextStart = maxMailCount * (pageNum - 1 - 1);
            nextLast = nextStart + maxMailCount - 1;
            //当上一页结束索引小于0,表示不可向上翻页
            if (nextLast < 0) 
                return false;

            //更新页码和显示索引
            pageNum--;
            startIndex = nextStart;
            lastIndex = nextLast;
            //上一页翻页应该不会出现越界
        }
        print("页码" + pageNum);
        print("页码" + startIndex + " - " + lastIndex);
        return true;
    }

    /// <summary>
    /// 根据页码显示内容
    /// </summary>
    public void ShowMail()
    {
        //根据数据激活对应信件
        //更新页码先隐藏之前数据
        for (int i = 0; i < mailObjs.Length; i++) 
        {
            mailObjs[i].gameObject.SetActive(false);
        }

        print("准备显示:" + startIndex + " - " + lastIndex);
        //更新数据显示
        for (int i = startIndex, j = 0; i <= lastIndex; i++, j++)  
        {
            //将数据设置到对象并更新界面显示
            mailObjs[j].SetMailInfo(prepMailData[i]);
            //激活对象
            mailObjs[j].gameObject.SetActive(true);
        }
    }
}

