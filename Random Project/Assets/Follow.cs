using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow;
    
    void Update()
    {
        transform.position = transformToFollow.position;
    }
}
