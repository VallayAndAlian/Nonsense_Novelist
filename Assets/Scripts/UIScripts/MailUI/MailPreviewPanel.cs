using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailPreviewPanel : BasePanel<MailPreviewPanel>
{
    //信件对象在场景的父节点位置
    public Transform MailPosFather;
    /*
     分类标签
        -报社编辑
        -佐佐木编辑
        -安德鲁医生
        -您的忠实粉丝彼得
     */
    public Toggle[] AuthorToggles;

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
    
    //初始化面板字段
    protected override void Init()
    {
        /* 初始化字段 */
        //显示页码
        pageNum = 1;
        //信箱可容纳信件数量
        maxMailCount = mailObjs.Length;
        //所有信件数据
        ReadMailsData();
        //待显示数据
        RefreshPrepMail();
        //显示索引
        startIndex = 0;
        lastIndex = maxMailCount > prepMailData.Count ? prepMailData.Count - 1 : maxMailCount - 1;
        //显示信件
        ShowMail();
        //更新翻页按钮显示
        ButnApprUpdate();
        //绑定筛选事件:重新筛选待显示数据,显示信件
        for (int i = 0; i < AuthorToggles.Length; i++)
        {
            AuthorToggles[i].onValueChanged.AddListener((isOn) => {
                RefreshPrepMail();
                ShowMail();
            });
        }
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
            //if (mailDataList[i].isDisPlay == false)
            //    continue;

            if (mailDataList[i].autherType == E_MailAuther.报社编辑 && AuthorToggles[0].isOn) 
                prepMailData.Add(mailDataList[i]);
            
            if (mailDataList[i].autherType == E_MailAuther.佐佐木编辑 && AuthorToggles[1].isOn)
                prepMailData.Add(mailDataList[i]);

            if (mailDataList[i].autherType == E_MailAuther.安德鲁医生 && AuthorToggles[2].isOn)
                prepMailData.Add(mailDataList[i]);
        
            if (mailDataList[i].autherType == E_MailAuther.您的忠实粉丝彼得 && AuthorToggles[3].isOn)
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
            ButnApprUpdate();
        }
    }

    /// <summary>
    /// 更新翻页按钮的显示情况:根据startIndex和lastIndex显示和隐藏左/右翻页按钮
    /// </summary>
    private void ButnApprUpdate()
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
            mailObjs[i].gameObject.SetActive(false);

        //根据数据显示预览信件
        for (int i = startIndex, j = 0; i <= lastIndex; i++, j++)  
            mailObjs[j].ShowPreMail(prepMailData[i]);
    }

}

