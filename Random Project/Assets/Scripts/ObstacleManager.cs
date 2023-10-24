using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] float startShootTime;
    float shootTime;

    private void Update()
    {
        if (shootTime <= 0)
        {
            shootTime = startShootTime;
            transform.GetChild(0).transform.position = transform.position;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            shootTime -= Time.deltaTime;
        }
    }
}
