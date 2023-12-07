using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(Play);
    }

    public IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }
}
