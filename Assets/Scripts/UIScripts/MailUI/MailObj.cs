using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum moveDir
{
    ToTop,
    ToDown
}

public class MailObj : MailEventBase
{
    //背景图片
    public Image bgImg;
    //dear信件头部
    public TextMeshProUGUI dearText;
    //信件内容
    public TextMeshProUGUI contentText;
    //信件作者
    public TextMeshProUGUI authorText;

    //信件UI原位置
    private Vector3 originPos;
    //信件UI浮动目标位置
    private Vector3 floatPos;
    
    //信件数据
    private MailInfo mailInfo;

    private void Start()
    {
        //初始化记录原此信件位置和浮动的目标位置
        originPos = this.transform.localPosition;
        floatPos = originPos + new Vector3(0, 30, 0);

        //初始化信件信息为NULL
        if (mailInfo == null)
            ShowPreMail(new MailInfo(E_MailAuther.未知发信人));

        /* 注册鼠标事件 */
        //鼠标进入:UI上浮动
        enterAction += () =>
        {
            MoveTarget(floatPos, moveDir.ToTop);
        };
        //鼠标退出:UI下浮动
        exitAction += () =>
        {
            MoveTarget(originPos, moveDir.ToDown);
        };
        //鼠标点击
        clickAction += () =>
        {
            print("鼠标点击了");
            /* 打开信件详情UI界面:传入MailInfo,显示信件详情
               取消未读的发光显示等
            */
            ClickAction();
        };
    }

    /// <summary>
    /// 信件的鼠标悬浮效果
    /// </summary>
    /// <param name="targetPos">目标位置</param>
    /// <param name="dir">跳动方向</param>
    private void MoveTarget(Vector3 targetPos,moveDir dir)
    {
        //当前位置
        Vector3 curPos = this.transform.localPosition;
        //每回合偏移量
        Vector3 delta = dir == moveDir.ToTop ? 
            new Vector3(0, Time.deltaTime, 0) : -new Vector3(0, Time.deltaTime, 0);
        //更新偏移量
        while (true)
        {
            curPos += delta;
            if (dir == moveDir.ToTop && curPos.y > targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            else if(dir == moveDir.ToDown && curPos.y < targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            this.transform.localPosition = curPos;
        }
    }

    /// <summary>
    /// 根据MailInfo对象刷新页面信息,并显示信件
    /// </summary>
    /// <param name="mailInfo"></param>
    public void ShowPreMail(MailInfo mailInfo)
    {
        //根据新数据设置页面显示
        this.mailInfo = mailInfo;
        this.dearText.text = mailInfo.dear;
        this.contentText.text = mailInfo.mailBody;
        this.authorText.text = mailInfo.auther.ToString();
        //MailInfo其他信息补充显示

        //显示对象
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏此预览信件
    /// </summary>
    public void HidePreMail()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击此信件后的动作
    /// </summary>
    public void ClickAction()
    {
        //传入信件对象mailInfo,调用面板方法,打开信件详情
        MailDetailPanel.Instance.ShowMailDetailInfo(this.mailInfo);
        //隐藏预览面板
        MailPreviewPanel.Instance.Hide();
    }

}
