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
            case 5://垄断公司
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "枪击":
                            {
                                return "垄断公司无所谓向谁的背后开了一枪，只要这是一桩好买卖。";
                            }
                            break;
                        case "寄生虫":
                            {
                                return "随着垄断公司体量的增大，蛀虫在公司内部不断滋生，而贪婪是最好的营养剂，蛀虫会从根部开始吃起，直到将其蚕食殆尽。";
                            }
                            break;
                        case "产卵":
                            {
                                return "垄断公司产下了无数虫卵，这些虫卵会在社会中孵化，直到爬满每个阴暗的角落。";
                            }
                            break;
                        case "本杰士堆":
                            {
                                return "垄断公司召开股民大会发表演讲：亲爱的本杰士堆，等你们的嫩芽冒出石堆，我就要把你们大口吞下。";
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
            case 9://迪卡德
                {
                    #region 文本
                    switch (_word.wordName)
                    {
                        case "心神激荡":
                            {
                                return "狄卡德凝视着瑞秋，不由得心神激荡，面色潮红，情感汹涌，爱的激情比死亡更神秘。";
                            }
                            break;
                        case "沙浴":
                            {
                                return "狄卡德在细沙中来回翻滚，以去除身体上的灰尘，但他站起身时身上沾满了沙子，似乎更脏了。";
                            }
                            break;
                        case "茶杯":
                            {
                                return "狄卡德在居酒屋独自喝酒，把玩着手中的富士山杯，久久没有说话。";
                            }
                            break;
                        case "被植入的记忆":
                            {
                                return "狄卡德又梦到了那只独角兽，它泛着诡异的粉红色，消失在了山林。";
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
