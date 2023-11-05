using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    [HideInInspector] public enum Anim { Player_Idle, Player_Run }
    Anim currentAnim;

    Animator anim => GetComponent<Animator>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeAnim(Anim.Player_Idle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeAnim(Anim.Player_Run);
        }
    }

    public void ChangeAnim(Anim newAnim)
    {
        if (newAnim == currentAnim)
            return;

        anim.Play(newAnim.ToString());
        currentAnim = newAnim;
    }
}