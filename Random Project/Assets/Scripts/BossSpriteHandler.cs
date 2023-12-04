using UnityEngine;

public class BossSpriteHandler : MonoBehaviour
{
    public enum Anim { Idle, Run, Jump, JumpRight,JumpLeft,Land }
    [HideInInspector] public Anim currentAnim;

    Animator anim => GetComponent<Animator>();

    int jumpNum;

    public void ChangeAnim(Anim newAnim)
    {
        if (newAnim == currentAnim)
            return;

        anim.Play(newAnim.ToString());
        currentAnim = newAnim;

        print(currentAnim.ToString());
    }

    public void JumpAnim()
    {
        print("Jump Number: " + jumpNum);

        if (jumpNum == 0)
        {
            ChangeAnim(Anim.JumpRight);
            jumpNum = 1;
        }
        else
        {
            ChangeAnim(Anim.JumpLeft);
            jumpNum = 0;
        }
    }
}
