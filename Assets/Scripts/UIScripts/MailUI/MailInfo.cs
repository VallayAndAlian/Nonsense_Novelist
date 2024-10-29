/// <summary>
/// 信件作者,根据其筛选
/// </summary>
public enum E_MailAutherType
{
    Default,
    BaoShe,
    AnDelu,
    BiDe,
    ZuoZuoMu,
}

/// <summary>
/// 信件信息类
/// 配置格式{信件id,信件作者,信件称呼,信件内容}
/// </summary>
public class MailInfo
{
    //信件序号
    public int id;
    //信件名称
    public string mailName;
    //发件人类型:区分发件人,同一发件人类型可能以不同称呼出现
    public E_MailAutherType autherType;
    //发件人实际显示姓名
    public string autherName;
    //称呼内容:发件人对收件人的称呼
    public string dear;
    //信件内容
    public string mailBody;
    //信件读者评分
    public int score;
    //是否已读
    public bool isRead;
    //是否显示:此时信件是否载入信箱
    public bool isDisplay;
    //附件id
    public int attachId;
    //附件数量
    public int attachNum;
    //附件是否已经被领取(拿出)
    public bool attachIsTake;

    public MailInfo()
    {
        
    }

    /// <summary>
    /// 仅初始化auther字段的信件
    /// </summary>
    /// <param name="auther"></param>
    public MailInfo(E_MailAutherType auther)
    {
        this.autherType = auther;
    }

}

