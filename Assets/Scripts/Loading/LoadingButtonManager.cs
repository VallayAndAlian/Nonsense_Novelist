using UnityEngine.SceneManagement;
using UnityEngine;

///<summary>
///ȫ����ת���水ťManager
///</summary>
class LoadingButtonManager : MonoBehaviour
{
    public GaiZhangAnim gaiZhangAnim;

    public LoadingScript loadingScript;
    public string startGame = "Login0";
    public string bookDesk = "BookDesk2";
    public string newGame = "NewGame3";
    public string combat = "ShootCombat";

    private int settingNum = 1;
    public AudioSource audioSource;

    private void Start()
    {
        /* ���ʹ��ͬһ��loading����
         * if (SceneManager.GetActiveScene().buildIndex == 1)
         {
             sceneNum = 2;
         }*/
    }
    /// <summary>
    /// ��ʼ��Ϸ���濪ʼ��ť 
    /// </summary>
    public void StartGame()
    {
        Invoke("LoadingScript", 1f);
    }
    /// ���ؿ�ʼ��Ϸ����
    public void BackToStartGame()
    {
        SceneManager.LoadSceneAsync(startGame);
     
    }
    public void NextToNewGame()
    {
        SceneManager.LoadSceneAsync(newGame);
    }
    public void BackToBookDesk()
    {
        SceneManager.LoadSceneAsync(bookDesk);
    }
    /// <summary>
    /// ���ü��س����ű�
    /// </summary>
    public void LoadingScript()
    {
        loadingScript.enabled = true;
    }
    /// <summary>
    /// ����Ϸ���濪ʼ��ť ��ս������
    /// </summary>
    public void StartCombat()
    {
        //���Ÿ��¶���
        gaiZhangAnim.gaizhang.SetBool("gaizhang", true);
        audioSource.Play();
        Invoke("LoadingScript", 4.6f);
    }
    /// <summary>
    /// ��������Ϸ����
    /// </summary>
    public void BackNewGame()
    {
        SceneManager.LoadSceneAsync(newGame);
    }

    /// <summary>
    /// ��Ϸ��ͣ
    /// </summary>
    public void GameSetting()
    {
        settingNum++;
        Time.timeScale = settingNum%2;
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void GameQuit()
    {
        Application.Quit();
    }
}
