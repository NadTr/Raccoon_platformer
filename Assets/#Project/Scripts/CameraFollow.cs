using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float yGap;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yGap , - 10f);
    }
}
