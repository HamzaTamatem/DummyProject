using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] float startShootTime;
    float shootTime;

    [SerializeField] ParticleSystem par;

    private void Update()
    {
        if (shootTime <= 0)
        {
            shootTime = startShootTime;
            transform.GetChild(0).transform.position = transform.position;
            transform.GetChild(0).gameObject.SetActive(true);

            if(par != null)
            {
                par.Play();
            }
        }
        else
        {
            shootTime -= Time.deltaTime;
        }
    }
}
