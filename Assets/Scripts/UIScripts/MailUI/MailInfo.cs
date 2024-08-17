using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 信件作者,根据其筛选
/// </summary>
public enum E_MailAuther 
{
    
}

/// <summary>
/// 信件信息类
/// 配置格式{信件id,信件作者,信件称呼,信件内容}
/// </summary>
public class MailInfo
{
    //新建序号
    public int id;
    //发件人
    public E_MailAuther auther;
    //称呼内容
    public string dearContent;
    //信件题内容
    public string mailBody;
    //信件读者评分
    public int score;

    //附件内容:当前存储路径,可能会加载预设体作为动画等
    public string path;
}

