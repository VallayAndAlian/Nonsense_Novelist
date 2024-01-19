using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoadingSlider_t : MonoBehaviour
{
    private GameObject obj;
    private Slider slider;
    bool hasLeastTime = false;
    bool hasRealLoad = false;
    bool hasFun = false;
    private void Start()
    {
       
    }
    /// <summary>
    /// �ṩ���ⲿ�� �첽���صĽӿڷ���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name, float leastTime)
    {
        obj = this.gameObject;
        slider = GetComponentInChildren<Slider>();
        DontDestroyOnLoad(obj);
        UnityAction fun = () => { Destroy(obj); };
        PoolMgr.GetInstance().Clear();
        StartCoroutine(ChangeSlider(leastTime, fun));
        StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private IEnumerator ChangeSlider(float leastTime, UnityAction fun)
    {
        for (float i = 0; i < leastTime; i += Time.fixedDeltaTime)
        {
             //slider.value =Mathf.Clamp(Mathf.Pow(i / leastTime, 0.7f),0,0.95f);
            slider.value = Mathf.Clamp(i / leastTime,0,0.95f);
            yield return waitForFixedUpdate;
        }
        hasLeastTime = true;

    }

    /// <summary>
    /// Э���첽���س���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        ao.allowSceneActivation = false;
        //���Եõ��������ص�һ������
        while ((!ao.isDone) && (!hasLeastTime))
        {

            yield return null;
        }
        ao.allowSceneActivation = true;
        //������ɹ��� �Ż�ȥִ��fun
        hasRealLoad = true;
        if (hasLeastTime && hasRealLoad && (!hasFun))
        {
            hasFun = true;
            StartCoroutine(Wait(fun));
        }
    }
    private IEnumerator Wait(UnityAction fun)
    {
        yield return new WaitForSeconds(0.5f);
        fun();
    }
}
