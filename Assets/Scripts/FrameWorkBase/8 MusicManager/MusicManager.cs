using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/**������
* AudioLister��� һ�����������,
* AudioSource��� һ���ڲ���Դͷ��,���ڲ�������
*/

/// <summary>
/// ���ֹ�����
/// �涨�������ֺ���Ч����Resources�ļ�����
/// �ֱ���Music/Back/ �� Music/Sound·����
/// </summary>
public class MusicManager : SingletonBase<MusicManager>
{
    //�������ֲ���ʹ�õ����
    private AudioSource backMusic = null;

    //��Ч���Ŷ���
    private GameObject soundObj = null;
    //��Ч��������
    private List<AudioSource> soundList = new List<AudioSource>();

    //�������ִ�С
    private float backValue = 0.5f;
    private float soundValue = 0.5f;

    public MusicManager()
    {
        //���һ��֡����,������Ч������ϵļ��
        MonoManager.GetInstance().AddUpdateListener(MusicUpdate);
    }

    //֡����,���ڼ����Ч������ϵ��Ƴ�
    public void MusicUpdate()
    {
        for (int i = soundList.Count - 1; i >= 0; i--) 
        {
            //������û���ڲ���ʱ,���Ѿ��������
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="name">�������ֵ�����</param>
    public void PlayBackMusic(string name)
    {
        //������
        if (backMusic == null)
        {
            GameObject backMusicObj = new GameObject();
            backMusicObj.name = "backMusic";
            backMusic = backMusicObj.AddComponent<AudioSource>();
        }
        //�첽������Դ������Դʹ�õ�AudioSource
        ResourceManager.GetInstance().LoadResourceAsync<AudioClip>("Music/Back/" + name, (clip) =>
        {
            backMusic.clip = clip;
            backMusic.volume = backValue;
            backMusic.Play();
        });
    }


    /// <summary>
    /// ��ͣ��ǰ���ŵı�������
    /// </summary>
    public void PauseBackMusic()
    {
        if (backMusic == null)
            return;
        backMusic.Pause();
    }


    /// <summary>
    /// ֹͣ��ǰ���ŵı�������
    /// </summary>
    public void StopBackMusic()
    {
        if (backMusic == null)
            return;
        backMusic.Stop();
    }


    /// <summary>
    /// ���ñ������ִ�С
    /// </summary>
    /// <param name="value">�µ�������С</param>
    public void ChangeBackvalue(float value)
    {
        backValue = value;
        if (backMusic == null)
            return;
        backMusic.loop = true;
        backMusic.volume = backValue;
    }

    /// <summary>
    /// ������Ч
    /// ͨ���������Ӻ��Ƴ�������Ч,ÿ����Ч����һ���������
    /// </summary>
    /// <param name="name">��Ч����</param>
    /// <param name="isLoop">�Ƿ�ѭ��</param>
    /// <param name="callback">��Ч�����õ����</param>
    public void PlaySound(string name,bool isLoop, UnityAction<AudioSource> callback = null)
    {
        //������Ч����
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "soundObj";
        }

        //������Ч��Դ��ֵ
        ResourceManager.GetInstance().LoadResourceAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource sound = soundObj.AddComponent<AudioSource>();
            sound.clip = clip;
            sound.volume = soundValue;
            sound.loop = isLoop;
            sound.Play();
            soundList.Add(sound);
            //ʹ����Ч����
            if (callback!=null)
            {
                callback(sound);
            }
        });

    }

    /// <summary>
    /// �ı���Ч��С
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeSoundValue(float Value)
    {
        soundValue = Value;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = soundValue;
        }
    }

    /// <summary>
    /// ֹͣ��Ч
    /// </summary>
    /// <param name="soundSource">��Ч�����õ����</param>
    public void StopSound(AudioSource soundSource)
    {
        if (soundList.Contains(soundSource))
        {
            soundList.Remove(soundSource);
            soundSource.Stop();
            GameObject.Destroy(soundSource);
        }
    }
}
