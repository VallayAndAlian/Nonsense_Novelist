using UnityEngine;
using UnityEngine.SceneManagement;
///<summary>
///���س������ԣ�������������ʹ�ã�
///</summary>
class LoadingTest : MonoBehaviour
{
    public string sceneName;
    public GaiZhangAnim gaiZhangAnim;

    public void NextLayer()
    {        
        SceneManager.LoadSceneAsync(sceneName);
    }
    /// <summary>
    /// ����Ϸ�����start��ť
    /// </summary>
    public void StartCombat()
    {
        gaiZhangAnim.gaizhang.SetBool("gaizhang",true);
        Invoke("NextLayer", 2.8f);
    }
}
