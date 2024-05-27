using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioClip[] main_Clips;
    public AudioClip[] boss_Clips;
    private AudioSource audioSource;
    public AudioClip[] event_Clips;
    int a = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RandomPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if(main_Clips != null && audioSource.time == main_Clips[a].length)//�ж����ֲ������
        {
            print("������һ��");
            RandomPlay();
        }
    }
    public void RandomPlay() {
        a = UnityEngine.Random.Range(0, main_Clips.Length);
        audioSource.loop = false;
        while (main_Clips[a] == audioSource.clip) {
            a = UnityEngine.Random.Range(0, main_Clips.Length); 
        }
        audioSource.clip = main_Clips[a];
        audioSource.Play();
    }
    public void Boss_HuaiYiZhuYi()
    {
        audioSource.clip = boss_Clips[0];
        audioSource.Play();
        audioSource.loop = true;
    }
    public void Boss_GuaiWu()
    {
        audioSource.clip = boss_Clips[1];
        audioSource.Play();
        audioSource.loop = true;
    }
    public void Event_WeiJi()
    {
        audioSource.clip = event_Clips[0];
        audioSource.Play();
        audioSource.loop = true;
    }
    public void Event_QiTa()
    {
        audioSource.clip = event_Clips[1];
        audioSource.Play();
        audioSource.loop = true;
    }
}
