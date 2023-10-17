using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] float startTimeBtwShoots;
    float timeBtwShoots;

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (timeBtwShoots <= 0)
        {
            timeBtwShoots = startTimeBtwShoots;
            Instantiate(fireball, transform.position, Quaternion.identity);
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }
}
