using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        // Vector3 pos = transform.position;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y , - 10f);
    }
}
