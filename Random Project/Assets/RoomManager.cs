using UnityEngine;

public class RoomManager : MonoBehaviour
{
    Transform player;

    [SerializeField] GameObject[] tileFolder;
    [SerializeField] GameObject[] roomFolder;
    [SerializeField] Transform[] startPos;

    int currentRoom;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void NextRoom()
    {
        currentRoom++;

        if (currentRoom > roomFolder.Length - 1)
        {
            print("Next scene");
        }
        else
        {
            tileFolder[currentRoom - 1].SetActive(false);
            roomFolder[currentRoom - 1].SetActive(false);
            tileFolder[currentRoom].SetActive(true);
            roomFolder[currentRoom].SetActive(true);
            player.position = startPos[currentRoom].position;
        }
    }
}
