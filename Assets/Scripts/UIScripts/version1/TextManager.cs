
using UnityEngine;
using UnityEngine.UI;

///<summary>
///��Ϸ�е��ı�����(����TextManager����
///</summary>
class TextManager : MonoBehaviour
{
    /// <summary>�������</summary>
    public Text headline;
    /// <summary>�ؿ�����</summary>
    public Text levelText;
    /// <summary>����ű�</summary>
    public BookNvWuXueTu firstText;
    /// <summary>��ȡ�ؿ�num�Ľű�</summary>
    public CharacterTranslateAndCamera characterTranslateAndCamera;
    private int num = 0;
    public Text bookContent;
    public Button back;
    public GameObject bookPanel;

    private void Start()
    {
        //ÿ�ص��ı�
        //�籾����+�籾����
        if (characterTranslateAndCamera.chapterNum == 1 && characterTranslateAndCamera.guanQiaNum == 0)
        {//��һ�µ�һ��
            headline.text = firstText.GetText(1, 0, 1);
            levelText.text = firstText.GetText(1, 1, 1) + firstText.GetText(1, 1, 2) + firstText.GetText(1, 1, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 1 && characterTranslateAndCamera.guanQiaNum == 1)
        {//��һ�µڶ���
            headline.text = firstText.GetText(1, 0, 1);
            levelText.text = firstText.GetText(1, 2, 1) + firstText.GetText(1, 2, 2) + firstText.GetText(1, 2, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 0)
        {//�ڶ��µ�һ��
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 1, 1) + firstText.GetText(2, 1, 2) + firstText.GetText(2, 1, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 1)
        {//�ڶ��µڶ���
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 2, 1) + firstText.GetText(2, 2, 2) + firstText.GetText(2, 2, 3);
        }
        if (characterTranslateAndCamera.chapterNum == 2 && characterTranslateAndCamera.guanQiaNum == 2)
        {//�ڶ��µ�����
            headline.text = firstText.GetText(2, 0, 1);
            levelText.text = firstText.GetText(2, 3, 1) + firstText.GetText(2, 3, 2) + firstText.GetText(2, 3, 3);
        }

        //ÿ�µ��ı�
        headline.text = firstText.GetText(2, 0, 1);
        bookContent.text = "�ڶ��µ�һĻ\n\n" + firstText.GetText(2, 1, 1) +"\n"+ firstText.GetText(2, 1, 2) + "\n" + firstText.GetText(2, 1, 3)+ "\n\n"
            + "�ڶ��µڶ�Ļ\n\n" + firstText.GetText(2, 2, 1) + "\n" + firstText.GetText(2, 2, 2) + "\n" + firstText.GetText(2, 2, 3) + "\n\n" +
            "�ڶ��µ���Ļ\n\n" + firstText.GetText(2, 3, 1) + "\n" + firstText.GetText(2, 3, 2) + "\n" + firstText.GetText(2, 3, 3);

    }
   /* private void FixedUpdate()
    {
        if (num == 0)//Ŀ¼
        {
            bookContent.text =
                "��һ�£�����֮��\n�ڶ��£�������\n�����£���Ĭ��ɭ��\n�����£��ξ�̽��\n�����£����ټ���\n�����£���������\n�����£�������˹�Ĺ���\n�ڰ��£����ֽ��\n�ھ��£���ڵؽ�";
        }
        else if (num == 1)//�ڶ��µ�һĻ��һ����
        {            
            bookContent.text = "�ڶ��µ�һĻ\n"+firstText.GetText(2, 1, 1) + firstText.GetText(2, 1, 2) + firstText.GetText(2, 1, 3);
        }
        
        else if (num == 2)//�ڶ��µڶ�Ļ
        {
            bookContent.text = "�ڶ��µڶ�Ļ\n" + firstText.GetText(2, 2, 1) + firstText.GetText(2, 2, 2) + firstText.GetText(2, 2, 3);
        }
        else if (num == 3)//�ڶ��µ���Ļ
        {
            bookContent.text = "�ڶ��µ���Ļ\n" + firstText.GetText(2, 3, 1) + firstText.GetText(2, 3, 2) + firstText.GetText(2, 3, 3);
            back.interactable = true;
        }

    }*/
    public void RightButton()
    {
        if (num < 3)
        {
            num++;
        }
    }

    public void LeftButton()
    {
        if (num > 0)
        {
            num--;
        }
    }
    public void BackButton()
    {
        bookPanel.SetActive(false);
    }
}
