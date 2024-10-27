using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI�㼶
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI������
/// 1.����������ʾ�����
/// 2.�ṩ���ⲿ ��ʾ�����صȵȽӿ�
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanel>  panelDic = new Dictionary<string, BasePanel>();

    private Transform bot;//�ײ�
    private Transform mid;//�в�
    private Transform top;//����
    private Transform system;//������ϲ�

    //��¼����UI��Canvas������ �����Ժ��ⲿ���ܻ�ʹ����
    public RectTransform uiCanvas;
    public RectTransform worldCanvas;
    public UIManager()
    {
        //����Canvas �����������ʱ�� �����Ƴ�
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        uiCanvas = obj.transform.Find("UICanvas").transform as RectTransform;
        worldCanvas = obj.transform.Find("WorldCanvas").transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        //�ҵ�����
        bot = uiCanvas.Find("Bot");
        mid = uiCanvas.Find("Mid");
        top = uiCanvas.Find("Top");
        system = uiCanvas.Find("System");

        //����EventSystem �����������ʱ�� �����Ƴ�
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    /// <summary>
    /// ͨ���㼶ö�� �õ���Ӧ�㼶�ĸ�����
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bot:
                return this.bot;
            case E_UI_Layer.Mid:
                return this.mid;
            case E_UI_Layer.Top:
                return this.top;
            case E_UI_Layer.System:
                return this.system;
        }
        return null;
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">���ű�����</typeparam>
    /// <param name="panelName">�����</param>
    /// <param name="layer">��ʾ����һ��</param>
    /// <param name="callBack">�����Ԥ���崴���ɹ��� ����������</param>
    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null, bool _inWorldCanvas = false) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Show();
            // ������崴����ɺ���߼�
            if (callBack != null)
                callBack(panelDic[panelName] as T);
            //��������ظ����� ������ڸ���� ��ֱ����ʾ ���ûص�������  ֱ��return ���ٴ��������첽�����߼�
            return;
        }

        ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        { 
            T panel = obj.GetComponent<T>();
            panelDic.Add(panelName, panel);
            panel.Show();

            if (!_inWorldCanvas)
            {
                //������Ϊ Canvas���Ӷ���
                //���� Ҫ�����������λ��
                //�ҵ������� �㵽����ʾ����һ��
                Transform father = bot;
                switch (layer)
                {
                    case E_UI_Layer.Mid:
                        father = mid;
                        break;
                    case E_UI_Layer.Top:
                        father = top;
                        break;
                    case E_UI_Layer.System:
                        father = system;
                        break;
                }
                //���ø�����  �������λ�úʹ�С
                obj.transform.SetParent(father);

                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;
            }
            else
            {
                //���ø�����  �������λ�úʹ�С
                obj.transform.SetParent(worldCanvas);

                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;
            }

            // ������崴����ɺ���߼�
            if (callBack != null)
                callBack(panel);

        });
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="panelName">����RES/UI�������Ԥ��������</param>
    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Hide();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// �õ�ĳһ���Ѿ���ʾ����� �����ⲿʹ��
    /// </summary>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        
        return null;
    }

    /// <summary>
    /// ���ؼ�����Զ����¼�����
    /// </summary>
    /// <param name="control">�ؼ�����</param>
    /// <param name="type">�¼�����</param>
    /// <param name="callBack">�¼�����Ӧ����</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }

}
