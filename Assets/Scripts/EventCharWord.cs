using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventCharWord 
{
    /// <summary>
    /// 在每个角色获得词条都执行。检测是否有特殊组合触发。
    /// </summary>
    /// <param name="_chara"></param>
    /// <param name="_word"></param>
    public static string fuction(AbstractCharacter _chara, AbstractWord0 _word)
    {
        switch (_chara.characterID)
        {
            case 2://林黛玉
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "葬花":
                            { 
                                Animsation(_word.wordName); 
                                return "林黛玉登山渡水，过树穿花，把些残花落瓣去掩埋，桃花无情，流水无心：侬今葬花人笑痴，他年葬侬知为谁？"; 
                            } break;
                        case "七重纱之舞":
                            {
                                return "林黛玉在王殿前跳起七重纱之舞，月亮如血鲜红，她赤足在血里跳舞，满地都溅着月光，七重面纱褪去，是她如月般莹白的脸庞：在银盘里呈上你的头颅。" +
                                    "林黛玉在王殿前跳起七重纱之舞，月亮如血鲜红，她赤足在血里跳舞，满地都溅着月光，七重面纱褪去，是她如月般莹白的脸庞：在银盘里呈上你的头颅。";
                            }
                            break;
                        case "心碎":
                            {
                                return "林黛玉感到心口一阵尖锐的疼痛袭来，不由得眉头紧皱，面色苍白。";
                            }
                            break;
                        case "Nexus-6型手臂":
                            {
                                return "林黛玉替换上Nexus-6型手臂，腰身一沉，怒喝一声，两米高的柳树竟被连根拔起，周围的人纷纷拍手叫好。";
                            }
                            break;
                    }
                    #endregion
                    }
                    break;
            case 3://王熙凤
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "冷香丸":
                            {
                                return "王熙凤在花根底下的旧瓷坛内得到了一颗冷香丸，“呦，这可是个稀罕物”。";
                            }
                            break;
                    }
                    #endregion
                }
                break;
            case 4://木乃伊
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "肺炎":
                            {
                                return "木乃伊身患肺炎，在大殿中央游荡，时不时发出剧烈的咳嗽声，并咳出骨头碎片。";
                            }
                            break;
                    }
                    #endregion
                }
                break;
            case 6://老鼠
                {
                    #region 文本
                    #endregion
                }
                break;
            case 7://阿努比斯
                {
                    #region 文本
                    #endregion
                }
                break;
            case 8://莎乐美
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "紫水晶":
                            {
                                return "莎乐美在月下起舞时，腰间悬挂的紫水晶在月光下熠熠生辉，如地上喷洒的约翰的血。";
                            }
                            break;
                        case "图灵测试":
                            {
                                return "莎乐美对（对象）进行了图灵测试：请给我写出有关“先知的头颅”的十四行诗，（对象）黯然神伤：我从来不会写诗，诗人只是灵感的传声筒。";
                            }
                            break;
                        case "先知的头颅":
                            {
                                return "莎乐美从银盘中捧起先知的头颅，像咬一颗熟透的水蜜桃那样，用牙齿咬住约翰的嘴。";
                            }
                            break;
                        case "荷鲁斯之眼":
                            {
                                return "莎乐美额间的荷鲁斯之眼为神明所赠，她是众神的宠儿。";
                            }
                            break;
                    }
                    #endregion
                }
                break;
        }
       
        return null;
    }



    /// <summary>
    /// 有些事件有特殊的动画，则调用此函数执行动画。
    /// </summary>
    /// <param name="i"></param>
    public static void Animsation(string i)
    {
        
    }
}
