using UnityEngine;
using UnityEngine.Playables;

public class CustomAnimationClip : PlayableAsset
{
    public AnimationClip clip;
    //public string animationName;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CustomAnimationBehaviour>.Create(graph);

        CustomAnimationBehaviour CustomAnimationBehaviour = playable.GetBehaviour();
        CustomAnimationBehaviour.animationName = clip;
        return playable;
    }
}
