using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventCharWord 
{
    /// <summary>
    /// ��ÿ����ɫ��ô�����ִ�С�����Ƿ���������ϴ�����
    /// </summary>
    /// <param name="_chara"></param>
    /// <param name="_word"></param>
    public static string fuction(AbstractCharacter _chara, AbstractWord0 _word)
    {
        switch (_chara.characterID)
        {
            case 2://������
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "�Ứ":
                            { 
                                Animsation(_word.wordName); 
                                return "�������ɽ��ˮ��������������Щ�л����ȥ�����һ����飬��ˮ���ģ�ٯ���Ứ��Ц�գ�������ٯ֪Ϊ˭��"; 
                            } break;
                        case "����ɴ֮��":
                            {
                                return "������������ǰ��������ɴ֮�裬������Ѫ�ʺ죬��������Ѫ�����裬���ض������¹⣬������ɴ��ȥ���������°�Ө�׵����ӣ���������������ͷ­��" +
                                    "������������ǰ��������ɴ֮�裬������Ѫ�ʺ죬��������Ѫ�����裬���ض������¹⣬������ɴ��ȥ���������°�Ө�׵����ӣ���������������ͷ­��";
                            }
                            break;
                        case "����":
                            {
                                return "������е��Ŀ�һ��������ʹϮ�������ɵ�üͷ���壬��ɫ�԰ס�";
                            }
                            break;
                        case "Nexus-6���ֱ�":
                            {
                                return "�������滻��Nexus-6���ֱۣ�����һ����ŭ��һ�������׸ߵ�������������������Χ���˷׷����ֽкá�";
                            }
                            break;
                    }
                    #endregion
                    }
                    break;
            case 3://������
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "������":
                            {
                                return "�������ڻ������µľɴ�̳�ڵõ���һ�������裬���ϣ�����Ǹ�ϡ�����";
                            }
                            break;
                    }
                    #endregion
                }
                break;
            case 4://ľ����
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "����":
                            {
                                return "ľ���������ף��ڴ�������ε���ʱ��ʱ�������ҵĿ����������ȳ���ͷ��Ƭ��";
                            }
                            break;
                    }
                    #endregion
                }
                break;
            case 6://����
                {
                    #region �ı�
                    #endregion
                }
                break;
            case 7://��Ŭ��˹
                {
                    #region �ı�
                    #endregion
                }
                break;
            case 8://ɯ����
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "��ˮ��":
                            {
                                return "ɯ��������������ʱ���������ҵ���ˮ�����¹����������ԣ������������Լ����Ѫ��";
                            }
                            break;
                        case "ͼ�����":
                            {
                                return "ɯ�����ԣ����󣩽�����ͼ����ԣ������д���йء���֪��ͷ­����ʮ����ʫ����������Ȼ���ˣ��Ҵ�������дʫ��ʫ��ֻ����еĴ���Ͳ��";
                            }
                            break;
                        case "��֪��ͷ­":
                            {
                                return "ɯ������������������֪��ͷ­����ҧһ����͸��ˮ����������������ҧסԼ�����졣";
                            }
                            break;
                        case "��³˹֮��":
                            {
                                return "ɯ�������ĺ�³˹֮��Ϊ������������������ĳ����";
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
    /// ��Щ�¼�������Ķ���������ô˺���ִ�ж�����
    /// </summary>
    /// <param name="i"></param>
    public static void Animsation(string i)
    {
        
    }
}
