using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承UI接口,提供动UI悬浮的画效果
public class PreviewPanel : MonoBehaviour
{
    //数据存储/临时数据存储
    List<MailInfo> allInfo = new List<MailInfo>();

    void Start()
    {
        //绑定UI事件

        //读取信件数据并存储

    }

    //UI悬浮的函数

    /* Toggle 
     标签点击函数:根据点击的标签切换显示资源
     根据点击的人物将外部位数组某位置0/1
     完成后调用刷新资源显示的方法
    */

    /*资源显示方法
     根据外部Toggle的位状态,加载某个资源
     */

    /*
     资源显示判断函数
     根据状态位数组和资源中的人名查询是否显示该资源,返回bool
     */

}

