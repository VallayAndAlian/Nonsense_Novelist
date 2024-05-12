using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum StudyUIType
{
    xiezuo,
    shujia,
    tuichu,
    xinjian,
    zuopin
}

public class StudyMouseOn : MonoBehaviour
{
    Animator animThis;
    Animator animLogo;
    public StudyUIType type;
    [Header("对应的元件")]
    public GameObject logo;
    bool hasEnter = false;
    float enterTime=0;
    float delayTime = 0.05f;
    AudioSource audioSource;
    GameObject studyUI;
    private void Start()
    {
        animThis = this.GetComponent<Animator>();
        animLogo = logo.GetComponent<Animator>();
        studyUI = GameObject.Find("StudyUI");
        audioSource=studyUI.GetComponent<AudioSource>();
    }


    private void OnMouseOver()
    {

        enterTime += Time.deltaTime;

        if (hasEnter) return;
        if (enterTime > delayTime)
        {
            animThis.Play("show");
            animLogo.Play("show");
            hasEnter = true;
        } 
    }
    private void OnMouseExit()
    {
        print("OnMouseExit");
        animThis.Play("nothing");
        animLogo.Play("nothing");
        hasEnter = false;
        enterTime = 0;
    }
    private void OnMouseDown()
    {
        print("OnMouseDown");
        animThis.Play("press");
        switch (type)
        {
            case StudyUIType.xiezuo:
                {
                   var ls=GetComponent<LoadingScene>();
                    if (ls == null)
                    {
                        ls=this.gameObject.AddComponent<LoadingScene>();
                        ls.sceneName = "ShootCombat";
                    }
                    ls.EnterNextScene();
                    audioSource.Play();
                }
                break;
            case StudyUIType.shujia: {
                    /*var ls = GetComponent<LoadingScene>();
                    if (ls == null)
                    {
                        ls = this.gameObject.AddComponent<LoadingScene>();
                        ls.sceneName = "BookShelf";
                    }
                    ls.EnterNextScene();*/
                    SceneManager.LoadScene("BookShelf");
                    audioSource.Play();
                }
                break;
            case StudyUIType.xinjian:
                {
                    //SceneManager.LoadScene("BookShelf");
                    audioSource.Play();
                }
                break;
            case StudyUIType.zuopin:
                {
                    //SceneManager.LoadScene("BookShelf");
                    audioSource.Play();
                }
                break;
            case StudyUIType.tuichu:
                {
                    audioSource.Play();
                    Application.Quit();
                }
                break;
        }
    }
    private void OnMouseUp()
    {
        print("OnMouseUp");
    }
}
