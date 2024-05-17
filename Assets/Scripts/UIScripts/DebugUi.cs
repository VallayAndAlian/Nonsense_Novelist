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
    public GameObject button;
    private void Awake()
    {
       
        Time.timeScale = 0;

        text1 = transform.Find("Time1").GetComponent<Text>();
        text2 = transform.Find("Time2").GetComponent<Text>();
        text1.text = GameMgr.instance.time1.ToString();
        text2.text = GameMgr.instance.time2.ToString();
    }

    #region 外部点击事件
    public void Exit()
    {
        
        Time.timeScale = GameMgr.instance.timeSpeed;
        Destroy(this.gameObject);
       
    }

    public void AddTouzi()
    {
        GameMgr.instance.AddDice(100);
    }


    bool hasShowCharaButton = false;
    public void ShowOrHideCharaButton(GameObject _show)
    {
        if (!hasShowCharaButton)
        { 

            var _charas = GameMgr.instance.UiCanvas.GetComponent<CreateOneCharacter>().charaPrefabs;
            for (int i = 0; i < _charas.Length;i++)
            {
                var _b=Instantiate(button, _show.transform.GetChild(0));
                _b.transform.localScale = Vector3.one;
                _b.GetComponentInChildren<Text>().text = _charas[i].name;
                var _bET= _b.AddComponent<EventTrigger>();
                AddPointerEvent(_bET, EventTriggerType.PointerClick, (obj) => { AddCharacter(i); });
                hasShowCharaButton = true;


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

    /// <summary>
    ///
    /// </summary>
    /// <param name="eventTrigger">需要添加事件的EventTrigger</param>
    /// <param name="eventTriggerType">事件类型</param>
    /// <param name="callback">回调函数</param>
    void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
        Debug.Log("AddPointerEnterEvent");
    }



    #endregion
    public void AddCharacter(int _chara)
    {
        //创建固定危机事件
        PoolMgr.GetInstance().GetObj(FangKeObj, (a) =>
        {

            a.transform.SetParent(GameMgr.instance.characterCanvas.transform);
            a.transform.localPosition = Vector3.zero;
            a.transform.localScale = Vector3.one;
            a.GetComponent<Bubble>().StartEventBefore(EventType.FangKe, false, _chara);
         
        });
    }

    public void DeleteCharacter(Situation _s)
    {
        Destroy(_s.GetComponentInChildren<AbstractCharacter>().gameObject);
        Exit();
    }

    public void ChangeSpeed(float _speed)
    {
        GameMgr.instance.timeSpeed = _speed;
    }
   
}
