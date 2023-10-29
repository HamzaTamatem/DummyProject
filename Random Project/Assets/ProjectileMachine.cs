using UnityEngine;

public class ProjectileMachine : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float ZDirection;

    [SerializeField] float speed, spawnTime;
    [SerializeField] int amount;

    [SerializeField] float xDistance;
    [SerializeField] float yDistance;

    [SerializeField] bool stackX;

    private void OnEnable()
    {
        Spawn();
    }

    private void Spawn()
    {
        //FindObjectOfType<CameraShake>().Shake(0.1f, 0.1f, 1);

        float stackYNum = 0;
        float stackXNum = 0;

        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(projectile, new Vector2(transform.position.x + stackXNum, transform.position.y + stackYNum), Quaternion.Euler(0, 0, ZDirection));
            bullet.GetComponent<ProjectileBase>().speed = speed;
            if (stackX)
            {
                stackXNum += xDistance;
            }
            else
            {
                stackYNum += yDistance;
            }
        }
    }
}
