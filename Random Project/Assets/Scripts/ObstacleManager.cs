using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] float startShootTime;
    float shootTime;

    [SerializeField] ParticleSystem par;

    Vector2 obstacleFirstPos;

    [SerializeField] bool shootWithAnim;

    private void Start()
    {
        obstacleFirstPos = transform.GetChild(0).transform.position;
    }

    private void Update()
    {
        if (shootWithAnim)
            return;

        ShootWithTime();
    }

    private void ShootWithTime()
    {
        if (shootTime <= 0)
        {
            shootTime = startShootTime;
            //transform.GetChild(0).transform.position = transform.position;
            transform.GetChild(0).transform.position = obstacleFirstPos;
            transform.GetChild(0).gameObject.SetActive(true);

            if (par != null)
            {
                par.Play();
            }
        }
        else
        {
            shootTime -= Time.deltaTime;
        }
    }

    public void ShootWithAnim()
    {
        transform.GetChild(0).transform.position = obstacleFirstPos;
        transform.GetChild(0).gameObject.SetActive(true);

        if (par != null)
        {
            par.Play();
        }
    }
}
