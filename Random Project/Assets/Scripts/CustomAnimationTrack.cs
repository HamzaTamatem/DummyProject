using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;
using UnityEngine;

[TrackBindingType(typeof(Animator))]
[TrackClipType(typeof(CustomAnimationClip))]
public class CustomAnimationTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CustomAnimationTrackMixer>.Create(graph, inputCount);
    }
}
