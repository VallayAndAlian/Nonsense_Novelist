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
    GameObject skipButton;
    private PlayableDirector director = null;
    public List<PlayableAsset> cgs;
    void Awake()
    {
        skipButton = this.transform.Find("Skip").gameObject;
        skipButton.SetActive(false);
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
        skipButton.SetActive(true);
        //���ŵ�ʱ�� ��ͣ��Ϸ
        beforeCGPlay = CharacterManager.instance.pause;
        CharacterManager.instance.pause = true;
        //GameMgr.instance.HideGameUI();
        Ele_Kc(playName);
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
            print("�д�");
            return;
        }

        this.gameObject.SetActive(true);
        director.playableAsset = cgs[x];
        director.Play();
    }


    /// <summary>
    /// ���ⲿ��ť�󶨣�������ǰ�Ķ���
    /// </summary>
    public void Skip()
    {
        PlayEventCGEnd();
    }


    public void HideGameUI()
    {
        GameMgr.instance.HideGameUI();
    }
    #region CG����

    public void PlayEventCGEnd()
    {
        print("PlayEventCGEnd");
        CharacterManager.instance.pause = beforeCGPlay;
        director.Stop();
        director.playableAsset = null; 
        this.gameObject.SetActive(false);
        skipButton.SetActive(false);
        GameMgr.instance.ShowGameUI();
  
    }
    public void GetChara_JC1()
    {
        print("GetChara_JC1");
        var _jc1=this.transform.Find("�糡1");
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


    #region ����
    public void Ele_Kc(string playName)
    {
        switch (playName)
        {
            case "ElecSheep_start1":
                {
                    GameMgr.instance.draftUi.AddContent("�����������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                        "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                        "����������˾����Ĺˡ��������ɣ�" +
                    "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
                    "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
                    "�������һ�����е�ʱ����");
                    GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
                     "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
                     "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
                     "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
                     "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
                     "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
                     "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
                     "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
                     "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");
                    foreach (var _chara in CharacterManager.instance.charas)
                    {
                        GameMgr.instance.draftUi.AddContent(_chara.ShowText(_chara));
                    }
                }
                break;

            case "":
                {
                    GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
                        "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
                        "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
                        "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
                        "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
                        "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
                        "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
                        "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
                        "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");
                }
                break;
        }
      
    }




    
    #endregion
}
