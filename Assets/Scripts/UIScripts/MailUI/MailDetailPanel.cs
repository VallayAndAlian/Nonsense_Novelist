using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MailDetailPanel : BasePanel<MailDetailPanel>
{
    //返回按钮
    public Button backBtn;
    //信件内容
    public TextMeshProUGUI contentText;
    //发件人
    public TextMeshProUGUI dearText;
    //读者评分
    public TextMeshProUGUI scoreText;
    //点击此按钮领取附件
    public Button attchBtn;
    //信件信息
    private MailInfo mailInfo;

    //储存附件列表:从UpdateDetailInfo函数读入
    protected override void Init()
    {
        //返回按钮
        backBtn.onClick.AddListener(() => {
            //当前隐藏面板
            Hide();
            //显示预览面板
            MailPreviewPanel.Instance.Show();
        });

        //领取附件:根据MailInfo中的`附件id和数量`领取
        attchBtn.onClick.AddListener(() => {

        });

        //初始化完成后,详情面板暂时隐藏
        Hide();
    }

    /// <summary>
    /// 更新面板显示信息:根据传入的MailInfo
    /// </summary>
    public void ShowMailDetailInfo(MailInfo info)
    {
        this.mailInfo = info;
        Show();
    }

    /// <summary>
    /// 重写基类的Show,在每次显示时根据信件信息显示
    /// </summary>
    public override void Show()
    {
        dearText.text = mailInfo.dear;
        contentText.text = mailInfo.mailBody;
        scoreText.text = mailInfo.score.ToString();
        base.Show();
    }
}
