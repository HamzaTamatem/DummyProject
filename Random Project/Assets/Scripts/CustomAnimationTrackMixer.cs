using UnityEngine;
using UnityEngine.Playables;

public class CustomAnimationTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Animator anim = playerData as Animator;

        if (!anim) { return; }

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight > 0f)
            {
                ScriptPlayable<CustomAnimationBehaviour> inputPlayable = (ScriptPlayable<CustomAnimationBehaviour>)playable.GetInput(i);
                CustomAnimationBehaviour input = inputPlayable.GetBehaviour();

                if(input.animationName != null)
                {
                    anim.Play(input.animationName.name);
                }
                else
                {
                    Debug.LogError("You didn't assign animation clip to this clip");
                }
            }
        }
    }
}
