/// <summary>
/// 信件作者,根据其筛选
/// </summary>
public enum E_MailAuther
{
    报社编辑,
    安德鲁医生,
    您的忠实粉丝彼得,
    佐佐木编辑,
    未知发信人,
}

/// <summary>
/// 信件信息类
/// 配置格式{信件id,信件作者,信件称呼,信件内容}
/// </summary>
public class MailInfo
{
    //信件序号
    public int id;
    //发件人类型:区分发件人,同一发件人类型可能以不同称呼出现
    public E_MailAuther autherType;
    //发件人实际显示姓名
    public string autherName;
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
    //附件id
    public int attchId;
    //附件数量
    public int attchNum;
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
        this.autherType = auther;
    }

}

