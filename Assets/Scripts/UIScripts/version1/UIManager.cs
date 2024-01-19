
using UnityEngine;
using UnityEngine.UI;

///<summary>
///UI管理器(建立一个UIManager空物体，挂在上面)
///</summary>
class UIManager  : MonoBehaviour
{
    /// <summary>战斗场景是否进入下一关</summary>
    public static bool nextQuanQia;
    /// <summary>脚本</summary>
    public static CharacterTranslateAndCamera charaTransAndCamera;
    /// <summary>获取关卡结束面板</summary>
    private static GameObject endPanel;
    /// <summary>获取章节结束面板</summary>
    public static GameObject bookEndPanel;
    /// <summary>碰撞体射线遮挡面板</summary>
    public static GameObject boxColliderF;
    public static LoadingScript loadingScript;
    public static AudioSource audioSource_write;
    private AudioSource audioSource_BGM;
    public AudioClip[] audioClips;
    public CharacterTranslateAndCamera transAndCamera;

    /// <summary>友方父物体数组</summary>
    public static GameObject[] friendParentF=new GameObject[1];
    /// <summary>敌方父物体数组</summary>
    public static GameObject[] enemyParentF= new GameObject[3];
    /// <summary>敌方父物体数组</summary>
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
    /// 每一章的下一关按钮
    /// </summary>
    public void NextQuanQia()
    {
        //打开关卡结束面板
        endPanel.transform.GetChild(0).gameObject.SetActive(false);
        audioSource_BGM.clip = audioClips[transAndCamera.guanQiaNum + 1];
        audioSource_BGM.Play();
    }
    /// <summary>
    /// 关卡战斗胜利
    /// </summary>
    /// <returns></returns>
    public static bool WinEnd()
    {
            /*if (GameObject.Find("EnemyCharacter").transform.childCount <= 1 && charaTransAndCamera.guanQiaNum <= 1)
            {
                //打开关卡结束面板
                endPanel.transform.GetChild(0).gameObject.SetActive(true);
                return true;
            }*/
            if (enemyParentF[2].transform.childCount <= 1 && charaTransAndCamera.guanQiaNum == 2)
            {
                //打开章节结束面板
                bookEndPanel.transform.GetChild(0).gameObject.SetActive(true);
                boxColliderF.transform.GetChild(0).gameObject.SetActive(true);
                audioSource_write.Play();
                return true;
            }
        
        return false;
    }

    /// <summary>
    /// 关卡战斗失败
    /// </summary>
    /// <returns></returns>
    public static bool LoseEnd()
    {
        if (friendParentF[0].transform.childCount <= 1)
        {
            //失败先跳转到loading界面，再跳转到startgame界面
            loadingScript.enabled = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 章节结束界面的接下来的start按钮
    /// </summary>
    public void ShowEndBook()
    {
        chapterEndPanel.SetActive(false);
    }
}
