/// <summary>
/// 信件作者,根据其筛选
/// </summary>
public enum E_MailAuther
{
    Auther1,
    Auther2,
    Auther3,
    NULLAuther
}

/// <summary>
/// 信件信息类
/// 配置格式{信件id,信件作者,信件称呼,信件内容}
/// </summary>
public class MailInfo
{
    //信件序号
    public int id;
    //发件人
    public E_MailAuther auther;
    //称呼内容:也许可以根据发件人优化
    public string dear;
    //信件内容
    public string mailBody;
    //信件读者评分
    public int score;
    //是否已读
    public bool isRead;
    //是否应该显示
    public bool isDisPlay;
    //附件内容:配置为id等,根据其可以增加游戏中的道具
    public string attch;
    //附件数量
    public string attchNum;
    //附件是否已经被领取(拿出)
    public string attchIsOut;

    public MailInfo()
    {
        
    }

    /// <summary>
    /// 仅初始化auther字段的信件
    /// </summary>
    /// <param name="auther"></param>
    public MailInfo(E_MailAuther auther)
    {
        this.auther = auther;
    }

}

