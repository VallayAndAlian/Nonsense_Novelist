using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class EventCg : MonoBehaviour
{
    bool beforeCGPlay = false;
    Animator anim;
    TextMeshProUGUI text;

    private PlayableDirector director = null;
    public List<PlayableAsset> cgs;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        text = this.GetComponentInChildren<TextMeshProUGUI>();

        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            director = gameObject.AddComponent<PlayableDirector>();
        }

        this.gameObject.SetActive(false);
    }



    public void PlayEventCG(string playName, string textContent)
    {

        this.gameObject.SetActive(true);
        //播放的时候 暂停游戏
        beforeCGPlay = CharacterManager.instance.pause;
        CharacterManager.instance.pause = true;
        GameMgr.instance.HideGameUI();

        //anim.Play(playName);
        //text.text = textContent;
        int x = -1;
        for (int i = 0; i < cgs.Count; i++)
        {
            if (cgs[i].name == playName)
            {
                x = i;
                break;
            }

        }
        if (x == -1)
        {
            print("有错");
            return;
        }

        director.playableAsset = cgs[x];
        director.Play();
    }


    public void HideGameUI()
    {
        GameMgr.instance.HideGameUI();
    }
    #region CG结束

    public void PlayEventCGEnd()
    {
        print("PlayEventCGEnd");
        CharacterManager.instance.pause = beforeCGPlay;
        this.gameObject.SetActive(false);

        GameMgr.instance.ShowGameUI();
    }

    #endregion

}
