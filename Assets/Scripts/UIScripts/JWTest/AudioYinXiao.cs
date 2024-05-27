using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioYinXiao : MonoBehaviour
{

    public AudioClip click;
    public AudioClip hover;
    public AudioClip wallCol;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ClickPlay()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    public void TanHui() {
        audioSource.clip = hover;
        audioSource.Play();
    }
    public void WallCol()
    {
        audioSource.clip = wallCol;
        audioSource.Play();
    }
}
