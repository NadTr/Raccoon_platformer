using UnityEngine;
public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public Camera cam;
    [Range(0.2f, 1f)] public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + 0.4 * length) startpos += length; 
        else if (temp < startpos - 0.8 * length) startpos -= length;
    }
}
