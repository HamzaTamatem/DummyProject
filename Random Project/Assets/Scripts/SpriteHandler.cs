using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    public enum Anim { Idle, Run, Jump, Fall, Land, Slide }
    [HideInInspector] public Anim currentAnim;

    Animator anim => GetComponent<Animator>();

    public void ChangeAnim(Anim newAnim)
    {
        if (newAnim == currentAnim)
            return;

        anim.Play(newAnim.ToString());
        currentAnim = newAnim;
    }
}