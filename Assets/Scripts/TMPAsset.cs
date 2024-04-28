
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;
[System.Serializable]
public class TMPAsset : PlayableAsset
{
    public string Text;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        //ͨ����������д�����д���һ���µ�Playable��Script���ͣ���Ȼ��ͨ��GetBehavior�����ʸոմ�����
        var scriptPlayable = ScriptPlayable<TMPBehavior>.Create(graph);
        //�Ϸ�createʵ�ʽ���������������һ��������graph���ڶ������������Ǵ��������Playable���ܼ������룬Ĭ�ϲ���д��ô����0�����롣
        var scriptBehavior = scriptPlayable.GetBehaviour();
        scriptBehavior.Text = Text;

        //���ظոմ���������Playable��Unity����������Զ������ߡ�
        return scriptPlayable;
    }
}
