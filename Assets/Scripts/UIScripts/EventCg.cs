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
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        text = this.GetComponentInChildren<TextMeshProUGUI>();

        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            director = gameObject.AddComponent<PlayableDirector>();
        }
        director.Stop();
        director.playableAsset = null;

        this.gameObject.SetActive(false);
    }



    public void PlayEventCG(string playName)
    {
        
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

        this.gameObject.SetActive(true);
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
        director.Stop();
        director.playableAsset = null; 
        this.gameObject.SetActive(false);

        GameMgr.instance.ShowGameUI();
  
    }
    public void GetChara_JC1()
    {
        print("GetChara_JC1");
        var _jc1=this.transform.Find("剧场1");
        var _cs = CharacterManager.instance.charas;
        if (_cs[0] != null)
        {
           
            _jc1.Find("a1").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[0].wordName);
            _jc1.Find("a1").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            
            _jc1.Find("a1").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[1] != null)
        {
            _jc1.Find("a2").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[1].wordName);
            _jc1.Find("a2").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("a2").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[2] != null)
        {
            _jc1.Find("b1").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[2].wordName);
            _jc1.Find("b1").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("b1").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }

        if (_cs[3] != null)
        {
            _jc1.Find("b2").GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("WordImage/Character/" + _cs[3].wordName);
            _jc1.Find("b2").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else
        {
            _jc1.Find("b2").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }
    }
    #endregion

}
