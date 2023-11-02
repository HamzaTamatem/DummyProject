using System;
using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    [Header("Fireball Ability")] [SerializeField]
    private AbilityData fireballData;
    [SerializeField] GameObject fireball;
    // [SerializeField] float startTimeBtwShoots;
    // float timeBtwShoots;

    [Header("Ring Of Fire Ability")] 
    [SerializeField] private AbilityData fireRingData;
    [SerializeField] GameObject fireRing;
    // [SerializeField] float startTimeBtwFireRing;
    // [SerializeField] float fireRingDesTime;
    // float timeBtwFireRing;
    // public static bool ringAttack;

    [Header("Vertical Attack Ability")] 
    [SerializeField] private AbilityData verticalAttackData;
    [SerializeField] GameObject verticalFire;
    // [SerializeField] float startTimeBtwVerticalFire;
    // float timeBtwVerticalFire;
    // public static bool verticalFireAttack;

    public static event Action<int> OnFireRateIncrease;

    private void OnEnable()
    {
        OnFireRateIncrease += IncreaseFireRate;
    }

    private void Start()
    {
        fireballData.isUnlocked = true;
    }

    void Update()
    {
        if (fireballData.isUnlocked)
        {
            Shoot();
        }

        if (fireRingData.isUnlocked)
        {
            FireRingAttack();
        }
        
        if (verticalAttackData.isUnlocked)
        {
            VerticalFire();
        }
    }

    private void OnDisable()
    {
        OnFireRateIncrease += IncreaseFireRate;
    }

    private void Shoot()
    {
        if (fireballData.timer <= 0)
        {
            fireballData.timer = fireballData.fireRate;
            Instantiate(fireball, transform.position, Quaternion.identity);
        }
        else
        {
            fireballData.timer -= Time.deltaTime;
        }
    }

    private void FireRingAttack()
    {
        if (fireRingData.timer <= 0)
        {
            fireRingData.timer = fireRingData.fireRate;
            GameObject fireRingScript = Instantiate(fireRing, transform.position, Quaternion.identity);
            fireRingScript.GetComponent<FireRing>().desTime = fireRingData.lifetime;
        }
        else
        {
            fireRingData.timer -= Time.deltaTime;
        }
    }

    private void VerticalFire()
    {
        if (verticalAttackData.timer <= 0)
        {
            verticalAttackData.timer = verticalAttackData.fireRate;
            Instantiate(verticalFire, transform.position, Quaternion.identity);
        }
        else
        {
            verticalAttackData.timer -= Time.deltaTime;
        }
    }

    // public static void EnableRingAttack(bool b)
    // {
    //     ringAttack = b;
    // }

    // public static void EnableVerticalAttack(bool b)
    // {
    //     verticalFireAttack = b;
    // }

    public void InvokeIncreaseFireRate(int value)
    {
        OnFireRateIncrease?.Invoke(value);
    }

    public void IncreaseFireRate(int fireRateAbility)
    {
        // convert int to enum
        Ability ability = Ability.None;
        if (Enum.IsDefined(typeof(Ability), fireRateAbility))
        {
            ability = (Ability)fireRateAbility;
            Debug.Log("Converted enum value: " + ability);
        }
        else
        {
            Debug.LogError("Invalid integer value for the enum");
        }
        
        switch (ability)
        {
            case Ability.FireBall:
                fireballData.Upgrade();
                break;
            case Ability.VerticalAttack:
                verticalAttackData.Upgrade();
                break;
            case Ability.FireRing:
                fireRingData.Upgrade();
                break;
            case Ability.None:
            default:
                break;
        }
    }
}

public enum Ability
{
    None,
    FireBall,
    VerticalAttack,
    FireRing
}

[Serializable]
public class AbilityData
{
    [Header("Fire Rate")]
    public float fireRate = 0;
    public float fireRateLevelUpRate;

    [Header("Lifetime")]
    public float lifetime = 0;
    public float lifetimeLevelUpRate;
    
    [Space]
    public int level = 0;
    public float timer;
    public bool isUnlocked = false;

    public void Upgrade()
    {
        if (isUnlocked == false)
        {
            isUnlocked = true;
        }
        else
        {
            level++;
            if (fireRate - fireRateLevelUpRate > 0)
            {
                fireRate -= fireRateLevelUpRate;
            }
            lifetime += lifetimeLevelUpRate;
        }
        // customized upgrades
        /*switch (level)
        {
            case 1: 
                fireRate = 1;
                lifetime = 0.4f;
                break;
            case 2:
                fireRate = 1.5f;
                lifetime = 0.8f;
                break;
            case 3:
                break;
        }*/
    }
}