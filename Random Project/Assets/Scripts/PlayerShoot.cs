using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShots = 1;
    [SerializeField] private OnButtonDown shootButton;

    public enum Prototype
    {
        None,
        AimShoot,
        MegaMan
    }

    public Prototype prototype = Prototype.None;

    private float timer = 0f;
    private bool isShooting;

    private Controls _controls;

    private Vector2 aimDirection;

    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Shoot.performed += GetAimDirection;
        _controls.Player.Shoot.canceled += ResetAimDirection;
    }

    private void Update()
    {
        if (prototype == Prototype.AimShoot)
        {
            if (aimDirection.magnitude != 0 && isShooting == false)
            {
                StartCoroutine(ShootCoroutine(timeBetweenShots));
            }
        } else if (prototype == Prototype.MegaMan)
        {
            if (!isShooting && shootButton.buttonIsHeld)
            {
                StartCoroutine(ShootCoroutine(timeBetweenShots));
            }
        }
        
    }
    
    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Shoot.performed -= GetAimDirection;
        _controls.Player.Shoot.canceled -= ResetAimDirection;
    }

    private void GetAimDirection(InputAction.CallbackContext obj)
    {
        // get input from right stick (gamepad)
        aimDirection = obj.ReadValue<Vector2>();
        
        // normalize it
        aimDirection.Normalize();
    }

    private void ResetAimDirection(InputAction.CallbackContext obj)
    {
        aimDirection = Vector2.zero;
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (prototype == Prototype.AimShoot)
        {
            newProjectile.GetComponent<Projectile>().direction = aimDirection;
        } else if (prototype == Prototype.MegaMan)
        {
            newProjectile.GetComponent<Projectile>().direction = transform.right;
        }
        newProjectile.SetActive(true);
    }

    private IEnumerator ShootCoroutine(float fireRate)
    {
        isShooting = true;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }
}