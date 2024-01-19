
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///���¶���(ս������Ĺؿ����½ڽ���panel�����start)
///</summary>
class GaiZhangAnim : MonoBehaviour
{
    /// <summary>���µĶ���</summary>
    public Animator gaizhang;
    /// <summary>ѡ�صĶ���</summary>
    public Animator level;
    /// <summary>�Ƿ��һ�ε����ť</summary>
    private bool isGZFirst = false;
    /// <summary>�Ƿ��һ�ε����ť</summary>
    private bool isLevelFirst = false;
    public AudioSource gzAudio;

    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Combat")
        {
            gaizhang.SetBool("start", true);
        }
    }
    public void GZ1_2()
    {
        if (isGZFirst == false)
        {
            gaizhang.SetBool("GZ1_2", true);
            isGZFirst = true;
        }
        Invoke("PlayGZClip", 1.5f);
    }
    public void Level1_2()
    {
        if(isLevelFirst == false)
        {
            level.SetBool("level1_2", true);
            isLevelFirst = true;
        }
    }
    public void PlayGZClip()
    {
        gzAudio.Play();
    }
}
