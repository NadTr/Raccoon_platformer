using System.Collections;
using UnityEngine;

public class MovingPlatefromBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool goRight = true;
    private float delay = 0.5f;
    private bool isDelayed = false;
    void Update()
    {
        if(!isDelayed) Move();

        Vector3 origin = transform.position + Vector3.up * 0.4f + (goRight ? 2.5f : -0.5f) * Vector3.right * 1f;
        Vector3 direction = (goRight ? 1f : -1f) * Vector3.right;
        RaycastHit2D sideHit = Physics2D.Raycast(origin, direction, 0.02f);
        Debug.DrawRay(origin, direction, Color.cyan);
        if (sideHit.collider != null) InverseSpeed();
        // if (sideHit.collider != null) StartCoroutine(InverseSpeed(delay));
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


    // private IEnumerator InverseSpeed(float delay = 0f)
    // {
    //     goRight = !goRight;
    //     // isDelayed = true;
    //     yield return new WaitForSeconds(delay);
    //     // isDelayed = false;
    // }
}
