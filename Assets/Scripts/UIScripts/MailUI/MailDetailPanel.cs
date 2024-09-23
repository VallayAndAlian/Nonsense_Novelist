using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailDetailPanel : MonoBehaviour
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

    //储存附件列表:从UpdateDetailInfo函数读入
    void Start()
    {
        //返回:隐藏面板
        backBtn.onClick.AddListener(() => {
            Hide();
        });

        //领取附件
        attchBtn.onClick.AddListener(() => { 
            
        });
    }

    /// <summary>
    /// 更新面板显示信息:根据传入的MailInfo
    /// </summary>
    public void UpdateDetailInfo(MailInfo info)
    {
        dearText.text = info.dear;
        contentText.text = info.mailBody;
        scoreText.text = info.score.ToString();
    }

    public void Hide() 
    {
        this.gameObject.SetActive(false);
        //清空附件列表
    }

}
