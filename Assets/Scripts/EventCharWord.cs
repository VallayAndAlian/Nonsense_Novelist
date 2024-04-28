using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventCharWord 
{


    public static Dictionary<string, string> dicCG = new Dictionary<string, string>();
    static EventCharWord()
    {
       // dicCG.Add("ElecSheep_start1", "�����������������\n�������ε���̤�Ųݵ�\n���峿��������ƮƮ����������\n���״����ҵĻ���\n\n\n�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�\n����������˾����Ĺˡ��������ɣ���߬����̭��������������\n״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����\n���ǻ������ҡ��˴�ʹ�ޣ�\n������·���ߡ���·���ˣ�\n���Ե���·���ߵ�δ����\n�������һ�����е�ʱ����);
    }

    public static string GetCG(string i)
    {
        
        return dicCG[i];
    }

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
            case 5://¢�Ϲ�˾
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "ǹ��":
                            {
                                return "¢�Ϲ�˾����ν��˭�ı�����һǹ��ֻҪ����һ׮��������";
                            }
                            break;
                        case "������":
                            {
                                return "����¢�Ϲ�˾���������������ڹ�˾�ڲ�������������̰������õ�Ӫ�����������Ӹ�����ʼ����ֱ�������ʳ������";
                            }
                            break;
                        case "����":
                            {
                                return "¢�Ϲ�˾�������������ѣ���Щ���ѻ�������з�����ֱ������ÿ�������Ľ��䡣";
                            }
                            break;
                        case "����ʿ��":
                            {
                                return "¢�Ϲ�˾�ٿ������ᷢ���ݽ����װ��ı���ʿ�ѣ������ǵ���ѿð��ʯ�ѣ��Ҿ�Ҫ�����Ǵ�����¡�";
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
            case 9://�Ͽ���
                {
                    #region �ı�
                    switch (_word.wordName)
                    {
                        case "���񼤵�":
                            {
                                return "�ҿ���������������ɵ����񼤵�����ɫ���죬�����ӿ�����ļ�������������ء�";
                            }
                            break;
                        case "ɳԡ":
                            {
                                return "�ҿ�����ϸɳ�����ط�������ȥ�������ϵĻҳ�������վ����ʱ����մ����ɳ�ӣ��ƺ������ˡ�";
                            }
                            break;
                        case "�豭":
                            {
                                return "�ҿ����ھӾ��ݶ��ԺȾƣ����������еĸ�ʿɽ�����þ�û��˵����";
                            }
                            break;
                        case "��ֲ��ļ���":
                            {
                                return "�ҿ������ε�����ֻ�����ޣ������Ź���ķۺ�ɫ����ʧ����ɽ�֡�";
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
    /// �ڽ�ɫbeAttack��ִ��
    /// </summary>
    /// <param name="_charaUse"></param>
    /// <param name="_charaBe"></param>
    /// <param name="_word"></param>
    /// <returns></returns>
    public static string HD_Attack(AbstractCharacter _charaUse, AbstractCharacter _charaBe)
    {
        switch (_charaUse.characterID)
        {
            case 2://������
                {
                    #region �ı�
                    if (_charaBe.characterID == 3)//����������
                    {
                        return "������Ц�ŵ������񣬡��ȳ����ҼҲ裬��ʱ�������Ǽ�ϱ������������������յ�����ʲôƶ����࣬���������ᣡ��";
                    }
                    #endregion
                }
                break;
            case 3://������
                {
                    
                }
                break;
            case 4://ľ����
                {
                    
                }
                break;
            case 5://¢�Ϲ�˾
                {
                   
                }
                break;
            case 6://����
                {
                    if (_charaBe.characterID == 4)//����ľ����
                    {
                        return "�ع���ҹ���������ģ��O�O�@�@��������ľ������ǧ��ĳ�˯�л��������������𣬴��ۿ��������������޼ɵ���ʳ�����ֱۡ�";
                    }
                    if (_charaBe.characterID == 112)//��������
                    {
                        return "������������յľ��к�è��������Цһ�����Ű����������ˮ����";
                    }
                }
                break;
            case 7://��Ŭ��˹
                {
                    #region �ı�
                    if (_charaBe.characterID == 114)//�����ͽ�����
                    {
                        return "��Ŭ��˹�����ͽ�������Цһ������Ϊ�˽�Ǯ���������˺����ۣ������Ǳ�����ϧ���ǵ��������չ����֡���";
                    }
                    #endregion
                }
                break;
            case 8://ɯ����
                {
                    #region �ı�
                   
                    #endregion
                }
                break;
            case 9://�Ͽ���
                {
                    #region �ı�
                    if (_charaBe.characterID == 111)//������������
                    {
                        return "�������ٵļٵģ����������ӿ�Ц����ҿ��µ���ײײ�������ҿ�����Į��̧��ǹ����������������ʵ�ġ���";
                    }
                    #endregion
                }
                break;
            case 11://����Ա
                {
                    #region �ı�
                    if (_charaBe.characterID == 113)//����ƫ��
                    {
                        return "��ԭ�����ο͹���Ͷι�����أ�����Ա̾�����˷���Щƫ�����ǵ����ҳ���";
                    }
                    #endregion
                }
                break;
            case 12://ʧ��
                {
                    #region �ı�
                    if (_charaBe.characterID == 2)//����������
                    {
                        return "���������߷�բ�ű��һ�����һ��ʯͷ�����ţ�������һ������������������ǣ���������˾�˼������ʧ����������ʹ��ۣ�����������";
                    }
                    #endregion
                }
                break;
            case 110://������΢��
                {
                    #region �ı�
                    if (_charaBe.characterID == 10)//�������弧������
                    {
                        return "��Ѩ��ɱ����ͷŵķ�����΢�������˲��أ����弧�����ݵ������ϰ�������۹����Ա��档";
                    }
                    #endregion
                }
                break;
        }


        return null;
    }

    public static string HD_Cure(AbstractCharacter _charaUse, AbstractCharacter _charaBe)
    {
        switch (_charaUse.characterID)
        {
            case 2://������
                {
                    #region �ı�
                    if (_charaBe.characterID == 3)//����������
                    {
                        return "������Ц�ŵ������񣬡��ȳ����ҼҲ裬��ʱ�������Ǽ�ϱ������������������յ�����ʲôƶ����࣬���������ᣡ��";
                    }
                    #endregion
                }
                break;
            case 3://������
                {

                }
                break;
            case 4://ľ����
                {

                }
                break;
            case 5://¢�Ϲ�˾
                {

                }
                break;
            case 6://����
                {
                    if (_charaBe.characterID == 4)//����ľ����
                    {
                        return "�ع���ҹ���������ģ��O�O�@�@��������ľ������ǧ��ĳ�˯�л��������������𣬴��ۿ��������������޼ɵ���ʳ�����ֱۡ�";
                    }
                    if (_charaBe.characterID == 112)//��������
                    {
                        return "������������յľ��к�è��������Цһ�����Ű����������ˮ����";
                    }
                }
                break;
            case 7://��Ŭ��˹
                {
                    #region �ı�
                    if (_charaBe.characterID == 114)//�����ͽ�����
                    {
                        return "��Ŭ��˹�����ͽ�������Цһ������Ϊ�˽�Ǯ���������˺����ۣ������Ǳ�����ϧ���ǵ��������չ����֡���";
                    }
                    #endregion
                }
                break;
            case 8://ɯ����
                {
                    #region �ı�

                    #endregion
                }
                break;
            case 9://�Ͽ���
                {
                    #region �ı�
                    if (_charaBe.characterID == 111)//������������
                    {
                        return "�������ٵļٵģ����������ӿ�Ц����ҿ��µ���ײײ�������ҿ�����Į��̧��ǹ����������������ʵ�ġ���";
                    }
                    #endregion
                }
                break;
            case 11://����Ա
                {
                    #region �ı�
                    if (_charaBe.characterID == 113)//����ƫ��
                    {
                        return "��ԭ�����ο͹���Ͷι�����أ�����Ա̾�����˷���Щƫ�����ǵ����ҳ���";
                    }
                    #endregion
                }
                break;
            case 12://ʧ��
                {
                    #region �ı�
                    if (_charaBe.characterID == 2)//����������
                    {
                        return "���������߷�բ�ű��һ�����һ��ʯͷ�����ţ�������һ������������������ǣ���������˾�˼������ʧ����������ʹ��ۣ�����������";
                    }
                    #endregion
                }
                break;
            case 110://������΢��
                {
                    #region �ı�
                    if (_charaBe.characterID == 10)//�������弧������
                    {
                        return "��Ѩ��ɱ����ͷŵķ�����΢�������˲��أ����弧�����ݵ������ϰ�������۹����Ա��档";
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
