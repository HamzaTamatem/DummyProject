using UnityEditor;
using UnityEngine;

public class MenuItems
{
    
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("-- all Player Prefs deleted successfully --");
    }

    /// <summary>
    /// Changes the sort order of sprite renderers of game objects with tag enemy.
    /// </summary>
    /// <param name="sortOrder"></param>
    [MenuItem("Tools/ChangeEnemySortOrder")]
    private static void ChangeSortOrderOfEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyGo in enemies)
        {
            var spriteRenderers = enemyGo.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in spriteRenderers)
            {
                renderer.sortingOrder = -2;
            }
        }
    }
}
