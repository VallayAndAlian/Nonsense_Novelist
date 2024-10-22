
using UnityEngine;
using UnityEngine.UI;

///<summary>
///游戏中的文本内容(挂在TextManager身上
///</summary>
class TextManager : MonoBehaviour
{
    /// <summary>剧情标题</summary>
    public Text headline;
    /// <summary>关卡剧情</summary>
    public Text levelText;
    /// <summary>剧情脚本</summary>
    public BookNvWuXueTu firstText;
    /// <summary>获取关卡num的脚本</summary>
    public CharacterTranslateAndCamera characterTranslateAndCamera;
    private int num = 0;
    public Text bookContent;
    public Button back;
    public GameObject bookPanel;

 
}
