using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gm;
    private Transform player;

    public void Initialize(GameManager gm, Transform player)
    {
        this.gm = gm;   
        this.player = player;   
    }
    public void Process()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2 , - 10f);
    }
}
