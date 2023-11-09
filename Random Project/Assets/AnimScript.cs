using UnityEngine;

public class AnimScript : MonoBehaviour
{
    [SerializeField] Animation animationEditor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animationEditor.Play("Capsule");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animationEditor.Play("Circle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animationEditor.Play("Square");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animationEditor.Play("Triangle");
        }
    }
}
