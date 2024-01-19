using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 德拉瑞斯的笔记碎片
/// </summary>
class NoteFragment_x : AbstractItems
{
    public override void Awake()
    {
        base.Awake();
        itemID = 3;
        wordName = "德洛瑞斯的笔记碎片";
        bookName = BookNameEnum.StudentOfWitch;
        description = "记载了一些德洛瑞斯写的笔记，提升5点精神。";
        nickname.Add("笔记本");
        holdEnum = HoldEnum.handSingle;
        VoiceEnum = MaterialVoiceEnum.Book;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "从地上捡起了一个纸片，“这上面写的是……一个魔法咒语？”";

    }
}
