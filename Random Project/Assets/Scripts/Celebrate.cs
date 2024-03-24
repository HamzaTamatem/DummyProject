using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Celebrate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().FreezePlayer(20f);
            FindObjectOfType<PlayerMovement>().PausePlayerMovement(20f);
            StartCoroutine(EndCelebration());
        }
    }

    private IEnumerator EndCelebration()
    {
        //yield return new WaitForSeconds(0.4f);
        FindObjectOfType<PlayerMovement>().spriteHandler.ChangeAnim(SpriteHandler.Anim.Dance_1);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
