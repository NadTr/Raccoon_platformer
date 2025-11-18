using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatefromBehavior : MonoBehaviour
{
    GameManager gm;
    private float speed;
    private bool goRight;
    private List<MovingPlatefromBehavior> listOfPlatforms;

    public void Initialize(GameManager gm, Transform[] positions, float speed)
    {
        this.gm = gm;
        this.speed = speed;

        goRight = true;
        for (int i = 0; i < positions.Length; i++)
        {
            Instantiate(this.gameObject, positions[i].position, Quaternion.identity);
            listOfPlatforms.Add(this);
        }
    }

    public void Process()
    {
        Debug.Log("m p process");
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
        this.transform.Translate((goRight ? 1f : -1f) * speed * Time.deltaTime, 0f, 0f);
    }

}
