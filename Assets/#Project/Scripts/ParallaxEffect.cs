using UnityEngine;
public class ParallaxEffect : MonoBehaviour
{
    private GameManager gm;
    private CameraFollow cam;
    [Range(0.2f, 1f)] public float parallaxEffect;
    private float length, startpos;

    public void Initialize(GameManager gm, CameraFollow cam)
    {
        this.gm = gm;
        this.cam = cam;
        startpos = transform.position.x;
        length = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void Process()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + 0.2f * length) startpos += length; 
        else if (temp < startpos - 0.9f * length) startpos -= length;
    }
}
