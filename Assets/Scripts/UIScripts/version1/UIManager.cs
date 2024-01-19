
using UnityEngine;
using UnityEngine.UI;

///<summary>
///UI������(����һ��UIManager�����壬��������)
///</summary>
class UIManager  : MonoBehaviour
{
    /// <summary>ս�������Ƿ������һ��</summary>
    public static bool nextQuanQia;
    /// <summary>�ű�</summary>
    public static CharacterTranslateAndCamera charaTransAndCamera;
    /// <summary>��ȡ�ؿ��������</summary>
    private static GameObject endPanel;
    /// <summary>��ȡ�½ڽ������</summary>
    public static GameObject bookEndPanel;
    /// <summary>��ײ�������ڵ����</summary>
    public static GameObject boxColliderF;
    public static LoadingScript loadingScript;
    public static AudioSource audioSource_write;
    private AudioSource audioSource_BGM;
    public AudioClip[] audioClips;
    public CharacterTranslateAndCamera transAndCamera;

    /// <summary>�ѷ�����������</summary>
    public static GameObject[] friendParentF=new GameObject[1];
    /// <summary>�з�����������</summary>
    public static GameObject[] enemyParentF= new GameObject[3];
    /// <summary>�з�����������</summary>
    public GameObject[] friendParentUseless;
    public GameObject[] enemyParentsUseless;

    public GameObject chapterEndPanel;
    private void Awake()
    {
        charaTransAndCamera = GameObject.Find("MainCamera").GetComponent<CharacterTranslateAndCamera>();
        endPanel = GameObject.Find("endPanel");
        bookEndPanel = GameObject.Find("BookEndPanel");
        loadingScript = GameObject.Find("MainCamera").GetComponent<LoadingScript>();
        audioSource_write = GameObject.Find("AudioSource_wirte").GetComponent<AudioSource>();
        transAndCamera = GameObject.Find("MainCamera").GetComponent<CharacterTranslateAndCamera>();
        audioSource_BGM = GameObject.Find("AudioSource_BGM").GetComponent<AudioSource>();
        //chapterEndPanel = GameObject.Find("ChapterEndPanel");
        boxColliderF = GameObject.Find("boxColliderF");

        for (int i = 0; i < friendParentUseless.Length; i++)
        {
            friendParentF[i] = friendParentUseless[i];
        }
        for(int i = 0; i < enemyParentsUseless.Length; i++)
        {
            enemyParentF[i] = enemyParentsUseless[i];
        }

    }
    /// <summary>
    /// ÿһ�µ���һ�ذ�ť
    /// </summary>
    public void NextQuanQia()
    {
        //�򿪹ؿ��������
        endPanel.transform.GetChild(0).gameObject.SetActive(false);
        audioSource_BGM.clip = audioClips[transAndCamera.guanQiaNum + 1];
        audioSource_BGM.Play();
    }
    /// <summary>
    /// �ؿ�ս��ʤ��
    /// </summary>
    /// <returns></returns>
    public static bool WinEnd()
    {
            /*if (GameObject.Find("EnemyCharacter").transform.childCount <= 1 && charaTransAndCamera.guanQiaNum <= 1)
            {
                //�򿪹ؿ��������
                endPanel.transform.GetChild(0).gameObject.SetActive(true);
                return true;
            }*/
            if (enemyParentF[2].transform.childCount <= 1 && charaTransAndCamera.guanQiaNum == 2)
            {
                //���½ڽ������
                bookEndPanel.transform.GetChild(0).gameObject.SetActive(true);
                boxColliderF.transform.GetChild(0).gameObject.SetActive(true);
                audioSource_write.Play();
                return true;
            }
        
        return false;
    }

    /// <summary>
    /// �ؿ�ս��ʧ��
    /// </summary>
    /// <returns></returns>
    public static bool LoseEnd()
    {
        if (friendParentF[0].transform.childCount <= 1)
        {
            //ʧ������ת��loading���棬����ת��startgame����
            loadingScript.enabled = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// �½ڽ�������Ľ�������start��ť
    /// </summary>
    public void ShowEndBook()
    {
        chapterEndPanel.SetActive(false);
    }
}
