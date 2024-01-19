using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 画笔
/// </summary>
class PaintBrush_x : AbstractItems
{
    public override void Awake()
    {
        base.Awake();
        itemID = -1;
        wordName = "画笔";
        description = "一支平平无奇的画笔";
        nickname.Add("笔刷");
        holdEnum = HoldEnum.handSingle;
        VoiceEnum = MaterialVoiceEnum.Soft;
    }
}
