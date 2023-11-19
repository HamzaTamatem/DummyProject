using UnityEngine;

public class BossSpriteHandler : MonoBehaviour
{
    public enum Anim { Idle, Run, Jump, Run_Jump }
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
