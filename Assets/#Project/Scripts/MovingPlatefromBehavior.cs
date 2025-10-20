using System.Collections;
using UnityEngine;

public class MovingPlatefromBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool goRight = true;
    void Update()
    {
        Move();

        Vector3 origin = transform.position + Vector3.up * 0.4f + (goRight ? 2.5f : -0.5f) * Vector3.right * 1f;
        Vector3 direction = (goRight ? 1f : -1f) * Vector3.right;
        RaycastHit2D sideHit = Physics2D.Raycast(origin, direction, 0.02f);
        Debug.DrawRay(origin, direction, Color.cyan);
        if (sideHit.collider != null) InverseSpeed();
    }
    public float GetSpeed()
    {
        return (goRight ? 1f : -1f) * speed;
    }

    private void InverseSpeed()
    {
        goRight = !goRight;
    }
    private void Move()
    {
        transform.Translate((goRight ? 1f : -1f) * speed * Time.deltaTime, 0f, 0f);
    }

}
