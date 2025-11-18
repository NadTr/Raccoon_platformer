using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager uI;
    private CameraFollow cam;
    GameObject background;
    private Dictionary<Transform, MovingPlateformBehavior> allMovingPlatforms;
    private GameObject movingPlatforms;
    private RaccoonController player;
    private GameObject prizes;
    public void Initialize(CameraFollow cam, RaccoonController player, GameObject prizes, UIManager uI, GameObject background, GameObject movingPlatforms)
    {
        this.uI = uI;   
        this.cam = cam;   
        this.player = player;   
        this.prizes = prizes;   
        this.background = background;   
        this.movingPlatforms = movingPlatforms;   
    }
    void Update()
    {
        cam.Process();
        player.Process();
        // foreach( KeyValuePair<Transform, MovingPlateformBehavior> kvp in allMovingPlatforms )
        // {
        //     Debug.Log("foreachmovingplateform");
        //     kvp.Value.Process();
        // }  
         for (int i = 0; i < movingPlatforms.transform.childCount; i++)
        {
            if (movingPlatforms.transform.GetChild(i).TryGetComponent<MovingPlateformBehavior>(out MovingPlateformBehavior movingPlatform))
            {
                movingPlatform.Process();
            }
        }

        for (int i = 0; i < background.transform.childCount; i++)
        {
            background.transform.GetChild(i).gameObject.TryGetComponent<ParallaxEffect>(out ParallaxEffect pe);
            pe.Process();
        }
    }
    
    public void IncreaseCounter()
    {

        uI.IncreaseCounter();
    }
    public void Finish()
    {
        SceneManager.LoadScene("TheEnd");
    }
}
