using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    //FireBall
    [SerializeField] GameObject fireball;
    [SerializeField] float startTimeBtwShoots;
    float timeBtwShoots;

    //Ring of fire
    [SerializeField] GameObject fireRing;
    [SerializeField] float startTimeBtwFireRing;
    [SerializeField] float fireRingDesTime;
    float timeBtwFireRing;
    public bool ringAttack;

    //Vertical Fire
    [SerializeField] GameObject verticalFire;
    [SerializeField] float startTimeBtwVerticalFire;
    float timeBtwVerticalFire;
    public bool verticalFireAttack;

    void Update()
    {
        Shoot();

        if (ringAttack)
        {
            FireRingAttack();
        }
        if (verticalFireAttack)
        {
            VerticalFire();
        }
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

    private void FireRingAttack()
    {
        if (timeBtwFireRing <= 0)
        {
            timeBtwFireRing = startTimeBtwFireRing;
            GameObject fireRingScript = Instantiate(fireRing, transform.position, Quaternion.identity);
            fireRingScript.GetComponent<FireRing>().desTime = fireRingDesTime;
        }
        else
        {
            timeBtwFireRing -= Time.deltaTime;
        }
    }

    private void VerticalFire()
    {
        if (timeBtwVerticalFire <= 0)
        {
            timeBtwVerticalFire = startTimeBtwVerticalFire;
            Instantiate(verticalFire, transform.position, Quaternion.identity);
        }
        else
        {
            timeBtwVerticalFire -= Time.deltaTime;
        }
    }
}
