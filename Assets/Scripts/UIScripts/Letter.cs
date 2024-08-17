using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    //������������:�˶����򲻱ع���,�°汾ɾ��
    public GameObject firstPanel;
    public Text nulltext;
    
    //�ż�����
    public TextMeshProUGUI letterContentText;
    //�������
    public TextMeshProUGUI coinText;
    //��������
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
     // FirstLetter();
    }

    void FirstLetter()
    {
        switch (RecordMgr.instance.recordList.Count)
        {
            case 0:
                {
                    nulltext.gameObject.SetActive(true);
                    firstPanel.SetActive(false);
                    nulltext.text = "Ŀǰ��û�յ��ż���";
                }
                break;
            case 1:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "�װ��İ���ɭ:\r\n\t����׼���������ÿ��ǵó��ݣ�������־�Ͽ�����������ˣ��㲻��������״̬���ڼ���д����!\r\n\r\n\t\t\t�����ˣ�ҽ��";
                    scoreText.text = RecordMgr.instance.recordList[0].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�װ��İ���ɭ:\r\n\t����׼���������ÿ��ǵó��ݣ�������־�Ͽ�����������ˣ��㲻��������״̬���ڼ���д����!\r\n\r\n\t\t\t�����ˣ�ҽ��";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[0].rand.ToString();
                }
                break;
            case 2:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "����ɭ������\r\n\t�Ұݶ���������������ֱ���������ֶ�������ʵ�����Ȳ������뿴�����Ĵ���ˣ���һ��...��һ�ڣ�����������Ʒ��ÿһ���Ҷ��޲�����ɱ�����أ���ʵ�ڱ�Ǹ�����ֿ��Ʋ�ס�������ﻰ�ˡ�\r\n\t\t�����ˣ����ȷ�˿";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[1].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����ɭ������\r\n\t�Ұݶ���������������ֱ���������ֶ�������ʵ�����Ȳ������뿴�����Ĵ���ˣ���һ��...��һ�ڣ�����������Ʒ��ÿһ���Ҷ��޲�����ɱ�����أ���ʵ�ڱ�Ǹ�����ֿ��Ʋ�ס�������ﻰ�ˡ�\r\n\t\t�����ˣ����ȷ�˿";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[1].rand.ToString();

                }
                break;
            case 3:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "����ɭ��\r\n\t��Ȼ�ܱ�Ǹ�����Ҳ��ò�˵���������Ǿ�����ȡ��\r\n\r\n\t\t\t�����ˣ�ҽ��";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[2].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����ɭ��\r\n\t��Ȼ�ܱ�Ǹ�����Ҳ��ò�˵���������Ǿ�����ȡ��\r\n\r\n\t\t\t�����ˣ�ҽ��";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[2].rand.ToString();
                }
                break;
            case 4:
                {
                    nulltext.gameObject.SetActive(false);
                    firstPanel.SetActive(true);
                    letterContentText.text = "����ɭ����\r\n\t���ϴ��ʼĵ���Ʒ�ѱ���������5�ڣ����⣬�����´α����������ظ����ں���֪���ģ������������������ң�Ҳ���Ǵδζ��ܿ���ʱ��ġ�\r\n\r\n\t\t�����ˣ�����༭";
                    coinText.text = "1    0";
                    scoreText.text = RecordMgr.instance.recordList[3].rand.ToString();
                    //firstPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text =  "����ɭ����\r\n\t���ϴ��ʼĵ���Ʒ�ѱ���������5�ڣ����⣬�����´α����������ظ����ں���֪���ģ������������������ң�Ҳ���Ǵδζ��ܿ���ʱ��ġ�\r\n\r\n\t\t�����ˣ�����༭";
                    //firstPanel.transform.GetChild(6).GetComponent<TextMeshPro>().text = "1 0";
                    //firstPanel.transform.GetChild(7).GetComponent<TextMeshPro>().text = RecordMgr.instance.recordList[3].rand.ToString();
                }
                break;
            default:
                {
                    nulltext.gameObject.SetActive(true);
                    firstPanel.SetActive(false);
                    nulltext.text = "û�и����ż��ˣ�";
                }
                break;
        }
    }
}
