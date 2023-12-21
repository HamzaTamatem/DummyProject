using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    Transform playerPos;
    [SerializeField] float playerDistance;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distanceX = transform.position.x - playerPos.position.x;

        if(distanceX <= playerDistance)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            Destroy(gameObject);
        }
    }
}
