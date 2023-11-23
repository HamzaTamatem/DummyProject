using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    public enum Anim { Idle, Run, Jump, Fall, Land, Slide, DashStart, DashMid, DashEnd }
    [HideInInspector] public Anim currentAnim;

    Animator anim => GetComponent<Animator>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlaySound("Slide");
        }
    }

    public void ChangeAnim(Anim newAnim)
    {
        if (newAnim == currentAnim)
            return;

        anim.Play(newAnim.ToString());
        currentAnim = newAnim;
    }

    public void PlaySound(string soundName)
    {
        audioManager.Play(soundName);
    }
}