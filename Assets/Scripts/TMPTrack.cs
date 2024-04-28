
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;
[TrackColor(2, 5, 1)]
[TrackBindingType(typeof(MonoBehaviour))]
//[TrackClipType(typeof(Asset))]
public class TMPTrack : PlayableTrack
{
}


public class TMPBehavior : PlayableBehaviour
{
    public string Text;
    public TextMeshProUGUI tmp;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //ͨ������������룬�����ڰ󶨹�����Ĺ���Ͻ��д��ݲ����Ĳ�����playData�������Ƿ��ڹ�������Զ����Asset��
        TextMeshProUGUI sayHello = playerData as TextMeshProUGUI;
        if (sayHello != null)
        {
            //�Թ���󶨵Ķ������ֵ�Ĵ��ݡ�
            tmp.text = Text;
        }
    }

}
    [System.Serializable]
public class TMPAsset : PlayableAsset
{
    public string Text;
    public TextMeshProUGUI tmp;
        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            //ͨ����������д�����д���һ���µ�Playable��Script���ͣ���Ȼ��ͨ��GetBehavior�����ʸոմ�����
            var scriptPlayable = ScriptPlayable<TMPBehavior>.Create(graph);
            //�Ϸ�createʵ�ʽ���������������һ��������graph���ڶ������������Ǵ��������Playable���ܼ������룬Ĭ�ϲ���д��ô����0�����롣
            var scriptBehavior = scriptPlayable.GetBehaviour();
            scriptBehavior.Text = Text;
            scriptBehavior.tmp = tmp;
            //���ظոմ���������Playable��Unity����������Զ������ߡ�
            return scriptPlayable;
        }
}