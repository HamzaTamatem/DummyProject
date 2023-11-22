using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [MenuItem("Tools/Switch to Main Menu #1")]
    private static void SwitchToScene1()
    {
        SwitchScene("MainMenu");
    }

    [MenuItem("Tools/Switch to Level01 #2")]
    private static void SwitchToScene2()
    {
        SwitchScene("Level01");
    }

    // Add more functions and menu items for other scenes as needed
    private static void SwitchScene(string sceneName)
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
    }
}
