using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DebugUi : MonoBehaviour
{
    Text text1;
    Text text2;
    public GameObject FangKeObj;
    public GameObject weijiObj;
    public GameObject button;
    private void Awake()
    {
       
        Time.timeScale = 0;

        text1 = transform.Find("Time1").GetComponent<Text>();
        text2 = transform.Find("Time2").GetComponent<Text>();
        text1.text = GameMgr.instance.time1.ToString();
        text2.text = GameMgr.instance.time2.ToString();
    }

    #region �ⲿ����¼�
    public void Exit()
    {
        
        Time.timeScale = GameMgr.instance.timeSpeed;
        Destroy(this.gameObject);
       
    }

    public void AddTouzi()
    {
        GameMgr.instance.AddDice(100);
    }
    public void AddSentence1()
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
        GameMgr.instance.draftUi.AddContent("�����������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                  "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                  "����������˾����Ĺˡ��������ɣ�" +
              "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
              "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
              "�������һ�����е�ʱ���������������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                  "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                  "����������˾����Ĺˡ��������ɣ�" +
              "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
              "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
              "�������һ�����е�ʱ�������������������,�������ε���̤�Ųݵ�,���峿��������ƮƮ����������,���״����ҵĻ��衣" +
                  "�ǳ�������2037�꣬�޺���˸����ˮ�����������������������������������ҡ�" +
                  "����������˾����Ĺˡ��������ɣ�" +
              "��߬����̭��������������״������˻���ֽ�ҡ�ʹ���ƽ�̰�������������΢����" +
              "���ǻ������ҡ��˴�ʹ�ޣ�������·���ߡ���·���ˣ����Ե���·���ߵ�δ����" +
              "�������һ�����е�ʱ��");
        GameMgr.instance.draftUi.AddContent("���������Ĵ�¥���������̡������Ƶ��������߿��ĵ�" +
         "�������������̸����һ���շ��䶵����Ʒ��������������������ͣ������������������ĩ��" +
         "ս����ȱȽ��ǣ��»��ķ��䳾��������Ƭ�ɵأ���������ɵ�ǰ�����ǣ����з������˽���" +
         "����ֳ��ƻ����������Ǻ��ܲ������䳾�Ǵ������Ҫô����Ҫô�˻�����������Ƭ�����" +
         "�׳�Ϊ������֮�ء��Դ�1998����Ŧ6�͸����ܷ�����������������������ȡ�����¹�˾���з�" +
         "��Ŧ�����ǲ��ٸ�Ը��Ϊ������Թ�����棬���Ǳ��������ࡣ����֮ʱ����������ϴ��Ӳ����" +
         "�ļ��䣬�Ӵ����ǲ������Ժ󱳣����Ƕ��Ƿ�������֮�ص������ࡣ������ץ�����ӵķ�" +
         "���˳�֮Ϊ�����ۡ�����������������Բ�ˬ��Կ�ף�һ����Ա�ڲ����п�ǹ������Լ�����" +
         "�������·��պ��Ӳ���뾯Աͬʱ��Ȼ�����������ֿ��������Ĵ�Ц��");
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
    }

    public void AddSentence2()
    {
        GameMgr.instance.draftUi.AddContent("����ã�����һ��");
        GameMgr.instance.draftUi.AddContent("�ڶ�����������");
       
       
    }
    public void EndGame()
    {
       
        CharacterManager.instance.EndGame();
        Exit();
    }

    bool hasShowCharaButton = false;

    public void ShowOrHideCharaButton(GameObject _show)
    {
        if (!hasShowCharaButton)
        {
            if (_show.name == "chara")
            {
                var _charas = GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().charaPrefabs;
                for (int i = 0; i < _charas.Length;i++)
                {
                    var _b=Instantiate(button, _show.transform.GetChild(0));
                    _b.transform.localScale = Vector3.one;
                    _b.GetComponentInChildren<Text>().text = _charas[i].name;
                    var _bET= _b.AddComponent<EventTrigger>();
                    AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { AddCharacter(_bET.transform); });

                    print("0++" + i);
                    hasShowCharaButton = true;
                }
            }
            if (_show.name == "monster")
            {
                var _charas = GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().monsterPrefabs;
                for (int i = 0; i < _charas.Length; i++)
                {
                    var _b = Instantiate(button, _show.transform.GetChild(0));
                    _b.transform.localScale = Vector3.one;
                    _b.GetComponentInChildren<Text>().text = _charas[i].name;
                    var _bET = _b.AddComponent<EventTrigger>();
                    AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { AddMonster(_bET.transform); });
                   
                    hasShowCharaButton = true;
                }
            }
        }
        else
        {
            for (int _t = _show.transform.GetChild(0).childCount -1; _t >=0; _t--)
            {
                Destroy(_show.transform.GetChild(0).GetChild(_t).gameObject);
            }
            hasShowCharaButton = false;
        }
    }
    public void DeleteCharaButton(GameObject _show)
    {
        var _charas = CharacterManager.instance.charas;
        for (int i = 0; i < _charas.Length; i++)
        {
            var _b = Instantiate(button, _show.transform.GetChild(0));
            _b.transform.localScale = Vector3.one;
            _b.GetComponentInChildren<Text>().text = _charas[i].name;
            var _bET = _b.AddComponent<EventTrigger>();
            AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { DeleteCharacter(_bET.transform); });
            hasShowCharaButton = true;


        }

    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="eventTrigger">��Ҫ����¼���EventTrigger</param>
    /// <param name="eventTriggerType">�¼�����</param>
    /// <param name="callback">�ص�����</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
       
    }



    #endregion
    public void AddCharacter(Transform _chara)
    {

        //�����̶�Σ���¼�
        GameMgr.instance.gameProcess.CreateFangke(false, _chara.GetSiblingIndex() + 1);
        //PoolMgr.GetInstance().GetObj(FangKeObj, (a) =>
        //{

        //    a.transform.SetParent(GameMgr.instance.characterCanvas.transform);
        //    a.transform.localPosition = Vector3.zero;

        //    print("debugUI" +( _chara.GetSiblingIndex() + 1).ToString());
        //    a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, _chara.GetSiblingIndex() + 1);
         
        //});
        Exit();
    }

    public void ClickXiWang_A()
    {
        GameMgr.instance.gameProcess.CreateXiWang(false);
        Exit();
    }
    public void ClickXiWang_B()
    {
        GameMgr.instance.gameProcess.CreateXiWang(true);
        Exit();
    }
    public void ClickSetting_A()
    {

        GameMgr.instance.gameProcess.CreateSetting(true);
        Exit();
    }
    public void ClickSetting_B()
    {
        GameMgr.instance.gameProcess.CreateSetting(false);
        Exit();
    }
    public void ClickYiwai_A()
    {

        GameMgr.instance.gameProcess.CreateYiwai(true);
        Exit();
    }
    public void ClickJiaoyi_A()
    {

        GameMgr.instance.gameProcess.CreateJiaoYi(true);
        Exit();
    }
    public void ClickChangjing_A()
    {

        GameMgr.instance.gameProcess.CreateChangJing(true);
        Exit();
    }
    public void AddMonster(Transform _chara)
    {
        //�����̶�Σ���¼�
        PoolMgr.GetInstance().GetObj(weijiObj, (a) =>
        {

            a.transform.SetParent(GameMgr.instance.characterCanvas.transform);
            a.transform.localPosition = Vector3.zero;

            a.GetComponent<Bubble>().StartEventBefore(EventType.WeiJi, false, _chara.GetSiblingIndex()) ;
            
        });
        Exit();
    }

    public void DeleteCharacter(Transform _s)
    {
        var _c = CharacterManager.instance.charas[_s.GetSiblingIndex()];
        _c.camp = CampEnum.left;
        CharacterManager.instance.RefreshStanger();
        Destroy(_c.gameObject);
        Exit();
    }

    public void ChangeSpeed(float _speed)
    {
        GameMgr.instance.timeSpeed = _speed;
    }
   
}
