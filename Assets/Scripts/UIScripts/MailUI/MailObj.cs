using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum moveDir
{
    ToTop,
    ToDown
}

public class MailObj : MailObjBase
{
    //背景图片
    public Image bgImg;
    //[调试使用]:信件标签,筛选条件
    public TextMeshProUGUI textTag;

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
            SetMailInfo(new MailInfo(E_MailAuther.NULLAuther));
        //显示初始标签内容
        textTag.text = mailInfo.auther.ToString();

        /* 注册鼠标事件 */
        //鼠标进入:UI上浮动
        enterAction += () =>{
            MoveTarget(floatPos,moveDir.ToTop);
         };
        //鼠标退出:UI下浮动
        exitAction += () =>
        {
            MoveTarget(originPos, moveDir.ToDown);
        };
        //鼠标点击
        clickAction += () =>
        {
            /* 打开信件详情UI界面:传入MailInfo,显示信件详情
               取消未读的发光显示等
            */
            ClickAction();
        };
    }

    /// <summary>
    /// 鼠标在悬浮在信件上,信件跳动和回复
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
    /// 更新信件数据MailInfo,并刷新页面显示
    /// </summary>
    /// <param name="mailInfo"></param>
    public void SetMailInfo(MailInfo mailInfo)
    {
        this.mailInfo = mailInfo;

        //根据新数据设置页面显示
        textTag.text = mailInfo.auther.ToString();
        //其他信息待UI完善设置即可显示...
    }
    
    /// <summary>
    /// 点击此信件后的动作
    /// </summary>
    public void ClickAction()
    {
        //打开信件
        /*
         打开信件详情UI界面:传入MailInfo,显示信件详情
         取消未读的发光显示等,并存储信件相关状态
         */
    }

}
