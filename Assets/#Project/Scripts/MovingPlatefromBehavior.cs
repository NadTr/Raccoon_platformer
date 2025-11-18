using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateformBehavior : MonoBehaviour
{
    GameManager gm;
    private float speed;
    private bool goRight;

    public void Initialize(GameManager gm, float speed, bool goRight)
    {
        this.gm = gm;
        this.speed = speed;
        this.goRight = goRight;
    }

    public void Process()
    {

        Move();

        Vector3 origin = transform.position + Vector3.up * 0.4f + (goRight ? 3.5f : -0.5f) * Vector3.right * 1f;
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
