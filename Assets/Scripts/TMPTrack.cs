
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
        //通过下面的这句代码，即可在绑定过对象的轨道上进行传递参数的操作。playData就是我们放在轨道上面自定义的Asset。
        TextMeshProUGUI sayHello = playerData as TextMeshProUGUI;
        if (sayHello != null)
        {
            //对轨道绑定的对象进行值的传递。
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
            //通过下面的两行代码进行创建一个新的Playable（Script类型），然后通过GetBehavior来访问刚刚创建的
            var scriptPlayable = ScriptPlayable<TMPBehavior>.Create(graph);
            //上方create实际接受两个参数，第一个参数是graph，第二个参数是我们创建的这个Playable接受几个输入，默认不填写那么就是0个输入。
            var scriptBehavior = scriptPlayable.GetBehaviour();
            scriptBehavior.Text = Text;
            scriptBehavior.tmp = tmp;
            //返回刚刚创建出来的Playable，Unity会帮助我们自动的连线。
            return scriptPlayable;
        }
}