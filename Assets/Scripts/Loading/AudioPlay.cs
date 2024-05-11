using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioClip[] main_Clips;
    private AudioSource audioSource;
    int a = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RandomPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if(main_Clips != null && audioSource.time == main_Clips[a].length)//判断音乐播放完成
        {
            print("播放下一首");
            RandomPlay();
        }
    }
    void RandomPlay() {
        a = UnityEngine.Random.Range(0, main_Clips.Length);

        while (main_Clips[a] == audioSource.clip) {
            a = UnityEngine.Random.Range(0, main_Clips.Length); 
        }
        audioSource.clip = main_Clips[a];
        audioSource.Play();
    }
}
