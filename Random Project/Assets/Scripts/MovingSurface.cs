using UnityEngine;

public class MovingSurface : MonoBehaviour
{
    GameObject something;
    [SerializeField] float speed;
    [SerializeField] Vector2 direction;

    private void Update()
    {
        if (!something)
            return;

        if(something.transform.right.x < 0)
        {
            something.transform.Translate(-direction * speed * Time.deltaTime);
        }
        else
        {
            something.transform.Translate(direction * speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        something = col.gameObject;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        something = null;
    }
}
