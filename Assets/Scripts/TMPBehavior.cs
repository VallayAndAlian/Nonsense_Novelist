
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;
public class TMPBehavior : PlayableBehaviour
{
    public string Text;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //ͨ������������룬�����ڰ󶨹�����Ĺ���Ͻ��д��ݲ����Ĳ�����playData�������Ƿ��ڹ�������Զ����Asset��
        TextMeshProUGUI sayHello = playerData as TextMeshProUGUI;
        if (sayHello != null)
        {
            //�Թ���󶨵Ķ������ֵ�Ĵ��ݡ�
            sayHello.text = Text;
        }
    }

}

