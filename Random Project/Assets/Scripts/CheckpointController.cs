using UnityEngine;

public class CheckpointController : MonoBehaviour
{
   // implicit value is Vector3.zero
   public static Vector3 RespawnPos;

   private void Start()
   {
      if (RespawnPos != Vector3.zero)
      {
         transform.position = RespawnPos;
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Checkpoint"))
      {
         Debug.Log("Just hit a checkpoint");
         RespawnPos = transform.position;
      }
   }
}
