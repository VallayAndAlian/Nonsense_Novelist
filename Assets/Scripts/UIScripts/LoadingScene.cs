using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    [Header("����˰�ť����ת�ĳ���������")]
    public string sceneName;


    [Header("���س�����������ʱ��")]
    public float leastLoadTime=2f;
    private float process = 0;

    [Header("���س������õĻ���(���ֶ����þ�Ĭ��)")]
    public GameObject canvas;


    private GameObject obj;
    private Slider slider;
    
    private void Start()
    {
        if(canvas==null)
            canvas = Resources.Load<GameObject>("UI/SceneChangeCanvas");
        //if(sceneName!=null)
        //    StartCoroutine(ReallyLoadSceneAsyn(sceneName, null));
    }

    bool hasLoad = false;
    public void EnterNextScene()
    {

       
        obj = Instantiate<GameObject>(canvas);

        slider = obj.GetComponentInChildren<Slider>();
        //���ɼ�����壬����ʼ�첽���س���
        obj.GetComponentInChildren<LoadingSlider_t>().LoadSceneAsyn(sceneName, leastLoadTime);
        //���ؽ����󣬹ر����
    }
    
    public void SwitchStudy()
    {
        SceneManager.LoadScene("Study");
        PoolMgr.GetInstance().Clear();
    }

    public void SwitchShelf()
    {
        SceneManager.LoadScene("BookShelf");

        PoolMgr.GetInstance().Clear();
    }

}
